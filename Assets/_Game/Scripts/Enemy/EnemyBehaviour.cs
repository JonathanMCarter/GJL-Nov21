using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DeadTired
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private Transform target;

        private NavMeshAgent agent;


        public void ActivateEnemy(Transform passedTarget)
        {
            target = passedTarget;
            agent.enabled = true;
        }

        public void DeactivateEnemy()
        {
            agent.enabled = false;
            target = null;
        }


        void Start ()
        {
            agent = GetComponent<NavMeshAgent>();

            //just in case, we dont want them enabled straight off the bat
            if(agent.enabled == true)
                agent.enabled = false;

        }

        void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }

        }
    }
}
