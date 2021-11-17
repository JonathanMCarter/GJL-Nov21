// ----------------------------------------------------------------------------
// MultiSceneLoadEditor.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 31/08/2021
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using JTools.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace JTools.MultiScene.Editor
{
    [CustomEditor(typeof(MultiSceneManager))]
    public class MultiSceneManagerEditor : UnityEditor.Editor
    {
        private readonly string ScenePath = "Assets/Scenes/";
        private readonly string SceneExtension = ".unity";
        private List<string> sceneNames;
        private MultiSceneManager multiSceneManager;
        
        

        private void OnEnable()
        {
            multiSceneManager = target as MultiSceneManager;
            if (multiSceneManager.scenesToLoad == null) return;
            sceneNames = multiSceneManager.scenesToLoad.scenes;
        }

        public override void OnInspectorGUI()
        {
            Button.YellowButton("Load Scenes In Editor", LoadScenesInEditor);
            Layout.Space();
            base.OnInspectorGUI();
        }

        private void LoadScenesInEditor()
        {
            for (var i = 0; i < sceneNames.Count; i++)
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene($"{ScenePath}{sceneNames[i]}{SceneExtension}", OpenSceneMode.Additive);
            }
        }
    }
}