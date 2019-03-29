using UnityEngine;

public static class RandomDirectionGenerator
{
    //private static Random rand;
    //private const string seed = "procedural_maze@generation-Algorithm%withUnItY!";
    private const int min = 0;
    private const int max = 4;

    public static Direction GetRandomDirection()
    {
        //rand = new Random(seed.GetHashCode());
        //int i = rand.Next(min, max);
        int i = Random.Range(min, max);

        switch (i)
        {
            case 0: return Direction.North;
            case 1: return Direction.South;
            case 2: return Direction.Weast;
            default: return Direction.East;
        }
    }
}
