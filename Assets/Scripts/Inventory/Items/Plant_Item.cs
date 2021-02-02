using System.Text;
using UnityEngine;

/*NOTE:
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    [CreateAssetMenu(fileName = "New Seed Item", menuName = "Items/Seeds")]

    public class Plant_Item : InventoryItem
    {
        [Header("Seed")]
        [SerializeField] private string useText = "Grows something?";

        [SerializeField] public GameObject Flower;

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

           //builder.Append(Name).AppendLine();
            builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

            return builder.ToString();
        }

        public override void Use()
        {
            Debug.Log("using");
            Plant();
        }

        void Plant()
        {
            Debug.Log("noRaycastYet");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("RaycastHit" );
                Debug.DrawLine(ray.origin, hit.point);
                Transform objectHit = hit.transform;
                Debug.Log(objectHit.tag);
                if (objectHit.tag == "Terrain")
                {
                    Debug.Log("planting");
                    GameObject temp = GameObject.Instantiate(Flower);
                    temp.transform.position = hit.point;
                    //randomize z axis
                    Vector3 euler = temp.transform.eulerAngles;
                    euler.y = Random.Range(0.0f, 360.0f);
                    temp.transform.eulerAngles = euler;
                }
                if(objectHit.tag == "Pot")
                {
                    Debug.Log("Pot found, planting in there");
                    Pot tempPot = objectHit.gameObject.GetComponent<Pot>();
                    GameObject temp = GameObject.Instantiate(Flower);
                    temp.transform.position = tempPot.spwanPoint.position;
                    tempPot.plant = temp;

                    //randomize z axis
                    Vector3 euler = temp.transform.eulerAngles;
                    euler.y = Random.Range(0.0f, 360.0f);
                    temp.transform.eulerAngles = euler;

                }
            }
        }
    }
}
