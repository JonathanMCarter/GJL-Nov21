using System.Collections.Generic;
using System.Linq;
using MultiScene.Core;
using UnityEngine;

namespace DeadTired.Sokoban
{
    public class SokobanManager : MonoBehaviour
    {
        [SerializeField] private List<SokobanTile> tiles;
        [SerializeField] private int gridColumnCount = 6;
        [SerializeField] private int gridRowCount = 6;

        [SerializeField] private List<SokobanRequirementData> sokobanRequirements;

        public SokobanTile GetTileOn(string id)
        {
            var _tile = tiles.Where(t => t.OccupyingBlock != null);

            foreach (var option in _tile)
            {
                if (!option.IsOccupied) continue;
                if (!option.OccupyingBlock.BlockID.Equals(id)) continue;
                return option;
            }

            Debug.LogError("Unable to find tile with block...");
            return null;
        }


        private void Awake()
        {
            tiles = SceneElly.GetComponentsFromScene<SokobanTile>("Level2-Sokoban-1");
        }


        public void CompleteBlock(string blockID)
        {
            var _element = sokobanRequirements.FirstOrDefault(t => t.id.Equals(blockID));

            if (_element == null) return;
            _element.isComplete = true;
        }
        

        public List<SokobanTile> GetNeighbours(SokobanTile tile)
        {
            var _tiles = new List<SokobanTile>();
            var _tileLocation = tiles.IndexOf(tile);

            // Cause 0 % 6 is 0 lol xD
            if (_tileLocation.Equals(0))
                _tiles.Add(tiles[1]);

            // tile - grindcount of this tile...
            if (_tileLocation - gridColumnCount >= 0)
                _tiles = TryAddTile(_tiles, tiles[_tileLocation - gridColumnCount]);

            // tile + grindcount of this tile...
            if (_tileLocation + gridColumnCount <= gridColumnCount * gridRowCount)
            {
                if (_tileLocation + gridColumnCount < gridColumnCount * gridRowCount)
                    _tiles = TryAddTile(_tiles, tiles[_tileLocation + gridColumnCount]);
            }

            // tile - 1 if it is on the end of a row...
            if (_tileLocation % gridColumnCount != 5)
            {
                if (_tileLocation - 1 >= 0)
                    _tiles = TryAddTile(_tiles, tiles[_tileLocation - 1]);
            }
            else
            {
                if (_tileLocation - gridColumnCount >= 0)
                    _tiles = TryAddTile(_tiles, tiles[_tileLocation - gridColumnCount]);
            }


            // tile + 1 if it is on the end of a row...
            if (_tileLocation % gridColumnCount != 0)
            {
                if (_tileLocation + 1 < gridColumnCount * gridRowCount)
                    _tiles = TryAddTile(_tiles, tiles[_tileLocation + 1]);
            }
            else
            {
                if (_tileLocation + gridColumnCount < gridColumnCount * gridRowCount)
                    _tiles = TryAddTile(_tiles, tiles[_tileLocation + gridColumnCount]);
            }


            return _tiles;
        }


        public List<SokobanTile> TryAddTile(List<SokobanTile> list, SokobanTile tile)
        {
            if (list.Contains(tile)) return list;
            list.Add(tile);
            return list;
        }
    }
}