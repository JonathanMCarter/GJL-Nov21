using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DeadTired
{
    public class EnemyBehaviour : MonoBehaviour
    {
        public Transform target;

        private NavMeshAgent agent;

        public void ActivateEnemy(Transform passedTarget)
        {
            target = passedTarget;

            if(agent == null)
            {
                //sometimes the parent scripts work faster than this one can set itself up
                agent = GetComponent<NavMeshAgent>();
            }

            agent.enabled = true;

            Debug.Log("Activating Enemy");
        }

        public void DeactivateEnemy()
        {
            agent.enabled = false;
            target = null;
        }


        void Start ()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (target != null && agent.enabled == true)
            {
                agent.SetDestination(target.position);
            }

        }
    }
}
