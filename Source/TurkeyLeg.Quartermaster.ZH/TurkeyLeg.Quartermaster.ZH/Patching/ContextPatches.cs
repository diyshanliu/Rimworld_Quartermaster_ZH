using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TurkeyLeg.Quartermaster.ZH
{
    /// <summary>
    /// Quartermaster 各窗口的翻译上下文。
    ///
    /// 这些类属于 Quartermaster 内部 UI，未来最可能被作者重构，
    /// 因此全部使用字符串类型名弱绑定。
    ///
    /// 目标不存在：
    /// Prepare() -> false -> Harmony 跳过，不阻止游戏启动。
    /// </summary>

    // =========================================================
    // 主窗口
    // =========================================================

    [HarmonyPatch]
    public static class MainWindowContextPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                "Quartermaster.MainTabWindow_BestArmor",
                "DoWindowContents",
                typeof(Rect)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix()
        {
            QmZh.Enter();
        }

        public static void Finalizer()
        {
            QmZh.Exit();
        }
    }

    // =========================================================
    // 自定义加成预设
    // =========================================================

    [HarmonyPatch]
    public static class BuffPresetEditorContextPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                "Quartermaster.Dialog_BuffPresetEditor",
                "DoWindowContents",
                typeof(Rect)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix()
        {
            QmZh.Enter();
        }

        public static void Finalizer()
        {
            QmZh.Exit();
        }
    }

    // =========================================================
    // 排除项管理
    // =========================================================

    [HarmonyPatch]
    public static class ExclusionManagerContextPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                "Quartermaster.Dialog_ExclusionManager",
                "DoWindowContents",
                typeof(Rect)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix()
        {
            QmZh.Enter();
        }

        public static void Finalizer()
        {
            QmZh.Exit();
        }
    }

    // =========================================================
    // 固定物品选择器
    // =========================================================

    [HarmonyPatch]
    public static class SetItemPickerContextPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                "Quartermaster.Dialog_SetItemPicker",
                "DoWindowContents",
                typeof(Rect)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix()
        {
            QmZh.Enter();
        }

        public static void Finalizer()
        {
            QmZh.Exit();
        }
    }

    // =========================================================
    // Mod 设置页
    //
    // 如果作者以后改名/删除此方法，本补丁会自动跳过。
    // QmZh.TOutsideContext 仍可为已知设置文本提供一层兜底。
    // =========================================================

    [HarmonyPatch]
    public static class QuartermasterSettingsContextPatch
    {
        private static readonly MethodBase Target =
            QmPatchTools.FindMethod(
                "Quartermaster.QuartermasterMod",
                "DoSettingsWindowContents",
                typeof(Rect)
            );

        public static bool Prepare()
        {
            return Target != null;
        }

        public static MethodBase TargetMethod()
        {
            return Target;
        }

        public static void Prefix()
        {
            QmZh.Enter();
        }

        public static void Finalizer()
        {
            QmZh.Exit();
        }
    }
}
