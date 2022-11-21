using BehTree;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.CombatSkill;
using GameData.Domains.Item;
using GameData.Domains.SpecialEffect.CombatSkill.Emeipai.FistAndPalm;
using GameData.Domains.Taiwu;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Qsc
{

    public class QscLootUtil
    {
        public static List<ItemKey> SimpleGenerateItemList(TaiwuEvent ev, QscData.LootClass loot, int qualityModifier, int count, string affix = "")
        {
            int worldState = QscCoreUtils.GetWorldState(ev);
            int baseChancesIdx = Math.Clamp(worldState + qualityModifier / 100, 0, 8);
            int otherChancesIdx = baseChancesIdx;
            int mixmod = Math.Abs(qualityModifier) % 100;
            if (qualityModifier > 0 && baseChancesIdx != 8)
            {
                otherChancesIdx = baseChancesIdx + 1;
            }
            else if (qualityModifier < 0 && baseChancesIdx != 0)
            {
                otherChancesIdx = baseChancesIdx - 1;
            }
            GradeTable TmpTable = new GradeTable(QscData.GenWeights[baseChancesIdx], 100 - mixmod, QscData.GenWeights[otherChancesIdx], mixmod);
            return GenerateItemList(ev, loot, TmpTable, count, affix);

        }
        /*public static int RandomJueji(TaiwuEvent ev, GradeTable weights=null)
        {
            int grade = 0;
            if (weights == null)
            {
                weights = new GradeTable();
            }
            grade = weights.draw(ev, "jueji");

            int twid = DomainManager.Taiwu.GetTaiwuCharId();
            Dictionary<short, GameData.Domains.CombatSkill.CombatSkill> selfCombatSkills = DomainManager.CombatSkill.GetCharCombatSkills(twid);

            return -1;
        }*/
        public static int RandomGongFa(TaiwuEvent ev, QscData.GongFaClass type, GradeTable weights=null)
        {
            return -1;
        }
        public static void ActivateCombatSkill(short id)
        {
            DataContext context = DataContextManager.GetCurrentThreadDataContext();
            TaiwuDomain twdom = GameData.Domains.DomainManager.Taiwu;
            var twid = twdom.GetTaiwuCharId();
            ushort readingState;
            if (id == 676 || id == 693)
            {
                readingState = 31 + (31 << 10) + (1 << 7) + (1 << 8); // 老君拂尘功，扁鹊神针不允许正练
            }
            else
            {
                readingState = 0xFFFF;
            }

            TaiwuCombatSkill taiwuCombatSkill;
            bool flag = DomainManager.Taiwu.TryGetElement_CombatSkills(id, out taiwuCombatSkill);
            if (!flag)
            {
                DomainManager.Taiwu.TaiwuLearnCombatSkill(context, id, 0);
            }
            var skill = DomainManager.CombatSkill.GetElement_CombatSkills(
                new CombatSkillKey(twid, id)
                );

            GameData.Domains.CombatSkill.CombatSkill element_CombatSkills = DomainManager.CombatSkill.GetElement_CombatSkills(
                new CombatSkillKey(twid, id));
            element_CombatSkills.SetPracticeLevel(100, context);
            element_CombatSkills.SetReadingState(readingState, context);

            // GameData.Domains.CombatSkill.CombatSkill skill = new CombatSkill(twid, id, 100, readingState);
            AdaptableLog.Info("ActivateCombatSkill:" + id);
            //twdom.RegisterCombatSkill(GameData.Domains.DomainManager.TaiwuEvent.MainThreadDataContext, skill);
            QscGongFaData.removeFromGongFaTbl(id)  ;

        }
        public static void ActivateLifeSkill(short id)
        {
            TaiwuDomain twdom = GameData.Domains.DomainManager.Taiwu;
            var twid = twdom.GetTaiwuCharId();
            GameData.Domains.Character.LifeSkillItem skill = new GameData.Domains.Character.LifeSkillItem(id);
            skill.ReadingState = 31;
            twdom.RegisterLifeSkill(GameData.Domains.DomainManager.TaiwuEvent.MainThreadDataContext, skill);
        }

        public static List<ItemKey> GenerateSameGradeItemList(TaiwuEvent ev, QscData.LootClass loot, int level, int count, string affix = "")
        {
            // probably unused
            // generate a list of rewatd items, all from same level
            int[] source = QscData.LootClassMap[(int)loot][level];
            var creator = QscData.LootCreator[(int)loot];
            List<ItemKey> result = new List<ItemKey> { };
            for (int i = 0; i < count; i++)
            {
                short itemID = (short)QscCoreUtils.RandInt32(ev, 0, source.Length, affix);
                ItemKey created = creator(GameData.Domains.DomainManager.TaiwuEvent.MainThreadDataContext, itemID);
                result.Append(created);
            }
            return result;
        }




        public static List<ItemKey> GenerateItemList(TaiwuEvent ev, QscData.LootClass loot, GradeTable levelchance, int count, string affix = "")
        {
            // generate a list of reward items, weighted by level chance, level chance need to be length 9...
            AdaptableLog.Info("Create Item Class" + loot);

            int[][] sources = QscData.LootClassMap[(int)loot];
            var creator = QscData.LootCreator[(int)loot];
            List<ItemKey> result = new List<ItemKey> { };
            for (int i = 0; i < count; i++)
            {
                int grade = levelchance.Draw(ev, affix);
                int[] source = sources[grade];
                short itemID = (short) source[QscCoreUtils.RandInt32(ev, 0, source.Length, affix)];
                //AdaptableLog.Info("Create Item Grade" + grade + "id" +itemID);
                //AdaptableLog.Info("Source is " + String.Join(',', source));
                ItemKey created = creator(GameData.Domains.DomainManager.TaiwuEvent.MainThreadDataContext, itemID);
                result.Add(created);
            }
            return result;
        }
        /*
        public static ItemKey GenerateItemReward(TaiwuEvent ev, QscData.LootClass loot, int level, string affix = "")
        {
            // generates a single reward item.
            int[] source = LootClassMap[(int)loot][level];
            var creator = LootCreator[(int)loot];
            short itemID = (short)QscCoreUtils.RandInt32(ev, 0, source.Length, affix);
            ItemKey created = creator(GameData.Domains.DomainManager.TaiwuEvent.MainThreadDataContext, itemID);
            return created;
        }*/

        /*public static int GenerateGongFa(TaiwuEvent ev, QscData.GongFaClass type, int level, int count, string affix = "")
        {

        }
        public static int GenerateJueji(TaiwuEvent ev, int level, int count, string affix = "")
        {

        }*/


    }
}
