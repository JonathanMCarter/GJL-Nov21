using System.Collections.Generic;
using JTools;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired
{
    public class LightPuzzleManager : MonoBehaviour, IMultiSceneAwake
    {
        public List<GameObject> paths;


        public void OnMultiSceneAwake()
        {
            // Picks a random path to set, so each time it could be different xD
            paths[Random.Range(0, paths.Count)].SetActive(true);
        }
    }
}