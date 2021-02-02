using System.Text;
using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    [CreateAssetMenu(fileName = "New Tool", menuName = "Items/Tools")]

    public class Tool_Item : InventoryItem
    {
        [Header("Tool")]
        [SerializeField] private string useText = "Does something?";

        //Functionality here?
        /*[SerializeField] private GameObject toolPrefab = null;
          public GameObject ToolPrefab => toolPrefab; */

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<color=orange>Use: ").Append(useText).Append("</color>").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

            return builder.ToString();
        }

        public override void Use()
        {
            //TODO
        }
    }
}
