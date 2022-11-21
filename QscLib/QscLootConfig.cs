using Qsc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qsc
{
    public class LootTable
    {
        public readonly LootTableEntry[] entry;
        public int count;
        public LootTable(LootTableEntry[] entry, int count)
        {
            this.entry = entry;
            this.count = count;
        }
    }

    public class RefLootTables
    {
        public static LootTable Farmland1 = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Food, 1, 150),
            new LootTableEntry(QscData.LootClass.Food, 3, 50),
            new LootTableEntry(QscData.LootClass.Food, 3, 0),
            new LootTableEntry(QscData.LootClass.TeaWine, 1, 90),
            new LootTableEntry(QscData.LootClass.TeaWine, 3, 40),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 0),
        }, 3);

        public static LootTable Gardens1 = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Food, 2, 120),
            new LootTableEntry(QscData.LootClass.Food, 2, 10),
            new LootTableEntry(QscData.LootClass.TeaWine, 2, 199),
            new LootTableEntry(QscData.LootClass.TeaWine, 1, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 20),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 0),
            new LootTableEntry(QscData.LootClass.Material, 1, 0),
        }, 3);

        public static LootTable StoneForest1 = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 40),
            new LootTableEntry(QscData.LootClass.Material, 2, 30),
            new LootTableEntry(QscData.LootClass.Weapon, 2, -75),
            new LootTableEntry(QscData.LootClass.Weapon, 5, -102),
            new LootTableEntry(QscData.LootClass.Armor, 2, -150),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -150),
        }, 1);

        public static LootTable MulberryField1 = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 40),
            new LootTableEntry(QscData.LootClass.Material, 2, 30),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -80),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -75),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -102),
            new LootTableEntry(QscData.LootClass.Armor, 4, -99),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -190),
            new LootTableEntry(QscData.LootClass.Food, 2, 10),
        }, 1);

        public static LootTable HerbalGarden1 = new LootTable( new LootTableEntry[] 
{
            new LootTableEntry(QscData.LootClass.TeaWine, 3, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 6, 20),
            new LootTableEntry(QscData.LootClass.Poison, 2, 0),
            new LootTableEntry(QscData.LootClass.Food, 2, 0),
}, 2);
        public static LootTable JadeMountain1 = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 40),
            new LootTableEntry(QscData.LootClass.Material, 2, 30),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 4, -120),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -60),
            new LootTableEntry(QscData.LootClass.Weapon, 2, -102),
            new LootTableEntry(QscData.LootClass.Armor, 1, -120),
            new LootTableEntry(QscData.LootClass.Jewelry, 3, -90),
            new LootTableEntry(QscData.LootClass.Food, 1, 0),
          }, 1);
        public static LootTable Mountain1 = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 70),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -60),
            new LootTableEntry(QscData.LootClass.Jewelry, 3, -90),
            new LootTableEntry(QscData.LootClass.Poison, 1, 30),

        }, 1);
        public static LootTable BigMountain1 = Mountain1;


        public static LootTable Canyon1 = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Material, 1, 100),
            new LootTableEntry(QscData.LootClass.TeaWine, 1, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 1, 70),
            new LootTableEntry(QscData.LootClass.Poison, 1, 30),
         }, 2);
        public static LootTable BigCanyon1 = Canyon1;
        public static LootTable BigHill1 = Canyon1;
        public static LootTable Hill1 = Canyon1;
        public static LootTable Field1 = Farmland1;
        public static LootTable BigField1 = Farmland1;
        public static LootTable Woodland1 = StoneForest1;
        public static LootTable BigWoodland1 = StoneForest1;


        public static LootTable RiverBeach1 = new LootTable( new LootTableEntry[] 
 {
            new LootTableEntry(QscData.LootClass.Material, 1, 10),
            new LootTableEntry(QscData.LootClass.Food, 4, 60),
            new LootTableEntry(QscData.LootClass.TeaWine, 2, 40),
            new LootTableEntry(QscData.LootClass.Poison, 1, 0),
            new LootTableEntry(QscData.LootClass.Medicine, 4, 10),
   }, 2); 
        public static LootTable BigRiverBeach1 = RiverBeach1;
        public static LootTable Lake1 = RiverBeach1;

        public static LootTable Jungle1 = new LootTable( new LootTableEntry[] 
{
            new LootTableEntry(QscData.LootClass.Material, 1, 0),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, -90),
            new LootTableEntry(QscData.LootClass.Weapon, 3, -102),
            new LootTableEntry(QscData.LootClass.Armor, 3, -102),
            new LootTableEntry(QscData.LootClass.Poison, 2, 20),
}, 1);
        public static LootTable Cave1 = Jungle1;
        public static LootTable Swamp1 = new LootTable( new LootTableEntry[] 
{
            new LootTableEntry(QscData.LootClass.Material, 1, 0),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -150),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 20),
            new LootTableEntry(QscData.LootClass.Poison, 1, 110),
            new LootTableEntry(QscData.LootClass.Poison, 1, 50),
            new LootTableEntry(QscData.LootClass.Poison, 5, 0),
}, 2);
        public static LootTable TaoYuan1 = new LootTable( new LootTableEntry[] 
{
            new LootTableEntry(QscData.LootClass.Food, 2, 150),
            new LootTableEntry(QscData.LootClass.TeaWine, 2, 105),
            new LootTableEntry(QscData.LootClass.Material, 2, 50),
            new LootTableEntry(QscData.LootClass.Medicine, 2, 105),
            new LootTableEntry(QscData.LootClass.Jewelry, 1, -90),
}, 2);
        public static LootTable Valley1 = Jungle1;
        public static LootTable Wild1 = new LootTable( new LootTableEntry[] 
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
}, 1);
        public static LootTable DarkPool1 = new LootTable( new LootTableEntry[] 
{
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 1, 0),
            new LootTableEntry(QscData.LootClass.Weapon, 1, -50),
            new LootTableEntry(QscData.LootClass.Weapon, 3, -110),
            new LootTableEntry(QscData.LootClass.Medicine, 3, 180),
            new LootTableEntry(QscData.LootClass.Poison, 1, 150),
}, 1);

        public static LootTable TestTable = new LootTable( new LootTableEntry[] 
        {
            new LootTableEntry(QscData.LootClass.Material, 3, -99),
            new LootTableEntry(QscData.LootClass.Medicine, 8, 99),
            new LootTableEntry(QscData.LootClass.Weapon, 5, 99),
            new LootTableEntry(QscData.LootClass.Armor, 5, 99),
            new LootTableEntry(QscData.LootClass.JingzhiMaterial, 5, 299),
            new LootTableEntry(QscData.LootClass.TeaWine, 3, -299),
            new LootTableEntry(QscData.LootClass.Poison, 3, -99),
            new LootTableEntry(QscData.LootClass.Jewelry, 3, 199),
        }, 5);


    }
}
