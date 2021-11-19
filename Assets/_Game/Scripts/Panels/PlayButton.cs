using DeadTired.Scenes;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.UI
{
    public class PlayButton : MonoBehaviour, IMultiSceneAwake
    {
        private MenuSceneGroupChange levelLoad;

        public void OnMultiSceneAwake()
        {
            levelLoad = SceneElly.GetComponentFromScene<MenuSceneGroupChange>("Menu");
        }

        public void PlayGame()
        {
            AkSoundEngine.PostEvent("PlayButton", gameObject);
            levelLoad.LevelSelected = Levels.Level1;
            levelLoad.LoadSceneGroup();
        }
    }
}