using System;
using System.Collections;
using DeadTired.UI;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Interactables
{
    public class SpiritOrb : MonoBehaviour
    {
        [SerializeField] private BoolReference isGhostForm;
        [SerializeField] private IntReference playerOrbCount;

        private Hashtable spiritOrbIdleTween;
        private ParticleSystem[] particles;
        private Light light;

        private void Start()
        {
            AkSoundEngine.PostEvent("PlayOrbz", gameObject);
        }

        private void Awake()
        {
            spiritOrbIdleTween = new Hashtable
            {
                { "y", transform.localPosition.y + .5f },
                { "time", 2f },
                { "looptype", iTween.LoopType.pingPong },
                { "islocal", true }
            };
            
            iTween.MoveTo(gameObject, spiritOrbIdleTween);
            particles = transform.parent.GetComponentsInChildren<ParticleSystem>();
            light = GetComponentInChildren<Light>();
        }


        private void OnEnable()
        {
            // AkSoundEngine.PostEvent("PlayOrbz", gameObject);
            
            foreach (var p in particles)
                p.Play();

            light.enabled = true;
        }

        private void OnDisable()
        {
            AkSoundEngine.PostEvent("OrbPickup", gameObject);
            
            foreach (var p in particles)
                p.Stop();
            
            light.enabled = false;
        }

        
        private void OnTriggerEnter(Collider other)
        {
            if (!isGhostForm.Value) return;
            if (!other.CompareTag("Player")) return;
            playerOrbCount.variable.IncrementValue(1);
            PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
            gameObject.SetActive(false);
        }
    }
}