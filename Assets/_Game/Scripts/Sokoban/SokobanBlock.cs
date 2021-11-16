using DeadTired.Interactables;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanBlock : MonoBehaviour, IInteractable
    {
        [SerializeField] private BoolReference isPlayerGhost;
        [SerializeField] private string blockID;

        public string BlockID => blockID;
        
        public void OnPlayerInteract()
        {
            //TODO - make this do something xD
        }
    }
}