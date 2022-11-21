using GameData.Common;
using GameData.Domains.Item;
using GameData.Domains;
using Qsc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qsc
{
    public class QscData
    {

        public enum LootClass
        {
            Weapon = 1,
            Armor = 2,
            TeaWine = 3,
            Food = 4,
            Material = 5,
            JingzhiMaterial = 6, // 精制材料在竹屋事件处进行
            Jewelry = 7, // 宝物
            Poison = 8, // 涂毒在竹屋事件处进行
            Medicine = 9,
        }

        [Flags]
        public enum GongFaClass
        {
            Neigong = 2 ^ 1,
            Shenfa = 2 ^ 2,
            Huti = 2 ^ 3,
            Cuipo = 2 ^ 4,
        }
        // the configurations of game...
        public static int[] BannedGongFa = { }; // 移魂大法

        public static GradeTable[] GenWeights = // TODO change this to dict that has -1, -2
        {
           // new GradeTable(new int[] {80, 10, 05, 00, 00, 00, 00, 00, 00 }), 
            new GradeTable(new int[] {40, 25, 10, 05, 00, 00, 00, 00, 00 }), 
            new GradeTable(new int[] {25, 40, 15, 12, 06, 03, 01, 01, 00 }),
            new GradeTable(new int[] {20, 25, 40, 10, 05, 05, 01, 00, 00 }),

            new GradeTable(new int[] {15, 20, 25, 40, 10, 05, 01, 01, 00 }),
            new GradeTable(new int[] {10, 15, 20, 25, 40, 10, 05, 01, 01 }),
            new GradeTable(new int[] {10, 10, 15, 20, 25, 40, 10, 05, 01 }),

            new GradeTable(new int[] {10, 10, 10, 15, 20, 25, 40, 10, 5 }),
            new GradeTable(new int[] {10, 10, 10, 10, 15, 20, 25, 40, 10 }),
            new GradeTable(new int[] {10, 10, 10, 10, 10, 15, 20, 25, 40 }), 
           // new GradeTable(new int[] {01, 01, 01, 01, 01, 05, 10, 18, 30 }), 
           // new GradeTable(new int[] {01, 01, 01, 01, 01, 01, 01, 10, 50 }),
        };


        // nothing interesting here, just copying the configs
        public static int[][] ArmorList = Enumerable.Range(0, 9).Select(
            j => Enumerable.Range(0, 57).Select(i => 9 * i + j).ToArray()
            ).ToArray();
        public static int[][] WeaponList = Enumerable.Range(0, 9).Select(
            j => Enumerable.Range(0, 91).Select(i => 9 * i + j + 3).ToArray()
            ).ToArray();
        public static int[][] TeaWineList = Enumerable.Range(0, 9).Select(
            j => Enumerable.Range(0, 4).Select(i => 9 * i + j).ToArray()
            ).ToArray();
        public static int[][] FoodList =
        {
            new int[] {0,9,51,93,135},
            new int[] { 1, 10, 18, 52, 60, 94, 102, 136, 144 },
            new int[] {2,11,19,26,53,61,68,95,103,110,137,145,152 },
            new int[] {3,12,20,27,33,54,62,69,75,96,104,111,117,138,146,153,159 },
            new int[] { 4, 13, 21, 28, 34, 39, 55, 63, 70, 76, 81, 97, 105, 112, 118, 123, 139, 147, 154, 160, 165 },
            new int[] { 5, 14, 22, 29, 35, 40, 44, 56, 64, 71, 77, 82, 86, 98, 106, 113, 119, 124, 128, 140, 148, 155, 161, 166, 170 },
            new int[] { 6, 6, 15, 23, 30, 36, 41, 45, 48, 57, 65, 72, 78, 83, 87, 90, 99, 107, 114, 120, 125, 129, 132, 141, 149, 156, 162, 167, 171, 174 },
            new int[] { 7, 7, 16, 24, 31, 37, 42, 46, 49, 58, 66, 73, 79, 84, 88, 91, 100, 108, 115, 121, 126, 130, 133, 142, 150, 157, 163, 168, 172, 175 },
            new int[] { 8, 8, 17, 25, 32, 38, 43, 47, 50, 59, 67, 74, 80, 85, 89, 92, 101, 109, 116, 122, 127, 131, 134, 143, 151, 158, 164, 169, 173, 176 },
        };
        public static int[][] MaterialList = new int[] { 0, 1, 2,  3, 4, 5, 5,  6,6, }.Select(
            j => Enumerable.Range(0, 8).Select(i => 7 * i + j).ToArray()
            ).ToArray(); // no grade 0 8 material
        public static int[][] JingzhiMaterialList = new int[] { 0, 0, 1,  2, 3, 4,  4, 5, 6, }.Select(
            j => Enumerable.Range(0, 8).Select(i => 7 * i + j + 84).ToArray()
            ).ToArray(); // no grade 7 8 material
        public static int[][] JewelryList = Enumerable.Range(0, 9).Select(
            j => Enumerable.Range(0, 25).Select(i => 9 * i + j).ToArray()
            ).ToArray(); // TODO exclude 10 (bag)
        public static int[][] PoisonList = Enumerable.Range(0, 9).Select(
            j => Enumerable.Range(0, 6).Select(i => 9 * i + j).ToArray()
            ).ToArray();
        public static int[][] MedicineList =
        {
            new int[] {54,66,82,94,106,118,130,142,154,166,178,190,202,214,226,238,250,262,274,286,298,310,322,334},
            new int[] {55,67,83,95,107,119,131,143,155,167,179,191,203,215,227,239,251,263,275,287,299,311,323,335},
            new int[] {56,68,84,96,108,120,132,144,156,168,180,192,204,216,228,240,252,264,276,288,300,312,324,336},
            new int[] {57,60,69,72,85,88,97,100,109,112,121,124,133,136,145,148,157,160,169,172,181,184,193,196,205,208,217,220,229,232,241,244,253,256,265,268,277,280,289,292,301,304,313,316,325,328,337,340},
            new int[] {58,61,70,73,86,89,98,101,110,113,122,125,134,137,146,149,158,161,170,173,182,185,194,197,206,209,218,221,230,233,242,245,254,257,266,269,278,281,290,293,302,305,314,317,326,329,338,341},
            new int[] {59,62,71,74,87,90,99,102,111,114,123,126,135,138,147,150,159,162,171,174,183,186,195,198,207,210,219,222,231,234,243,246,255,258,267,270,279,282,291,294,303,306,315,318,327,330,339,342},
            new int[] {63,75,91,103,115,127,139,151,163,175,187,199,211,223,235,247,259,271,283,295,307,319,331,343},
            new int[] {64,76,92,104,116,128,140,152,164,176,188,200,212,224,236,248,260,272,284,296,308,320,332,344},
            new int[] {65,77,93,105,117,129,141,153,165,177,189,201,213,225,237,249,261,273,285,297,309,321,333,345},
        };
        public static List<int[][]> LootClassMap = new List<int[][]> { null, WeaponList, ArmorList, TeaWineList, FoodList, MaterialList, JingzhiMaterialList, JewelryList, PoisonList, MedicineList };
        public static List<Func<DataContext, short, ItemKey>> LootCreator = new List<Func<DataContext, short, ItemKey>>
        {
            null,
            _CreateWeapon,
            _CreateArmor,
            DomainManager.Item.CreateTeaWine,
            DomainManager.Item.CreateFood,
            DomainManager.Item.CreateMaterial,
            DomainManager.Item.CreateMaterial,
            _CreateAccessory,
            DomainManager.Item.CreateMedicine,
            DomainManager.Item.CreateMedicine,
        };

        public static ItemKey _CreateWeapon(DataContext dc, short s) { return DomainManager.Item.CreateWeapon(dc, s); }
        public static ItemKey _CreateArmor(DataContext dc, short s) { return DomainManager.Item.CreateArmor(dc, s); }
        public static ItemKey _CreateAccessory(DataContext dc, short s) { return DomainManager.Item.CreateAccessory(dc, s); }







    }
}
