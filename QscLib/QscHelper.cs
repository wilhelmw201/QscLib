using BehTree;
using Config;
using GameData.Domains.Character;
using GameData.Domains;
using GameData.Domains.Item;
using GameData.Domains.Item.Display;
using GameData.Domains.Map;
using GameData.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameData.Domains.Taiwu;
using GameData.Common;
using Misc = Config.Misc;
using GameData.Domains.Building;
using GameData.Domains.Combat;
using HarmonyLib;

namespace Qsc
{
    public class QscHelper
    {
        public static unsafe bool MoveNeili(int amount, int element)
        {
            DataContext context = DataContextManager.GetCurrentThreadDataContext();
            TaiwuDomain TWDomain = DomainManager.Taiwu;
            var Taiwu = TWDomain.GetTaiwu();
            NeiliProportionOfFiveElements props = Taiwu.GetBaseNeiliProportionOfFiveElements();
            NeiliProportionOfFiveElements props1 = new NeiliProportionOfFiveElements(new sbyte[] { 0,0,0,0,0 });
            int otherFour = 100 - props.Items[element] - amount;
            int otherFourActual = 0;
            if (otherFour <= 0)
            {
                props1.Items[element] = 100;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == element)
                    {
                        continue;
                    }
                    props1.Items[i] = (sbyte)((double)props.Items[i] * otherFour / 100);
                    otherFourActual += props1.Items[i];
                }
            }
            props1.Items[element] = (sbyte)(100 - otherFourActual);
            Taiwu.SetBaseNeiliProportionOfFiveElements(props1, context);

            return true;
        }
        public static bool setNeiLi(int Jin, int Mu, int Shui, int Huo)
        {
            DataContext context = DataContextManager.GetCurrentThreadDataContext();

            int Tu = 100 - Jin - Mu - Shui - Huo;
            if (Jin < 0 || Jin > 100 || Mu < 0 || Shui < 0 || Huo < 0 || Mu > 100 || Shui > 100 || Huo > 100 ||
                Tu < 0 || Tu > 100)
            {
                return false;
            }

            TaiwuDomain TWDomain = DomainManager.Taiwu;
            NeiliProportionOfFiveElements props =
                new NeiliProportionOfFiveElements(new sbyte[] { (sbyte)Jin, (sbyte)Mu, (sbyte)Shui, (sbyte)Huo, (sbyte)Tu });
            TWDomain.GetTaiwu().SetBaseNeiliProportionOfFiveElements(props, context);
            return true;
        }
        public static bool MyRefine(ItemKey targetKey, ItemKey matKey)
        {
            // 并不检查是否能精制，不要乱传东西
            DataContext context = DataContextManager.GetCurrentThreadDataContext();
            TaiwuDomain TWDomain = DomainManager.Taiwu;
            BuildingDomain BDDomain = DomainManager.Building;
            ItemDomain IDomain = DomainManager.Item;

            var RefEffs = IDomain.GetRefinedEffects(targetKey);
            int count = RefEffs.GetTotalRefiningCount();
            if (count >= 5) return false;

            IDomain.SetRefinedEffects(context, IDomain.GetBaseItem(targetKey), count, matKey.TemplateId);
            TWDomain.GetTaiwu().RemoveItem(context, matKey, 1, 0, true);

            return true;
        }
        public static bool MyPoison(ItemKey targetKey, ItemKey poisonKey)
        {
            DataContext context = DataContextManager.GetCurrentThreadDataContext();
            TaiwuDomain TWDomain = DomainManager.Taiwu;
            BuildingDomain BDDomain = DomainManager.Building;
            ItemDomain IDomain = DomainManager.Item;

            var FRemoveMaterial = AccessTools.Method(typeof(BuildingDomain), "RemoveMaterial");
            var FChangeItem = AccessTools.Method(typeof(BuildingDomain), "RemoveMaterial");


            GameData.Domains.Character.Character character = TWDomain.GetTaiwu();

            ItemKey[] equipment = character.GetEquipment();
            ItemBase itemToAddPoisonOn = DomainManager.Item.GetBaseItem(targetKey);
            bool flag = ModificationStateHelper.IsActive(targetKey.ModificationState, 1);
            if (flag)
            {
                PoisonEffects poisonEffects = DomainManager.Item.GetPoisonEffects(targetKey);
                bool flag2 = poisonEffects.GetTotalPoisonCount() > 0 && !poisonEffects.IsIdentified && !ItemType.IsEquipmentItemType(targetKey.ItemType);
                if (flag2)
                {
                    return false;
                }
            }
            ValueTuple<ItemBase, bool> valueTuple = DomainManager.Item.SetAttachedPoisons(context, itemToAddPoisonOn, 0, poisonKey.TemplateId);
            ItemBase newItemObj = valueTuple.Item1;
            bool keyChanged = valueTuple.Item2;
            bool flag3 = keyChanged;
            if (flag3)
            {
                ItemKey newKey = newItemObj.GetItemKey();
                int index = equipment.IndexOf(targetKey);
                bool flag4 = index > -1;
                if (flag4)
                {
                    equipment[index] = newKey;
                    character.ChangeEquipment(context, equipment);
                }
                else
                {
                    FChangeItem.Invoke(BDDomain, new object[] { context, character, targetKey, 1, newKey, 1 });
                }
            }
            FRemoveMaterial.Invoke(BDDomain, new object[] { context, character, poisonKey, 1, true });

            //RemoveMaterial(context, character, poisonKey, 1, true);
            return true;
        }
        public static bool MyDisassemble(ItemKey itemKey)
        {
            ItemDomain IDomain = DomainManager.Item;
            TaiwuDomain TWDomain = DomainManager.Taiwu;
            DataContext context = DataContextManager.GetCurrentThreadDataContext();


            GameData.Domains.Character.Character character = TWDomain.GetTaiwu();
            ItemBase item = IDomain.GetBaseItem(itemKey);
            sbyte resourceType = ItemTemplateHelper.GetResourceType(itemKey.ItemType, itemKey.TemplateId);
            bool flag = resourceType == -1;
            if (flag)
            {
            }
            else
            {
                List<ItemKey> keyList = new List<ItemKey>();
                short disassemblyMaterialId = ItemTemplateHelper.GetDisassemblyMaterial(itemKey.ItemType, itemKey.TemplateId, context.Random);
                bool flag2 = disassemblyMaterialId > -1;
                if (flag2)
                {
                    ItemKey disassemblyMaterialKey = IDomain.CreateMaterial(context, disassemblyMaterialId);
                    character.AddInventoryItem(context, disassemblyMaterialKey, 1);
                    keyList.Add(disassemblyMaterialKey);
                }
                bool flag3 = ModificationStateHelper.IsActive(item.GetModificationState(), 2);
                if (flag3)
                {
                    short[] allMaterialTemplateIds = DomainManager.Item.GetRefinedEffects(itemKey).GetAllMaterialTemplateIds();
                    if (allMaterialTemplateIds != null)
                    {
                        allMaterialTemplateIds.ForEach(delegate (int i, short materialId)
                        {
                            bool flag9 = materialId <= -1;
                            bool flag10;
                            if (flag9)
                            {
                                flag10 = false;
                            }
                            else
                            {
                                ItemKey materialKey = IDomain.CreateMaterial(context, materialId);
                                character.AddInventoryItem(context, materialKey, 1);
                                keyList.Add(materialKey);
                                flag10 = false;
                            }
                            return flag10;
                        });
                    }
                }
                bool flag4 = ItemType.IsEquipmentItemType(itemKey.ItemType);
                if (flag4)
                {
                    EquipmentBase equipItem = DomainManager.Item.GetBaseEquipment(itemKey);
                    ResourceInts resourceInts = ItemTemplateHelper.GetDisassembleResources(equipItem.GetMaterialResources(), itemKey.ItemType, itemKey.TemplateId);
                    character.ChangeResources(context, ref resourceInts);
                }
                else
                {
                    bool flag5 = itemKey.ItemType == 12;
                    if (flag5)
                    {
                        MiscItem miscConfig = Misc.Instance[itemKey.TemplateId];
                        MakeItemSubTypeItem makeConfig = MakeItemSubType.Instance[miscConfig.MakeItemSubType];
                        ResourceInts resourceInts2 = ItemTemplateHelper.GetDisassembleResources(makeConfig.MaxMaterialResources, itemKey.ItemType, itemKey.TemplateId);
                        character.ChangeResources(context, ref resourceInts2);
                    }
                    else
                    {
                        ResourceInts resource = ItemTemplateHelper.GetDisassembleResources(default(MaterialResources), itemKey.ItemType, itemKey.TemplateId);
                        character.ChangeResources(context, ref resource);
                    }
                }
                bool flag6 = character.GetInventory().Items.ContainsKey(itemKey);
                if (flag6)
                {
                    character.RemoveInventoryItem(context, itemKey, 1, true, true);
                }
                else
                {
                    ItemKey[] equip = character.GetEquipment();
                    int index = equip.IndexOf(itemKey);
                    bool flag7 = index >= 0;
                    if (flag7)
                    {
                        equip[index] = ItemKey.Invalid;
                        character.SetEquipment(equip, context);
                    }
                }


            }
            return true;


        }
    }
}
