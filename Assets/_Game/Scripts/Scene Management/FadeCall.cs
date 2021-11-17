using MultiScene.Core;
using MultiScene.Extensions.DoNotDestroy;
using UnityEngine;

namespace DeadTired.Scenes
{
    public class FadeCall : MonoBehaviour, IMultiSceneAwake
    {
        public void OnMultiSceneAwake()
        {
            DoNotDestroyAccessor.GetComponentInDoNotDestroy<FadeTransition>().FadeToClear();
        }
    }
}