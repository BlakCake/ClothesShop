using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using ClothesShop.Collectibles;
using ClothesShop.UI;

namespace ClothesShop.InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
    public class Inventory : SerializedScriptableObject
    {
        public int money { 
            get
            {
                return _money;
            } 
            set
            {
                _money = value;
                WalletUIController.Instance.UpdateWallet(_money);
            }}
        [InlineEditor]
        public Equipment equipment;
        public List<ICollectible> items = new List<ICollectible>();

        [SerializeField] private int _money = 420;

        public void AddToInventory(ICollectible item)
        {
            items.Add(item);
            InventoryUIController.Instance.UpdateInventory();
        }
    }
}
