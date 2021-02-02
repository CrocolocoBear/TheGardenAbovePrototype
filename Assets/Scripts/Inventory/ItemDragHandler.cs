using GCUWebGame.Events;
using UnityEngine;
using UnityEngine.EventSystems;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    [RequireComponent(typeof(CanvasGroup))]

    //handles mousing over, clicking, dragging, & releasing
    public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI = null;
        [SerializeField] protected HotBarItemEvent onMouseStartHoverItem = null;
        [SerializeField] protected VoidEvent onMouseEndHoverItem = null;

        private CanvasGroup canvasGroup = null;
        private Transform originalParent = null;
        private bool isHovering = false;

        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private void Start() => canvasGroup = GetComponent<CanvasGroup>();

        private void OnDisable()
        {
            if (isHovering)
            {
                onMouseEndHoverItem.Raise();
                isHovering = false;
            }
        }

        //when grabbing item
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            //when dragging item over another slot, ignore item being dragged & look @ item below it
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onMouseEndHoverItem.Raise();

                originalParent = transform.parent;
                transform.SetParent(transform.parent.parent);
                canvasGroup.blocksRaycasts = false;
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            //when dragging to slot, set pos wherever cursor is
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }

        //when release item
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(originalParent);
                transform.localPosition = Vector3.zero;
                canvasGroup.blocksRaycasts = true;
            }
        }

        //
        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseStartHoverItem.Raise(ItemSlotUI.SlotItem);
            isHovering = true;
        }

        //
        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseEndHoverItem.Raise();

            isHovering = false;
        }
    }
}
