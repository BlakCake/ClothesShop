using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using ClothesShop.Collectibles;

namespace ClothesShop.UI
{
    public class BuyDialogController : MonoBehaviour
    {
        public static BuyDialogController Instance;

        [Header("Movement")]
        public Vector2 showPosition;
        public Vector2 hidePosition;
        public float speed = .5f;

        [Header("State")]
        [SerializeField] private bool isShowing = false;
        [SerializeField] private bool isHiding = false;

        [Header("UI Elements")]
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemCost;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button buyAndEquipButton;

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

        public void SetData(ICollectible item, GameObject spawnerObject)
        {
            // Clear previous data
            buyButton.onClick.RemoveAllListeners();
            buyAndEquipButton.onClick.RemoveAllListeners();
            // Set item sprite
            itemSprite.sprite = item.sprite;
            // Set item name
            itemName.text = item.name;
            // Set item cost
            itemCost.text = item.price.ToString();
            // Set buy button
            buyButton.onClick.AddListener(() => 
            {
                item.Buy();
                Destroy(spawnerObject);
            });
            // Set buy and equip button
            buyAndEquipButton.onClick.AddListener(() => 
            {
                var clothes = item.Buy() as Clothes;
                clothes.Equip();
                Destroy(spawnerObject);
            });
        }

        [Button]
        public void Show()
        {
            // Start showing
            isShowing = true;
            isHiding = false;
            LeanTween.moveLocal(this.gameObject, showPosition, speed).setEaseSpring().setOnComplete(() => isShowing = false);
        }

        [Button]
        public void Hide()
        {
            // If already showing or hiding, don't do anything
            if (isShowing || isHiding)
                return;
            // Start hiding
            isShowing = false;
            isHiding = true;
            LeanTween.moveLocal(this.gameObject, hidePosition, speed).setEaseSpring().setOnComplete(() => isHiding = false);
        }
    }
}