using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject.MazeGame
{
    public static class Map
    {
        //baseWidth = 12, baseHeight = 16
        //0 - wall
        //1 - free to step
        //2 - start
        //3 - goal
        public static int[,] CurrentMap = new int[,] {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,3,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0 },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
            };

        public static int GetStartPositonX()
        {
            for (int y = 0; y < CurrentMap.GetLength(0); y++)
            {
                for (int x = 0; x < CurrentMap.GetLength(1); x++)
                {
                    if (CurrentMap[y,x]==2) { return x; }
                }
            }
            return -1;
        }

        public static int GetStartPositonY() {
            for (int y = 0; y < CurrentMap.GetLength(0); y++)
            {
                for (int x = 0; x < CurrentMap.GetLength(1); x++)
                {
                    if (CurrentMap[y, x] == 2) { return y; }
                }
            }
            return -1;
        }
    }
}
