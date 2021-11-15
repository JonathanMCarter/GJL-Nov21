using DeadTired.Scenes;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.UI
{
    public class PlayButton : MonoBehaviour, IMultiSceneAwake
    {
        private SceneGroupChangeWithFade levelLoad;

        public void OnMultiSceneAwake()
        {
            levelLoad = SceneElly.GetComponentFromScene<SceneGroupChangeWithFade>("Menu");
        }

        public void PlayGame()
        {
            levelLoad.LevelSelected = Levels.Level1;
            levelLoad.LoadSceneGroup();
        }
    }
}