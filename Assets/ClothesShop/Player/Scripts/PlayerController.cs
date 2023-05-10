using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
using ClothesShop.InventorySystem;
using ClothesShop.Collectibles;
using ClothesShop.UI;

namespace ClothesShop.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float speed = .75f;
        public float runMultiplier = 1.5f;

        [Header("Inventory"), InlineEditor]
        public Inventory inventory;
        [SerializeField] private Inventory clothesInventory;

        [Header("Equipment Slots")]
        [SerializeField] private SpriteRenderer headSlot;
        [SerializeField] private SpriteRenderer topSlot;
        [SerializeField] private SpriteRenderer bottomSlot;
        [SerializeField] private SpriteRenderer shoesSlot;
        [SerializeField] private SpriteRenderer leftHandSlot;
        [SerializeField] private SpriteRenderer rightHandSlot;

        private Vector2 movement;
        private Rigidbody2D rb2d;
        private Camera playerCamera;

        // Start is called before the first frame update
        void Start()
        {
            rb2d = this.GetComponent<Rigidbody2D>();
            playerCamera = Camera.main;
            GetRandomEquipment();
        }

        void FixedUpdate()
        {
            rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, new Vector3(rb2d.position.x, rb2d.position.y, playerCamera.transform.position.z), 0.1f);
        }

        void OnMove(InputValue value)
        {
            movement = value.Get<Vector2>();
        }

        [Button]
        void GetRandomEquipment()
        {
            // Head
            Clothes head = Instantiate(GetRandomEquipable(EquipmentType.Head) as Clothes);
            inventory.AddToInventory(head);
            Equip(head);
            // Top
            Clothes top = Instantiate(GetRandomEquipable(EquipmentType.Top) as Clothes);
            inventory.AddToInventory(top);
            Equip(top);
            // Bottom
            Clothes bottom = Instantiate(GetRandomEquipable(EquipmentType.Bottom) as Clothes);
            inventory.AddToInventory(bottom);
            Equip(bottom);
            // Shoes
            Clothes shoes = Instantiate(GetRandomEquipable(EquipmentType.Shoes) as Clothes);
            inventory.AddToInventory(shoes);
            Equip(shoes);            
        }

        IEquipable GetRandomEquipable(EquipmentType type)
        {
            List<IEquipable> equipables = clothesInventory.items.OfType<IEquipable>().ToList().Where(x => x.type == type).ToList();
            if (equipables.Count == 0)
            {
                return null;
            }
            else
            {
                return equipables[Random.Range(0, equipables.Count)];
            }
        }

        public void Equip(IEquipable equipable)
        {
            // Check if equipable is null
            if (equipable == null)
            {
                return;
            }
            // Check if equipable is already equipped
            if (equipable.isEquipped)
            {
                return;
            }
            // Unequip previous equipable
            switch (equipable.type)
            {
                case EquipmentType.Head:
                    if (inventory.equipment.head != null)
                    {
                        Unequip(inventory.equipment.head);
                    }
                    break;
                case EquipmentType.Top:
                    if (inventory.equipment.top != null)
                    {
                        Unequip(inventory.equipment.top);
                    }
                    break;
                case EquipmentType.Bottom:
                    if (inventory.equipment.bottom != null)
                    {
                        Unequip(inventory.equipment.bottom);
                    }
                    break;
                case EquipmentType.Shoes:
                    if (inventory.equipment.shoes != null)
                    {
                        Unequip(inventory.equipment.shoes);
                    }
                    break;
                default:
                    break;
            }
            // Get as Clothes
            var clothes = equipable as Clothes;
            // Equip
            switch (clothes.type)
            {
                case EquipmentType.Head:
                    inventory.equipment.head = clothes;
                    headSlot.sprite = clothes.sprite;
                    clothes.isEquipped = true;
                    break;
                case EquipmentType.Top:
                    inventory.equipment.top = clothes;
                    topSlot.sprite = clothes.sprite;
                    clothes.isEquipped = true;
                    break;
                case EquipmentType.Bottom:
                    inventory.equipment.bottom = clothes;
                    bottomSlot.sprite = clothes.sprite;
                    clothes.isEquipped = true;
                    break;
                case EquipmentType.Shoes:
                    inventory.equipment.shoes = clothes;
                    shoesSlot.sprite = clothes.sprite;
                    clothes.isEquipped = true;
                    break;
                default:
                    break;
            }
            InventoryUIController.Instance.UpdateInventory();
        }

        public void Unequip(IEquipable equipable)
        {
            // Check if equipable is null
            if (equipable == null)
            {
                return;
            }
            // Unequip
            switch (equipable.type)
            {
                case EquipmentType.Head:
                    inventory.equipment.head = null;
                    headSlot.sprite = null;
                    equipable.isEquipped = false;
                    break;
                case EquipmentType.Top:
                    inventory.equipment.top = null;
                    topSlot.sprite = null;
                    equipable.isEquipped = false;
                    break;
                case EquipmentType.Bottom:
                    inventory.equipment.bottom = null;
                    bottomSlot.sprite = null;
                    equipable.isEquipped = false;
                    break;
                case EquipmentType.Shoes:
                    inventory.equipment.shoes = null;
                    shoesSlot.sprite = null;
                    equipable.isEquipped = false;
                    break;
                default:
                    break;
            }
        }
    }
}

