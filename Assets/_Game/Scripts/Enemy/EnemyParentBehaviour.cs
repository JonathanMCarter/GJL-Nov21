using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MultiScene.Core;

namespace DeadTired
{
    public class EnemyParentBehaviour : MonoBehaviour
    {

        public List<GameObject> enemyObjects = new List<GameObject>();

        private EnemyBehaviour grabbedBehaviour;

        [Header("Setup")]
        public GameObject enemyPrefab;
        public GameObject enemyPool;
        public Transform playerObject;

        public int maxPositionX, minPositionX, maxPositionZ, minPositionZ;

        [Header("Settings")]
        
        public int enemyCount;
        public Transform HidingSpot;
        //hide the enemies here once they're disabled instead of destroying them

        public void SpawnEnemies()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Debug.Log("MAKE AN ENEMY");
            }
        }

        public void DisableEnemies()
        {
            //when the user goes back into their body we need to diable the enemies
            foreach (var enemyObject in enemyObjects)
            {
                AkSoundEngine.PostEvent("Stop_Ghost", gameObject);
                //disable thier nav agents
                grabbedBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
                grabbedBehaviour.DeactivateEnemy();

                //move them to the hiding spot
                enemyObject.transform.position = HidingSpot.position;
            }
        }

        public void EnableEnemies()
        {
            
            foreach (var enemyObject in enemyObjects)
            {
                
                //make a new spot for the enemies
                enemyObject.transform.position = GenerateEnemyPosition();
                grabbedBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
                grabbedBehaviour.ActivateEnemy(playerObject);
                AkSoundEngine.PostEvent("Play_Ghost", enemyObject);
            }
        }

        private Vector3 GenerateEnemyPosition()
        {
            int positoinY = 0;
            int positionX = Random.Range(minPositionX, maxPositionX);
            int positionZ = Random.Range(minPositionZ, maxPositionZ);

            NavMeshHit hit;
            NavMesh.SamplePosition(new Vector3(positionX, positoinY, positionZ), out hit, Mathf.Infinity, NavMesh.AllAreas);
            return hit.position;
        }


        // Start is called before the first frame update
        void Start()
        {
            int count = enemyPool.transform.childCount;
            if(count < enemyCount)
            {
                for (int i = count; i < enemyCount; i++)
                {
                    GameObject newEnemy = Instantiate(enemyPrefab, HidingSpot.position, enemyPool.transform.rotation, enemyPool.transform);

                    enemyObjects.Add(newEnemy);

                    count++;
                }
                
            }

            //EnableEnemies();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
