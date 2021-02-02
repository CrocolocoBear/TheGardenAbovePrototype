using UnityEngine;
using UnityEngine.EventSystems;


/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    //logic for dragging inventory item rather than hotbar
    public class InventoryItemDragHandler : ItemDragHandler
    {
        [SerializeField] private ItemDestroyer itemDestroyer = null;

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);

                //releasing over nothing (into world) to destroy
                if (eventData.hovered.Count == 0)
                {
                    InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                    itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
                }
            }
        }
    }
}
