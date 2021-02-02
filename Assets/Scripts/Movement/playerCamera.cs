using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

/*NOTE: 
 *This code is based on Dapper Dino's and Infallible Code's tutorials :)
 */

namespace GCUWebGame.Player
{
    public class playerCamera : MonoBehaviour
    {
        [SerializeField] private float sensitivity;
        [SerializeField] private float smoothing;

        private GameObject player;
        private Vector2 smoothedVelocity;
        private Vector2 currentLookingPosition;

        private void Start()
        {
            player = transform.parent.gameObject;

            //locks cursor to center of screen, ESC to unlock (will need to disable to use inventory!)
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //allow camera to move if inventory canvas is not active
        private void Update()
        {
            if (GameObject.Find("Inventory Canvas") == null && GameObject.Find("PauseMenu") == null && GameObject.Find("AudioMenu") ==null)
            {
                RotateCamera();
            }
        }

        private void RotateCamera()
        {
            //storing input
            Vector2 inputValues = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            //scales input
            inputValues = Vector2.Scale(inputValues, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            //smoothing, lerp prevents jolting by only going part of the way to value, "linear interpolation": going btwn two values smoothly
            smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
            smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

            currentLookingPosition += smoothedVelocity;

            //above code calculates looking, below does the looking
            transform.localRotation = Quaternion.AngleAxis(-currentLookingPosition.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(currentLookingPosition.x, player.transform.up);

        }
    }
}