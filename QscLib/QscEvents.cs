using Qsc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Qsc.QscEvents;

namespace Qsc
{

    public class QscEvents
    {
        public static RandomWeightedTable<BaseEventCollection> GetEventTblFromBlock(EMapBlockSubType type)
        {
            RandomWeightedTable<BaseEventCollection> result = null;
            switch (type)
            {
 
                case EMapBlockSubType.HerbalGarden:
                    result = YaoYuanTbl;
                    break;
                case EMapBlockSubType.JadeMountain:
                case EMapBlockSubType.Mountain:
                case EMapBlockSubType.BigMountain:
                case EMapBlockSubType.Woodland:
                case EMapBlockSubType.BigWoodland:
                case EMapBlockSubType.Jungle:
                    result = ForgeTbl;
                    break;

                case EMapBlockSubType.TaoYuan:
                    result = SimpleTbl;
                    break;

                case EMapBlockSubType.DarkPool:
                    result = AnyuanTbl;
                    break;
                default:
                    result = SimpleTbl;
                    break;
            }
            return result;
        }



    /*
     * 注意：！！！
     * 由于我比较懒所以使用了这种构造方式，请务必确保按顺序初始化，后初始化先发生的事件，反过来初始化会导致有的项是null
     * 
     * 
     * 
     */


        public abstract class BaseEventCollection
        {
            public abstract BaseEditorEvent EntryEvent();
        }
        public class TestEventCollection : BaseEventCollection
        {
            public override BaseEditorEvent EntryEvent() { return Entry; }

            // 这是两个终节点，最先初始化
            public static EditorSimpleSkillGenEvent bt1_sl1 =
            new EditorSimpleSkillGenEvent(1, Enumerable.Repeat(QscGongFaData.AllGongFaTypes, 6).ToArray() , new int[] { 0, 70, 140, 210, 280, 350 }, 50);
            public static EditorSimpleLootGenEvent bt1_lsl1 = new EditorSimpleLootGenEvent(null, 8, 234);
            // 之后初始化随机跳转节点
            public static RandomJumpEvent rje1 = new RandomJumpEvent(
                new BaseEditorEvent[] { bt1_sl1, null, bt1_lsl1,  }, new int[] { 40, 20, 40 }
                );
            // 最后初始化入口节点。否则rje1 = null.
            public static EditorBattleEvent Entry = new EditorBattleEvent(rje1, null, 50);
        }

        public class YaoyuanSpecialEventCollection : BaseEventCollection
        {
            public override BaseEditorEvent EntryEvent() { return Entry; }

            public static HealingEvent healev = new HealingEvent(2);
            public static EditorSimpleLootGenEvent yygen = new EditorSimpleLootGenEvent(null, -1, 100);
            public static PlayerSelectEvent Sel =
                new PlayerSelectEvent(
                    "你来到了一片药园之中...\n你可以花时间疗伤，或者四处搜寻物品......",
                    new string[] {"奋力疗伤...", "四处搜刮..."},
                    new BaseEditorEvent[] {healev, yygen}
                    );
            public static GetGoldEvent GoldEv = new GetGoldEvent(140, 15, 0, 18);

            public static EditorBattleEvent Entry = new EditorBattleEvent(
                new ListOfEventsEvent(new BaseEditorEvent[] { GoldEv, Sel }),
                Sel, 100);
        }
        public class ForgeEventCollection : BaseEventCollection
        {
            public override BaseEditorEvent EntryEvent() { return Entry; }

            public static ForgeEvent forgeEv = new ForgeEvent();
            public static EditorSimpleLootGenEvent yygen = new EditorSimpleLootGenEvent(null, -1, 100);
            public static PlayerSelectEvent Sel =
                new PlayerSelectEvent(
                    "这里有一个废弃的铁匠铺，你可以试图使用它打造点什么，或者搜寻物品......",
                    new string[] { "检查铁匠铺...", "四处搜刮..." },
                    new BaseEditorEvent[] { forgeEv, yygen }
                    );
            public static GetGoldEvent GoldEv = new GetGoldEvent(120, 14, 0, 25);

            public static EditorBattleEvent Entry = new EditorBattleEvent(
                new ListOfEventsEvent(new BaseEditorEvent[] { GoldEv, Sel }),
                Sel, 100);
        }
        public class AnYuanEventCollection : BaseEventCollection
        {
            public override BaseEditorEvent EntryEvent() { return Entry; }


            public static EditorSimpleSkillGenEvent loot2 = 
                new EditorSimpleSkillGenEvent(2,
                    new GongFaType[][] { 
                        new GongFaType[] {GongFaType.NeiGong},
                        new GongFaType[] {GongFaType.NeiGong},
                        new GongFaType[] {GongFaType.ShenFa},
                        new GongFaType[] {GongFaType.JueJi},
                        new GongFaType[] {GongFaType.JueJi},
                           }
                    , new int[] { 120, 0, -550, 40, -550 }, 200);

            public static EditorSimpleLootGenEvent loot1 = new EditorSimpleLootGenEvent(null, -1, 200);

            public static EditorBattleEvent battle2 = new EditorBattleEvent(
                new ListOfEventsEvent (new BaseEditorEvent[] { loot2, new GetGoldEvent(200, 25, 0, 40)}), 
                loot1, 300
                );

            public static EditorBattleEvent Entry = new EditorBattleEvent(
                battle2,
                loot1,100);
        }

        public class SimpleEventCollection : BaseEventCollection
        {
            public override BaseEditorEvent EntryEvent() { return Entry; }

            public static EditorSimpleLootGenEvent LootEv = new EditorSimpleLootGenEvent(null, -1, 200);
            public static EditorSimpleSkillGenEvent SkillEv1 = new EditorSimpleSkillGenEvent(1, 2, QscGongFaData.CuiPoTypes, 30, 200);
            public static EditorSimpleSkillGenEvent SkillEv2 = new EditorSimpleSkillGenEvent(1, 3, QscGongFaData.CuiPoTypes, 0, 200);
            public static EditorSimpleSkillGenEvent SkillEv3 = new EditorSimpleSkillGenEvent(1, 2, QscGongFaData.AllGongFaTypes, 0, 200);
            public static EditorSimpleSkillGenEvent SkillEv4 = new EditorSimpleSkillGenEvent(1, 2,
                new GongFaType[] { GongFaType.NeiGong, GongFaType.ShenFa}, 0, 250);


            public static RandomJumpEvent JumpEv = new RandomJumpEvent(
                new BaseEditorEvent[] { LootEv, SkillEv1, SkillEv2, SkillEv3, SkillEv4 }, new int[] { 160, 40, 40, 40, 40 }
                );
            public static GetGoldEvent GoldEv = new GetGoldEvent(100, 20, 0, 15);
            public static RandomJumpEvent JumpEv_1 = new RandomJumpEvent(
                new BaseEditorEvent[] { LootEv, SkillEv2 }, new int[] { 40, 40 }
                );

            public static EditorBattleEvent Entry = new EditorBattleEvent(
                new ListOfEventsEvent( new BaseEditorEvent[] { GoldEv, JumpEv }),
                JumpEv_1, 0);

        }

        static RandomWeightedTable<BaseEventCollection> SimpleTbl =
            new RandomWeightedTable<BaseEventCollection>(
                new List<int> { 100 },
                new List<BaseEventCollection> { new SimpleEventCollection() }
                );
        static RandomWeightedTable<BaseEventCollection> YaoYuanTbl =
            new RandomWeightedTable<BaseEventCollection>(
                new List<int> { 100 },
                new List<BaseEventCollection> { new YaoyuanSpecialEventCollection() }
                );
        static RandomWeightedTable<BaseEventCollection> ForgeTbl =
            new RandomWeightedTable<BaseEventCollection>(
                new List<int> { 100, 10 },
                new List<BaseEventCollection> { new SimpleEventCollection(), new ForgeEventCollection() }
                );
        static RandomWeightedTable<BaseEventCollection> AnyuanTbl =
            new RandomWeightedTable<BaseEventCollection>(
                new List<int> { 100 },
                new List<BaseEventCollection> { new AnYuanEventCollection() }
                );


    }    
}
