using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingBox : MonoBehaviour
{
    //uses a serialized saving box
    [SerializeField] private BoxCollider savingBox = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == savingBox)
        {
            //Teleports player to ship position if they fall into the collision box.
            this.GetComponent<Transform>().position = new Vector3(14.974f, 207.787f, 69.71f);
        }
    }
}
