using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GCUWebGame.Inventory
{
    public class InventoryBehaviour : MonoBehaviour, IItemContainer
    {
        [SerializeField] private Inventory inventory = null;

        private void Start()
        {
            inventory.SetSize(20);
        }

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            return inventory.AddItem(itemSlot);
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            return inventory.GetTotalQuantity(item);
        }

        public bool HasItem(InventoryItem item)
        {
            return inventory.HasItem(item);
        }

        public void RemoveAt(int slotIndex)
        {
            inventory.RemoveAt(slotIndex);
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            inventory.RemoveItem(itemSlot);
        }

        public void Swap(int indexOne, int indexTwo)
        {
            inventory.Swap(indexOne, indexTwo);
        }
    }
}
