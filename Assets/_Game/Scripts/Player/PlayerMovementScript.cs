using System.Collections;
using System.Collections.Generic;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired
{
    public class PlayerMovementScript : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 4f;

        public Transform playerObject;
        public float nextDistance;
        private PlayerBehaviour playerBehaviour;
        
        Vector3 forward, right, nextPosition; // differes from the world axis so we need to specifiy ourself


        [SerializeField] private Vector3Reference facing;
        
        
        private void Start()
        {
            forward = Camera.main.transform.forward;
            forward.y = 0f;

            forward = Vector3.Normalize(forward);
            right = Quaternion.Euler(new Vector3(0,90,0)) * forward;

            nextPosition = playerObject.position;

            playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
        }

        
        
        // Update is called once per frame
        void Update()
        {
            if (Input.GetAxisRaw("Horizontal") > .1f || Input.GetAxisRaw("Vertical") > .1f || Input.GetAxisRaw("Horizontal") < -.1f|| Input.GetAxisRaw("Vertical") < -.1f)
                Move();
        }
        
        

        //maybe move this somewhere else for cleanliness???
        void Move()
        {                            
            nextPosition = playerObject.position;

            facing.SetValue(new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")));
            Vector3 rightMovement = right * movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            // handles the rotations of the player object
            playerObject.forward = Vector3.Lerp(playerObject.forward, heading, 0.2f);;

            if(playerBehaviour.currentState == PlayerState.Ghost)
            {
                //want to check where the player is heading to in the next frame
                nextPosition += rightMovement;
                nextPosition += upMovement;

                nextDistance = Vector3.Distance(playerBehaviour.PlayerAnchorPosition, playerObject.position);

                //if the next position ownt take the player otuside the max distance from the anchor then let them go
                if(nextDistance + .2f < playerBehaviour.MaxDistanceFromAnchor)
                {
                    playerObject.position = nextPosition;
                }
                else
                {
                    //if somehow the player has managed to go too far we need to pull them back
                    playerObject.position = Vector3.MoveTowards(playerObject.position, playerBehaviour.PlayerAnchorPosition, 1f * Time.deltaTime);
                }

            
            }
            else if (playerBehaviour.currentState == PlayerState.Body)
            {
                playerObject.position += rightMovement;
                playerObject.position += upMovement;
            }

        }

    }
}
