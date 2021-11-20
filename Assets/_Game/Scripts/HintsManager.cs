using JTools;
using MultiScene.Core;
using UnityEngine;
using SceneElly = MultiScene.Core.SceneElly;

namespace DeadTired
{
    public class HintsManager : MonoBehaviour, IMultiSceneAwake
    {
        private PlayerBehaviour player;

        
        public void OnMultiSceneAwake()
        {
            player = SceneElly.GetComponentFromScene<PlayerBehaviour>("Player");
        }


        private void Update()
        {
            if (!FrameLimiter.LimitEveryXFrames(5)) return;
            if (player == null) return;
            if (player.currentState != PlayerState.Ghost) return;
            gameObject.SetActive(false);
        }
    }
}