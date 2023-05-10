using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ClothesShop.InventorySystem;
using ClothesShop.Collectibles;
using ClothesShop.UI;

namespace ClothesShop.Spawner
{
    public class ShopItemSpawner : MonoBehaviour
    {
        public ICollectible item;

        [SerializeField] private Inventory itemInventory;

        private SpriteRenderer itemSpriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            // Get Sprite Renderer
            itemSpriteRenderer = GetComponent<SpriteRenderer>();
            // Get Random Item
            List<ICollectible> equipables = itemInventory.items.OfType<ICollectible>().ToList();
            item = equipables[Random.Range(0, equipables.Count)];
            // Display Item
            itemSpriteRenderer.sprite = item.sprite;
            // Fix Display if depending on Equipment Type
            if (item is Clothes)
            {
                Clothes clothes = item as Clothes;
                if (clothes.type == EquipmentType.Head)
                {
                    itemSpriteRenderer.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
                    GetComponent<BoxCollider2D>().offset = Vector2.zero;
                }
            }
            // Start Floating Animation
            StartCoroutine(FloatingAnimation());
        }

        IEnumerator FloatingAnimation()
        {
            while (true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * 0.0001f, transform.position.z);
                yield return null;
            }
        }

        void OnTriggerEnter2D(Collider2D collider) 
        {
            if (collider.CompareTag("Player"))
            {
                BuyDialogController.Instance.SetData(item, this.gameObject);
                BuyDialogController.Instance.Show();
            }
        }

        void OnTriggerExit2D(Collider2D collider2D) 
        {
            if (collider2D.CompareTag("Player"))
            {
                BuyDialogController.Instance.Hide();
            }
        }
    }
}