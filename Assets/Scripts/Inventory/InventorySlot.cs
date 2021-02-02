using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    //defines how an inventory slot is different from hotbar slot
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        //public InventorySlot[] inventorySlot;
        //public HotBarSlot[] hotBarSlots;

        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        public override HotBarItem SlotItem
        {
            get { return ItemSlot.item; }
            set { }
        }

        //when reffering to item slot, gets from inventory
        public ItemSlot ItemSlot => inventory.GetSlotByIndex(SlotIndex);

        public override void OnDrop(PointerEventData eventData)
        {
            //when something is dropped on x slot, get drag handler script
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            
            if (itemDragHandler == null) { return; }

            //if inventory slot is dropped on another inv. slot
            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                inventory.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }

            //for (int i = 0; 0 < hotBarSlots.Length; i++)
            //{
            //    hotBarSlots[i].SlotItem = inventory.ItemContainer.GetSlotByIndex(i).item;
            //}

        }

        //update slot's UI
        public override void UpdateSlotUI()
        {
            if (ItemSlot.item == null)
            {
                EnableSlotUI(false);
                return;
            }
            EnableSlotUI(true);

            itemIconImage.sprite = ItemSlot.item.Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
    
}
