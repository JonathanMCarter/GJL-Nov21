using DeadTired.Interactables;
using DeadTired.UI;
using DependencyLibrary;
using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanSpiritBlock : SokobanBlock
    {
        [SerializeField] private IntReference playerOrbCount;
        [SerializeField] private bool isPowered;
        
        
        public override void OnPlayerInteract()
        {
            if (isPowered)
                base.OnPlayerInteract();
            else
            {
                if (playerOrbCount.Value <= 0) return;
                playerOrbCount.variable.IncrementValue(-1);
                PlayerOrbDisplay.OnOrbCountChanged?.Invoke();
                isPowered = true;
            }
        }
    }
}