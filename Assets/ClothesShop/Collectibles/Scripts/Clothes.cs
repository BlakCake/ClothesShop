using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using ClothesShop.Player;

namespace ClothesShop.Collectibles
{
    [CreateAssetMenu(fileName = "New Clothes", menuName = "Collectible/Clothes"), System.Serializable]
    public class Clothes : SerializedScriptableObject, ICollectible, IEquipable
    {
        [PreviewField(100, ObjectFieldAlignment.Left)]
        [SerializeField] public Sprite sprite { get; set; }
        [SerializeField] public string description { get; set; }
        [SerializeField] public int price { get; set; }
        [SerializeField] public bool isEquipped { get; set; }
        [SerializeField] public EquipmentType type { get; set; }

        public void AddToInventory()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().inventory.items.Add(Instantiate(this));
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

        public void Equip()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Equip(this);
        }

        public void Unequip()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Unequip(this);
        }

        public void ToggleEquip()
        {
            if (isEquipped)
            {
                Unequip();
            }
            else
            {
                Equip();
            }
        }
    }
}