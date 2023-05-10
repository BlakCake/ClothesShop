using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using ClothesShop.Collectibles;
using ClothesShop.Player;

namespace ClothesShop.UI
{
    public class InventoryUIController : MonoBehaviour
    {
        public static InventoryUIController Instance;

        [Header("Movement")]
        public Vector2 showPosition;
        public Vector2 hidePosition;
        public float speed = .5f;

        [Header("State")]
        [SerializeField] private bool isOpen = false;
        [SerializeField] private bool isShowing = false;
        [SerializeField] private bool isHiding = false;

        [Header("UI Elements")]
        [SerializeField] private Button flap;
        [SerializeField] private Image background;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Sprite equippedSprite;
        void Awake()
        {
            // Singleton
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        [Button]
        public void Toggle()
        {
            if (isOpen)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        [Button]
        public void Show()
        {
            // Start showing
            isShowing = true;
            isHiding = false;
            LeanTween.moveLocal(this.gameObject, showPosition, speed).setEaseSpring().setOnComplete(() => 
            {
                // Finish showing
                isShowing = false;
                isOpen = true;
            });
        }

        [Button]
        public void Hide()
        {
            // Start hiding
            isShowing = false;
            isHiding = true;
            LeanTween.moveLocal(this.gameObject, hidePosition, speed).setEaseSpring().setOnComplete(() => 
            {
                // Finish hiding
                isHiding = false;
                isOpen = false;
            });
        }

        public void UpdateInventory()
        {
            // Clear previous items
            foreach (Transform child in scrollRect.content)
            {
                Destroy(child.gameObject);
            }
            // Get items
            var items = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().inventory.items;
            // Add new items
            foreach (ICollectible item in items)
            {
                GameObject itemObject = Instantiate(itemPrefab, scrollRect.content);
                itemObject.GetComponent<InventorySlot>().item = item;
                var clothes = item as Clothes;
                if (clothes.isEquipped)
                {
                    itemObject.GetComponent<Image>().sprite = equippedSprite;
                }
                else
                {
                    itemObject.GetComponent<Image>().sprite = emptySprite;
                }
            }
        }
    }
}
