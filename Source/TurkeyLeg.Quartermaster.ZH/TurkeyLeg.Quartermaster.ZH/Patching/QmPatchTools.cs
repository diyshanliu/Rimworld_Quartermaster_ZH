using System;
using System.Reflection;
using HarmonyLib;

namespace TurkeyLeg.Quartermaster.ZH
{
    /// <summary>
    /// Harmony 目标解析工具。
    ///
    /// Quartermaster 自己的 UI 类型尽量通过完整类型名弱绑定：
    /// 类型/方法在以后版本中被删除或改名时返回 null，由 Patch.Prepare() 静默跳过。
    /// </summary>
    internal static class QmPatchTools
    {
        internal static Type FindType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }

            return AccessTools.TypeByName(typeName);
        }

        internal static MethodBase FindMethod(
            string typeName,
            string methodName,
            params Type[] argumentTypes)
        {
            Type type = FindType(typeName);
            if (type == null)
            {
                return null;
            }

            return AccessTools.Method(
                type,
                methodName,
                argumentTypes
            );
        }

        /// <summary>
        /// 参数类型本身也属于 Quartermaster 时使用。
        /// 任意一个类型不存在就直接返回 null。
        /// </summary>
        internal static MethodBase FindMethodByTypeNames(
            string typeName,
            string methodName,
            params string[] argumentTypeNames)
        {
            Type type = FindType(typeName);
            if (type == null)
            {
                return null;
            }

            if (argumentTypeNames == null ||
                argumentTypeNames.Length == 0)
            {
                return AccessTools.Method(type, methodName);
            }

            Type[] argumentTypes =
                new Type[argumentTypeNames.Length];

            for (int i = 0; i < argumentTypeNames.Length; i++)
            {
                Type argumentType =
                    FindType(argumentTypeNames[i]);

                if (argumentType == null)
                {
                    return null;
                }

                argumentTypes[i] = argumentType;
            }

            return AccessTools.Method(
                type,
                methodName,
                argumentTypes
            );
        }

        /// <summary>
        /// RimWorld / Verse 公共 API 的精确重载查找。
        /// 找不到时同样返回 null，便于 Prepare() 跳过。
        /// </summary>
        internal static MethodBase FindMethod(
            Type type,
            string methodName,
            params Type[] argumentTypes)
        {
            if (type == null)
            {
                return null;
            }

            return AccessTools.Method(
                type,
                methodName,
                argumentTypes
            );
        }
    }
}
