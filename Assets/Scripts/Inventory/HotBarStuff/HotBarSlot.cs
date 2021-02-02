using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */
namespace GCUWebGame.Inventory

{
    //needs to reference inventory space
    public class HotBarSlot : ItemSlotUI, IDropHandler
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;
        [SerializeField] private Image active;
        public KeyCode theKey = KeyCode.None;

        [SerializeField] private HotBarItem slotItem = null;



        private void Update()
        {

            //to highlight selected hotbar item
            if (Input.GetKeyDown(theKey))
            {
                //Debug.Log(GameObject.Find("Active"));
                if (GameObject.Find("Active") != null)
                {
                    GameObject.Find("Active").gameObject.SetActive(false);
                }
                active.gameObject.SetActive(true);

                //need to set item in game world to active as well, ^this is just UI
            }

        }

        public override HotBarItem SlotItem 
        { 
            get { return slotItem; }
            set { slotItem = value; UpdateSlotUI(); }
        }

        public bool AddItem(HotBarItem itemToAdd)
        {
            //if occupied, don't add
            if (SlotItem != null) 
            { return false; }

            //otherwise...
            SlotItem = itemToAdd;
            return true;
        }

        //when key is pressed, use item
        public void UseSlot()
        {
            
        }

        //when something is dropped on slot
        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            if (itemDragHandler == null) { return; }

            //check what previous slot is & then set or swap accordingly
            InventorySlot inventorySlot = itemDragHandler.ItemSlotUI as InventorySlot;
            
            /*treat slot as inventory slot, check if it actually is..
              *If YES, get item from slot, set, return..*/
            if (inventorySlot != null)
            {
                SlotItem = inventorySlot.ItemSlot.item;
                slotItem.inventoryItem = inventorySlot.ItemSlot.item;
                return;
            }

            HotBarSlot hotBarSlot = itemDragHandler.ItemSlotUI as HotBarSlot;
            
            /*SWAP..
             * if a hotbar slot is being dropped onto other hbs, store current slot item, set to new item, 
             * THEN set item dropped to previous item*/
            if (hotBarSlot != null)
            {
                HotBarItem oldItem = SlotItem;
                SlotItem = hotBarSlot.SlotItem;
                hotBarSlot.SlotItem = oldItem;
                return;
            }
        }

        public override void UpdateSlotUI()
        {
            //if slot is empty, disable slotUI
            if (SlotItem == null)
            {
                EnableSlotUI(false);
                return;
            }

            //if slot is occupied
            itemIconImage.sprite = SlotItem.Icon;

            EnableSlotUI(true);

            SetItemQuantityUI();
        }

        //check if what's being stored has a quantity
        private void SetItemQuantityUI()
        {
            //for when item is being stored & player uses the item, checks if quant. has reached 0
            if (SlotItem is InventoryItem inventoryItem)
            {
                if (inventory.HasItem(inventoryItem))
                {
                    int quantityCount = inventory.GetTotalQuantity(inventoryItem);
                    //if quantCount is > 1 (2+), then set to actual quantCount or blank string.. Hides count unless > 1
                    itemQuantityText.text = quantityCount > 1 ? quantityCount.ToString() : "";
                }
                else
                {
                    //clear slot if item is no longer there
                    SlotItem = null;
                }
            }
            else
            {
                itemQuantityText.enabled = false;
            }
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}
