using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */
namespace GCUWebGame.Inventory
{
    public class HotBar : MonoBehaviour
    {
       [SerializeField] private HotBarSlot[] hotBarSlots { get; } = new HotBarSlot[10];

        public void Add(HotBarItem itemToAdd)
        {
            //checks slots to see if item can be held
            foreach (HotBarSlot hotBarSlot in hotBarSlots)
            {
                //stop checking
                if (hotBarSlot.AddItem(itemToAdd))
                {
                    return;
                }
            }
        }

        //necessary?
        public int Length()
        {
            return hotBarSlots.Length;
        }

        public HotBarSlot returnById(int id)
        {
            return hotBarSlots[id];
        }
    }
}