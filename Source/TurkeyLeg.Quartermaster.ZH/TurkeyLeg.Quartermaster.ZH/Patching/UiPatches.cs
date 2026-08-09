using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace TurkeyLeg.Quartermaster.ZH
{
    // =========================================================
    // Quartermaster 内部方法
    // 弱绑定：作者重构后目标不存在就静默跳过
    // =========================================================

    [HarmonyPatch]
    public static class MainWindowLblPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                "Quartermaster.MainTabWindow_BestArmor",
                "Lbl",
                typeof(Rect),
                typeof(string)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(ref string s)
        {
            if (QmZh.Active)
            {
                s = QmZh.T(s);
            }
        }
    }

    [HarmonyPatch]
    public static class MainWindowBuildTooltipPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethodByTypeNames(
                "Quartermaster.MainTabWindow_BestArmor",
                "BuildTooltip",
                "Quartermaster.ArmorRecommendation"
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Postfix(ref string __result)
        {
            if (QmZh.Active &&
                !string.IsNullOrEmpty(__result))
            {
                __result = QmZh.T(__result);
            }
        }
    }

    // =========================================================
    // Verse.Widgets
    //
    // 这些是 RimWorld 公共 UI API。
    // 目标重载找不到时 Prepare=false，不因 API 变化阻止启动。
    // =========================================================

    [HarmonyPatch]
    public static class WidgetsLabelStringPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Widgets),
                "Label",
                typeof(Rect),
                typeof(string)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(ref string label)
        {
            if (QmZh.Active)
            {
                label = QmZh.T(label);
            }
            else
            {
                // 仅允许极少数 Quartermaster 设置/Options 文本翻译。
                label = QmZh.TOutsideContext(label);
            }
        }
    }

    [HarmonyPatch]
    public static class WidgetsLabelTaggedPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Widgets),
                "Label",
                typeof(Rect),
                typeof(TaggedString)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(ref TaggedString label)
        {
            if (QmZh.Active)
            {
                label = QmZh.T(label);
            }
            else
            {
                label = QmZh.TOutsideContext(label);
            }
        }
    }

    /// <summary>
    /// CheckboxLabeled 在不同 RimWorld 小版本可能增加/调整可选参数。
    /// 不硬编码完整签名：只 Patch 含 string label 参数的重载。
    /// 如果未来不存在匹配重载，TargetMethods 返回空集合并自然跳过。
    /// </summary>
    [HarmonyPatch]
    public static class WidgetsCheckboxLabeledPatch
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            IEnumerable<MethodInfo> methods =
                AccessTools.GetDeclaredMethods(typeof(Widgets));

            foreach (MethodInfo method in methods)
            {

                if (!string.Equals(
                    method.Name,
                    "CheckboxLabeled",
                    StringComparison.Ordinal))
                {
                    continue;
                }

                ParameterInfo[] parameters =
                    method.GetParameters();

                for (int p = 0; p < parameters.Length; p++)
                {
                    ParameterInfo parameter = parameters[p];

                    if (parameter.ParameterType == typeof(string) &&
                        string.Equals(
                            parameter.Name,
                            "label",
                            StringComparison.Ordinal))
                    {
                        yield return method;
                        break;
                    }
                }
            }
        }

        public static void Prefix(ref string label)
        {
            if (QmZh.Active)
            {
                label = QmZh.T(label);
            }
            else
            {
                label = QmZh.TOutsideContext(label);
            }
        }
    }

    [HarmonyPatch]
    public static class WidgetsButtonTextPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Widgets),
                "ButtonText",
                typeof(Rect),
                typeof(string),
                typeof(bool),
                typeof(bool),
                typeof(bool),
                typeof(TextAnchor?)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(ref string label)
        {
            if (QmZh.Active)
            {
                label = QmZh.T(label);
            }
            else
            {
                label = QmZh.TOutsideContext(label);
            }
        }
    }

    // =========================================================
    // Tooltips
    // =========================================================

    [HarmonyPatch]
    public static class TooltipTipSignalPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(TooltipHandler),
                "TipRegion",
                typeof(Rect),
                typeof(TipSignal)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(
            Rect rect,
            ref TipSignal tip)
        {
            if (!QmZh.Active ||
                !Mouse.IsOver(rect) ||
                string.IsNullOrEmpty(tip.text))
            {
                return;
            }

            tip.text = QmZh.T(tip.text);
        }
    }

    [HarmonyPatch]
    public static class TooltipFuncPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(TooltipHandler),
                "TipRegion",
                typeof(Rect),
                typeof(Func<string>),
                typeof(int)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(
            Rect rect,
            ref Func<string> textGetter)
        {
            if (!QmZh.Active ||
                textGetter == null ||
                !Mouse.IsOver(rect))
            {
                return;
            }

            Func<string> original = textGetter;

            // 只有鼠标真的悬停在 Quartermaster 区域时才包一层委托。
            textGetter = delegate
            {
                return QmZh.T(original());
            };
        }
    }

    // =========================================================
    // Quartermaster 创建的确认框
    // =========================================================

    internal static class QuartermasterConfirmationFilter
    {
        internal static bool ShouldTranslate(
            TaggedString text)
        {
            if (QmZh.Active)
            {
                return true;
            }

            string raw = text.RawText;

            return !string.IsNullOrEmpty(raw) &&
                raw.IndexOf(
                    "Quartermaster",
                    StringComparison.Ordinal) >= 0;
        }
    }

    [HarmonyPatch]
    public static class DialogConfirmationPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Dialog_MessageBox),
                "CreateConfirmation",
                typeof(TaggedString),
                typeof(Action),
                typeof(bool),
                typeof(string),
                typeof(WindowLayer)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(
            ref TaggedString text,
            ref string title)
        {
            if (!QuartermasterConfirmationFilter
                .ShouldTranslate(text))
            {
                return;
            }

            text = QmZh.T(text);
            title = QmZh.T(title);
        }
    }

    [HarmonyPatch]
    public static class DialogConfirmationWithCancelPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Dialog_MessageBox),
                "CreateConfirmation",
                typeof(TaggedString),
                typeof(Action),
                typeof(Action),
                typeof(bool),
                typeof(string),
                typeof(WindowLayer)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(
            ref TaggedString text,
            ref string title)
        {
            if (!QuartermasterConfirmationFilter
                .ShouldTranslate(text))
            {
                return;
            }

            text = QmZh.T(text);
            title = QmZh.T(title);
        }
    }
}
