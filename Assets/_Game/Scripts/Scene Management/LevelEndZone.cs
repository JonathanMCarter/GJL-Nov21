using System;
using UnityEngine;

namespace DeadTired.Scenes
{
    public class LevelEndZone : MonoBehaviour
    {
        private SceneGroupChangeWithFade sceneGroupChangeWithFade;


        private void Awake()
        {
            sceneGroupChangeWithFade = GetComponent<SceneGroupChangeWithFade>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            sceneGroupChangeWithFade.LoadSceneGroup();
        }
    }
}