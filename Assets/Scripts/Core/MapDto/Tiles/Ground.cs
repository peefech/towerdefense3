using UnityEngine;

namespace Core.MapDto.Tiles
{
    public class Ground : Tile
    {
        public Ground(Vector2Int cords) : base(cords)
        {
        }

        public Ground(int x, int y) : base(x, y)
        {
        }

        public override bool Passable => true;
        public override TileType Type => TileType.Sand;
    }
}