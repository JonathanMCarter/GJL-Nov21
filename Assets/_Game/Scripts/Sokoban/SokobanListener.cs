using System;
using System.Linq;
using JTools;
using MultiScene.Core;
using UnityEngine;
using UnityEngine.Events;
using SceneElly = MultiScene.Core.SceneElly;

namespace DeadTired.Sokoban
{
    public class SokobanListener : MonoBehaviour, IMultiSceneAwake
    {
        [SerializeField] private string id;
        [SerializeField, ReadOnly] private SokobanManager sokobanManager;

        public UnityEvent OnPuzzleFinished;


        public void OnMultiSceneAwake()
        {
            sokobanManager = SceneElly.GetComponentsFromAllScenes<SokobanManager>().FirstOrDefault(t => t.GetID.Equals(id));
            sokobanManager.OnPuzzleCompleted += CallPuzzleFinished;
        }

        private void OnDisable()
        {
            sokobanManager.OnPuzzleCompleted -= CallPuzzleFinished;
        }


        private void CallPuzzleFinished()
        {
            Debug.Log("Called Puzzle Done On Listener");
            OnPuzzleFinished?.Invoke();
        }
    }
}