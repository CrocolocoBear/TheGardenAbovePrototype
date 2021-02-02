using UnityEngine.EventSystems;

/*NOTE: 
 *This code is based on Dapper Dino's and tutorials :)
 */
namespace GCUWebGame.Inventory
{
    public class HotBarItemDragHandler : ItemDragHandler
    {
        //mouse released
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);

                //when released over nothing, clear hotbar slot
                if (eventData.hovered.Count == 0)
                {
                    (ItemSlotUI as HotBarSlot).SlotItem = null;
                }
            }
        }
    }
}
