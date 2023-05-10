using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ClothesShop.Collectibles;

namespace ClothesShop.UI
{
    public class SellSlot : MonoBehaviour
    {
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemCost;

        public void SetData(ICollectible item)
        {
            // Set item sprite
            itemSprite.sprite = item.sprite;
            // Set item name
            itemName.text = item.name;
            // Set item cost
            itemCost.text = ((int)(item.price * .75f)).ToString();
        }
    }
}
