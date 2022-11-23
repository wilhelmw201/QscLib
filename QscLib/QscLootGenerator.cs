using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Qsc.QscGongFaData;

namespace Qsc
{
    public class LootTableEntry
    {
        public QscData.LootClass type { get; }
        public int count { get; }
        public int qualityBonus { get; }
        public LootTableEntry(QscData.LootClass type, int count, int QualityBonus)
        {
            this.type = type;
            this.count = count;
            this.qualityBonus = QualityBonus;
        }
    }

 
    public class QscLootGenerator
    {
        public static LootTable GetLootTableFromBlock(EMapBlockSubType type)
        {
            LootTable result = null;
            switch (type)
            {
                case EMapBlockSubType.Farmland:
                    result = RefLootTables.Farmland1;
                    break;
                case EMapBlockSubType.Gardens:
                    result = RefLootTables.Gardens1;
                    break;
                case EMapBlockSubType.StoneForest:
                    result = RefLootTables.StoneForest1;
                    break;
                case EMapBlockSubType.MulberryField:
                    result = RefLootTables.MulberryField1;
                    break;
                case EMapBlockSubType.HerbalGarden:
                    result = RefLootTables.HerbalGarden1;
                    break;
                case EMapBlockSubType.JadeMountain:
                    result = RefLootTables.JadeMountain1;
                    break;
                case EMapBlockSubType.Mountain:
                    result = RefLootTables.Mountain1;
                    break;
                case EMapBlockSubType.BigMountain:
                    result = RefLootTables.BigMountain1;
                    break;
                case EMapBlockSubType.Canyon:
                    result = RefLootTables.Canyon1;
                    break;
                case EMapBlockSubType.BigCanyon:
                    result = RefLootTables.BigCanyon1;
                    break;
                case EMapBlockSubType.Hill:
                    result = RefLootTables.Hill1;
                    break;
                case EMapBlockSubType.BigHill:
                    result = RefLootTables.BigHill1;
                    break;
                case EMapBlockSubType.Field:
                    result = RefLootTables.Field1;
                    break;
                case EMapBlockSubType.BigField:
                    result = RefLootTables.BigField1;
                    break;
                case EMapBlockSubType.Woodland:
                    result = RefLootTables.Woodland1;
                    break;
                case EMapBlockSubType.BigWoodland:
                    result = RefLootTables.BigWoodland1;
                    break;
                case EMapBlockSubType.RiverBeach:
                    result = RefLootTables.RiverBeach1;
                    break;
                case EMapBlockSubType.BigRiverBeach:
                    result = RefLootTables.BigRiverBeach1;
                    break;
                case EMapBlockSubType.Lake:
                    result = RefLootTables.Lake1;
                    break;
                case EMapBlockSubType.Jungle:
                    result = RefLootTables.Jungle1;
                    break;
                case EMapBlockSubType.Cave:
                    result = RefLootTables.Cave1;
                    break;
                case EMapBlockSubType.Swamp:
                    result = RefLootTables.Swamp1;
                    break;
                case EMapBlockSubType.TaoYuan:
                    result = RefLootTables.TaoYuan1;
                    break;
                case EMapBlockSubType.Valley:
                    result = RefLootTables.Valley1;
                    break;
                case EMapBlockSubType.Wild:
                    result = RefLootTables.Wild1;
                    break;
                case EMapBlockSubType.DarkPool:
                    result = RefLootTables.DarkPool1;
                    break;
                default:
                    result = RefLootTables.TestTable;
                    break;
            }
            return result;
        }
        public static int GenerateLootCharFromTable(TaiwuEvent ev, LootTableEntry[] table, string affix)
        {
            
            int charid = EventHelper.CreateNonIntelligentCharacter(211); // 随便抓一个动物
            // 清理
            Character LootChar = EventHelper.GetCharacterById(charid);
            Dictionary<ItemKey, int> OrigItems = new Dictionary<ItemKey, int>(LootChar.GetInventory().Items);
            foreach (var Item in OrigItems)
            {
                LootChar.RemoveInventoryItem(DomainManager.TaiwuEvent.MainThreadDataContext, Item.Key, Item.Value, true);
            }
            // 创建
            AdaptableLog.Info("Cleaned char " + charid + " with " + LootChar.GetInventory().Items.Count + " items");
            foreach (var LootEntry in table)
            {
                AdaptableLog.Info($"LootEntry:{LootEntry.type}, {LootEntry.qualityBonus}");
                var res = QscLootUtil.SimpleGenerateItemList(ev, LootEntry.type, LootEntry.qualityBonus, LootEntry.count, affix);
                LootChar.AddInventoryItemList(DomainManager.TaiwuEvent.MainThreadDataContext, res);
            }
            AdaptableLog.Info("Created char " + charid + " with " + LootChar.GetInventory().Items.Count + " items");
            return charid;

        }

        public static List<short> GenerateRandomGongFa(TaiwuEvent ev, GongFaType[] GongFaType, int grade, int count)
        {
            // Generates random unlearned GongFa with the given criterion.
            List<short> result = new List<short> { };
            List<short> EligibleGongFa = new List<short> { };
            foreach (var gongfa in GongFaType)
            {
                var tbl = QscGongFaData.GetGongFaTbl()[(int)gongfa][grade];
                AdaptableLog.Info($"For grade {grade} of {gongfa}, tbl is " + String.Join(",", tbl));
                
                
                EligibleGongFa = EligibleGongFa.Concat(QscGongFaData.GetGongFaTbl()[(int)gongfa][grade]).ToList();
            }

            EligibleGongFa = EligibleGongFa.OrderBy(x => QscCoreUtils.RandInt32(ev, 0, 100000, "")).ToList();
            
            for (int i = 0; i < count && i < EligibleGongFa.Count; i++)
            {
                result.Add((short)EligibleGongFa[i]);
            }
            
            if (grade != 0 && (count > result.Count))
            {
                AdaptableLog.Info("QscGenerateRandomGongFa: Try to gen " + count + " GongFa, getting " + result.Count + "elements. Trying to generate at level" + (grade - 1));
                List<short> remaining = GenerateRandomGongFa(ev, GongFaType, grade - 1, count - result.Count);
                result = result.Concat(remaining).ToList();
            }
            
            return result;
        }


    }
}


/*
 * 
 * each event should have its own lootcount and such..
 */