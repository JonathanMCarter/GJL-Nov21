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
            levelLoad = SceneElly.GetComponentFromScene<MenuSceneGroupChange>("MenuScene");
        }

        public void Playbutton()
        {
                    AkSoundEngine.PostEvent("PlayButton", gameObject);
        }

        public void PlayGame()
        {
            
            levelLoad.LoadSceneGroup();
        }
       
    }
}