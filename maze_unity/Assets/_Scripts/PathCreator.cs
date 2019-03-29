using UnityEngine;

public class PathCreator
{
    private int currentRow = 0, currentColumn = 0;
    private int mazeRowSize, mazeColumnSize;
    private MazeCell[,] mazeBoard;
    private bool courseComplete = false;

    public PathCreator(MazeCell[,] board)
    {
        this.mazeBoard = board;
        mazeRowSize = board.GetLength(0);
        mazeColumnSize = board.GetLength(1);
    }

    public void CreatePath()
    {
        mazeBoard[currentRow, currentColumn].Visited = true;

        while (!courseComplete)
        {
            Kill();
            Hunt();
        }
    }

    private void Hunt()
    {
        courseComplete = true;

        for (int r = 0; r < mazeRowSize; r++)
        {
            for (int c = 0; c < mazeColumnSize; c++)
            {
                if (!mazeBoard[r, c].Visited && CellHasAnAdjacentVisitedCell(r, c))
                {
                    courseComplete = false;
                    currentRow = r;
                    currentColumn = c;
                    DestroyAdjacentWall(currentRow, currentColumn);
                    mazeBoard[currentRow, currentRow].Visited = true;
                    return;
                }
            }
        }
    }

    private void Kill()
    {
        while (ExistAnyAvaliableRoute(currentRow, currentColumn))
        {
            Direction direction = RandomDirectionGenerator.GetRandomDirection();

            if (direction == Direction.North && CellWasVisited(currentRow - 1, currentColumn))
            {
                DestroyWall(mazeBoard[currentRow, currentColumn].NorthWall);
                DestroyWall(mazeBoard[currentRow - 1, currentColumn].SouthWall);
                currentRow--;
            }
            else if (direction == Direction.South && CellWasVisited(currentRow + 1, currentColumn))
            {
                DestroyWall(mazeBoard[currentRow, currentColumn].SouthWall);
                DestroyWall(mazeBoard[currentRow + 1, currentColumn].NorthWall);
                currentRow++;
            }
            else if (direction == Direction.East && CellWasVisited(currentRow, currentColumn + 1))
            {
                DestroyWall(mazeBoard[currentRow, currentColumn].EastWall);
                DestroyWall(mazeBoard[currentRow, currentColumn + 1].WeastWall);
                currentColumn++;
            }
            else if (direction == Direction.Weast && CellWasVisited(currentRow, currentColumn - 1))
            {
                DestroyWall(mazeBoard[currentRow, currentColumn].WeastWall);
                DestroyWall(mazeBoard[currentRow, currentColumn - 1].EastWall);
                currentColumn--;
            }

            mazeBoard[currentRow, currentColumn].Visited = true;
        }
    }

    private bool ExistAnyAvaliableRoute(int row, int column)
    {
        int avaliableRoutCount = 0;

        if (row > 0 && !mazeBoard[(row - 1), column].Visited)
            avaliableRoutCount++;

        if (row < (mazeRowSize - 1) && !mazeBoard[(row + 1), column].Visited)
            avaliableRoutCount++;

        if (column > 0 && !mazeBoard[row, (column - 1)].Visited)
            avaliableRoutCount++;

        if (column < (mazeColumnSize - 1) && !mazeBoard[row, (column + 1)].Visited)
            avaliableRoutCount++;

        return avaliableRoutCount > 0;
    }

    private bool CellHasAnAdjacentVisitedCell(int row, int column)
    {
        int adjacentVisitedCellCount = 0;

        if (row > 0 && mazeBoard[(row - 1), column].Visited)
            adjacentVisitedCellCount++;

        if (row < (mazeRowSize - 2) && mazeBoard[(row + 1), column].Visited)
            adjacentVisitedCellCount++;

        if (column > 0 && mazeBoard[row, (column - 1)].Visited)
            adjacentVisitedCellCount++;

        if (column < (mazeColumnSize - 2) && mazeBoard[row, (column + 1)].Visited)
            adjacentVisitedCellCount++;
             
        return adjacentVisitedCellCount > 0;
    }

    private bool CellWasVisited(int row, int column)
    {
        if (row >= 0 && column >= 0 &&
            row < mazeRowSize && column < mazeColumnSize &&
            !mazeBoard[row, column].Visited)
            return true;
        else
            return false;
    }

    private void DestroyWall(GameObject wall) {
        if (wall != null)
            GameObject.Destroy(wall);
    }

    private void DestroyAdjacentWall(int row, int column)
    {
        bool wallDestroyed = false;

        while (!wallDestroyed)
        {
            Direction direction = RandomDirectionGenerator.GetRandomDirection();

            if (direction == Direction.North && row > 0 && mazeBoard[(row - 1), column].Visited)
            {
                DestroyWall(mazeBoard[row, column].NorthWall);
                DestroyWall(mazeBoard[(row - 1), column].SouthWall);
                wallDestroyed = true;
            }

            else if (direction == Direction.South && row < (mazeRowSize - 2) && mazeBoard[(row + 1), column].Visited)
            {
                DestroyWall(mazeBoard[row, column].SouthWall);
                DestroyWall(mazeBoard[(row + 1), column].NorthWall);
                wallDestroyed = true;
            }
            else if (direction == Direction.Weast && column > 0 && mazeBoard[row, column - 1].Visited)
            {
                DestroyWall(mazeBoard[row, column].WeastWall);
                DestroyWall(mazeBoard[row, (column - 1)].EastWall);
                wallDestroyed = true;
            }

            else if (direction == Direction.East && column < (mazeColumnSize - 2) && mazeBoard[row, (column + 1)].Visited)
            {
                DestroyWall(mazeBoard[row, column].EastWall);
                DestroyWall(mazeBoard[row, (column + 1)].WeastWall);
                wallDestroyed = true;
            }
        }
    }
}
