using UnityEngine;

namespace GCUWebGame.Utilities
{
    public class ToggleActive : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode = KeyCode.None;
        [SerializeField] private GameObject objectToToggle = null;
        private AudioSource popup;

        private void Start()
        {
            popup = GetComponent<AudioSource>();
        }

        //allows player to toggle UI (inventory)
        private void Update()
        {
            //locks & unlocks cursor
            if (Input.GetKeyDown(keyCode))
            {
                popup.Play();

                if (objectToToggle.activeSelf == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else if (objectToToggle.activeSelf == false)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            }
        }

        public bool getObjectToToggle()
        {
            return objectToToggle.activeSelf;
        }
    }
}
