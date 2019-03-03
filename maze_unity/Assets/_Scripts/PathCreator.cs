using UnityEngine;

public class PathCreator : MonoBehaviour
{
    private int currentRow = 0, currentColumn = 0;
    private int mazeRowSize, mazeColumnsSize;
    private MazeCell[] board;
    private bool courseComplete = false;

    public PathCreator(MazeCell[] board)
    {
        this.board = board;
        mazeRowSize = board.GetUpperBound(0);
        mazeColumnsSize = board.GetUpperBound(1);
    }

    private void HuntAndKill()
    {

    }

    private void Hunt()
    {
        while (!courseComplete)
        {
        }
    }

    private void Kill()
    {
    }

    private bool ExistAnyAvaliableRoute()
    {
        return true;
    }

    private bool CellIsAvaliable(int row, int column)
    {
        return true;
    }

    private void DestroyWall(GameObject wall) {
        if (wall != null)
            Destroy(wall);
    }
}
