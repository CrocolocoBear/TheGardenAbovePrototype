using UnityEngine;

namespace GCUWebGame.Inventory
{
    public abstract class InventoryItem : HotBarItem
    {
        [Header("Item Data")]
        [SerializeField] [Min(1)] private int maxStack = 1;

        //not finished
        public override string ColoredName => Name;
        public int MaxStack => maxStack;

        public abstract void Use();

    }
}
