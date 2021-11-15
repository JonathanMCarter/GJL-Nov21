using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class FollowPlayer : MonoBehaviour
    {

        public GameObject player;
        public Vector3 cameraOffset = new Vector3(6.7f,12f,15f);

        // Update is called once per frame
        void Update()
        {
            transform.position = player.transform.position + cameraOffset;
        }
    }
}
