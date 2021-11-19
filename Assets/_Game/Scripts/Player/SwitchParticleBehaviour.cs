using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadTired
{
    public class SwitchParticleBehaviour : MonoBehaviour
    {

        private ParticleSystem particleSystem;


        // Start is called before the first frame update
        void Start()
        {
            particleSystem = gameObject.GetComponent<ParticleSystem>();
        }

        public void emitParticle(Vector3 position)
        {
            gameObject.transform.position = position;
            particleSystem.Play();
        }
    }
}
