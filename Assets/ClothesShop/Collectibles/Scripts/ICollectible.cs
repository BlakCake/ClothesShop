using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothesShop.Collectibles
{
    public interface ICollectible
    {
        public string name { get; set; }
        public Sprite sprite { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public void AddToInventory();
        public ICollectible Buy();
        public void Sell();
    }
}
