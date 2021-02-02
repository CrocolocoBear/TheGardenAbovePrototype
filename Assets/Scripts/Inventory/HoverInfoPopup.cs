using GCUWebGame.Inventory;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Inventory
{
    public class HoverInfoPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvasObject = null;
        [SerializeField] private RectTransform popupObject = null;
        [SerializeField] private TextMeshProUGUI infoText = null;
        [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);
        [SerializeField] private float padding = 25f;

        private Canvas popupCanvas = null;

        private void Start() => popupCanvas = popupCanvasObject.GetComponent<Canvas>();

        private void Update() => FollowCursor();

        public void HideInfo() => popupCanvasObject.SetActive(false);

        private void FollowCursor()
        {
            //ensures the popup canvas is active
            if (!popupCanvasObject.activeSelf) { return; }

            //calculates the desired pos
            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;

            //padding
            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }
            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }
            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }
            popupObject.transform.position = newPos;
        }

        public void DisplayInfo(HotBarItem infoItem)
        {
            //create stringbuilder instance
            StringBuilder builder = new StringBuilder();

            //get item's custom display txt
            builder.Append("<size=35>").Append(infoItem.ColoredName).Append("</size>\n");
            builder.Append(infoItem.GetInfoDisplayText());

            //sets info text for display
            infoText.text = builder.ToString();

            //activates UI canvas
            popupCanvasObject.SetActive(true);

            //fix resize
            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }
    }
}