using System.Collections.Generic;
using System.Linq;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.UI
{
    public class OpenPanelWithID : MonoBehaviour, IMultiSceneAwake
    {
        private List<BasePanel> allPanels;


        public void OnMultiSceneAwake()
        {
            allPanels = SceneElly.GetComponentsFromAllScenes<BasePanel>();
        }


        public void OpenPanel(string id)
        {
            AkSoundEngine.PostEvent("QuitButton", gameObject);
            allPanels.FirstOrDefault(t => t.PanelID.Equals(id))?.OpenPanel();
        }
       
    }
}