using Qsc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QscLib
{


    public class RefLootTables
    {
        public static LootTableEntry[] Farmland1 = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Food, 1, 150),
            new LootTableEntry(QscData.LootClass.Food, 3, 50),
            new LootTableEntry(QscData.LootClass.Food, 3, 0),
            new LootTableEntry(QscData.LootClass.TeaWine, 1, 90),
            new LootTableEntry(QscData.LootClass.TeaWine, 3, 40),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 5),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 0),
            new LootTableEntry(QscData.LootClass.Material, 2, -10),
        };

        public static LootTableEntry[] Gardens1 = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Food, 2, 120),
            new LootTableEntry(QscData.LootClass.Food, 2, 10),
            new LootTableEntry(QscData.LootClass.TeaWine, 2, 199),
            new LootTableEntry(QscData.LootClass.TeaWine, 1, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 20),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 0),
            new LootTableEntry(QscData.LootClass.Material, 1, 0),
        };

        public static LootTableEntry[] StoneForest1 = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 40),
            new LootTableEntry(QscData.LootClass.Material, 2, 30),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -90),
            new LootTableEntry(QscData.LootClass.Weapon, 2, -75),
            new LootTableEntry(QscData.LootClass.Weapon, 2, -102),
            new LootTableEntry(QscData.LootClass.Armor, 2, -150),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -150),
            new LootTableEntry(QscData.LootClass.Food, 1, 0),
        };

        public static LootTableEntry[] MulberryField1 = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 40),
            new LootTableEntry(QscData.LootClass.Material, 2, 30),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -80),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -75),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -102),
            new LootTableEntry(QscData.LootClass.Armor, 4, -99),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -190),
            new LootTableEntry(QscData.LootClass.Food, 2, 10),
        };

        public static LootTableEntry[] HerbalGarden1 = new LootTableEntry[]
{
            new LootTableEntry(QscData.LootClass.TeaWine, 3, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 6, 20),
            new LootTableEntry(QscData.LootClass.Poison, 2, 0),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -101),
            new LootTableEntry(QscData.LootClass.Food, 2, 0),
};
        public static LootTableEntry[] JadeMountain1 = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 40),
            new LootTableEntry(QscData.LootClass.Material, 2, 30),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 4, -120),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -60),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -102),
            new LootTableEntry(QscData.LootClass.Armor, 1, -120),
            new LootTableEntry(QscData.LootClass.Jewelry, 3, -90),
            new LootTableEntry(QscData.LootClass.Food, 1, 0),
          };
        public static LootTableEntry[] Mountain1 = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 70),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -60),
            new LootTableEntry(QscData.LootClass.Jewelry, 2, -90),
            new LootTableEntry(QscData.LootClass.Poison, 1, 30),

        };
        public static LootTableEntry[] BigMountain1 = Mountain1;


        public static LootTableEntry[] Canyon1 = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 100),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -150),
            new LootTableEntry(QscData.LootClass.TeaWine, 1, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 70),
            new LootTableEntry(QscData.LootClass.Poison, 1, 30),
         };
        public static LootTableEntry[] BigCanyon1 = Canyon1;
        public static LootTableEntry[] BigHill1 = Canyon1;
        public static LootTableEntry[] Hill1 = Canyon1;
        public static LootTableEntry[] Field1 = Farmland1;
        public static LootTableEntry[] BigField1 = Farmland1;
        public static LootTableEntry[] Woodland1 = StoneForest1;
        public static LootTableEntry[] BigWoodland1 = StoneForest1;


        public static LootTableEntry[] RiverBeach1 = new LootTableEntry[]
 {
            new LootTableEntry(QscData.LootClass.Material, 1, 10),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -120),
            new LootTableEntry(QscData.LootClass.Food, 4, 60),
            new LootTableEntry(QscData.LootClass.TeaWine, 2, 40),
            new LootTableEntry(QscData.LootClass.Poison, 1, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 4, 10),
   }; 
        public static LootTableEntry[] BigRiverBeach1 = RiverBeach1;
        public static LootTableEntry[] Lake1 = RiverBeach1;

        public static LootTableEntry[] Jungle1 = new LootTableEntry[]
{
            new LootTableEntry(QscData.LootClass.Material, 3, 0),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -90),
            new LootTableEntry(QscData.LootClass.Weapon, 2, -102),
            new LootTableEntry(QscData.LootClass.Armor, 3, -102),
            new LootTableEntry(QscData.LootClass.Food, 3, 80),
            new LootTableEntry(QscData.LootClass.Poison, 2, 20),
};
        public static LootTableEntry[] Cave1 = Jungle1;
        public static LootTableEntry[] Swamp1 = new LootTableEntry[]
{
            new LootTableEntry(QscData.LootClass.Material, 1, 0),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -150),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 20),
            new LootTableEntry(QscData.LootClass.Poison, 1, 110),
            new LootTableEntry(QscData.LootClass.Poison, 1, 70),
            new LootTableEntry(QscData.LootClass.Poison, 5, 10),
};
        public static LootTableEntry[] TaoYuan1 = new LootTableEntry[]
{
            new LootTableEntry(QscData.LootClass.Food, 2, 150),
            new LootTableEntry(QscData.LootClass.TeaWine, 2, 105),
            new LootTableEntry(QscData.LootClass.Material, 2, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 105),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -90),
};
        public static LootTableEntry[] Valley1 = Jungle1;
        public static LootTableEntry[] Wild1 = new LootTableEntry[]
{
            new LootTableEntry(QscData.LootClass.Food, 1, 0),
            new LootTableEntry(QscData.LootClass.TeaWine, 1, 0),
            new LootTableEntry(QscData.LootClass.Material, 1, 0),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -100),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 0),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -150),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -180),
            new LootTableEntry(QscData.LootClass.Armor, 1, -180),
            new LootTableEntry(QscData.LootClass.Poison, 1, -20),
};
        public static LootTableEntry[] DarkPool1 = new LootTableEntry[]
{
            new LootTableEntry(QscData.LootClass.Weapon, 1, -50),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -110),
            new LootTableEntry(QscData.LootClass.Material, 1, 80),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 180),
            new LootTableEntry(QscData.LootClass.Poison, 1, 90),
};

        public static LootTableEntry[] TestTable = new LootTableEntry[]
        {
            new LootTableEntry(QscData.LootClass.Material, 3, -99),
            new LootTableEntry(QscData.LootClass.Medicine, 8, 99),
            new LootTableEntry(QscData.LootClass.Weapon, 5, 99),
            new LootTableEntry(QscData.LootClass.Armor, 5, 99),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 5, 299),
            new LootTableEntry(QscData.LootClass.TeaWine, 3, -299),
            new LootTableEntry(QscData.LootClass.Poison, 3, -99),
            new LootTableEntry(QscData.LootClass.Jewelry, 3, 199),
        };


    }
}
