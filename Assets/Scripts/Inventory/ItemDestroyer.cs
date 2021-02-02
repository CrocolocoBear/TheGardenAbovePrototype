using GCUWebGame.Inventory;
using TMPro;
using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    public class ItemDestroyer : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI doYouWishText = null;

        //need to store item's position (not what item it is), to avoid destroying wrong stack of same item
        private int slotIndex = 0;

        //safety measure
        private void OnDisable() => slotIndex = -1;

        public void Activate(ItemSlot itemSlot, int slotIndex)
        {
            this.slotIndex = slotIndex;

            doYouWishText.text = $"Do you wish to destroy {itemSlot.quantity}x {itemSlot.item.ColoredName}?";

            gameObject.SetActive(true);
        }

        //called when Yes button is clicked
        public void Destroy()
        {
            inventory.RemoveAt(slotIndex);

            gameObject.SetActive(false);
        }

    }
}
