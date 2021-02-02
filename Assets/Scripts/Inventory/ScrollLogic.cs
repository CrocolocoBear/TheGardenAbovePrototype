using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GCUWebGame.Inventory
{
    public class ScrollLogic : MonoBehaviour
    {
        GameObject currActive;
        int indexActive = 0;

        void Start()
        {
            SetActive(0);
            indexActive = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backward
            {
                //first (0) slot? go back to last (3)
                indexActive--;
                if (indexActive < 0)
                {
                    indexActive = 3;
                }

                //deactivate previous and activate new one
                DeactivateCurrent();
                SetActive(indexActive);


            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                //fourth (3) slot? go back to first (0)
                indexActive++;
                if (indexActive > 3)
                {
                    indexActive = 0;
                }

                //deactivate previous and activate new one
                DeactivateCurrent();
                SetActive(indexActive);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if(currActive != null)
                {
                    if(currActive.GetComponent<HotBarSlot>().SlotItem != null)
                    {
                        currActive.GetComponent<HotBarSlot>().SlotItem.inventoryItem.Use();
                    }
                }
            }

        }
        public void SetActive(int i)
        {
            currActive = transform.GetChild(i).gameObject;
            transform.GetChild(i).Find("Active").gameObject.SetActive(true);
        }
        private void DeactivateCurrent()
        {
            currActive.transform.Find("Active").gameObject.SetActive(false);
        }
    }

}