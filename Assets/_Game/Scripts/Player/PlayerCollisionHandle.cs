using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class PlayerCollisionHandle : MonoBehaviour
    {
        private PlayerBehaviour parentPlayerBehaviour;

        // Start is called before the first frame update
        void Start()
        {
            parentPlayerBehaviour = transform.parent.GetComponent<PlayerBehaviour>();
        }

        void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Enemy")
            {
                parentPlayerBehaviour.PlayerHit();
            }
        }
    }
}
