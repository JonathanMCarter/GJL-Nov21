using MultiScene.Core;
using MultiScene.Extensions.DoNotDestroy;

namespace DeadTired.Scenes
{
    public class SceneGroupChangeWithFade : BaseMultiSceneLoader
    {
        public override void LoadSceneGroup()
        {
            FadeTransition.OnTransitionFadeToBlack += LoadScenes;
            DoNotDestroyAccessor.GetComponentInDoNotDestroy<FadeTransition>().FadeToBlack();
        }

        private void LoadScenes()
        {
            FadeTransition.OnTransitionFadeToBlack -= LoadScenes;
            SceneElly.GetComponentFromAllScenes<MultiSceneManager>().LoadScenesKeepBase(loadGroup);
        }
    }
}