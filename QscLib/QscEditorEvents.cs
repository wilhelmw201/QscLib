using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public readonly int[] chances;

        public PlayerSelectEvent(string description, string[] optionDescrs, BaseEditorEvent[] dests)
        {
            if (optionDescrs.Length != dests.Length)
            {
                throw new InvalidOperationException("Incorrect event list!");
            }
            Description = description;
            OptionDescrs = optionDescrs;
            Dests = dests;
            chances = Enumerable.Repeat(100, dests.Length).ToArray();
        }
        public PlayerSelectEvent(string description, string[] optionDescrs, BaseEditorEvent[] dests, int[] chances)
        {
            if (optionDescrs.Length != dests.Length || optionDescrs.Length != chances.Length)
            {
                throw new InvalidOperationException("Incorrect event list!");
            }
            Description = description;
            OptionDescrs = optionDescrs;
            Dests = dests;
            this.chances = chances;
        }
    }
    public class GetGoldEvent: BaseEditorEvent
    {
        public override string Entry() { return "20db3563-41bc-42de-b5db-d48e949004f5"; }

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
        public override string Entry() { return "73479eab-8c22-4ad5-81ef-3d3072773532"; }

        public readonly int healCount;
        public HealingEvent (int heal)
        {
            healCount = heal;
        }
    }
    public class ForgeEvent: BaseEditorEvent
    {
        public override string Entry() { return "1349f475-ab70-4d8c-a228-42717a5b02ff"; }
        public LootTable table;
        public ForgeEvent(LootTable table=null)
        {
            this.table = table;
            if (table == null)
            {
                this.table = ForgeEvent.DefaultTbl;
            }
        }
        private static readonly LootTable DefaultTbl = new LootTable(
            new LootTableEntry[] {
            new LootTableEntry(QscData.LootClass.Weapon, 1, 80),
            new LootTableEntry(QscData.LootClass.Weapon, 1, 40),
            new LootTableEntry(QscData.LootClass.Weapon, 1, 0),
            new LootTableEntry(QscData.LootClass.Armor, 1, 80),
            new LootTableEntry(QscData.LootClass.Armor, 1, 40),
            new LootTableEntry(QscData.LootClass.Armor, 1, 0),
            new LootTableEntry(QscData.LootClass.Jewelry, 2, 30),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, 0),
            new LootTableEntry(QscData.LootClass.Material, 1, 120),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, 100),

            }, 1);
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
        public LootTable Table;
        public int count;
        public int gold;
        public EditorSimpleLootGenEvent(LootTable t, int countOverride=-1, int goldPerUnpicked=0)
        {
            Table = t;
            this.count = countOverride;
            this.gold = goldPerUnpicked;
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
        /*public EditorSimpleSkillGenEvent(int count, GongFaType[] type, int[] qualitybonus, int gold)
        {
            this.count = count;
            allowedTypes = Enumerable.Repeat(type, count).ToArray();
            qualityBonus = qualitybonus;
            this.gold = gold;
        }*/
        public EditorSimpleSkillGenEvent(int count, int genCount, GongFaType[] type, int qualitybonus, int gold)
            : this(count, Enumerable.Repeat(type, genCount).ToArray(), Enumerable.Repeat(qualitybonus, genCount).ToArray(),gold)
        {
            /*
             * copies type and qualitybonus into array and passes it to the other constructor.
             * 
             */
        }
        public EditorSimpleSkillGenEvent(int count, GongFaType[][]type, int[] qualityBonus, int gold)
        {
            /*
             * 
             * 
             */
            if (type.Length != qualityBonus.Length)
            {
                throw new ArgumentException("Incorrect EditorSimpleSkillGenEvent setup(2)");
            }
            this.count = count;
            allowedTypes = type;
            this.qualityBonus = qualityBonus;
            this.gold = gold;
            AdaptableLog.Info($"create ESSG event with {this.count} out of {this.allowedTypes.Length} ");
        }
    }

    /*
    public class EditorAttributeEvent : BaseEditorEvent
    {

    }*/
    // each event needs a corresponding IEditorEvent
}
