using System;
using System.Collections;
using DeadTired.Interactables;
using DependencyLibrary;
using JTools;
using MultiScene.Core;
using UnityEngine;
using SceneElly = MultiScene.Core.SceneElly;

namespace DeadTired
{
    public class PlayerBehaviour : MonoBehaviour, IMultiSceneAwake
    {
        // Cached user input strings
        private readonly string PlayerInteractInput = "Fire1";
        private readonly string PlayerStateSwitchInput = "Jump";
        
        // Cached user layer masks
        private int PlayerBodyLayer;
        private int PlayerGhostLayer;
        
        // Script references
        private GlobalVolumeManager volumeManager;
        private SwitchParticleBehaviour switchParticle;
        private SpiritLineBehaviour spiritLineBehaviour;
        private EnemyParentBehaviour enemyParentBehaviour;
        private InteractionsManager interactionsManager;

        [SerializeField] private BoolReference isPlayerGhost;
        [SerializeField] private MinMax distanceFromAnchor;
        [SerializeField] private FloatReference maxGhostTime;
        [SerializeField] private FloatReference returnSpeedAdjustment;
        
        // The state the player is in
        public PlayerState currentState = PlayerState.Body; //start the player in their body
        
        // Movement speed in units per second.
        public GameObject playerObject; // want to make this automatically grab the playerobject
        
        [Header("Values to Watch")]
        [SerializeField] private float currentDistanceFromAnchor;
        [SerializeField] private FloatReference timeTillReturn; //The player has a limited time as a ghost, this is that remaining time
        private Coroutine returnAfterTimerCo;
        
        // Time when the movement back to body started.
        private float startTime;
        private float journeyLength;


        [SerializeField] private GameObject anchorPrefab;
        private GameObject cachedAnchor;
        private Vector3 cachedAnchorPosition;
        private bool hasCachedTransform;


        public static Action<bool> OnPlayerStateChanged;
        public static Action OnPlayerKilled;


        public float MaxDistanceFromAnchor => distanceFromAnchor.max;
        public Vector3 PlayerAnchorPosition
        {
            get
            {
                if (hasCachedTransform)
                    return cachedAnchorPosition;

                cachedAnchorPosition = cachedAnchor.transform.position;
                return cachedAnchorPosition;
            }
        }


        private void Awake()
        {
            PlayerBodyLayer = LayerMask.NameToLayer("PlayerBody");
            PlayerGhostLayer = LayerMask.NameToLayer("PlayerGhost");
            
            //have it set in the physics settings so items on the player ghost layer cant interact with the anchor
            playerObject.layer = PlayerBodyLayer;
        }


        // Gets called when all the scenes for each level are loaded...
        public void OnMultiSceneAwake()
        {
            // Gets the interaction manager no matter which scene it is in...
            interactionsManager = SceneElly.GetComponentFromAllScenes<InteractionsManager>();
            enemyParentBehaviour = SceneElly.GetComponentFromAllScenes<EnemyParentBehaviour>();
            spiritLineBehaviour = SceneElly.GetComponentFromAllScenes<SpiritLineBehaviour>();
            switchParticle = SceneElly.GetComponentFromAllScenes<SwitchParticleBehaviour>();
            volumeManager = SceneElly.GetComponentFromAllScenes<GlobalVolumeManager>();

            enemyParentBehaviour.playerObject = playerObject.transform;
            volumeManager.setBodyVolume();
        }


        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown(PlayerStateSwitchInput))
            {
                if (currentState.Equals(PlayerState.Body))
                    DropAnchor();
                else
                    ReturnPlayerToBody();
                
                isPlayerGhost.SetValue(currentState.Equals(PlayerState.Ghost));
                OnPlayerStateChanged?.Invoke(isPlayerGhost.Value);
            }
            
            if (Input.GetButtonDown(PlayerInteractInput))
            {
                // Calls the interaction manager and tries to make an interaction if possible...
                interactionsManager.TryInteract();
            }

            if (!currentState.Equals(PlayerState.Returning)) return;
            MovePlayer();
        }


        private void FixedUpdate()
        {
            //if a ghost keep checking the distance
            if (currentState != PlayerState.Ghost) return;
            currentDistanceFromAnchor = Vector3.Distance(anchorPrefab.transform.position, playerObject.transform.position);
        }


        //when the player goes ghost we drop the anchor
        private void DropAnchor()
        {
            AkSoundEngine.PostEvent("Normal_breath", gameObject);

            //place the anchor prefab where the player is currently
            currentState = PlayerState.Ghost;
            playerObject.layer = PlayerGhostLayer;
            
            // Pooled as we only really need 1 ever....
            if (cachedAnchor == null)
            {
                cachedAnchor = Instantiate(anchorPrefab, playerObject.transform.position, playerObject.transform.rotation);
                hasCachedTransform = false;
            }
            else
            {
                cachedAnchor.transform.SetPositionAndRotation(playerObject.transform.position, playerObject.transform.rotation);
                cachedAnchor.SetActive(true);
                hasCachedTransform = false;
            }
            
            // some fancy particles so it looks nice
            switchParticle.emitParticle(PlayerAnchorPosition);
            spiritLineBehaviour.activateSpiritLine(playerObject.transform, cachedAnchor.transform);
            
            //changes the camera to look ghostie
            volumeManager.setGhostvolume();

            //plop the enemies about the place
            enemyParentBehaviour.EnableEnemies();
            
            // start countdown to return
            if (returnAfterTimerCo != null)
                StopCoroutine(returnAfterTimerCo);

            returnAfterTimerCo = StartCoroutine(PlayerReturnCountDownCo());
        }

        
        //return player
        private void ReturnPlayerToBody()
        {
            AkSoundEngine.PostEvent("Backto_body", gameObject);
            
            // Stops the return countdown
            if (returnAfterTimerCo != null)
                StopCoroutine(returnAfterTimerCo);

            startTime = Time.time;
            journeyLength = currentDistanceFromAnchor;
            
            //move the player back to position of the body   
            currentState = PlayerState.Returning;
            switchParticle.emitParticle(PlayerAnchorPosition);

            //hide and deactivate the enemies about the place
            enemyParentBehaviour.DisableEnemies();
        }

        
        private void MovePlayer()
        {
            // Distance moved equals elapsed time times speed..
            var _distCovered = (Time.time - startTime) * 5;
            var _fractionOfJourney = _distCovered / journeyLength;
            var _playerPos = playerObject.transform.position;
            
            playerObject.transform.position = Vector3.Lerp(_playerPos, PlayerAnchorPosition, (_fractionOfJourney * returnSpeedAdjustment) * Time.deltaTime);
            currentDistanceFromAnchor = Vector3.Distance(PlayerAnchorPosition, _playerPos);

            if (currentDistanceFromAnchor > distanceFromAnchor.min) return;
            
            volumeManager.setBodyVolume();
            spiritLineBehaviour.deactiveSpiritLine();

            // destroy the anchor we placed
            cachedAnchor.SetActive(false);

            currentState = PlayerState.Body;
            playerObject.layer = PlayerBodyLayer;
        }
        

        
        // call this from other scripts!!
        public void PlayerHit()
        {
            if (currentState == PlayerState.Body)
            {
                //deaded
                OnPlayerKilled?.Invoke();
            }
            else
            {
                //BACK TO NORMAL
                ReturnPlayerToBody();
            
                //then deaded
                OnPlayerKilled?.Invoke();
            }
        }


        private IEnumerator PlayerReturnCountDownCo()
        {
            timeTillReturn.SetValue(maxGhostTime);

            while (timeTillReturn.Value > 0)
            {
                timeTillReturn.variable.IncrementValue(-Time.deltaTime);
                yield return null;
            }
            
            ReturnPlayerToBody();
        }
    }
}
