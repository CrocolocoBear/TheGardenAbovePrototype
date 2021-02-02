using GCUWebGame.Events;
using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]

    //player's inventory
    public class Inventory : ScriptableObject, IItemContainer
    {
        [SerializeField] private VoidEvent onInventoryItemsUpdated = null;

        private ItemSlot[] itemSlots = new ItemSlot[0];

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];


        public void SetSize(int size)
        {
            itemSlots = new ItemSlot[size];
        }

        /*Loops to check if item being added has existing slot, loops again for those without,
          and also adds items to new slots when maxStack has been reached*/
        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            //loop for item with existing slot
            for (int i = 0; i < itemSlots.Length; i++)
            {
                //if there's an item in the slot
                if (itemSlots[i].item != null)
                {
                    //if slot's item matches desired item
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        //calculates the remaining space in slot from maxStack & quantity in question
                        int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                        /*if quantity is LESS then remaining space, add to slot & clear quantity in question to end loop...
                          (If adding 10, but maxStack is 5, then 5 are left and new slot is needed.)*/
                        if (itemSlot.quantity <= slotRemainingSpace)
                        {
                            itemSlots[i].quantity += itemSlot.quantity;

                            itemSlot.quantity = 0;

                            onInventoryItemsUpdated.Raise();

                            return itemSlot;
                        }
                        //when some can be added but not all, add items to slot and remove from quantity
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].quantity += slotRemainingSpace;

                            itemSlot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }

            //loop for when item does NOT have an existing slot
            for (int i = 0; i < itemSlots.Length; i++)
            {
                //looking for empty slot
                if (itemSlots[i].item == null)
                {
                    //if item's quantity is LESS/EQUAL to maxStack, use slot found and set quantity to 0 (because all was added)
                    if (itemSlot.quantity <= itemSlot.item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;

                        itemSlot.quantity = 0;

                        onInventoryItemsUpdated.Raise();

                        return itemSlot;
                    }
                    /*when trying to add more than maxStack (adding 15 items when maxStack is 5, need 3 slots)
                      Fills slots using maxStack amount and removes that amount from total quantity until 0.*/
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);

                        itemSlot.quantity -= itemSlot.item.MaxStack;
                    }
                }
            }

            onInventoryItemsUpdated.Raise();

            return itemSlot;
        }



        /*loops to check if slot has too little or too many compared to amount
          player is trying to remove*/
        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                //if there's an item in slot
                if (itemSlots[i].item != null)
                {
                    //if slot's item is desired item
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        //if slot's amount is LESS than amount to remove... remove & clear 
                        if (itemSlots[i].quantity < itemSlot.quantity)
                        {
                            itemSlot.quantity -= itemSlots[i].quantity;

                            itemSlots[i] = new ItemSlot();
                        }
                        //if slot's amount is GREATER than amount to remove (5/10)
                        else
                        {
                            itemSlots[i].quantity -= itemSlot.quantity;

                            /*if slot's now empty, clear...
                              (placed here bc if there are two stacks of 5, but trying to remove 10,
                              will loop until desired amount is removed*/
                            if (itemSlots[i].quantity == 0)
                            {
                                itemSlots[i] = new ItemSlot();

                                onInventoryItemsUpdated.Raise();

                                return;
                            }
                        }
                    }
                }
            }
        }



        //ensures slot index is valid, set slot to new (empty) item slot; used to destroy item
        public void RemoveAt(int slotIndex)
        {
            //safety check
            if (slotIndex < 0 || slotIndex > itemSlots.Length - 1)
            { return; }

            //clear
            itemSlots[slotIndex] = new ItemSlot();

            onInventoryItemsUpdated.Raise();
        }



        //switching items between slots AND combining items
        public void Swap(int indexOne, int indexTwo)
        {
            //storing; specific item slot = position in array
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            /*if item is picked & released back on itself, nothing should happen..
              (struct itemSlot allows for this comparison.)*/
            if (firstSlot == secondSlot)
            { return; }

            //when 2nd slot is not empty...
            if (secondSlot.item != null)
            {
                //when same item, combine
                if (firstSlot.item == secondSlot.item)
                {
                    //calculates 2nd slot's remaining space based on maxStack and quantity in question
                    int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                    //knowing how much space is left in 2nd slot, put all into the 2nd slot
                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].quantity += firstSlot.quantity;
                        //secondSlot.quantity += firstSlot.quantity;

                        //empty struct to first slot (clear slot)
                        itemSlots[indexOne] = new ItemSlot();

                        onInventoryItemsUpdated.Raise();

                        return;
                    }
                }
            }

            //when different item
            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            onInventoryItemsUpdated.Raise();
        }



        //checks item slots until searched item is found (or not)
        public bool HasItem(InventoryItem item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == null)
                { continue; }
                if (itemSlot.item != item)
                { continue; }

                return true;
            }

            return false;
        }



        //counts quantity of item
        public int GetTotalQuantity(InventoryItem item)
        {
            int totalCount = 0;

            //go through all item slots
            foreach (ItemSlot itemSlot in itemSlots)
            {
                /*goes to next iteration of loop; don't add on if slot is empty,
                  OR if item is not item being searched for.*/
                if (itemSlot.item == null)
                { continue; }
                if (itemSlot.item != item)
                { continue; }

                totalCount += itemSlot.quantity;
            }

            return totalCount;
        }
    }
}