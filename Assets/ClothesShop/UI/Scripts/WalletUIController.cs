using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ClothesShop.Player;

namespace ClothesShop.UI
{
    public class WalletUIController : MonoBehaviour
    {
        public static WalletUIController Instance;

        [SerializeField] private TextMeshProUGUI text;

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

        private void Start()
        {
            UpdateWallet(GameObject.Find("Player").GetComponent<PlayerController>().inventory.money);
        }

        public void UpdateWallet(int money)
        {
            text.text = money.ToString();
        }
    }
}