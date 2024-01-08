using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class GrenadeThrower : MonoBehaviour
    {
        public float throwForce = 40f;
        public int avaliableGrenades = 2;

        public GameObject grenade;

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                ThrowGrenade();
            }
        }

        void ThrowGrenade()
        {
            GameObject newGrenade =  Instantiate(grenade, transform.position, transform.rotation);
            Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwForce);
        }
    }
}
