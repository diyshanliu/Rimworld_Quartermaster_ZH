using System;
using System.Collections.Generic;

namespace TurkeyLeg.Quartermaster.ZH
{
    /// <summary>
    /// Quartermaster 的静态翻译目录。
    ///
    /// Exact：英文完整文本 -> RimWorld Keyed XML key。
    /// OutsideContext：允许在 Quartermaster 上下文之外翻译的白名单；
    ///                 它不是翻译表，因此对应文本仍必须保留在 Exact。
    /// SlotTokens / RequirementTokens：专门解析动态拼接 token，
    ///                 与 Exact 的独立 UI 文本用途不同。
    /// </summary>
    internal static class QmTranslationCatalog
    {
        internal static readonly Dictionary<string, string> Exact =
            new Dictionary<string, string>(StringComparer.Ordinal)
            {

                // =========================================================
                // 01. Main window
                // =========================================================

                { "Quartermaster", "TurkeyLeg_QM_Quartermaster" },
                { "Armor", "TurkeyLeg_QM_Armor" },
                { "Weapons", "TurkeyLeg_QM_Weapons" },
                { "Buffs", "TurkeyLeg_QM_Buffs" },
                { "Options", "TurkeyLeg_QM_Options" },
                { "Exclusions", "TurkeyLeg_QM_Exclusions" },
                { "Refresh", "TurkeyLeg_QM_Refresh" },
                { "Create policy", "TurkeyLeg_QM_CreatePolicy" },
                { "Edit preset", "TurkeyLeg_QM_EditPreset" },
                { "Show unavailable", "TurkeyLeg_QM_ShowUnavailable" },
                { "Best possible", "TurkeyLeg_QM_BestPossible" },
                { "Show crafted", "TurkeyLeg_QM_ShowCrafted" },
                { "Max armor:", "TurkeyLeg_QM_MaxArmor" },

                // =========================================================
                // 02. Presets
                // =========================================================

                { "Standard", "TurkeyLeg_QM_Standard" },
                { "Melee Heavy", "TurkeyLeg_QM_MeleeHeavy" },
                { "Melee Agile", "TurkeyLeg_QM_MeleeAgile" },
                { "Ranged Heavy", "TurkeyLeg_QM_RangedHeavy" },
                { "Ranged Agile", "TurkeyLeg_QM_RangedAgile" },
                { "Heat", "TurkeyLeg_QM_Heat" },
                { "Cold", "TurkeyLeg_QM_Cold" },
                { "+ Custom", "TurkeyLeg_QM_AddCustom" },
                { "Bonuses", "TurkeyLeg_QM_Bonuses" },

                // =========================================================
                // 03. Armor table
                // =========================================================

                { "ARMOR", "TurkeyLeg_QM_HeaderArmor" },
                { "SLOT", "TurkeyLeg_QM_HeaderSlot" },
                { "MATERIAL", "TurkeyLeg_QM_HeaderMaterial" },
                { "SHARP", "TurkeyLeg_QM_HeaderSharp" },
                { "BLUNT", "TurkeyLeg_QM_HeaderBlunt" },
                { "HEAT", "TurkeyLeg_QM_HeaderHeat" },
                { "STATUS", "TurkeyLeg_QM_HeaderStatus" },
                { "HEAD", "TurkeyLeg_QM_Head" },
                { "BODY", "TurkeyLeg_QM_Body" },
                { "LEGS", "TurkeyLeg_QM_Legs" },
                { "ACCESSORIES", "TurkeyLeg_QM_Accessories" },
                { "ON THE MAP", "TurkeyLeg_QM_OnTheMap" },
                { "+ Set item", "TurkeyLeg_QM_SetItem" },
                { "Refresh list", "TurkeyLeg_QM_RefreshList" },
                { "Include selected pawn's gear", "TurkeyLeg_QM_IncludeSelectedGear" },
                { "Show tainted", "TurkeyLeg_QM_ShowTainted" },
                { "Focus", "TurkeyLeg_QM_Focus" },
                { "Legs", "TurkeyLeg_QM_LegsNormal" },
                { "Body", "TurkeyLeg_QM_BodyNormal" },
                { "Head", "TurkeyLeg_QM_HeadNormal" },

                // =========================================================
                // 04. Armor summary
                // =========================================================

                { "ARMOR PER BODY PART", "TurkeyLeg_QM_ArmorPerBodyPart" },
                { "COMBINED SET STATS", "TurkeyLeg_QM_CombinedSetStats" },
                { "Torso", "TurkeyLeg_QM_Torso" },
                { "Arms", "TurkeyLeg_QM_Arms" },
                { "Utility", "TurkeyLeg_QM_Utility" },
                { "Combat", "TurkeyLeg_QM_Combat" },
                { "Social", "TurkeyLeg_QM_Social" },
                { "Crafting", "TurkeyLeg_QM_Crafting" },
                { "Nature", "TurkeyLeg_QM_Nature" },
                { "Knowledge", "TurkeyLeg_QM_Knowledge" },

                // =========================================================
                // 05. Weapons
                // =========================================================

                { "WEAPON", "TurkeyLeg_QM_Weapon" },
                { "SCORE", "TurkeyLeg_QM_Score" },
                { "DPS", "TurkeyLeg_QM_DPS" },
                { "AP", "TurkeyLeg_QM_AP" },
                { "DMG", "TurkeyLeg_QM_DMG" },
                { "RNG", "TurkeyLeg_QM_RNG" },
                { "aC", "TurkeyLeg_QM_AccuracyCloseShort" },
                { "aS", "TurkeyLeg_QM_AccuracyShortShort" },
                { "aM", "TurkeyLeg_QM_AccuracyMediumShort" },
                { "aL", "TurkeyLeg_QM_AccuracyLongShort" },
                { "TIME", "TurkeyLeg_QM_Time" },
                { "BRST", "TurkeyLeg_QM_Burst" },
                { "MASS", "TurkeyLeg_QM_Mass" },
                { "VALUE", "TurkeyLeg_QM_Value" },
                { "MELEE WEAPONS", "TurkeyLeg_QM_MeleeWeapons" },
                { "RANGED WEAPONS", "TurkeyLeg_QM_RangedWeapons" },
                { "THROWING WEAPONS", "TurkeyLeg_QM_ThrowingWeapons" },
                { "GRENADES", "TurkeyLeg_QM_Grenades" },
                { "LAUNCHERS", "TurkeyLeg_QM_Launchers" },
                { "Types:", "TurkeyLeg_QM_Types" },
                { "Melee", "TurkeyLeg_QM_Melee" },
                { "Ranged", "TurkeyLeg_QM_Ranged" },
                { "Throwing", "TurkeyLeg_QM_Throwing" },
                { "Grenades", "TurkeyLeg_QM_GrenadesNormal" },
                { "Launchers", "TurkeyLeg_QM_LaunchersNormal" },
                { "Score at:", "TurkeyLeg_QM_ScoreAt" },
                { "Close", "TurkeyLeg_QM_Close" },
                { "Short", "TurkeyLeg_QM_Short" },
                { "Medium", "TurkeyLeg_QM_Medium" },
                { "Long", "TurkeyLeg_QM_Long" },

                // =========================================================
                // 06. Buffs
                // =========================================================

                { "ITEM", "TurkeyLeg_QM_Item" },
                { "BONUS", "TurkeyLeg_QM_Bonus" },
                { "Filter:", "TurkeyLeg_QM_Filter" },
                { "search buffs: carry, research, work...", "TurkeyLeg_QM_SearchBuffsPlaceholder" },

                // =========================================================
                // 07. Main UI tooltips
                // =========================================================

                {
                    "Shows the best set your benches could craft if you had the materials. Ignores what's in your stockpiles right now.",
                    "TurkeyLeg_QM_BestPossibleTooltip"
                },
                {
                    "Armor % per body region the optimizer treats as 'enough' (default 200%). Above this, extra armor on that region scores nothing, so utility items are preferred there. RimWorld's effective armor cap is around 200%.",
                    "TurkeyLeg_QM_ArmorCapTooltip"
                },
                {
                    "Show items you already have - lying on the map or equipped by colonists - so you can compare them against what you can craft. The armor tab keeps its ON THE MAP section; the Weapons and Buffs lists mix the owned items into their rankings (marked owned / equipped / worn).",
                    "TurkeyLeg_QM_ShowCraftedTooltip"
                },
                {
                    "Create a custom preset that chases buffs you pick (e.g. work speed + construction, or research + work speed). It appears as a tab here and uses the same armor/bonuses slider.",
                    "TurkeyLeg_QM_CreateCustomPresetTooltip"
                },
                {
                    "Also consider the selected pawn's currently worn apparel (no weapons) when picking the best set on the map. Captured when you toggle this or press Refresh list; reselect a pawn and refresh to update.",
                    "TurkeyLeg_QM_IncludeSelectedGearTooltip"
                },
                {
                    "Cap how many apparel pieces the generated set may use. The optimizer always picks the most valuable pieces first, so a low cap keeps a lean set that gets close to the full result. 20 = no practical limit.",
                    "TurkeyLeg_QM_MaxPiecesTooltip"
                },
                {
                    "Rescan the map, research, benches, and materials, and rebuild all recommendations.",
                    "TurkeyLeg_QM_RefreshTooltip"
                },
                {
                    "Blend the set between pure protection (left) and this preset's bonus stats (right).",
                    "TurkeyLeg_QM_BlendTooltip"
                },
                {
                    "Lists every craftable item in the game, showing the research, bench, or materials you're still missing (in red) for each.",
                    "TurkeyLeg_QM_ShowUnavailableTooltip"
                },
                {
                    "Only count gear your stockpiles could craft at least this many times (any allowed material). Raise it when outfitting the whole colony: an item craftable just once won't gear ten colonists. 1 = show anything craftable at all.",
                    "TurkeyLeg_QM_MinCraftableTooltip"
                },
                { "Hide items, whole mods, weapon classes, or benches from recommendations.", "TurkeyLeg_QM_ExclusionsTooltip" },
                {
                    "Window options: lock/unlock dragging and resizing, reset the position, or open the mod settings (including the classic interface toggle).",
                    "TurkeyLeg_QM_OptionsTooltip"
                },
                {
                    "Include corpse-tainted apparel in this list (marked tainted). Colonists take a mood penalty for wearing tainted clothing.",
                    "TurkeyLeg_QM_ShowTaintedTooltip"
                },
                {
                    "Hide this item from recommendations (asks to confirm; Shift-click to skip).\nManage hidden items via the Exclusions button.",
                    "TurkeyLeg_QM_HideItemTooltip"
                },
                {
                    "Pin specific items into this category. Pinned items are always included in the generated set (marked \"enforced\"), and the rest of the set is built around them.",
                    "TurkeyLeg_QM_PinItemsTooltip"
                },
                {
                    "Create a RimWorld apparel policy allowing only the apparel in the set shown above (including pinned items). Named after the active tab, with a number to avoid clashes. Assign it from a colonist's Apparel tab.",
                    "TurkeyLeg_QM_CreatePolicyTooltip"
                },
                { "Jump to and select this item on the map.", "TurkeyLeg_QM_FocusTooltip" },
                {
                    "Re-scan items on the map for this set. The list stays put while you equip pieces, so you don't lose your place; press this to re-evaluate against what's left on the map.",
                    "TurkeyLeg_QM_RefreshListTooltip"
                },
                { "Show or hide the melee section of the weapon list.", "TurkeyLeg_QM_ShowMeleeTooltip" },
                { "Show or hide the ranged section of the weapon list.", "TurkeyLeg_QM_ShowRangedTooltip" },
                { "Show or hide the throwing section of the weapon list.", "TurkeyLeg_QM_ShowThrowingTooltip" },
                { "Show or hide the grenades section of the weapon list.", "TurkeyLeg_QM_ShowGrenadesTooltip" },
                { "Show or hide the launchers section of the weapon list.", "TurkeyLeg_QM_ShowLaunchersTooltip" },
                { "Click to collapse this buff category.", "TurkeyLeg_QM_CollapseBuffTooltip" },
                { "Click to expand this buff category.", "TurkeyLeg_QM_ExpandBuffTooltip" },

                // =========================================================
                // 08. Custom preset dialog
                // =========================================================

                { "Cancel", "TurkeyLeg_QM_Cancel" },
                { "Save", "TurkeyLeg_QM_Save" },
                { "Select a preset tab to edit it (Standard cannot be edited).", "TurkeyLeg_QM_SelectPresetToEdit" },
                { "New custom preset", "TurkeyLeg_QM_NewCustomPreset" },
                {
                    "Pick the buffs this outfit should chase (e.g. work speed + construction, or research + work speed). The preset gets its own tab, and its slider blends armor against these buffs. Stats marked \"lower is better\" are chased downward.",
                    "TurkeyLeg_QM_CustomPresetDescription"
                },
                { "Name:", "TurkeyLeg_QM_Name" },
                { "auto-named from picked buffs", "TurkeyLeg_QM_AutoNamed" },
                { "Search...", "TurkeyLeg_QM_SearchPlaceholder" },
                { "TEMPERATURE", "TurkeyLeg_QM_Temperature" },
                { "MOBILITY", "TurkeyLeg_QM_Mobility" },
                { "SHOOTING", "TurkeyLeg_QM_Shooting" },
                { "MELEE", "TurkeyLeg_QM_MeleeCategory" },
                { "WORK", "TurkeyLeg_QM_Work" },
                { "SOCIAL", "TurkeyLeg_QM_SocialCategory" },
                { "CARRY", "TurkeyLeg_QM_Carry" },
                { "OTHER", "TurkeyLeg_QM_Other" },
                { "lower is better", "TurkeyLeg_QM_LowerIsBetter" },
                { "No apparel in the current set to save into a policy.", "TurkeyLeg_QM_NoApparelForPolicy" },
                { "Pick at least one buff for the preset.", "TurkeyLeg_QM_PickAtLeastOneBuff" },
                { "Delete preset", "TurkeyLeg_QM_DeletePreset" },

                // =========================================================
                // 09. Exclusion manager
                // =========================================================

                { "Gear", "TurkeyLeg_QM_Gear" },
                { "Materials", "TurkeyLeg_QM_Materials" },
                { "Search:", "TurkeyLeg_QM_Search" },
                { "Suggest:", "TurkeyLeg_QM_Suggest" },
                { "Group by:", "TurkeyLeg_QM_GroupBy" },
                { "Heavy weapons", "TurkeyLeg_QM_HeavyWeapons" },
                { "Oversized (class/tag)", "TurkeyLeg_QM_OversizedClassTag" },
                { "By Mod", "TurkeyLeg_QM_ByMod" },
                { "By Type", "TurkeyLeg_QM_ByType" },
                { "By Class/Tag", "TurkeyLeg_QM_ByClassTag" },
                { "By Bench", "TurkeyLeg_QM_ByBench" },
                { "Staging: items to exclude", "TurkeyLeg_QM_StagingItems" },
                { "Staging: materials to exclude", "TurkeyLeg_QM_StagingMaterials" },
                { "Select all", "TurkeyLeg_QM_SelectAll" },
                { "None", "TurkeyLeg_QM_None" },
                { "No items match the current search/filter.", "TurkeyLeg_QM_NoItemsMatchSearchFilter" },
                { "No materials match the current search.", "TurkeyLeg_QM_NoMaterialsMatchSearch" },
                { "(not craftable)", "TurkeyLeg_QM_NotCraftable" },
                { "(no class/tag)", "TurkeyLeg_QM_NoClassTag" },
                { "Core", "TurkeyLeg_QM_Core" },
                { "Metals", "TurkeyLeg_QM_Metals" },
                { "Wood", "TurkeyLeg_QM_Wood" },
                { "Leather", "TurkeyLeg_QM_Leather" },
                { "Fabric", "TurkeyLeg_QM_Fabric" },
                { "Stony", "TurkeyLeg_QM_Stony" },
                { "Ingredients", "TurkeyLeg_QM_Ingredients" },

                // =========================================================
                // 10. Options & mod settings
                // =========================================================

                { "Lock window", "TurkeyLeg_QM_LockWindow" },
                { "Unlock window", "TurkeyLeg_QM_UnlockWindow" },
                { "Reset window position", "TurkeyLeg_QM_ResetWindowPosition" },
                { "Restore default presets", "TurkeyLeg_QM_RestoreDefaultPresets" },
                { "Mod settings..", "TurkeyLeg_QM_ModSettings" },
                { "Mod settings...", "TurkeyLeg_QM_ModSettings" },
                { "Interface", "TurkeyLeg_QM_Interface" },
                { "Use classic interface", "TurkeyLeg_QM_UseClassicInterface" },
                {
                    "The refreshed interface groups the view tabs, display toggles and actions into separate areas, and moves window locking/reset into the Options menu at the top right of the Quartermaster window.",
                    "TurkeyLeg_QM_RefreshedInterfaceDescription"
                },

                // =========================================================
                // 11. Weapon column tooltips
                // =========================================================

                {
                    "Weapon name.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_WeaponNameTooltip"
                },
                {
                    "Best material the colony can craft it from right now.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_BestMaterialTooltip"
                },
                {
                    "Default ranking: DPS(A) x (1 + 0.75 x armor penetration). Accuracy-weighted damage output with a bonus for punching through armor. Accuracy is averaged over the selected score ranges; a selected range the weapon can't fire at counts as 0.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_WeaponScoreTooltip"
                },
                {
                    "Ranged: damage per second weighted by accuracy over the selected score ranges (DPSA). Melee: average DPS.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_DPSTooltip"
                },
                {
                    "Armor penetration.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_APTooltip"
                },
                {
                    "Damage per shot (ranged) or average tool damage (melee).\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_DamageTooltip"
                },
                {
                    "Maximum range in cells (ranged only).\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_RangeTooltip"
                },
                {
                    "Accuracy at close range (3 cells). Grey value = the weapon can't fire at this distance (stat shown for reference; scores 0 if the range is selected). Color compares the weapons currently listed: red = worst, green = best.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_AccuracyCloseTooltip"
                },
                {
                    "Accuracy at short range (12 cells). Grey value = the weapon can't fire at this distance (stat shown for reference; scores 0 if the range is selected). Color compares the weapons currently listed: red = worst, green = best.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_AccuracyShortTooltip"
                },
                {
                    "Accuracy at medium range (25 cells). Grey value = the weapon can't fire at this distance (stat shown for reference; scores 0 if the range is selected). Color compares the weapons currently listed: red = worst, green = best.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_AccuracyMediumTooltip"
                },
                {
                    "Accuracy at long range (40 cells). Grey value = the weapon can't fire at this distance (stat shown for reference; scores 0 if the range is selected). Color compares the weapons currently listed: red = worst, green = best.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_AccuracyLongTooltip"
                },
                {
                    "Seconds per attack cycle: aim + cooldown (ranged), cooldown (melee). Lower is better.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_AttackCycleTooltip"
                },
                {
                    "Shots per burst (ranged only).\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_BurstTooltip"
                },
                {
                    "Mass in kg. Lower is better.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_MassTooltip"
                },
                {
                    "Market value.\n\nClick to sort by this column; click again to flip the order.",
                    "TurkeyLeg_QM_MarketValueTooltip"
                },

                // =========================================================
                // 12. Weapon score-range tooltips
                // =========================================================

                {
                    "Include close range (3 cells) in the ranged weapon score.\n\nThe score averages accuracy over the selected ranges; a weapon that can't fire at a selected range scores 0 for that range.",
                    "TurkeyLeg_QM_IncludeCloseRangeTooltip"
                },
                {
                    "Include short range (12 cells) in the ranged weapon score.\n\nThe score averages accuracy over the selected ranges; a weapon that can't fire at a selected range scores 0 for that range.",
                    "TurkeyLeg_QM_IncludeShortRangeTooltip"
                },
                {
                    "Include medium range (25 cells) in the ranged weapon score.\n\nThe score averages accuracy over the selected ranges; a weapon that can't fire at a selected range scores 0 for that range.",
                    "TurkeyLeg_QM_IncludeMediumRangeTooltip"
                },
                {
                    "Include long range (40 cells) in the ranged weapon score.\n\nThe score averages accuracy over the selected ranges; a weapon that can't fire at a selected range scores 0 for that range.",
                    "TurkeyLeg_QM_IncludeLongRangeTooltip"
                },

                // =========================================================
                // 13. Generated item / weapon tooltip fixed text
                // =========================================================

                { "Crafting cost:", "TurkeyLeg_QM_CraftingCost" },
                { "Utility stats:", "TurkeyLeg_QM_UtilityStats" },
                { "Ranged weapon", "TurkeyLeg_QM_RangedWeapon" },
                { "Thrown weapon", "TurkeyLeg_QM_ThrownWeapon" },
                { "Melee weapon", "TurkeyLeg_QM_MeleeWeapon" },
                { "any material", "TurkeyLeg_QM_AnyMaterial" },
                { "Missing: research not completed", "TurkeyLeg_QM_MissingResearch" },
                { "Missing: required crafting bench not built", "TurkeyLeg_QM_MissingBench" },
                { "Missing: not enough materials in stockpiles", "TurkeyLeg_QM_MissingMaterials" },
                { "Close, Short, Medium, Long", "TurkeyLeg_QM_AllScoreRanges" },
                { "(unreachable ranges count as 0)", "TurkeyLeg_QM_UnreachableRanges" },
                {
                    "No weapons can be crafted right now.\nBuild crafting benches and complete research,\nor enable \"Show unavailable\" to see what's missing.",
                    "TurkeyLeg_QM_NoCraftableWeapons"
                },
                { "Quality:", "TurkeyLeg_QM_Quality" },
                {
                    "No armor can be crafted right now.\nBuild crafting benches and complete research,\nor enable \"Show unavailable\" to see what's missing.",
                    "TurkeyLeg_QM_NoCraftableArmor"
                },
                {
                    "No craftable items with Buffs stats.\nEnable \"Show unavailable\" to see items you haven't unlocked yet.",
                    "TurkeyLeg_QM_NoCraftableBuffItems"
                },

                // =========================================================
                // 14. DLC / general grouping tags
                // =========================================================

                { "Anomaly", "TurkeyLeg_QM_Tag_Anomaly" },
                { "Biotech", "TurkeyLeg_QM_Tag_Biotech" },
                { "Odyssey", "TurkeyLeg_QM_Tag_Odyssey" },
                { "Royalty", "TurkeyLeg_QM_Tag_Royalty" },
                { "Thrown", "TurkeyLeg_QM_Tag_Thrown" },
                { "Animal Part", "TurkeyLeg_QM_Tag_AnimalPart" },
                { "Apparel_Yttakin", "TurkeyLeg_QM_Tag_ApparelYttakin" },
                { "Artifact", "TurkeyLeg_QM_Tag_Artifact" },
                { "Accessories", "TurkeyLeg_Tag_Accessories" },
                { "Other", "TurkeyLeg_Tag_Other" },
                { "persona", "TurkeyLeg_QM_Persona" },

                // =========================================================
                // 15. Weapon / apparel class tags
                // =========================================================

                { "Artillery", "TurkeyLeg_QM_Tag_Artillery" },
                { "Artillery_BaseDestroyer", "TurkeyLeg_QM_Tag_ArtilleryBaseDestroyer" },
                { "AssaultRifle", "TurkeyLeg_QM_Tag_AssaultRifle" },
                { "Autopistol", "TurkeyLeg_QM_Tag_Autopistol" },
                { "Axe", "TurkeyLeg_QM_Tag_Axe" },
                { "BasicClothing", "TurkeyLeg_QM_Tag_BasicClothing" },
                { "BeamGraserGun", "TurkeyLeg_QM_Tag_BeamGraserGun" },
                { "BeltDefense", "TurkeyLeg_QM_Tag_BeltDefense" },
                { "BeltDefensePop", "TurkeyLeg_QM_Tag_BeltDefensePop" },
                { "BeltDefenseTox", "TurkeyLeg_QM_Tag_BeltDefenseTox" },
                { "BestowerHood", "TurkeyLeg_QM_Tag_BestowerHood" },
                { "Bladelink", "TurkeyLeg_QM_Tag_Bladelink" },
                { "Cape", "TurkeyLeg_QM_Tag_Cape" },
                { "ChargeBlasterHeavyGun", "TurkeyLeg_QM_Tag_ChargeBlasterHeavyGun" },
                { "Clothing", "TurkeyLeg_QM_Tag_Clothing" },
                { "Drugs", "TurkeyLeg_QM_Tag_Drugs" },
                { "EltexStaff", "TurkeyLeg_QM_Tag_EltexStaff" },
                { "EmpireGrenadeDestructive", "TurkeyLeg_QM_Tag_EmpireGrenadeDestructive" },
                { "ExoticMisc", "TurkeyLeg_QM_Tag_ExoticMisc" },
                { "Flamethrower", "TurkeyLeg_QM_Tag_Flamethrower" },
                { "GrenadeDestructive", "TurkeyLeg_QM_Tag_GrenadeDestructive" },
                { "GrenadeEMP", "TurkeyLeg_QM_Tag_GrenadeEMP" },
                { "GrenadeFlame", "TurkeyLeg_QM_Tag_GrenadeFlame" },
                { "GrenadeSmoke", "TurkeyLeg_QM_Tag_GrenadeSmoke" },
                { "GrenadeTox", "TurkeyLeg_QM_Tag_GrenadeTox" },
                { "Gun", "TurkeyLeg_QM_Tag_Gun" },
                { "GunHeavy", "TurkeyLeg_QM_Tag_GunHeavy" },
                { "Gunlink", "TurkeyLeg_QM_Tag_Gunlink" },
                { "GunSingleUse", "TurkeyLeg_QM_Tag_GunSingleUse" },
                { "HeavyTox", "TurkeyLeg_QM_Tag_HeavyTox" },
                { "HellsphereCannonGun", "TurkeyLeg_QM_Tag_HellsphereCannonGun" },
                { "HiTechArmor", "TurkeyLeg_QM_Tag_HiTechArmor" },
                { "HoraxArmor", "TurkeyLeg_QM_Tag_HoraxArmor" },
                { "Horaxian", "TurkeyLeg_QM_Tag_Horaxian" },
                { "HoraxianCeremonial", "TurkeyLeg_QM_Tag_HoraxianCeremonial" },
                { "HoraxWeapon", "TurkeyLeg_QM_Tag_HoraxWeapon" },
                { "IndustrialAdvanced", "TurkeyLeg_QM_Tag_IndustrialAdvanced" },
                { "IndustrialBasic", "TurkeyLeg_QM_Tag_IndustrialBasic" },
                { "IndustrialGunAdvanced", "TurkeyLeg_QM_Tag_IndustrialGunAdvanced" },
                { "IndustrialMilitaryAdvanced", "TurkeyLeg_QM_Tag_IndustrialMilitaryAdvanced" },
                { "IndustrialMilitaryBasic", "TurkeyLeg_QM_Tag_IndustrialMilitaryBasic" },
                { "InfernoCannonGun", "TurkeyLeg_QM_Tag_InfernoCannonGun" },
                { "LongShots", "TurkeyLeg_QM_Tag_LongShots" },
                { "LongSword", "TurkeyLeg_QM_Tag_LongSword" },
                { "Mechlord", "TurkeyLeg_QM_Tag_Mechlord" },
                { "MechonoidGunBreach", "TurkeyLeg_QM_Tag_MechonoidGunBreach" },
                { "MechonoidGunHeavy", "TurkeyLeg_QM_Tag_MechonoidGunHeavy" },
                { "MechonoidGunLongRange", "TurkeyLeg_QM_Tag_MechonoidGunLongRange" },
                { "MechonoidGunMedium", "TurkeyLeg_QM_Tag_MechonoidGunMedium" },
                { "MechonoidGunMiniFlameblaster", "TurkeyLeg_QM_Tag_MechonoidGunMiniFlameblaster" },
                { "MechonoidGunNeedleLauncher", "TurkeyLeg_QM_Tag_MechonoidGunNeedleLauncher" },
                { "MechonoidGunShortRange", "TurkeyLeg_QM_Tag_MechonoidGunShortRange" },
                { "MechonoidGunSlugthrower", "TurkeyLeg_QM_Tag_MechonoidGunSlugthrower" },
                { "MechonoidGunSpiner", "TurkeyLeg_QM_Tag_MechonoidGunSpiner" },
                { "MechonoidGunToxicNeedle", "TurkeyLeg_QM_Tag_MechonoidGunToxicNeedle" },
                { "MedievalMeleeAdvanced", "TurkeyLeg_QM_Tag_MedievalMeleeAdvanced" },
                { "MedievalMeleeBasic", "TurkeyLeg_QM_Tag_MedievalMeleeBasic" },
                { "MedievalMeleeDecent", "TurkeyLeg_QM_Tag_MedievalMeleeDecent" },
                { "MedievalMilitary", "TurkeyLeg_QM_Tag_MedievalMilitary" },
                { "MeleeBlunt", "TurkeyLeg_QM_Tag_MeleeBlunt" },
                { "MeleePiercer", "TurkeyLeg_QM_Tag_MeleePiercer" },
                { "Minigun", "TurkeyLeg_QM_Tag_Minigun" },
                { "Neolithic", "TurkeyLeg_QM_Tag_Neolithic" },
                { "NeolithicMeleeAdvanced", "TurkeyLeg_QM_Tag_NeolithicMeleeAdvanced" },
                { "NeolithicMeleeBasic", "TurkeyLeg_QM_Tag_NeolithicMeleeBasic" },
                { "NeolithicMeleeDecent", "TurkeyLeg_QM_Tag_NeolithicMeleeDecent" },
                { "NeolithicMeleeDestructive", "TurkeyLeg_QM_Tag_NeolithicMeleeDestructive" },
                { "NeolithicRangedBasic", "TurkeyLeg_QM_Tag_NeolithicRangedBasic" },
                { "NeolithicRangedChief", "TurkeyLeg_QM_Tag_NeolithicRangedChief" },
                { "NeolithicRangedDecent", "TurkeyLeg_QM_Tag_NeolithicRangedDecent" },
                { "NeolithicRangedFlame", "TurkeyLeg_QM_Tag_NeolithicRangedFlame" },
                { "NeolithicRangedHeavy", "TurkeyLeg_QM_Tag_NeolithicRangedHeavy" },
                { "NerveSpiker", "TurkeyLeg_QM_Tag_NerveSpiker" },
                { "NoRelic", "TurkeyLeg_QM_Tag_NoRelic" },
                { "PackJump", "TurkeyLeg_QM_Tag_PackJump" },
                { "PrestigeCombatGear", "TurkeyLeg_QM_Tag_PrestigeCombatGear" },
                { "Psychic", "TurkeyLeg_QM_Tag_Psychic" },
                { "PsychicApparel", "TurkeyLeg_QM_Tag_PsychicApparel" },
                { "PsychicWeapon", "TurkeyLeg_QM_Tag_PsychicWeapon" },
                { "PumpShotgun", "TurkeyLeg_QM_Tag_PumpShotgun" },
                { "RangedHeavy", "TurkeyLeg_QM_Tag_RangedHeavy" },
                { "RangedLight", "TurkeyLeg_QM_Tag_RangedLight" },
                { "Revolver", "TurkeyLeg_QM_Tag_Revolver" },
                { "Robe", "TurkeyLeg_QM_Tag_Robe" },
                { "Royal", "TurkeyLeg_QM_Tag_Royal" },
                { "RoyalRobe", "TurkeyLeg_QM_Tag_RoyalRobe" },
                { "RoyalTier2", "TurkeyLeg_QM_Tag_RoyalTier2" },
                { "RoyalTier3", "TurkeyLeg_QM_Tag_RoyalTier3" },
                { "RoyalTier4", "TurkeyLeg_QM_Tag_RoyalTier4" },
                { "RoyalTier5", "TurkeyLeg_QM_Tag_RoyalTier5" },
                { "RoyalTier6", "TurkeyLeg_QM_Tag_RoyalTier6" },
                { "RoyalTier7", "TurkeyLeg_QM_Tag_RoyalTier7" },
                { "SentryDroneGunShortRange", "TurkeyLeg_QM_Tag_SentryDroneGunShortRange" },
                { "ShortShots", "TurkeyLeg_QM_Tag_ShortShots" },
                { "SimpleGun", "TurkeyLeg_QM_Tag_SimpleGun" },
                { "SniperRifle", "TurkeyLeg_QM_Tag_SniperRifle" },
                { "SpacerGun", "TurkeyLeg_QM_Tag_SpacerGun" },
                { "SpacerMilitary", "TurkeyLeg_QM_Tag_SpacerMilitary" },
                { "Spear", "TurkeyLeg_QM_Tag_Spear" },
                { "TurretGun", "TurkeyLeg_QM_Tag_TurretGun" },
                { "Ultratech", "TurkeyLeg_QM_Tag_Ultratech" },
                { "UltratechMelee", "TurkeyLeg_QM_Tag_UltratechMelee" },
                { "UtilitySpecial", "TurkeyLeg_QM_Tag_UtilitySpecial" },
                { "Vacsuit", "TurkeyLeg_QM_Tag_Vacsuit" },
                { "WeaponBeamRepeater", "TurkeyLeg_QM_Tag_WeaponBeamRepeater" },
                { "WeaponRanged", "TurkeyLeg_QM_Tag_WeaponRangedv" },
                { "Western", "TurkeyLeg_QM_Tag_Western" },

                // =========================================================
                // 16. Pin item picker & confirmations
                // =========================================================

                {
                    "Checked items are always included in the generated set (shown as \"enforced\"). The optimizer builds the rest of the set around them.",
                    "TurkeyLeg_QM_PinPickerDescription"
                },
                { "Pinned only", "TurkeyLeg_QM_PinnedOnly" },
                { "Done", "TurkeyLeg_QM_Done" },
                { "No items match the current filter.", "TurkeyLeg_QM_NoItemsMatchCurrentFilter" },
                {"No craftable weapon definitions found.\n(Some weapon types are unticked in the Types row above.)", "TurkeyLeg_QM_NoCraftableWeaponDefinitions"},
                {"No weapons can be crafted right now.\nBuild crafting benches and complete research,\nor enable \"Show unavailable\" to see what's missing.\n(Some weapon types are unticked in the Types row above.)",
    "TurkeyLeg_QM_NoCraftableWeaponsFiltered"},
                { "enforced", "TurkeyLeg_QM_enforced" },
                { "You can restore it anytime from the Exclusions menu.", "TurkeyLeg_QM_RestoreFromExclusions" },
            };

        // =============================================================
        // 20. Outside-context allowlist
        // =============================================================
        //
        // 注意：这里与 Exact 出现相同英文是有意的。
        // OutsideContext 只负责“是否允许在全局 UI 中翻译”；
        // 真正的英文 -> XML key 映射仍由 Exact 提供。

        internal static readonly HashSet<string> OutsideContext =
            new HashSet<string>(StringComparer.Ordinal)
            {
                "Quartermaster",
                "Interface",
                "Use classic interface",
                "The refreshed interface groups the view tabs, display toggles and actions into separate areas, and moves window locking/reset into the Options menu at the top right of the Quartermaster window.",
                "Lock window",
                "Unlock window",
                "Reset window position",
                "Restore default presets",
                "Mod settings..",
                "Mod settings..."
            };

        // =============================================================
        // 21. Body-part / apparel-layer tokens
        // =============================================================
        //
        // Body / Head / Legs / Torso / Arms 也可能作为独立 UI 标签出现，
        // 因此它们在 Exact 中的独立条目不要删除。

        internal static readonly Dictionary<string, string> SlotTokens =
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { "Body",      "TurkeyLeg_QM_Slot_Body" },
                { "Torso",     "TurkeyLeg_QM_Slot_Body" },

                { "Head",      "TurkeyLeg_QM_Slot_Head" },
                { "Legs",      "TurkeyLeg_QM_Slot_Legs" },
                { "Arms",      "TurkeyLeg_QM_Slot_Arms" },
                { "Hands",     "TurkeyLeg_QM_Slot_Hands" },
                { "Feet",      "TurkeyLeg_QM_Slot_Feet" },
                { "Neck",      "TurkeyLeg_QM_Slot_Neck" },
                { "Shoulders", "TurkeyLeg_QM_Slot_Shoulders" },
                { "Eyes",      "TurkeyLeg_QM_Slot_Eyes" },
                { "Ears",      "TurkeyLeg_QM_Slot_Ears" },
                { "Mouth",     "TurkeyLeg_QM_Slot_Mouth" },
                { "Face",      "TurkeyLeg_QM_Slot_Face" },

                { "Skin",      "TurkeyLeg_QM_Layer_Skin" },
                { "OnSkin",    "TurkeyLeg_QM_Layer_Skin" },

                { "Middle",    "TurkeyLeg_QM_Layer_Middle" },
                { "Mid",       "TurkeyLeg_QM_Layer_Middle" },

                { "Outer",     "TurkeyLeg_QM_Layer_Outer" },
                { "Shell",     "TurkeyLeg_QM_Layer_Shell" },
                { "Overhead",  "TurkeyLeg_QM_Layer_Overhead" },
                { "Belt",      "TurkeyLeg_QM_Layer_Belt" }
            };

        // =============================================================
        // 22. Requirement tokens
        // =============================================================

        internal static readonly Dictionary<string, string> RequirementTokens =
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { "research",  "TurkeyLeg_QM_Requirement_Research" },
                { "bench",     "TurkeyLeg_QM_Requirement_Bench" },
                { "materials", "TurkeyLeg_QM_Requirement_Materials" }
            };
    }
}
