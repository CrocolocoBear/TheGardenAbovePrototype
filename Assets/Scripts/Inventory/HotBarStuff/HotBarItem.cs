using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */
namespace GCUWebGame.Inventory
{
    public abstract class HotBarItem : ScriptableObject
    {
        [Header("Basic Info")]
        [SerializeField] private new string name = "New Item Name";
        [SerializeField] private Sprite icon = null;
        public InventoryItem inventoryItem;

        //external class can only get name, not change it
        public string Name => name;
        public abstract string ColoredName 
        { get; }

        public Sprite Icon => icon;

        //info displayed for each item
        public abstract string GetInfoDisplayText();
    }
}
