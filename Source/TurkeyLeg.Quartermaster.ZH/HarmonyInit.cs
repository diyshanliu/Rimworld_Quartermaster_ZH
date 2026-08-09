using HarmonyLib;
using Verse;

namespace TurkeyLeg.Quartermaster.ZH
{
    /// <summary>
    /// 汉化补丁入口。
    /// Patch 类本身负责判断目标是否存在；可选 Quartermaster UI 消失时会跳过对应补丁。
    /// </summary>
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        private const string HarmonyId = "TurkeyLeg.Quartermaster.ZH";

        static HarmonyInit()
        {
            Harmony harmony = new Harmony(HarmonyId);
            harmony.PatchAll(typeof(HarmonyInit).Assembly);
        }
    }
}
