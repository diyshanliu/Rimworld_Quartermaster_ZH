using System;
using System.Collections.Generic;
using Verse;

#if TURKEYLEG_DEVLOG
using System.IO;
using System.Text.RegularExpressions;
#endif

namespace TurkeyLeg.Quartermaster.ZH
{
    /// <summary>
    /// Quartermaster 汉化运行时。
    ///
    /// 设计目标：
    /// 1. 固定文本优先走 Dictionary，O(1) 查询。
    /// 2. 动态文本尽量使用 StartsWith/EndsWith/IndexOf，不在发行版热路径使用 Regex。
    /// 3. 未识别文本始终原样返回；作者更新 UI 文案时优先“漏翻”，而不是报错。
    /// 4. Debug 可记录漏翻；Release 编译时完全裁掉文件 IO 代码。
    /// </summary>
    public static class QmZh
    {
        private const int MaxCacheEntries = 4096;
        private const int MaxCacheTextLength = 4096;

        private static int contextDepth;

        private static readonly Dictionary<string, string> Cache =
            new Dictionary<string, string>(StringComparer.Ordinal);

#if TURKEYLEG_DEVLOG
        private static readonly HashSet<string> MissingLogged =
            new HashSet<string>(StringComparer.Ordinal);

        private static readonly string MissingLogPath =
            Path.Combine(
                GenFilePaths.ConfigFolderPath,
                "TurkeyLeg_Quartermaster_Untranslated.txt"
            );
#endif

        public static bool Active
        {
            get { return contextDepth > 0; }
        }

        public static void Enter()
        {
            contextDepth++;
        }

        public static void Exit()
        {
            if (contextDepth > 0)
            {
                contextDepth--;
            }
        }

        // =============================================================
        // Public translation entry points
        // =============================================================

        public static string T(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            bool cacheable = text.Length <= MaxCacheTextLength;

            string cached;
            if (cacheable && Cache.TryGetValue(text, out cached))
            {
                return cached;
            }

            string translated = TranslateUncached(text);

            if (cacheable)
            {
                if (Cache.Count >= MaxCacheEntries)
                {
                    // Quartermaster 的 UI 文本集合有限。
                    // 到达上限时整体清空比维护 LRU 链表更便宜、更简单。
                    Cache.Clear();
                }

                Cache[text] = translated;
            }

            return translated;
        }

        public static TaggedString T(TaggedString text)
        {
            return T(text.RawText);
        }

        /// <summary>
        /// 仅翻译设置页 / Options 浮动菜单中明确属于 Quartermaster 的文本。
        /// 其他 Mod 的通用 Label 不进入 QmZh.T()。
        /// </summary>
        public static string TOutsideContext(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            return QmTranslationCatalog.OutsideContext.Contains(text)
                ? T(text)
                : text;
        }

        public static TaggedString TOutsideContext(TaggedString text)
        {
            return TOutsideContext(text.RawText);
        }

        // =============================================================
        // Translation pipeline
        // =============================================================

        private static string TranslateUncached(string text)
        {
            string translated;

            // 1. 固定文本：最快路径。
            if (TryTranslateExact(text, out translated))
            {
                return translated;
            }

            // 2. Quartermaster 会把物品说明 + 自己生成的属性拼成一个多行 Tooltip。
            //    仅在真的包含换行时进入多行解析，避免普通 UI 文本产生额外分配。
            if (HasLineBreak(text))
            {
                translated = TranslateEmbeddedTooltip(text);
                if (translated != null)
                {
                    return translated;
                }
            }

            // 3. 动态单行文本。
            //    注意：动态翻译是正式功能，Debug/Release 都必须启用。
            translated = TranslateGeneratedTooltipLine(text);
            if (translated != null)
            {
                return translated;
            }

            translated = TranslateDynamicUi(text);
            if (translated != null)
            {
                return translated;
            }

#if TURKEYLEG_DEVLOG
            RecordMissing(text);
#endif

            // 未知的新 UI 文本直接原样返回。
            // 作者改文案/新增文本时只会出现漏翻，不会导致 UI 报错。
            return text;
        }

        private static bool TryTranslateExact(string text, out string translated)
        {
            string key;
            if (QmTranslationCatalog.Exact.TryGetValue(text, out key))
            {
                translated = key.Translate().ToString();
                return true;
            }

            translated = null;
            return false;
        }

        // =============================================================
        // Multi-line generated tooltips
        // =============================================================

        private static bool HasLineBreak(string text)
        {
            return text.IndexOf('\n') >= 0 || text.IndexOf('\r') >= 0;
        }

        private static string TranslateEmbeddedTooltip(string text)
        {
            string newline = text.IndexOf("\r\n", StringComparison.Ordinal) >= 0
                ? "\r\n"
                : "\n";

            // 只在这里做一次规范化；Tooltip 本身不是高频全局路径。
            string normalized = text
                .Replace("\r\n", "\n")
                .Replace("\r", "\n");

            string[] lines = normalized.Split('\n');
            bool changed = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string translatedLine = TranslateEmbeddedTooltipLine(lines[i]);

                if (!string.Equals(
                    translatedLine,
                    lines[i],
                    StringComparison.Ordinal))
                {
                    lines[i] = translatedLine;
                    changed = true;
                }
            }

            return changed
                ? string.Join(newline, lines)
                : null;
        }

        private static string TranslateEmbeddedTooltipLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return line;
            }

            // 手动找缩进，避免每一行都跑 Regex。
            int indentLength = 0;
            while (indentLength < line.Length &&
                   char.IsWhiteSpace(line[indentLength]))
            {
                indentLength++;
            }

            string indent = indentLength == 0
                ? string.Empty
                : line.Substring(0, indentLength);

            string body = indentLength == 0
                ? line
                : line.Substring(indentLength);

            if (body.Length == 0)
            {
                return line;
            }

            string translated;

            if (TryTranslateExact(body, out translated))
            {
                return indent + translated;
            }

            translated = TranslateGeneratedTooltipLine(body);
            if (translated != null)
            {
                return indent + translated;
            }

            // 名称、描述、材料名等本来已经由 RimWorld / 其他汉化提供中文，
            // 未命中时绝不改动。
            return line;
        }

        /// <summary>
        /// Quartermaster 生成的装备/武器 Tooltip 单行。
        /// 这里同时支持“整块 Tooltip 被拆行”和“未来作者把某一行改为单独 Tooltip”的情况。
        /// </summary>
        private static string TranslateGeneratedTooltipLine(string text)
        {
            string value;

            // Hide 冠冕 from Quartermaster's recommendations?
            if (TryExtract(
                text,
                "Hide ",
                " from Quartermaster's recommendations?",
                out value))
            {
                return "TurkeyLeg_QM_HideFromRecommendations"
                    .Translate(value)
                    .ToString();
            }

            // Quality: 一般
            if (TryAfter(text, "Quality:", out value))
            {
                return "TurkeyLeg_QM_TooltipQuality"
                    .Translate(value.Trim())
                    .ToString();
            }

            // You have 5 identical copies ...
            if (TryExtract(
                text,
                "You have ",
                " identical copies (same material and quality); Focus jumps to the healthiest one.",
                out value) &&
                IsUnsignedInteger(value))
            {
                return "TurkeyLeg_QM_IdenticalCopies"
                    .Translate(value)
                    .ToString();
            }

            // Score: 7.75  (DPS x (1 + 0.75 x AP))
            if (TryExtract(
                text,
                "Score:",
                "(DPS x (1 + 0.75 x AP))",
                out value))
            {
                value = value.Trim();
                if (value.Length > 0)
                {
                    return "TurkeyLeg_QM_TooltipScore"
                        .Translate(value)
                        .ToString();
                }
            }

            if (TryAfter(text, "DPSA (accuracy-weighted):", out value))
            {
                return "TurkeyLeg_QM_TooltipDPSA"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryAfter(text, "Average DPS:", out value))
            {
                return "TurkeyLeg_QM_TooltipAverageDPS"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryAfter(text, "Average tool damage:", out value))
            {
                return "TurkeyLeg_QM_TooltipAverageToolDamage"
                    .Translate(value.Trim())
                    .ToString();
            }

            // Score ranges: Medium, Long (unreachable ranges count as 0)
            // 距离组合由玩家当前勾选状态决定，因此动态翻译每个距离 token。
            if (TryExtract(
                text,
                "Score ranges:",
                "(unreachable ranges count as 0)",
                out value))
            {
                string rangesText = value.Trim();

                if (rangesText.Length > 0)
                {
                    string[] ranges = rangesText.Split(
                        new[] { ", " },
                        StringSplitOptions.None
                    );

                    for (int i = 0; i < ranges.Length; i++)
                    {
                        string translatedRange;

                        if (TryTranslateExact(
                            ranges[i],
                            out translatedRange))
                        {
                            ranges[i] = translatedRange;
                        }
                    }

                    return "TurkeyLeg_QM_TooltipScoreRangesDynamic"
                        .Translate(string.Join("、", ranges))
                        .ToString();
                }
            }

            if (TryAfter(text, "Damage per shot:", out value))
            {
                return "TurkeyLeg_QM_TooltipDamagePerShot"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryAfter(text, "Armor penetration:", out value))
            {
                return "TurkeyLeg_QM_TooltipArmorPenetration"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryExtract(text, "Range:", "cells", out value))
            {
                return "TurkeyLeg_QM_TooltipRange"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryAfter(text, "Accuracy C/S/M/L:", out value))
            {
                return "TurkeyLeg_QM_TooltipAccuracyRanges"
                    .Translate(value.Trim())
                    .ToString();
            }

            string first;
            string second;
            if (TryExtractPair(
                text,
                "Aim time:",
                "Cooldown:",
                out first,
                out second))
            {
                first = RemoveTrailingUnit(first.Trim(), "s");
                second = RemoveTrailingUnit(second.Trim(), "s");

                return "TurkeyLeg_QM_TooltipAimCooldown"
                    .Translate(first, second)
                    .ToString();
            }

            if (TryExtract(text, "Burst:", "shots", out value))
            {
                return "TurkeyLeg_QM_TooltipBurst"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryAfter(text, "Average cooldown:", out value))
            {
                value = RemoveTrailingUnit(value.Trim(), "s");

                return "TurkeyLeg_QM_TooltipAverageCooldown"
                    .Translate(value)
                    .ToString();
            }

            if (TryExtract(text, "Mass:", "kg", out value))
            {
                return "TurkeyLeg_QM_TooltipMass"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryAfter(text, "Market value:", out value))
            {
                return "TurkeyLeg_QM_TooltipMarketValue"
                    .Translate(value.Trim())
                    .ToString();
            }

            if (TryAfter(text, "any material x", out value))
            {
                return "TurkeyLeg_QM_TooltipAnyMaterial"
                    .Translate(value.Trim())
                    .ToString();
            }

            return null;
        }

        // =============================================================
        // Dynamic UI text
        // =============================================================

        private static string TranslateDynamicUi(string text)
        {
            string value;

            // Edit preset: xx
            if (TryAfter(text, "Edit preset:", out value))
            {
                value = value.Trim();

                if (value.Length > 0)
                {
                    return "TurkeyLeg_QM_EditPresetTitle"
                        .Translate(value)
                        .ToString();
                }
            }

            // Delete the custom preset "xx"?
            if (TryExtract(
                text,
                "Delete the custom preset \"",
                "\"?",
                out value))
            {
                return "TurkeyLeg_QM_DeletePresetConfirmation"
                    .Translate(value)
                    .ToString();
            }

            // Min craftable: 1
            if (TryAfter(text, "Min craftable:", out value))
            {
                value = value.Trim();
                if (IsUnsignedInteger(value))
                {
                    return "TurkeyLeg_QM_MinCraftable"
                        .Translate(value)
                        .ToString();
                }
            }

            // Max pieces: 20 (all)
            if (TryExtract(text, "Max pieces:", "(all)", out value))
            {
                value = value.Trim();
                if (IsUnsignedInteger(value))
                {
                    return "TurkeyLeg_QM_MaxPiecesAll"
                        .Translate(value)
                        .ToString();
                }
            }

            // Max pieces: 1
            if (TryAfter(text, "Max pieces:", out value))
            {
                value = value.Trim();
                if (IsUnsignedInteger(value))
                {
                    return "TurkeyLeg_QM_MaxPieces"
                        .Translate(value)
                        .ToString();
                }
            }

            // 2 buff types
            if (TryBefore(text, " buff types", out value) &&
                IsUnsignedInteger(value))
            {
                return "TurkeyLeg_QM_BuffTypes"
                    .Translate(value)
                    .ToString();
            }

            // 3 pinned
            if (TryBefore(text, " pinned", out value) &&
                IsUnsignedInteger(value))
            {
                return "TurkeyLeg_QM_PinnedCount"
                    .Translate(value)
                    .ToString();
            }

            // Rename "Heat", change its buffs, or delete it.
            if (TryExtract(
                text,
                "Rename \"",
                "\", change its buffs, or delete it.",
                out value))
            {
                return "TurkeyLeg_QM_RenamePresetTooltip"
                    .Translate(value)
                    .ToString();
            }

            // Body [Skin] +
            // Legs [Skin/Mid]
            string slotText = TranslateSlotExpression(text);
            if (slotText != null)
            {
                return slotText;
            }

            // research, bench, materials
            string requirements = TranslateRequirementList(text);
            if (requirements != null)
            {
                return requirements;
            }

            // 0 picked
            if (TryBefore(text, " picked", out value) &&
                IsUnsignedInteger(value))
            {
                return "TurkeyLeg_QM_PickedCount"
                    .Translate(value)
                    .ToString();
            }

            if (TryCountCommand(
                text,
                "Add exclusions (",
                out value))
            {
                return "TurkeyLeg_QM_AddExclusions"
                    .Translate(value)
                    .ToString();
            }

            if (TryCountCommand(
                text,
                "Remove exclusions (",
                out value))
            {
                return "TurkeyLeg_QM_RemoveExclusions"
                    .Translate(value)
                    .ToString();
            }

            if (TryCountCommand(
                text,
                "Clear all (",
                out value))
            {
                return "TurkeyLeg_QM_ClearAll"
                    .Translate(value)
                    .ToString();
            }

            if (TryCountCommand(
                text,
                "Exclusions (",
                out value))
            {
                return "TurkeyLeg_QM_ExclusionsCount"
                    .Translate(value)
                    .ToString();
            }

            if (TryExtract(
                text,
                "Excluded: ",
                " item(s)",
                out value) &&
                IsUnsignedInteger(value))
            {
                return "TurkeyLeg_QM_ExcludedItems"
                    .Translate(value)
                    .ToString();
            }

            if (TryExtract(
                text,
                "Excluded: ",
                " material(s)",
                out value) &&
                IsUnsignedInteger(value))
            {
                return "TurkeyLeg_QM_ExcludedMaterials"
                    .Translate(value)
                    .ToString();
            }

            if (TryAfter(text, "DPSA ", out value) &&
                value.Length > 0)
            {
                return "TurkeyLeg_QM_DPSAValue"
                    .Translate(value)
                    .ToString();
            }

            if (TryAfter(text, "DPS ", out value) &&
                value.Length > 0)
            {
                return "TurkeyLeg_QM_DPSValue"
                    .Translate(value)
                    .ToString();
            }

            if (TryExtract(text, "Sharp ", "%", out value) &&
                value.Length > 0)
            {
                return "TurkeyLeg_QM_SharpValue"
                    .Translate(value)
                    .ToString();
            }

            // Core  (87), Biotech (31), Head (45) ...
            string sectionName;
            string sectionCount;
            if (TryTrailingCount(text, out sectionName, out sectionCount))
            {
                string translatedSection;
                if (TryTranslateExact(sectionName, out translatedSection))
                {
                    sectionName = translatedSection;
                }

                return "TurkeyLeg_QM_SectionCount"
                    .Translate(sectionName, sectionCount)
                    .ToString();
            }

            // Pin items: Head
            if (TryAfter(text, "Pin items:", out value))
            {
                value = TranslateCategory(value.Trim());

                return "TurkeyLeg_QM_PinItemsTitle"
                    .Translate(value)
                    .ToString();
            }

            // needs research, bench, materials
            if (TryAfter(text, "needs ", out value))
            {
                requirements = TranslateRequirementList(value);
                if (requirements != null)
                {
                    return "TurkeyLeg_QM_NeedsRequirements"
                        .Translate(requirements)
                        .ToString();
                }
            }

            // Clear Accessories pins
            if (TryExtract(text, "Clear ", " pins", out value))
            {
                value = TranslateCategory(value);

                return "TurkeyLeg_QM_ClearCategoryPins"
                    .Translate(value)
                    .ToString();
            }

            // MATERIAL ^ / MATERIAL v / SCORE ^ ...
            string sortedHeader = TranslateSortableHeader(text);
            if (sortedHeader != null)
            {
                return sortedHeader;
            }

            // 冠冕 hidden from Quartermaster. Restore it from the Exclusions menu.
            const string hiddenSuffix =
                " hidden from Quartermaster. Restore it from the Exclusions menu.";

            if (TryBefore(text, hiddenSuffix, out value) &&
                value.Length > 0)
            {
                return "TurkeyLeg_QM_ItemHiddenMessage"
                    .Translate(value)
                    .ToString();
            }

            // Created apparel policy "Quartermaster Standard 2" allowing 2 item(s).
            // Assign it from/on a colonist's Apparel tab.
            string policyName;
            string itemCount;
            if (TryParsePolicyCreated(
                text,
                out policyName,
                out itemCount))
            {
                return "TurkeyLeg_QM_PolicyCreatedMessage"
                    .Translate(policyName, itemCount)
                    .ToString();
            }

            return null;
        }

        // =============================================================
        // Token translation
        // =============================================================

        private static string TranslateSlotExpression(string text)
        {
            int bracketStart = text.IndexOf(" [", StringComparison.Ordinal);
            if (bracketStart <= 0)
            {
                return null;
            }

            int contentStart = bracketStart + 2;
            int bracketEnd = text.IndexOf(']', contentStart);
            if (bracketEnd < contentStart)
            {
                return null;
            }

            string bodyPart = text.Substring(0, bracketStart);
            string translatedBodyPart = TranslateSlotToken(bodyPart);

            // 如果身体部位不是已知 token，则不要误判普通文本。
            if (string.Equals(
                translatedBodyPart,
                bodyPart,
                StringComparison.Ordinal) &&
                !QmTranslationCatalog.SlotTokens.ContainsKey(bodyPart))
            {
                return null;
            }

            string layerText =
                text.Substring(contentStart, bracketEnd - contentStart);

            string[] layers = layerText.Split('/');
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = TranslateSlotToken(layers[i]);
            }

            string suffix = bracketEnd + 1 < text.Length
                ? text.Substring(bracketEnd + 1)
                : string.Empty;

            return translatedBodyPart +
                " [" +
                string.Join("/", layers) +
                "]" +
                suffix;
        }

        private static string TranslateSlotToken(string token)
        {
            string key;
            if (QmTranslationCatalog.SlotTokens.TryGetValue(token, out key))
            {
                return key.Translate().ToString();
            }

            return token;
        }

        private static string TranslateCategory(string category)
        {
            string translated;

            if (TryTranslateExact(category, out translated))
            {
                return translated;
            }

            return TranslateSlotToken(category);
        }

        private static string TranslateRequirementList(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            string[] parts = text.Split(
                new[] { ", " },
                StringSplitOptions.None
            );

            for (int i = 0; i < parts.Length; i++)
            {
                string key;
                if (!QmTranslationCatalog.RequirementTokens.TryGetValue(
                    parts[i],
                    out key))
                {
                    return null;
                }

                parts[i] = key.Translate().ToString();
            }

            return string.Join("、", parts);
        }

        // =============================================================
        // Allocation-light parsers
        // =============================================================

        private static bool TryAfter(
            string text,
            string prefix,
            out string value)
        {
            if (text.StartsWith(prefix, StringComparison.Ordinal))
            {
                value = text.Substring(prefix.Length);
                return true;
            }

            value = null;
            return false;
        }

        private static bool TryBefore(
            string text,
            string suffix,
            out string value)
        {
            if (text.EndsWith(suffix, StringComparison.Ordinal))
            {
                value = text.Substring(
                    0,
                    text.Length - suffix.Length
                );
                return true;
            }

            value = null;
            return false;
        }

        private static bool TryExtract(
            string text,
            string prefix,
            string suffix,
            out string value)
        {
            if (!text.StartsWith(prefix, StringComparison.Ordinal) ||
                !text.EndsWith(suffix, StringComparison.Ordinal) ||
                text.Length < prefix.Length + suffix.Length)
            {
                value = null;
                return false;
            }

            value = text.Substring(
                prefix.Length,
                text.Length - prefix.Length - suffix.Length
            );

            return true;
        }

        private static bool TryExtractPair(
            string text,
            string prefix,
            string separator,
            out string first,
            out string second)
        {
            first = null;
            second = null;

            if (!text.StartsWith(prefix, StringComparison.Ordinal))
            {
                return false;
            }

            int separatorIndex = text.IndexOf(
                separator,
                prefix.Length,
                StringComparison.Ordinal
            );

            if (separatorIndex < 0)
            {
                return false;
            }

            first = text.Substring(
                prefix.Length,
                separatorIndex - prefix.Length
            );

            second = text.Substring(
                separatorIndex + separator.Length
            );

            return true;
        }

        private static bool TryCountCommand(
            string text,
            string prefix,
            out string count)
        {
            if (TryExtract(text, prefix, ")", out count) &&
                IsUnsignedInteger(count))
            {
                return true;
            }

            count = null;
            return false;
        }

        private static bool TryTrailingCount(
            string text,
            out string name,
            out string count)
        {
            name = null;
            count = null;

            if (text.Length < 4 || text[text.Length - 1] != ')')
            {
                return false;
            }

            int open = text.LastIndexOf('(');
            if (open <= 0)
            {
                return false;
            }

            string countCandidate =
                text.Substring(open + 1, text.Length - open - 2);

            if (!IsUnsignedInteger(countCandidate))
            {
                return false;
            }

            string nameCandidate =
                text.Substring(0, open).TrimEnd();

            if (nameCandidate.Length == 0)
            {
                return false;
            }

            name = nameCandidate;
            count = countCandidate;
            return true;
        }

        private static string TranslateSortableHeader(string text)
        {
            if (text.Length < 3)
            {
                return null;
            }

            char direction = text[text.Length - 1];

            if ((direction != 'v' && direction != '^') ||
                text[text.Length - 2] != ' ')
            {
                return null;
            }

            string baseHeader =
                text.Substring(0, text.Length - 2);

            string translatedHeader;
            if (!TryTranslateExact(
                baseHeader,
                out translatedHeader))
            {
                return null;
            }

            return translatedHeader +
                (direction == 'v' ? " ▼" : " ▲");
        }

        private static bool TryParsePolicyCreated(
            string text,
            out string policyName,
            out string itemCount)
        {
            const string prefix =
                "Created apparel policy \"";

            const string middle =
                "\" allowing ";

            const string suffixFrom =
                " item(s). Assign it from a colonist's Apparel tab.";

            const string suffixOn =
                " item(s). Assign it on a colonist's Apparel tab.";

            policyName = null;
            itemCount = null;

            if (!text.StartsWith(prefix, StringComparison.Ordinal))
            {
                return false;
            }

            int middleIndex = text.IndexOf(
                middle,
                prefix.Length,
                StringComparison.Ordinal
            );

            if (middleIndex < 0)
            {
                return false;
            }

            string suffix;

            if (text.EndsWith(
                suffixFrom,
                StringComparison.Ordinal))
            {
                suffix = suffixFrom;
            }
            else if (text.EndsWith(
                suffixOn,
                StringComparison.Ordinal))
            {
                suffix = suffixOn;
            }
            else
            {
                return false;
            }

            int countStart = middleIndex + middle.Length;
            int countLength =
                text.Length - countStart - suffix.Length;

            if (countLength <= 0)
            {
                return false;
            }

            string countCandidate =
                text.Substring(countStart, countLength);

            if (!IsUnsignedInteger(countCandidate))
            {
                return false;
            }

            policyName = text.Substring(
                prefix.Length,
                middleIndex - prefix.Length
            );

            itemCount = countCandidate;
            return policyName.Length > 0;
        }

        private static string RemoveTrailingUnit(
            string value,
            string unit)
        {
            if (!string.IsNullOrEmpty(value) &&
                value.EndsWith(unit, StringComparison.Ordinal))
            {
                return value.Substring(
                    0,
                    value.Length - unit.Length
                ).TrimEnd();
            }

            return value;
        }

        private static bool IsUnsignedInteger(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

#if TURKEYLEG_DEVLOG
        // =============================================================
        // Development-only untranslated text logger
        // Release 构建中整个区域不会进入 DLL。
        // =============================================================

        private static void RecordMissing(string text)
        {
            if (string.IsNullOrWhiteSpace(text) ||
                !ContainsEnglishLetter(text) ||
                ShouldIgnoreMissing(text))
            {
                return;
            }

            string oneLine = text
                .Replace("\r\n", "\n")
                .Replace("\r", "\n")
                .Replace("\n", "\\n");

            if (!MissingLogged.Add(oneLine))
            {
                return;
            }

            try
            {
                File.AppendAllText(
                    MissingLogPath,
                    oneLine + Environment.NewLine
                );
            }
            catch (Exception ex)
            {
                Log.Warning(
                    "[TurkeyLeg Quartermaster ZH] Failed to record untranslated text: " +
                    ex.Message
                );
            }
        }

        private static bool ShouldIgnoreMissing(string text)
        {
            if (Regex.IsMatch(
                text,
                @"^[0-9]+(?:\.[0-9]+)?s$"))
            {
                return true;
            }

            if (Regex.IsMatch(
                text,
                @"^x[0-9]+$"))
            {
                return true;
            }

            if (Regex.IsMatch(
                text,
                @"^[+-]?[0-9]+(?:\.[0-9]+)?C$"))
            {
                return true;
            }

            if (Regex.IsMatch(
                text,
                @"^[0-9]+(?:\.[0-9]+)?%$"))
            {
                return true;
            }

            return false;
        }

        private static bool ContainsEnglishLetter(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if ((c >= 'A' && c <= 'Z') ||
                    (c >= 'a' && c <= 'z'))
                {
                    return true;
                }
            }

            return false;
        }
#endif
    }
}
