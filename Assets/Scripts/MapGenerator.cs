using Core.MapDto;
using Core.MapDto.Tiles;
using UnityEngine;

namespace DefaultNamespace
{
    public static class MapGenerator
    {
        public static Map Generate()
        {
            var map = new Map(new Core.MapDto.Tile[34, 20]);

            for (var x = 0; x < map.Width; x++)
                for (var y = 0; y < map.Height; y++)
                    map[x, y] = new Ground(x, y);


            for (var y = 0; y < map.Height; y++)
            {
                for (var x = 0; x < map.Width; x++)
                {
                    if (y == 0 || y == map.Height - 1)
                        if (x < 13 || x > 14)
                            map[x, y] = new Brick(x, y);

                    if (y == 1 || y == map.Height - 2)
                        if (x < 13 || x > 14)
                            map[x, y] = new Brick(x, y);

                    if (y == 2 || y == map.Height - 3)
                        if (x < 11 || x > 16)
                            map[x, y] = new Brick(x, y);

                    if (y == 3 || y == map.Height - 4)
                        if (x < 11 || x > 17)
                            map[x, y] = new Brick(x, y);

                    if (y == 4 || y == map.Height - 5)
                        if (x < 9 || x > 19)
                            map[x, y] = new Brick(x, y);

                    if (y == 5 || y == map.Height - 6)
                        if (x < 2 || x > 23)
                            map[x, y] = new Brick(x, y);

                    if (y == 6 || y == map.Height - 7)
                        if (x > 23)
                        {
                            map[x, y] = new Brick(x, y);
                            Debug.Log($"new point {x} {y}");
                        }

                    if (y == 7 || y == map.Height - 8)
                        if (x > 27)
                        {
                            map[x, y] = new Brick(x, y);
                            Debug.Log($"new point {x} {y}");
                        }

                    if (y == 8 || y == map.Height - 9)
                        if (x > 31)
                        {
                            map[x, y] = new Brick(x, y);
                            Debug.Log($"new point {x} {y}");
                        }

                    if (y == 9 || y == map.Height - 10)
                        if (x == 13 || x == 14 || x == 9 || x == 10)
                        {
                            map[x, y] = new Brick(x, y);
                            Debug.Log($"new point {x} {y}");
                        }
                }
            }

            return map;
        }
    }
}