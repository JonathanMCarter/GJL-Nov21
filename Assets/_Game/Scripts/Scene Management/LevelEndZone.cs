using MultiScene.Core;
using UnityEngine;

namespace DeadTired.Scenes
{
    public class LevelEndZone : MonoBehaviour, IMultiSceneAwake
    {
        private SceneGroupChangeWithFade sceneGroupChangeWithFade;
        private PlayerBehaviour player;

        private void Awake()
        {
            sceneGroupChangeWithFade = GetComponent<SceneGroupChangeWithFade>();
        }


        public void OnMultiSceneAwake()
        {
            player = SceneElly.GetComponentFromAllScenes<PlayerBehaviour>();
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (player.currentState.Equals(PlayerState.Ghost)) return;
            sceneGroupChangeWithFade.LoadSceneGroup();
            gameObject.SetActive(false);
        }
    }
}