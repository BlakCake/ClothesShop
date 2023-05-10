using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ClothesShop.Collectibles;

namespace ClothesShop.UI
{    
    public class InventorySlot : MonoBehaviour
    {
        public ICollectible item {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                if (_item != null)
                {
                    itemSprite.sprite = _item.sprite;
                    itemName.text = _item.name;
                    button.onClick.AddListener(() => 
                    {
                        var clothes = _item as IEquipable;
                        if (clothes != null)
                        {
                            clothes.ToggleEquip();
                            InventoryUIController.Instance.UpdateInventory();
                        }
                    });
                }
        }}

        [SerializeField] private ICollectible _item;
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Button button;
    }
}
