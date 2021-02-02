
//stores all the functions anything containing items needs

namespace GCUWebGame.Inventory
{
    public interface IItemContainer
    {
        ItemSlot AddItem(ItemSlot itemSlot);
        void RemoveItem(ItemSlot itemSlot);
        
        //removes item at index in list..? (need to look into further)
        void RemoveAt(int slotIndex);
        //swaps item between two slots
        void Swap(int indexOne, int indexTwo);
        //checks if player has a certain item in inventory
        bool HasItem(InventoryItem item);
        //checks how many of a certain item player has
        int GetTotalQuantity(InventoryItem item);
    }
}
