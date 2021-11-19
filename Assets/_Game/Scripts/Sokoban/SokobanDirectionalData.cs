using System;

namespace DeadTired.Sokoban
{
    [Serializable]
    public class SokobanDirectionalData
    {
        public SokobanTile posX;
        public SokobanTile negX;
        public SokobanTile posZ;
        public SokobanTile negZ;
    }
}