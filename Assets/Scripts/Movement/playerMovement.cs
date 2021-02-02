using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's and Infallible Code's tutorials :)
 */

namespace GCUWebGame.Player
{
    public class playerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        private Rigidbody rb;
        private AudioSource audioSource;


        //gets player rigidbody
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        //utilizes move
        private void FixedUpdate()
        {
            Move();

            //raycast to check if player is functionally grounded
            bool grounded = Physics.Raycast(transform.position, -Vector3.up, (gameObject.GetComponent<CapsuleCollider>().height) / 2 + 0.2f);
            //apply upwards force to rigidbody if grounded
            if(grounded && Input.GetKeyDown(KeyCode.Space)) {
                rb.AddForce(0, jumpForce, 0);
            }
        }

        //player movement
        private void Move()
        {
            //gets input using input manager built within Unity
            float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            //calculates how far player wants to move in certain direction
            Vector3 movement = new Vector3(hAxis, 0, vAxis) * speed * Time.deltaTime;

            //calculate newPos, move where rb is facing
            Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

            //move there
            rb.MovePosition(newPosition);

            //audio on/off 
            if (movement != new Vector3(0.0f, 0.0f, 0.0f))
            {
                if (!audioSource.isPlaying) audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}