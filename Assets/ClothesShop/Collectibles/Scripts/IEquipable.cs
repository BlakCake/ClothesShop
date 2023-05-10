using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ClothesShop.Collectibles
{
    public interface IEquipable
    {
        public bool isEquipped { get; set; }
        public EquipmentType type { get; set; }
        public void Equip();
        public void Unequip();
        public void ToggleEquip();
    }

    public enum EquipmentType
        {
            Head,
            Top,
            Bottom,
            Shoes
        }
}