using MultiScene.Core;
using MultiScene.Extensions.DoNotDestroy;
using UnityEngine;

namespace DeadTired.Scenes
{
    public class MenuSceneGroupChange : BaseMultiSceneLoader
    {
        public override void LoadSceneGroup()
        {
            FadeTransition.OnTransitionFadeToBlack += LoadLevel;
            DoNotDestroyAccessor.GetComponentInDoNotDestroy<FadeTransition>().FadeToBlack();
        }

        private void LoadLevel()
        {
            FadeTransition.OnTransitionFadeToBlack -= LoadLevel;
            SceneElly.GetComponentFromAllScenes<MultiSceneManager>().LoadScenesKeepBase(loadGroup);
        }
    }
}