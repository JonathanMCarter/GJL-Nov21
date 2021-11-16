using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanEndTile : SokobanTile
    {
        [SerializeField] private string blockIDWanted;
        
        public bool HasCorrectBlock => IsOccupied && OccupyingBlock.BlockID.Equals(blockIDWanted);


        public override void SetBlockToTile(SokobanBlock block)
        {
            base.SetBlockToTile(block);

            if (!HasCorrectBlock) return;
            sokobanManager.CompleteBlock(block.BlockID);
        }
    }
}