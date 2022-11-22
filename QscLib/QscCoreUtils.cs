using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GameData;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Utilities;

namespace Qsc
{
    public enum XiangShuType
    {
        MoNv = 0,
        DaYueYaoChang = 1,
        JiuHan = 2,
        JinHuangEr = 3,
        YiYiHou = 4,
        WeiQi = 5,
        YiXiang = 6,
        XueFeng = 7,
        ShuFang = 8,
        LongYuFu = 100,
        ZiWuXiao = 101,
        HuanXin = 200,
        RanChenZi = 300,
}
    public class GradeTable
    {
        RandomWeightedTable<int> impl;
        public GradeTable(int[] chances)
        {
            impl = new RandomWeightedTable<int>(new List<int>(chances), Enumerable.Range(0, 9).ToList());
        }
        public GradeTable(Vector<int> chances)
        {
            impl = new RandomWeightedTable<int>();
            for (int i = 0; i < 9; i++)
            {
                impl.AddItemWithWeight(i, chances[i]);
            }
        }
        public GradeTable(GradeTable tb1, int weight1, GradeTable tb2, int weight2)
        {
            impl = new RandomWeightedTable<int>(tb1.impl, weight1, tb2.impl, weight2);
        }
        public int Draw(TaiwuEvent ev, string affix = "")
        {
            return impl.Draw(ev, affix);
        }
    }
    public class RandomWeightedTable<T> // where T : struct
    {
        List<int> weightsSoFar;
        public List<T> items;
        public RandomWeightedTable()
        {
            weightsSoFar = new List<int>();
            items = new List<T>();
        }
        public RandomWeightedTable(List<int> weights, List<T> items)
        {

            this.items = items;
            weightsSoFar = new List<int>();
            weightsSoFar.Add(weights[0]);
            for (int i = 1; i < weights.Count; i++)
            {
                weightsSoFar.Add(weightsSoFar.Last() + weights[i]);
            }
            //AdaptableLog.Info("Init RWT done, wsf="+
            //    String.Join(',', weightsSoFar)
            //    );
            Normalize();
        }
        public RandomWeightedTable(RandomWeightedTable<T> t1, int w1, RandomWeightedTable<T> t2, int w2):
            this(new RandomWeightedTable<T>[2] {t1, t2}, new int[2] {w1, w2})
        {
            
        }
        public RandomWeightedTable(RandomWeightedTable<T>[] t, int[] w)
        {

            
            this.weightsSoFar = new List<int>();
            this.items = new List<T>();
            int maxWeight = 0;
            for (int i = 0; i < t.Length; i++)
            {
                //AdaptableLog.Info("mergin rwt with " +
                //String.Join(',', t[i].weightsSoFar) + "\nand " +
                //String.Join(',', t[i].items)
                //);
                items = items.Concat(t[i].items).ToList();
                weightsSoFar = weightsSoFar.Concat(
                    t[i].weightsSoFar.ConvertAll(
                        (int orig) =>
                        {
                            return orig * w[i] +maxWeight;
                        }
                    )).ToList();
                maxWeight += w[i] * t[i].weightsSoFar.Last();
            }
            this.Normalize();
        }
        public void AddItemWithWeight(T item, int weight)
        {
            items.Add(item);
            if (weightsSoFar.Count == 0)
            {
                weightsSoFar.Add(weight);
            }
            else
            {
                weightsSoFar.Add(weightsSoFar.Last() + weight); // TODO binary search possible to shave off 1microsecond
            }
        }
        public T Draw(TaiwuEvent ev, string affix = "")
        {
            int drawn = QscCoreUtils.RandInt32(ev, 0, weightsSoFar.Last(), affix);
            // TODO change to binary search
            for (int i = 0; i < items.Count; i++)
            {
                if (weightsSoFar[i] >= drawn)
                {
                    return items[i];
                }
            }
            return items.Last();
        }
        public void Normalize()
        {
            if (weightsSoFar.Count == 0)
            {
                AdaptableLog.Info("Normalizing an empty array?");
                return;
            }
            if (weightsSoFar.Last() == 1000) return;
            for (int i = 0; i < weightsSoFar.Count; i++)
            {
                weightsSoFar[i] = weightsSoFar[i] * 1000 / weightsSoFar.Last();
            }

        }
        public override string ToString()
        {
            return "[RWTbl]weightSoFar=" + String.Join(',', weightsSoFar) + "|items=" + String.Join(",", weightsSoFar); 
        }

    }
    /*public class RandomWeightedTable<T> where T : class
    {
        Vector<int> weightsSoFar;
        Vector<T> items;
        public RandomWeightedTable()
        {
            weightsSoFar = new Vector<T>();
            items = new Vector<int>();
        }

        public void addItemWithWeight(T item, int weight)
        {
            weightsSoFar.append(weightsSoFar[-1] + weight); // binary search possible

        }
    }*/
    public class QscCoreUtils
    {
        public static int lastCheckProgress = 100;
        public static int GetQscProgress(TaiwuEvent ev)
        {
            /* 规定 1 为刚结束和衣服的对话，也是能获取到的最小值
             * 规定 100 对应九品世界主进度（0精纯）(没打第一个boss）
             * 110对应八品世界主进度（2精纯），一直到180对应一品世界主进度（18精纯）
             * 之后190是紫/龙二选一 200是焕心。
             * 没打染尘子是210
             * 打完染尘子就不是210了，跳到1000（全部结束） 
             * */
            int result = -1;
            var res = ev.GetModData("qsc_progress", true, ref result);
            if (res)
            {
                lastCheckProgress = result;
                return result;
            }
            else
            {
                lastCheckProgress = 1;
                QscCoreUtils.SetQscProgress(ev, 1);
            }
            return 1;
        }
        public static XiangShuType GetNextBoss(TaiwuEvent ev, int which=-1)
        {
            if (which < 0)
            {
                which = GetWorldState(ev);
            }
            if (which == 10)
            {
                return XiangShuType.HuanXin;
            }
            if (which == 11)
            {
                return XiangShuType.RanChenZi;
            }
            int result = -1;
            bool res = ev.GetModData("qsc_boss0", true, ref result);
            {
                if (!res)
                {
                    Random r = new Random();
                    int[] order = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
                    while (true)
                    {
                        order = order.OrderBy((x) => { return r.Next(); }).ToArray();
                        if (order[0] != 3 && order[1] != 3 && order[0] != 4 && order[1] != 4
                            && order[2] != 8) break;
                    }
                    for(int i = 0; i < 9; i++)
                    {
                        ev.SetModInt("qsc_boss" + i, true, order[i]);
                    }
                    ev.SetModInt("qsc_boss9", true, r.Next(2) + 100); // 定义龙=100，紫=101
                }
            }
            res = ev.GetModData("qsc_boss" + which, true, ref result);
            if (! res || which > 11 || which < 0)
            {
                throw new InvalidOperationException("Get next boss failed: which="+which);
            }
            AdaptableLog.Info($"Getting world boss for state {which}=>{result}");

            return (XiangShuType)result;
        }
        public static int GetWorldState(TaiwuEvent ev)
        {
            // 返回0-8：前9个boss，9=龙/紫，10=焕心，11=染
            var result = QscCoreUtils.GetQscProgress(ev) / 10 - 10;
            //AdaptableLog.Info($"Worldstate is{QscCoreUtils.GetQscProgress(ev)}=>{result}");
            return result;

        }
        public static void SetQscProgress(TaiwuEvent ev, int prog)
        {
            lastCheckProgress = prog;
            ev.SetModInt("qsc_progress", true, prog);
        }
        public static int GetQscSubProgress(TaiwuEvent ev)
        {
            int result = -1;
            var res = ev.GetModData("qsc_subprogress", true, ref result);
            if (res)
            {
                return result;
            }
            else SetQscSubProgress(ev, 0);
            return 0;
        }
        public static void SetQscSubProgress(TaiwuEvent ev, int prog)
        {
            ev.SetModInt("qsc_subprogress", true, prog);
        }
        public static void IncreaseQscSubProgress(TaiwuEvent ev)
        {
            int orig = GetQscSubProgress(ev);
            int ws = GetWorldState(ev);
            SetQscSubProgress(ev, orig + QscDependencyPlugin.ProgressPerEvent);

        }
        public static bool QscStageFinished(TaiwuEvent ev)
        {
            return GetQscSubProgress(ev) > 100;
        }
        static Random rnd = new Random();
        public static int RandInt32(TaiwuEvent ev, int min, int max, string affix = "")
        {
            return rnd.Next(min, max);
            // generate a Uint between [min, max)
            // DOES NOT INCLUDE MAX
            /*System.Int32 seed = 0;
            if (!ev.GetModData("qsc_rng" + affix, true, ref seed))
            {
                seed = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();
            }
            System.UInt32 next = 1103515245 * seed + 12345;
            ev.SetModInt("qsc_rng" + affix, true, next);
            return next % (max - min) + min;*/
        }
        public static void InitQsc(TaiwuEvent ev)
        {
            // give every lvl 0 weapon and every armor

            // remove all resource

            // remove monkey Adventure

            // edit state of initial skills: xiaozongyuegong, taizuchangquan, peiranjue
        }

        public static void RestoreMap(TaiwuEvent ev)
        {
            // restore all destroyed land blocks?


            // TODO randomize land block layout in the map
        }


        public static List<BaseEditorEvent> EventList = new List<BaseEditorEvent> { }; // 跳转都在这里
        public static string CallEvent(BaseEditorEvent Event, string MyReturn="d59219af-4b67-4313-8494-aa13206fdb53", bool doJump=true)
        {

            /* 注意，myreturn会从头执行！（废话）
             * 跳转到事件的模板：
               QscCoreUtils.EventList.Add(new TransitionDummyEvent("my_cleanup_event"));
               QscCoreUtils.EventList.Add(DestEvent);
               EventHelper.ToEvent(DestEvent.Entry());
             */
            //AdaptableLog.Info("[QscCore.CallEvent] entry " + Event.Entry());
            QscCoreUtils.EventList.Add(new TransitionDummyEvent(MyReturn));
            QscCoreUtils.EventList.Add(Event);
            //AdaptableLog.Info("[QscCore.CallEvent] Stack after push:");
            //QscCoreUtils.PrintStack();
            if (doJump) EventHelper.ToEvent(Event.Entry());


            return Event.Entry();
        }

        public static string PopAndReturn(bool doJump=true)
        {
            // 事件执行结束的时候执行。如果stack空了就结束，否则跳到上一个事件的入口
            // 这个函数会把栈顶pop掉，注意不要pop两次....
            //AdaptableLog.Info("[QscCore.Return] Stack before pop:");

            //QscCoreUtils.PrintStack();
            string NextEvent = "";
            if (QscCoreUtils.EventList.Count > 1)
            {
                NextEvent = QscCoreUtils.EventList[QscCoreUtils.EventList.Count - 2].Entry();
            }
            AdaptableLog.Info("[QscCore.Return] Returning to" + NextEvent);
            QscCoreUtils.EventList.RemoveAt(QscCoreUtils.EventList.Count - 1);
            if (doJump) EventHelper.ToEvent(NextEvent);
            return NextEvent; 
        }
        public static void PrintStack()
        {
            for (int i = QscCoreUtils.EventList.Count; i > 0; i--)
            {
                AdaptableLog.Info(i.ToString() + ": " + QscCoreUtils.EventList[i - 1]);
            }
        }
        /*public static void PushReturnAddress(TaiwuEvent ev, string returnEvent)
        {
            int stackSize = 0; // points to an empty position
            if (!ev.GetModData("qsc_stack", true, ref stackSize))
            {
                stackSize = 0;
            }
            if (stackSize > 20)
            {
                throw new InvalidOperationException("PushReturnAddress: Stack size >20! Check your call stack.");
            }
            ev.SetModString("qsc_stack_" + stackSize, false, returnEvent);
            ev.SetModInt("qsc_stack", false, stackSize + 1); // assume stack used only in event jumping...
           
        }
        public static string PopAndJump(TaiwuEvent ev)
        {
            int stackSize = 0; // points to an empty position
            if (!ev.GetModData("qsc_stack", true, ref stackSize) || stackSize <= 0)
            {
                throw new InvalidOperationException("PopAndJump: Empty stack! Check your call stack.");
            }
            stackSize -= 1;
            string returnEvent = null;
            ev.GetModData("qsc_stack_" + stackSize, false, ref returnEvent);
            ev.SetModInt("qsc_stack", false, stackSize);
            return returnEvent;
        }*/
    }
}
