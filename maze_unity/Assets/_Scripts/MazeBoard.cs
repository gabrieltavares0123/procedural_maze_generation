using UnityEngine;

public class MazeBoard
{
    private GameObject wallPrefab;
    private GameObject floorPrefab;

    private MazeCell[,] board;
    public MazeCell[,] Board
    {
        get { return this.board; }
    }

    private readonly float distance = 1f;

    public MazeBoard(int rows, int columns, GameObject wallPrefab, GameObject floorPrefab)
    {
        board = new MazeCell[rows, columns];
        this.wallPrefab = wallPrefab;
        this.floorPrefab = floorPrefab;
        CreateMaze();
    }

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

                Vector3 eastWallPos = new Vector3(floorPosition.x - .5f, floorPosition.y + .5f, floorPosition.z);
                Vector3 weastWallPos = new Vector3(floorPosition.x + .5f, floorPosition.y + .5f, floorPosition.z);
                Vector3 southWallPos = new Vector3(floorPosition.x, floorPosition.y + .5f, floorPosition.z - .5f);
                Vector3 northWallPos = new Vector3(floorPosition.x, floorPosition.y + .5f, floorPosition.z + .5f);

                if (r == 0)
                {
                    board[r, c].EastWall = GameObject.Instantiate<GameObject>(wallPrefab, eastWallPos, Quaternion.Euler(0, 90, 0));
                    board[r, c].EastWall.name = "EastWall " + r + ", " + c;
                }

                if (c == 0)
                {
                    board[r, c].SouthWall = GameObject.Instantiate<GameObject>(wallPrefab, southWallPos, Quaternion.identity);
                    board[r, c].SouthWall.name = "SouthWall " + r + ", " + c;
                }

                board[r, c].NorthWall = GameObject.Instantiate<GameObject>(wallPrefab, northWallPos, Quaternion.identity);
                board[r, c].NorthWall.name = "NorthWall " + r + ", " + c;

                board[r, c].WeastWall = GameObject.Instantiate<GameObject>(wallPrefab, weastWallPos, Quaternion.Euler(0, 90, 0));
                board[r, c].WeastWall.name = "WeastWall " + r + ", " + c;
            }
        }
    }
}
