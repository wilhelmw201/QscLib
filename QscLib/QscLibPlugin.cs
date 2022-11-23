using GameData.Utilities;
using System;
using TaiwuModdingLib.Core.Plugin;
using HarmonyLib;
using GameData.Domains.Combat;
using GameData.Domains.Character;
using GameData.Common;
using Config;
using GameData.DomainEvents;
using GameData.Domains.Item;
using GameData.Domains;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using Character = GameData.Domains.Character.Character;
using GameData.GameDataBridge;
using GameData.Domains.Map;
using GameData.Domains.CombatSkill;

namespace Qsc
{

    [PluginConfig("Qsc", "Wilhelmw", "0.01")]
    public class QscDependencyPlugin : TaiwuRemakePlugin
    {
        public static int ProgressPerEvent = 8;


        Harmony harmony;

        public override void Dispose()
        {
            if (harmony != null)
            {
                AdaptableLog.Info("Disposing Mod: QTWDependencyPlugin");
                harmony.UnpatchSelf();
            }

        }

        public override void Initialize()
        {
            AdaptableLog.Info("Loading Mod: QscPlugin");
            int steps = 10;
            GameData.Domains.DomainManager.Mod.GetSetting(this.ModIdStr, "steps", ref steps);
            AdaptableLog.Info($" 每阶段步数 {steps}");
            if (steps <= 3) steps = 8;
            AdaptableLog.Info($" 每阶段步数 => {steps}");

            ProgressPerEvent = (int)Math.Ceiling(100.0 / steps);

            harmony = Harmony.CreateAndPatchAll(typeof(QscDependencyPlugin));
        }

        static bool isActivated()
        {
            return QscCoreUtils.lastCheckProgress >= 100 && QscCoreUtils.lastCheckProgress <= 210;
        }

        // 1 turn absorb
        [HarmonyPostfix, HarmonyPatch(typeof(CombatSkillDomain), "CalcNeigongLoopingEffect")]
        static void CalcNeigongLoopingEffectPost(ref ValueTuple<short, short> __result)
        {
            if (!isActivated()) return;
            __result = new ValueTuple<short, short>(900, __result.Item2);
        }

        [HarmonyPrefix, HarmonyPatch(typeof(CombatCharacter), "OnCombatEnd")]
        public static unsafe bool OnCombatEnd(CombatCharacter __instance, DataContext context)
        {
            if (!isActivated()) return true;

            Character this_character = AccessTools.FieldRefAccess<CombatCharacter, Character>(__instance, "_character");
            DefeatMarkCollection this_defeatMarkCollection = AccessTools.FieldRefAccess<CombatCharacter, DefeatMarkCollection>(__instance, "_defeatMarkCollection");
            Injuries this_injuries = AccessTools.FieldRefAccess<CombatCharacter, Injuries>(__instance, "_injuries");
            PoisonInts this_poison = AccessTools.FieldRefAccess<CombatCharacter, PoisonInts>(__instance, "_poison");
            int this_id = AccessTools.FieldRefAccess<CombatCharacter, int>(__instance, "_id");
            ItemKey[] this_weapons = AccessTools.FieldRefAccess<CombatCharacter, ItemKey[]>(__instance, "_weapons");
            NeiliAllocation this_neiliAllocation = AccessTools.FieldRefAccess<CombatCharacter, NeiliAllocation>(__instance, "_neiliAllocation");
            NeiliAllocation this_originBaseNeiliAllocation = AccessTools.FieldRefAccess<CombatCharacter, NeiliAllocation>(__instance, "_originBaseNeiliAllocation");
            DataUid this_poisonResistUid = AccessTools.FieldRefAccess<CombatCharacter, DataUid>(__instance, "_poisonResistUid");
            DataUid this_defeatMarkUid = AccessTools.FieldRefAccess<CombatCharacter, DataUid>(__instance, "_defeatMarkUid");
            

            bool isIntelligent = this_character.GetCreatingType() == 1;

            // TODO fatal damage will cause random injuries
            int fatals = this_defeatMarkCollection.FatalDamageMarkCount;

            this_character.SetInjuries(this_injuries, context);

            this_poison.Initialize();
            this_character.SetPoisoned(ref this_poison, context);

            this_character.SetDisorderOfQi((short)0, context);

            bool flag8 = isIntelligent;
            if (flag8)
            {
            }
            else
            {
                this_character.ClearEatingItems(context);
            }

            List<short> learnedSkills = this_character.GetLearnedCombatSkills();
            for (int i = 0; i < __instance.ForgetAfterCombatSkills.Count; i++)
            {
                short skillId = __instance.ForgetAfterCombatSkills[i];
                learnedSkills.Remove(skillId);
                DomainManager.CombatSkill.RemoveCombatSkill(this_id, skillId);
            }
            this_character.SetLearnedCombatSkills(learnedSkills, context);
            bool flag9 = isIntelligent;
            if (flag9)
            {
                NeiliAllocation extraNeiliAllocation = this_character.GetExtraNeiliAllocation();
                int currNeili = this_character.GetCurrNeili();
                for (int j = 0; j < 4; j++)
                {
                    this_neiliAllocation.Items[j] = (short)Math.Clamp(
                        (int)this_neiliAllocation.Items[j] - extraNeiliAllocation.Items[j],
                        0,
                        (int)this_originBaseNeiliAllocation.Items[j]);
                }
                this_character.SpecifyBaseNeiliAllocation(context, this_neiliAllocation);
                this_character.SpecifyCurrNeili(context, currNeili);
            }
            else
            {
                this_character.SpecifyBaseNeiliAllocation(context, this_originBaseNeiliAllocation);
            }
            bool flag10 = __instance.AnimalConfig == null;
            if (flag10)
            {
                DomainManager.Item.RemoveItem(context, this_weapons[3]);
                DomainManager.Item.RemoveItem(context, this_weapons[4]);
                DomainManager.Item.RemoveItem(context, this_weapons[5]);
            }
            String DataHandlerKey = string.Format("CombatChar_{0}", this_id);
            GameDataBridge.RemovePostDataModificationHandler(this_poisonResistUid, DataHandlerKey);
            GameDataBridge.RemovePostDataModificationHandler(this_defeatMarkUid, DataHandlerKey);

            // repair items on taiwu
            foreach (var dictItem in DomainManager.Taiwu.GetTaiwu().GetInventory().Items)
            {
                var itemKey = dictItem.Key;
                // var itemcount = dictItem.Value;
                ItemBase baseItem = DomainManager.Item.GetBaseItem(itemKey);
                var iType = baseItem.GetItemType();
                if (iType <= 2 && iType >= 0)
                {
                    baseItem.SetCurrDurability(baseItem.GetMaxDurability(), GameData.Domains.DomainManager.TaiwuEvent.MainThreadDataContext);
                }
            }
            return false;
        }

        [HarmonyPrefix, HarmonyPatch(typeof(MapDomain), "ParallelUpdateOnMonthChange")]
        // prevent resource regen on all blocks.
        public static bool no_recover_patch(DataContext context, int areaIdInt)
        {
            if (!isActivated()) return true;
            return false;
        }


    }
}