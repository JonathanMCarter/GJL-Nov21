using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class PlayerMovementScript : MonoBehaviour
    {

        [SerializeField] 
        float movementSpeed = 4f;

        public Transform playerBody;
        public Transform playerGhost;

        Vector3 forward, right; // differes from the world axis so we need to specifiy ourself

        void Start()
        {
            forward = Camera.main.transform.forward;
            forward.y = 0f;

            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0,90,0)) * forward;
        }

        // Update is called once per frame
        void Update()
        {
            
            if(Input.anyKey)
                Move();

        }

        void Move()
        {

            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
            Vector3 rightMovement = right * movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            // handles the rotations of the player object
            playerBody.forward = Vector3.Lerp(playerBody.forward, heading, 0.2f);;

            // handles the position of the player object
            playerBody.position += rightMovement;
            playerBody.position += upMovement;
        }

    }
}
