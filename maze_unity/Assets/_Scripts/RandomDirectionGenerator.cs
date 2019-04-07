using UnityEngine;

namespace ProceduralMaze
{
    public static class RandomDirectionGenerator
    {
        public static int GetNextNumber(System.Random rand, int min, int max)
        {
            return rand.Next(min, max);
        }

        public static Direction GetDirection()
        {
            int i = Random.Range(1, 4);

            switch (i)
            {
                case 1: return Direction.North;
                case 2: return Direction.South;
                case 3: return Direction.East;
                default: return Direction.Weast;
            }
        }
    }
}