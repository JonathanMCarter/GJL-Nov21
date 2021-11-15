using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class PlayerBehaviour : MonoBehaviour
    {
        enum State {ghost, body};
        //as the game changes depending on the player state we might want to store this somewhere else...but this works for now
        [SerializeField]
        private State currentState = State.body; //start the player in their body

        [Header("Player Settings")]
        [SerializeField]
        private float maxDistanceFromAnchor =  5f;
        public float currentDistanceFromAnchor;
        public GameObject playerAnchor; // the players body

        public float maxGhostTimeSeconds = 60f;

        public string interactInput = "Fire1";
        public string changeStateInput = "Jump";

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetButtonDown(changeStateInput))
            {
                if(currentState == State.body)
                {
                    //GOING GHOST!!
                    DropAnchor();
                }
                else
                {
                    //BACK TO NORMAL
                    returnPlayerToBody();
                }
            }

            if(Input.GetButtonDown(interactInput))
            {
                Debug.Log("Interacting TO DO");
            }
        }


        //when the player goes ghost we drop the anchor
        private void DropAnchor()
        {
            //place the anchor prefab where the player is currently
            currentState = State.ghost;
        }

        //return player
        private void returnPlayerToBody()
        {
            //if the player goes too far/runs out of ghost time/asks to go back
            currentState = State.body;

            // destroy the anchor we placed
        }

        // call this from other scripts!!
        public void playerHit()
        {
            if(currentState == State.body)
            {
                //deaded
            }
            else
            {
                //BACK TO NORMAL
                returnPlayerToBody();

                //then deaded
            }
        }
    }
}
