using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ClothesShop.Player;

namespace ClothesShop.Collectibles
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Collectible/Item")]
    public class Item : SerializedScriptableObject, ICollectible
    {
        [PreviewField(100, ObjectFieldAlignment.Left)]
        [SerializeField] public Sprite sprite { get ; set; }
        [SerializeField] public string description { get; set; }
        [SerializeField] public int price { get; set; }
        [SerializeField] public bool isEquipped { get; set; }

        public void AddToInventory()
        {
            throw new System.NotImplementedException();
        }

        public ICollectible Buy()
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            if (player.inventory.money >= price)
            {
                player.inventory.money -= price;
                var item = Instantiate(this);
                player.inventory.AddToInventory(item);
                return item;
            }
            else
            {
                return null;
            }
        }

        public void Sell()
        {
            throw new System.NotImplementedException();
        }
    }
}

