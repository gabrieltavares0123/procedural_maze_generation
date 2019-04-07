using UnityEngine;

namespace ProceduralMaze
{
    public class MazeBoard
    {
        private readonly GameObject wallPrefab;
        private readonly GameObject floorPrefab;

        private MazeCell[,] board;
        public MazeCell[,] Board { get => board; set => board = value; }

        private readonly float distance = 1f;

        private int rowsCount;
        private int columnsCount;

        public MazeBoard(int rows, int columns, GameObject wallPrefab, GameObject floorPrefab)
        {
            board = new MazeCell[rows, columns];
            rowsCount = board.GetLength(0);
            columnsCount = board.GetLength(1);
            this.wallPrefab = wallPrefab;
            this.floorPrefab = floorPrefab;
            CreateMaze();
        }

                //south
        //weast         //east
                //north

        /*
         * As paredes são criadas da seguinte forma: primeiro são criadas paredes oeste(weast) e uma parede sul(south)
         * para quando r ou c igual a 0. Depois de criadas estas paredes, as outras células recebem paredes norte e 
         * leste para r ou c maior que 0.
         * 
         * A orde de creacimento das linhas e colunas é de baixo para cima, da esquerda para a direita
         */
        private void CreateMaze()
        {
            for (int r = 0; r < board.GetLength(0); r++)
            {
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    board[r, c] = new MazeCell();
                    Vector3 floorPosition = new Vector3((r * distance) + .5f, 0, (c * distance) + .5f);
                    board[r, c].Floor = GameObject.Instantiate<GameObject>(floorPrefab, floorPosition, Quaternion.Euler(90f, 0f, 0f));
                    board[r, c].Floor.name = "Floor " + r + ", " + c;

                    Vector3 eastWallPos = new Vector3(floorPosition.x + .5f, floorPosition.y + .5f, floorPosition.z);
                    Vector3 weastWallPos = new Vector3(floorPosition.x - .5f, floorPosition.y + .5f, floorPosition.z);
                    
                    Vector3 southWallPos = new Vector3(floorPosition.x, floorPosition.y + .5f, floorPosition.z - .5f);
                    Vector3 northWallPos = new Vector3(floorPosition.x, floorPosition.y + .5f, floorPosition.z + .5f);

                    if (r == 0)
                    {
                        board[r, c].WeastWall = GameObject.Instantiate<GameObject>(wallPrefab, weastWallPos, Quaternion.Euler(0, 90, 0));
                        board[r, c].WeastWall.name = "WeastWall " + r + ", " + c;
                    }

                    if (c == 0)
                    {
                        board[r, c].SouthWall = GameObject.Instantiate<GameObject>(wallPrefab, southWallPos, Quaternion.identity);
                        board[r, c].SouthWall.name = "SouthWall " + r + ", " + c;
                    }

                    board[r, c].NorthWall = GameObject.Instantiate<GameObject>(wallPrefab, northWallPos, Quaternion.identity);
                    board[r, c].NorthWall.name = "NorthWall " + r + ", " + c;

                    //if (r > 0 && r < rowsCount)
                     //   board[r, c].SouthWall = board[r - 1, c].NorthWall;

                    board[r, c].EastWall = GameObject.Instantiate<GameObject>(wallPrefab, eastWallPos, Quaternion.Euler(0, 90, 0));
                    board[r, c].EastWall.name = "WeastWall " + r + ", " + c;

                    //if (c > 0 && c < columnsCount)
                      //  board[r, c].WeastWall = board[r, c - 1].EastWall;

                }
            }
        }
    }
}