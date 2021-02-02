using System;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    [Serializable]
    public struct ItemSlot
    {
        public InventoryItem item;
        public int quantity;

        public ItemSlot(InventoryItem item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }
        
        //allows struct to be compared!!
        public static bool operator == (ItemSlot a, ItemSlot b)
        {
            return a.Equals(b);
        }
        public static bool operator != (ItemSlot a, ItemSlot b)
        {
            return !a.Equals(b);
        }
    }
}

