using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    //base class for all types of item slots (inventory & hotbar slots)
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected Image itemIconImage;

        //setting slot index, but can also get
        public int SlotIndex
        { get; private set; }

        public abstract HotBarItem SlotItem
        { get; set; }

        //each time inventory is opened
        private void OnEnable()
        {
            UpdateSlotUI(); 
        }

        /*When invetory is first opened...
          Checks where we are in the hierarchy, then sets*/
        protected virtual void Start()
        {
            SlotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
        }


        //handles logic when dropped on slot
        public abstract void OnDrop(PointerEventData eventData);

        //how to update slot UI
        public abstract void UpdateSlotUI();

        protected virtual void EnableSlotUI(bool enable) => itemIconImage.enabled = enable;
    }
}
