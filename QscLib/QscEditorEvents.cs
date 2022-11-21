using GameData.Domains.TaiwuEvent.EventHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qsc
{


    abstract public class BaseEditorEvent
    {
        public abstract string Entry(); // 入口地址，必填
    }
    public class TransitionDummyEvent: BaseEditorEvent
    {
        // 枢纽性质的事件，如果你在一个事件中想跳到另一个事件，那么先把这个push到栈上，
        // 这样那个事件返回时可以跳到你想要的地方（比如cleanup）接着执行。
        public override string Entry () { return "4d1027ea-e416-464e-8675-5206582c0e31"; }
        public readonly string Exit;
        public TransitionDummyEvent(string exit)
        {
            Exit = exit;
        }
        public override string ToString()
        {
            return "Qsc.TransitDummy => " + Entry();
        }
    }
    public class RandomJumpEvent: BaseEditorEvent
    {
        public override string Entry() { return "ed2d6cdb-5873-46cb-92ce-2fda1f8e6b2a"; }
        public BaseEditorEvent[] Events;
        public RandomWeightedTable<int> Weights;
        public RandomJumpEvent(BaseEditorEvent[] Events, int[] weights)
        {
            this.Events = Events;
            Weights = new RandomWeightedTable<int>(new List<int>(weights), new List<int>(Enumerable.Range(0, weights.Length)));
        }
    }
    public class ListOfEventsEvent: BaseEditorEvent
    {
        public override string Entry() { return "f13a3710-1e32-41db-ab58-3d9489cf0f67"; }
        public BaseEditorEvent[] Events;
        public int current;
        public ListOfEventsEvent(BaseEditorEvent[] e)
        {
            current = 0;
            Events = e;
        }
    }
    public class PlayerSelectEvent: BaseEditorEvent
    {
        public override string Entry() { return "41f1ee15-0d19-4fd4-a8c2-dff6f90ce379"; }
        public readonly string Description;
        public readonly string[] OptionDescrs;
        public BaseEditorEvent[] Dests;

        public PlayerSelectEvent (string description, string[] optionDescrs, BaseEditorEvent[] dests)
        {
            Description = description;
            OptionDescrs = optionDescrs;
            Dests = dests;
        }
    }
    public class GetGoldEvent: BaseEditorEvent
    {
        public override string Entry() { return; }

        public readonly int baseGold;
        public readonly int GoldPerWorld;
        public readonly int GoldPerSubWorld;
        public readonly int maxBonusPercent;

        public GetGoldEvent (int baseGold, int goldPerWorld, int goldPerSubWorld, int maxBonusPercent)
        {
            this.baseGold = baseGold;
            GoldPerWorld = goldPerWorld;
            GoldPerSubWorld = goldPerSubWorld;
            this.maxBonusPercent = maxBonusPercent;
        }
    }
    public class HealingEvent: BaseEditorEvent
    {
        public override string Entry() { return; }

        public readonly int healCount;
        public HealingEvent (int heal)
        {
            healCount = heal;
        }
    }
    public class EditorBattleEvent: BaseEditorEvent
    {
        /*
         * 罐头事件之一。
         * 遇到一个敌人，有不同的走向。
         */
        public override string Entry() { return "fb86b88d-44b8-4023-a973-c8a46454dc07"; }

        public BaseEditorEvent DestOnWin;
        public BaseEditorEvent DestOnEscape;
        public readonly int escapeChance;
        public readonly int EnemyId;

        public EditorBattleEvent(BaseEditorEvent DestOnWin, BaseEditorEvent DestOnEscape, int escapeChance, int EnemyId=-1)
        {
            this.escapeChance = escapeChance;
            this.EnemyId = EnemyId;
            this.DestOnEscape = DestOnEscape;
            this.DestOnWin = DestOnWin;
        }
    }
    public class EditorSimpleLootEvent : BaseEditorEvent
    {
        public override string Entry() { return "9274a890-d48f-41ab-8569-8695d9283c24"; }
        // 注意：这个事件只负责把选择面板打开，你需要自己生成战利品并且放在一个临时的角色身上并且传进去。
        public int containerId;
        public int count; // 允许拿几个
        public int current = 0; // 已经拿了几个
        public int goldPerCount = 0; // 换钱
        public EditorSimpleLootEvent(int containerId, int count, int goldPerCount=0)
        {
            this.containerId = containerId;
            this.count = count;
            this.goldPerCount = goldPerCount;
        }  
    }
    public class EditorSimpleLootGenEvent: BaseEditorEvent
    {
        public override string Entry() { return "05d730a1-734c-4eff-b377-8dfd75187f40"; }
        // 包装了simplelootevent，用LootTableEntry控制生成的东西
        public LootTableEntry[] Table;
        public int count;
        public int gold;
        public EditorSimpleLootGenEvent(LootTableEntry[] t, int pickcount, int goldPerUnpicked=0)
        {
            this.count = pickcount;
            this.gold = goldPerUnpicked;
            Table = t;
        }
    }

    
    public class EditorSimpleSkillPickEvent : BaseEditorEvent
    {
        public override string Entry() { return "083d8631-be60-479f-b65f-312c3da9ca63"; }
        public List<int> Ids;
        public List<bool> isGongFa; // True: 功法 False:生活技能
        public int count; // how many are allowed?
        public int current;
        public int gold; // gold for no pick
        public EditorSimpleSkillPickEvent(List<int> ids, List<bool> isGongFa, int count, int gold)
        {
            current = 0;
            Ids = ids;
            this.isGongFa = isGongFa;
            this.count = count;
            this.gold = gold;
        }
    }

    public class EditorSimpleSkillGenEvent : BaseEditorEvent
    {
        public override string Entry() { return "84c4968e-db08-418d-bc19-0141ddd41d62"; }
        public int count;
        public GongFaType[][] allowedTypes; // dim 1: index dim2: array of allowed
        public int[] qualityBonus;
        public int gold;
        public EditorSimpleSkillGenEvent(int count, GongFaType[] type, int[] qualitybonus, int gold)
        {
            this.count = count;
            allowedTypes = Enumerable.Repeat(type, count).ToArray();
            qualityBonus = qualitybonus;
            this.gold = gold;
        }
        public EditorSimpleSkillGenEvent(int count, GongFaType[] type, int qualitybonus, int gold)
        {
            this.count = count;
            allowedTypes = Enumerable.Repeat(type, count).ToArray();
            qualityBonus = Enumerable.Repeat(qualitybonus, count).ToArray();
            this.gold = gold;
        }
        public EditorSimpleSkillGenEvent(int count, GongFaType[][]type, int[] qualityBonus, int gold)
        {
            this.count = count;
            allowedTypes = type;
            this.qualityBonus = qualityBonus;
            this.gold = gold;
        }
    }

    /*
    public class EditorAttributeEvent : BaseEditorEvent
    {

    }*/
    // each event needs a corresponding IEditorEvent
}
