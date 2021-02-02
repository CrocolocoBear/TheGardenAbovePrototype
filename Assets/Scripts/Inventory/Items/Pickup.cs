using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GCUWebGame.Inventory
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private ItemSlot itemSlot = new ItemSlot();
        private AudioSource pickupSound;

        public void Start()
        {
            pickupSound = GameObject.Find("Pickup_AudioSource").GetComponent<AudioSource>();
        }

        public void OnMouseUp()
        {
            var itemContainer = GameObject.Find("Player").GetComponent<IItemContainer>();

            if (itemContainer == null) { return; }

            pickupSound.Play();
            itemContainer.AddItem(itemSlot);

            Destroy(gameObject);
            

        }
    }
}
