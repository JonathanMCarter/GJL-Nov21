using System;

namespace DeadTired.Sokoban
{
    [Serializable]
    public class SokobanTileData
    {
        public SokobanTile tile;
        public SokobanBlock blockOnTile;
        
        public SokobanTileData(){}

        public SokobanTileData(SokobanTile tile)
        {
            this.tile = tile;
            if (!tile.IsOccupied) return;
            blockOnTile = tile.OccupyingBlock;
        }
    }
}