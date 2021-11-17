using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class EnemyParentBehaviour : MonoBehaviour
    {

        public GameObject[] enemyObjects;
        [Header("Setup")]
        public GameObject enemyPrefab;
        public GameObject enemyPool;
        public Transform playerObject;

        [Header("Settings")]
        
        public int currentLevel = 1;
        public int enemyCount;
        public Vector3 HidingSpot;
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
            Debug.Log("Diabling Enemies");
            foreach (var enemyObject in enemyObjects)
            {
                //disable thier nav agents


                //move them to the hiding spot
                enemyObject.transform.position = HidingSpot;
            }
        }

        public void EnableEnemies()
        {
            Debug.Log("Enabling Enemies");
            foreach (var enemyObject in enemyObjects)
            {
                //make a new spot for the enemies
                //enemyObject.transform.position = HidingSpot; // TO DO

                enemyObject.GetComponent<EnemyBehaviour>().ActivateEnemy(playerObject);

            }
        }



        // Start is called before the first frame update
        void Start()
        {
            EnableEnemies();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
