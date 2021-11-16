using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanEndTile : SokobanTile
    {
        [SerializeField] private string blockIDWanted;
        
        public bool HasCorrectBlock => IsOccupied && OccupyingBlock.BlockID.Equals(blockIDWanted);

        private SokobanManager sokobanManager;
        
        
        public override void SetBlockToTile(SokobanBlock block)
        {
            base.SetBlockToTile(block);

            if (HasCorrectBlock)
            {
                
            }
        }
    }
}