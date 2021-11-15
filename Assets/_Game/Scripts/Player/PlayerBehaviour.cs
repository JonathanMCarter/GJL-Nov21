using System.Collections;
using System.Collections.Generic;
using DeadTired.Interactables;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired
{
    public class PlayerBehaviour : MonoBehaviour, IMultiSceneAwake
    {
        public enum State {ghost, body, isReturning};
        
        //as the game changes depending on the player state we might want to store this somewhere else...but this works for now
        public State currentState = State.body; //start the player in their body

        [Header("Player Settings")]
        [SerializeField]
        private GameObject playerAnchor; // the players body
        public float maxDistanceFromAnchor =  5f;
        private float minDistanceFromAnchor = 0.2f;

        // Movement speed in units per second.
        public float returnSpeed = .5F;

        public float currentDistanceFromAnchor;
        public GameObject playerObject; // want to make this automatically grab the playerobject


        public float maxGhostTimeSeconds = 60f;

        public string interactInput = "Fire1";
        public string changeStateInput = "Jump";

        public string playerBodyLayer = "PlayerBody";
        public string playerGhostLayer = "PlayerGhost";

        // Time when the movement back to body started.
        private float startTime;
        private float journeyLength;
        public GameObject anchor;


        private InteractionsManager cachedInteractionsManager;

        // Gets called when all the scenes for each level are loaded...
        public void OnMultiSceneAwake()
        {
            // Gets the interaction manager no matter which scene it is in...
            cachedInteractionsManager = SceneElly.GetComponentFromAllScenes<InteractionsManager>();
        }


        // Start is called before the first frame update
        void Start()
        {
            //have it set in the physics settings so items on the player ghost layer cant interact with the anchor
            playerObject.layer = LayerMask.NameToLayer(playerBodyLayer);
        }

        // Update is called once per frame
        void Update()
        {

            if(currentState == State.isReturning)
            {
                movePlayer();
            }
            else
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
                    // Calls the interaction manager and tries to make an interaction if possible...
                    cachedInteractionsManager.TryInteract();
                }

                //if a ghost keep checkign the distance
                if(currentState == State.ghost)
                {
                    currentDistanceFromAnchor = Vector3.Distance(anchor.transform.position, playerObject.transform.position);
                } 
            }
        }


        //when the player goes ghost we drop the anchor
        private void DropAnchor()
        {
            //place the anchor prefab where the player is currently
            currentState = State.ghost;
            playerObject.layer = LayerMask.NameToLayer(playerGhostLayer);

            anchor = Instantiate(playerAnchor, playerObject.transform.position, playerObject.transform.rotation); //pooling this somewhere instead of instantiating might be better??

            //want to bump the player forward slightly???
        }

        //return player
        private void returnPlayerToBody()
        {
            startTime = Time.time;

            journeyLength = currentDistanceFromAnchor;

            //move the player back to position of the body   
            currentState = State.isReturning;         
        }

        private void movePlayer()
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * returnSpeed;

            float fractionOfJourney = distCovered / journeyLength;

            playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, anchor.transform.position, fractionOfJourney);
            
            currentDistanceFromAnchor = Vector3.Distance(anchor.transform.position, playerObject.transform.position);

            if(currentDistanceFromAnchor <= minDistanceFromAnchor)
            {
                // destroy the anchor we placed
                Destroy(anchor);

                currentState = State.body;

                playerObject.layer = LayerMask.NameToLayer(playerBodyLayer);

            }
        
        
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
