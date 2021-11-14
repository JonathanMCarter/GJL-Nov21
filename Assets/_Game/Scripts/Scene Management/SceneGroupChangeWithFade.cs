using MultiScene.Core;
using MultiScene.Extensions.DoNotDestroy;
using UnityEngine;

namespace DeadTired.Scenes
{
    public class SceneGroupChangeWithFade : BaseMultiSceneLoader
    {
        [SerializeField] private SceneGroup[] levels;
        [SerializeField] private Levels levelChoice;

        public Levels LevelSelected
        {
            get => levelChoice;
            set => levelChoice = value;
        }
        
        
        public override void LoadSceneGroup()
        {
            FadeTransition.OnTransitionFadeToBlack += LoadScenes;
            DoNotDestroyAccessor.GetComponentInDoNotDestroy<FadeTransition>().FadeToBlack();
        }

        private void LoadScenes()
        {
            FadeTransition.OnTransitionFadeToBlack -= LoadScenes;

            switch (LevelSelected)
            {
                case Levels.Level1:
                    LoadLevel(levels[0]);
                    break;
                case Levels.Level2:
                    LoadLevel(levels[1]);
                    break;
                case Levels.Level3:
                    LoadLevel(levels[2]);
                    break;
                case Levels.Unassigned:
                    Debug.LogError("You forgot to assign the level choice xD");
                    break;
                default:
                    Debug.LogError("Scene was not an option... somehow you got this to fire xD");
                    break;
            }
        }


        private void LoadLevel(SceneGroup groupToLoad)
        {
            SceneElly.GetComponentFromAllScenes<MultiSceneManager>().LoadScenesKeepBase(groupToLoad);
        }
    }
}