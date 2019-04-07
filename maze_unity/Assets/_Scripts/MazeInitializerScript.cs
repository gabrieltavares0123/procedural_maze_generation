using System;
using UnityEngine;

namespace ProceduralMaze
{
    public class MazeInitializerScript : MonoBehaviour
    {
        [SerializeField] private int rowSize, columnSize;
        [SerializeField] private GameObject wallPrefab, floorPrefab;

        private MazeBoard mazeBoard;
        private MazeCreatorAlgorithm mazeCreator;
        private System.Random rand;
        private string seed;

        private void Start()
        {
            seed = "'!@maze--GeneRatoR+=Algrithm;.;?/][";
            rand = new System.Random(seed.GetHashCode());

            mazeBoard = new MazeBoard(rowSize, columnSize, wallPrefab, floorPrefab);
            mazeCreator = new MazeCreatorAlgorithm(mazeBoard.Board);
            mazeCreator.CreateMaze();
        }

    }
}
