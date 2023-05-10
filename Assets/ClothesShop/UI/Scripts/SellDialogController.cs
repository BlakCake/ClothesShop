using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using ClothesShop.Collectibles;
using ClothesShop.Player;

namespace ClothesShop.UI
{
    public class SellDialogController : MonoBehaviour
    {
        public static SellDialogController Instance;

        [Header("Movement")]
        public Vector2 showPosition;
        public Vector2 hidePosition;
        public float speed = .5f;

        [Header("State")]
        [SerializeField] private bool isShowing = false;
        [SerializeField] private bool isHiding = false;

        [Header("UI Elements")]
        [SerializeField] private GameObject sellSlotPrefab;
        [SerializeField] private GameObject sellSlotsContainer;

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
        public void Show()
        {
            // Start showing
            isShowing = true;
            isHiding = false;
            LeanTween.move(this.gameObject, showPosition, speed).setEaseSpring().setOnComplete(() => 
            {
                isShowing = false;
            });
        }

        [Button]
        public void Hide()
        {
            // Start hiding
            isHiding = true;
            isShowing = false;
            LeanTween.move(this.gameObject, hidePosition, speed).setEaseSpring().setOnComplete(() => 
            {
                isHiding = false;
            });
        }

        public void GetSellableItems()
        {
            // Clear sell slots
            foreach (Transform child in sellSlotsContainer.transform)
            {
                Destroy(child.gameObject);
            }
            // Get items from player inventory
            var items = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().inventory.items;
            List<ICollectible> sellableItems = new List<ICollectible>();
            for (int i = 0; i < Random.Range(1, items.Count); i++)
            {
                var item = items[Random.Range(0, items.Count)];
                // Instantiate sell slot
                var sellSlot = Instantiate(sellSlotPrefab, sellSlotsContainer.transform).GetComponent<SellSlot>();
                // Set item sprite
                sellSlot.SetData(item);
            }
        }
    }
}
