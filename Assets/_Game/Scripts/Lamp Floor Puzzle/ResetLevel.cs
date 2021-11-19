using System;
using DeadTired.Death;
using DeadTired.Scenes;
using MultiScene.Core;
using MultiScene.Extensions.DoNotDestroy;
using UnityEngine;

namespace DeadTired
{
    public class ResetLevel : MonoBehaviour
    {
        private SceneGroupChangeWithFade sceneGroupChangeWithFade;


        public void Awake()
        {
            sceneGroupChangeWithFade = GetComponent<SceneGroupChangeWithFade>();
        }

        private void OnTriggerEnter(Collider other)
        {
            FadeTransition.OnTransitionFadeToBlack += Fade;
            DoNotDestroyAccessor.GetComponentInDoNotDestroy<FadeTransition>().FadeToBlack();
        }


        private void Fade()
        {
            FadeTransition.OnTransitionFadeToBlack -= Fade;
            sceneGroupChangeWithFade.LoadSceneGroup();
        }
    }
}