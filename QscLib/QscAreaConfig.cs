using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Qsc
{
    public class AreaDescr
    {
        public List<Tuple<string, int>> EventWeightTable; // WeightTable<string>
        public int EscapePossibility; // 0-100
        public int EnemyPossibility; // 0-100
    }

    public class QscAreaConfig
    {
        public static List<Tuple<string, int>> FarmlandTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> GardensTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> StoneForestTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> MulberryFieldTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> HerbalGardenTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> JadeMountainTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> MountainTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> BigMountainTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> CanyonTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> BigCanyonTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> HillTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> BigHillTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> FieldTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> BigFieldTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> WoodlandTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> BigWoodlandTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> RiverBeachTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> BigRiverBeachTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> LakeTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> JungleTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> CaveTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> SwampTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> TaoYuanTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> ValleyTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> WildTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> RuinTable = new List<Tuple<string, int>> { };
        public static List<Tuple<string, int>> DarkPoolTable = new List<Tuple<string, int>> { };

        public static Dictionary<EMapBlockSubType, List<Tuple<string, int>>> BlockTransitionTable = new Dictionary<EMapBlockSubType, List<Tuple<string, int>>>
        {
            { EMapBlockSubType.Farmland, FarmlandTable },//农田
            { EMapBlockSubType.Gardens, GardensTable },//园林
            { EMapBlockSubType.StoneForest, StoneForestTable },//石林
            { EMapBlockSubType.MulberryField, MulberryFieldTable },//桑园
            { EMapBlockSubType.HerbalGarden, HerbalGardenTable },//药园
            { EMapBlockSubType.JadeMountain, JadeMountainTable },//玉山
            { EMapBlockSubType.Mountain, MountainTable },//山岳
            { EMapBlockSubType.BigMountain, BigMountainTable },//山脉
            { EMapBlockSubType.Canyon, CanyonTable },//峡谷
            { EMapBlockSubType.BigCanyon, BigCanyonTable },//天险
            { EMapBlockSubType.Hill, HillTable },//丘陵
            { EMapBlockSubType.BigHill, BigHillTable },//高地
            { EMapBlockSubType.Field, FieldTable },//原野
            { EMapBlockSubType.BigField, BigFieldTable },//平原
            { EMapBlockSubType.Woodland, WoodlandTable },//林地
            { EMapBlockSubType.BigWoodland, BigWoodlandTable },//森林
            { EMapBlockSubType.RiverBeach, RiverBeachTable },//河滩
            { EMapBlockSubType.BigRiverBeach, BigRiverBeachTable },//河谷
            { EMapBlockSubType.Lake, LakeTable },//湖泊
            { EMapBlockSubType.Jungle, JungleTable },//密林
            { EMapBlockSubType.Cave, CaveTable },//洞穴
            { EMapBlockSubType.Swamp, SwampTable },//沼泽
            { EMapBlockSubType.TaoYuan, TaoYuanTable },//桃源
            { EMapBlockSubType.Valley, ValleyTable },//溪谷
            { EMapBlockSubType.Wild, WildTable },//荒野
            { EMapBlockSubType.Ruin, RuinTable },//毁坏地块
            { EMapBlockSubType.DarkPool, DarkPoolTable },//暗渊
        };

    }

   
}
