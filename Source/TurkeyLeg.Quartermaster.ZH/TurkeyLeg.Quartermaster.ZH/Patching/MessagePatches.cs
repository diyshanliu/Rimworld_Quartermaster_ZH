using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace TurkeyLeg.Quartermaster.ZH
{
    /// <summary>
    /// 只允许明确属于 Quartermaster 的左上角消息进入翻译器。
    /// 其他原版 / Mod 消息只经过两个 Ordinal 字符串判断，不进入缓存、动态解析或漏翻日志。
    /// </summary>
    internal static class QuartermasterMessageFilter
    {
        private const string HiddenSuffix =
            " hidden from Quartermaster. Restore it from the Exclusions menu.";

        private const string PolicyPrefix =
            "Created apparel policy \"";

        internal static void TranslateIfNeeded(ref string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            if (text.EndsWith(
                HiddenSuffix,
                StringComparison.Ordinal))
            {
                text = QmZh.T(text);
                return;
            }

            if (text.StartsWith(
                PolicyPrefix,
                StringComparison.Ordinal))
            {
                text = QmZh.T(text);
            }
        }
    }

    // =========================================================
    // Messages.Message(string, MessageTypeDef, bool)
    // =========================================================

    [HarmonyPatch]
    public static class MessagesMessageSimplePatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Messages),
                "Message",
                typeof(string),
                typeof(MessageTypeDef),
                typeof(bool)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(ref string text)
        {
            QuartermasterMessageFilter.TranslateIfNeeded(
                ref text
            );
        }
    }

    // =========================================================
    // Messages.Message(string, LookTargets, MessageTypeDef, bool)
    // =========================================================

    [HarmonyPatch]
    public static class MessagesMessageLookTargetsPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Messages),
                "Message",
                typeof(string),
                typeof(LookTargets),
                typeof(MessageTypeDef),
                typeof(bool)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(ref string text)
        {
            QuartermasterMessageFilter.TranslateIfNeeded(
                ref text
            );
        }
    }

    // =========================================================
    // Messages.Message(
    //     string, LookTargets, MessageTypeDef, Quest, bool)
    // =========================================================

    [HarmonyPatch]
    public static class MessagesMessageQuestPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                typeof(Messages),
                "Message",
                typeof(string),
                typeof(LookTargets),
                typeof(MessageTypeDef),
                typeof(Quest),
                typeof(bool)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix(ref string text)
        {
            QuartermasterMessageFilter.TranslateIfNeeded(
                ref text
            );
        }
    }
}
