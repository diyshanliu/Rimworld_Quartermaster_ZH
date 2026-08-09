# Quartermaster 简繁汉化 DLL 源码

本仓库保存 RimWorld Mod **Quartermaster** 的运行时汉化 DLL 源码。

这个项目的目标不是修改 Quartermaster 原始程序集，而是通过 **Harmony** 在运行时拦截 Quartermaster 的部分 UI 文本、动态文本、Tooltip、消息与确认框，再调用 RimWorld 自带的 `Translate()` 系统读取 Keyed XML，从而实现简体 / 繁体中文共用一套 DLL。

> 本项目只负责汉化逻辑，不修改 Quartermaster 原始功能、数值或玩法。

---

## 汉化思路

Quartermaster 的文本并不全部来自 XML，其中一部分是代码运行时拼接出来的，例如：

- 主窗口标签、按钮、选项菜单
- 武器 / 护甲表格标题
- 动态排序标题
- `Pin items`、缺少研究 / 工作台 / 材料等状态
- 武器与装备 Tooltip
- 排除物品后的左上角消息
- 创建着装策略后的提示
- 确认框文本

因此项目采用两层翻译方式。

### 1. 固定文本

固定文本统一放在 `QmTranslationCatalog.cs`：

```csharp
{ "Armor", "TurkeyLeg_QM_Armor" }
```

DLL 中只保存：

```text
英文原文 -> XML Key
```

真正的中文位于：

```text
Languages/ChineseSimplified/Keyed/
Languages/ChineseTraditional/Keyed/
```

这样简体和繁体可以共用同一个 DLL，C# 中尽量不直接硬编码中文。

### 2. 动态文本

对于包含变量的字符串，不使用大量完整字典条目，而是在 `QmZh.cs` 中解析结构，例如：

```text
Min craftable: 3
Body [Skin/Mid]
needs research, bench, materials
Created apparel policy "..." allowing 2 item(s).
```

解析出变量后，再调用对应的 XML Key：

```csharp
"TurkeyLeg_QM_MinCraftable"
    .Translate(value)
```

身体部位、服装层级、研究 / 工作台 / 材料等 token 也使用独立 XML Key，不直接在 DLL 中返回中文。

---

## 翻译流程

主要入口为：

```text
QmZh.T()
```

处理顺序大致为：

```text
缓存命中
  ↓
Exact 固定文本
  ↓
多行装备 / 武器 Tooltip
  ↓
动态 Tooltip 单行
  ↓
动态 UI 文本
  ↓
未识别文本原样返回
```

已翻译结果会进入小型缓存，减少同一 UI 文本的重复解析。

Release 版本的动态解析主要使用：

```text
Dictionary.TryGetValue
StartsWith
EndsWith
IndexOf
Substring
```

避免在正常 UI 热路径中大量使用 Regex。

---

## Harmony Patch 安全策略

项目尽量区分两类目标。

### Quartermaster 内部 UI

例如：

```text
MainTabWindow_BestArmor
Dialog_BuffPresetEditor
Dialog_ExclusionManager
Dialog_SetItemPicker
QuartermasterMod
```

这些类型和方法可能随着 Quartermaster 更新而重构，因此尽量通过：

```csharp
AccessTools.TypeByName(...)
AccessTools.Method(...)
Prepare()
```

进行弱绑定。

如果作者以后删除或重命名某个 UI：

```text
目标不存在
→ TargetMethod 返回 null
→ Prepare() 返回 false
→ 对应汉化 Patch 被跳过
```

设计目标是：

> **优先出现局部英文，而不是因为一个 UI 方法改名导致整个汉化 DLL 报错。**

### RimWorld / Verse 公共 API

例如：

```text
Widgets.Label
Widgets.ButtonText
TooltipHandler.TipRegion
Messages.Message
Dialog_MessageBox.CreateConfirmation
```

这些属于 RimWorld 公共接口，使用精确 Harmony Patch。

对于全局 UI API，汉化器会额外检查：

```text
QmZh.Active
```

或者使用严格白名单，避免无条件处理其他 Mod 的 UI。

---

## 对其他 Mod 的影响控制

本项目会 Patch 少量 RimWorld 全局 UI 方法，因此重点是限制实际翻译范围。

例如左上角 `Messages.Message()` 并不会把所有消息送入汉化器，而是先判断是否符合 Quartermaster 自己的消息格式：

```text
... hidden from Quartermaster ...
Created apparel policy "...
```

只有命中后才调用：

```csharp
QmZh.T()
```

其他原版或 Mod 消息直接放行。

设置页和 Options 浮动菜单则使用 `OutsideContext` 白名单，只允许明确属于 Quartermaster 的少量字符串在主窗口上下文之外翻译，避免通用词误伤其他 Mod。

---

## Debug 与 Release

项目使用条件编译符号：

```text
TURKEYLEG_DEVLOG
```

区分开发版与发行版。

### Debug

Debug 配置可启用：

```text
TURKEYLEG_DEVLOG
```

未识别的英文文本会记录到：

```text
TurkeyLeg_Quartermaster_Untranslated.txt
```

用于开发过程中查找：

- 新增 UI
- 漏翻文本
- Quartermaster 更新后变化的字符串
- 尚未覆盖的动态文本

记录器会去重，并过滤纯数字、时间、百分比等无意义内容。

### Release

Release 配置 **不定义**：

```text
TURKEYLEG_DEVLOG
```

因此以下开发功能会在编译阶段直接被裁掉：

```text
TXT 写入
System.IO
漏翻 HashSet
英语字符检测
漏翻过滤 Regex
RecordMissing()
```

Release 版本只保留正式汉化逻辑，不在用户本地生成漏翻 TXT。

推荐发布时使用：

```text
Release
Optimize code = 开启
```

---

## 主要源码结构

```text
HarmonyInit.cs
    Harmony 初始化

QmZh.cs
    翻译入口、缓存、动态文本与 Tooltip 解析

QmTranslationCatalog.cs
    固定文本、OutsideContext、SlotTokens、
    RequirementTokens 等翻译映射

QmPatchTools.cs
    安全查找 Quartermaster 类型与方法

ContextPatches.cs
    Quartermaster 窗口翻译上下文

UiPatches.cs
    Label、Button、Tooltip、确认框等 UI Patch

MessagePatches.cs
    Quartermaster 左上角消息翻译与过滤
```

---

## 构建环境

项目类型：

```text
Class Library
.NET Framework 4.8
```

主要引用：

```text
Assembly-CSharp.dll
0Harmony.dll
Quartermaster.dll
UnityEngine.CoreModule.dll
UnityEngine.TextRenderingModule.dll
```

RimWorld / Mod DLL 建议：

```text
Copy Local = False
```

生成后的程序集放入：

```text
1.6/Assemblies/
```

---

## 维护建议

Quartermaster 更新后，如果只是新增或修改文本，正常情况应该只是出现局部英文。

维护时建议优先：

1. 检查 Debug 漏翻 TXT；
2. 固定文本加入 `QmTranslationCatalog.Exact`；
3. 动态文本优先增加结构化解析；
4. 中文放入 Keyed XML，不直接写进 C#；
5. 如果某个 Quartermaster UI 方法被作者重构，再更新对应的安全 Patch；
6. 发布前使用 Release 构建并完整测试主要页面。

不建议直接修改 Quartermaster 原 DLL，也不建议为了一个新文本增加过于宽泛的全局 Harmony Hook。

---

## 说明

本项目是第三方汉化补丁，与 Quartermaster 原作者无隶属关系。

Quartermaster 及 RimWorld 的相关名称、程序集和资源版权归各自作者所有。
