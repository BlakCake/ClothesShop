using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClothesShop.UI;

public class Shopkeeper : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            SellDialogController.Instance.GetSellableItems();
            SellDialogController.Instance.Show();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            SellDialogController.Instance.Hide();
        }
    }
}
