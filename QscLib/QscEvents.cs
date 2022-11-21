using Qsc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qsc
{

    /*
     * 注意：！！！
     * 由于我比较懒所以使用了这种构造方式，请务必确保按顺序初始化，后初始化先发生的事件，反过来初始化会导致有的项是null
     * 
     * 
     * 
     */

    public class QscEvents
    {

        public class TestEventCollection
        {
            // 这是两个终节点，最先初始化
            public static EditorSimpleSkillGenEvent bt1_sl1 =
            new EditorSimpleSkillGenEvent(5, QscGongFaData.AllGongFaTypes, new int[] { 0, 70, 140, 210, 280, 350 }, 50);
            public static EditorSimpleLootGenEvent bt1_lsl1 = new EditorSimpleLootGenEvent(null, 5, 234);
            // 之后初始化随机跳转节点
            public static RandomJumpEvent rje1 = new RandomJumpEvent(
                new BaseEditorEvent[] { bt1_sl1, null, bt1_lsl1,  }, new int[] { 40, 20, 40 }
                );
            // 最后初始化入口节点。否则rje1 = null.
            public static EditorBattleEvent Entry = new EditorBattleEvent(rje1, null, 50);
        }

        public class YaoyuanSpecialEventCollection
        {
            public static HealingEvent healev = new HealingEvent(2);
            public static EditorSimpleLootGenEvent yygen = new EditorSimpleLootGenEvent(null, 2, 100);
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

        public class SimpleEventCollection
        {
            public static EditorSimpleLootGenEvent LootEv = new EditorSimpleLootGenEvent(null, 1, 200);
            public static EditorSimpleSkillGenEvent SkillEv1 = new EditorSimpleSkillGenEvent(2, QscGongFaData.CuiPoTypes, new int[] {30, 10}, 200);
            public static EditorSimpleSkillGenEvent SkillEv2 = new EditorSimpleSkillGenEvent(3, QscGongFaData.CuiPoTypes, 0, 200);
            public static EditorSimpleSkillGenEvent SkillEv3 = new EditorSimpleSkillGenEvent(3, QscGongFaData.AllGongFaTypes, 0, 200);
            public static EditorSimpleSkillGenEvent SkillEv4 = new EditorSimpleSkillGenEvent(3, 
                new GongFaType[] { GongFaType.NeiGong, GongFaType.ShenFa}, 0, 200);


            public static RandomJumpEvent JumpEv = new RandomJumpEvent(
                new BaseEditorEvent[] { LootEv, SkillEv1, SkillEv2, SkillEv3, SkillEv4 }, new int[] { 160, 40, 40, 40, 40 }
                );
            public static GetGoldEvent GoldEv = new GetGoldEvent(100, 20, 0, 15);
            public static RandomJumpEvent JumpEv_1 = new RandomJumpEvent(
                new BaseEditorEvent[] { LootEv, SkillEv2, null }, new int[] { 40, 20, 40 }
                );

            public static EditorBattleEvent Entry = new EditorBattleEvent(
                new ListOfEventsEvent( new BaseEditorEvent[] { GoldEv, JumpEv }),
                JumpEv_1, 0);

        }
    }    
}
