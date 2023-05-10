using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ClothesShop.Collectibles;

namespace ClothesShop.InventorySystem
{
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment")]
    public class Equipment : SerializedScriptableObject
    {
        [Header("Clothes")]
        [SerializeField] public IEquipable head;
        [SerializeField] public IEquipable top;
        [SerializeField] public IEquipable bottom;
        [SerializeField] public IEquipable shoes;

        [Header("Hands")]
        [SerializeField] public IEquipable leftHand;
        [SerializeField] public IEquipable rightHand;
    }
}

