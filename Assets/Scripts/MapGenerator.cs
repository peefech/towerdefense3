using Core.MapDto;
using Core.MapDto.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public static class MapGenerator
    {
        public static Map Generate(Tilemap block, Tilemap ground)
        {
            block.origin = ground.origin = new Vector3Int(0, 0, 0);
            
            var left = 0;
            var right = block.size.x > ground.size.x ? block.size.x : ground.size.x;
            var bottom = 0;
            var top = block.size.y > ground.size.y ? block.size.y : ground.size.y;;
            
            var map = new Map(new Core.MapDto.Tile[right - left, top - bottom]);
            
            for(var x = left; x < right; x++)
            for (var y = bottom; y < top; y++)
            {
                var g = ground.GetTile(new Vector3Int(x, y, 0));
                var b = block.GetTile(new Vector3Int(x, y, 0));
                if (b != null)
                    map[x, y] = new Brick(x, y);
                else if (g != null)
                    map[x, y] = new Ground(x, y);
                else map[x, y] = null;
            }

            return map;
        }
    }
}