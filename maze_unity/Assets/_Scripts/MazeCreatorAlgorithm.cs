using System;
using UnityEngine;

namespace ProceduralMaze
{
    public class MazeCreatorAlgorithm
    {
        private readonly MazeCell[,] mazeCells;
        private bool courseComplete = false;

        private readonly int rowsCount;
        private readonly int columnsCount;

        private int currentRow = 0;
        private int currentColumn = 0;

        public MazeCreatorAlgorithm(MazeCell[,] mazeCells)
        {
            this.mazeCells = mazeCells;
            rowsCount = mazeCells.GetLength(0);
            columnsCount = mazeCells.GetLength(1);
        }

        public void CreateMaze()
        {
            HuntAndKill();
        }

        private void HuntAndKill()
        {
            // Considera a primeira célula visitada
            mazeCells[currentRow, currentColumn].Visited = true;

            while (!courseComplete)
            {
                courseComplete = Hunt();
                Kill();
            }
        }

        // Busca uma célula, linha por linha que ainda não foi visitada
        private bool Hunt()
        {
            for (int r = 0; r < rowsCount; r++)
            {
                for (int c = 0; c < columnsCount; c++)
                {
                    if (!mazeCells[r, c].Visited && CellHasAnAdjacentVisitedCell(r, c))
                    {
                        DestroyAdjacentWall(r, c);
                        currentRow = r;
                        currentColumn = c;
                        mazeCells[r, c].Visited = true; // Marca a nova célula atual como visitada 
                        return false;// Caminho não acabou
                    }
                }
            }

            return true;// Caminho acabou
        }

        private bool CellHasAnAdjacentVisitedCell(int row, int column)
        {
            int visitedCells = 0;

            // Olha uma linha abaixo caso esteja na linha 1
            if (row > 0 && mazeCells[row - 1, column].Visited)
                visitedCells++;

            // Olha um alinha acima caso não esteja na penultima linha
            if (row < (rowsCount - 2) && mazeCells[row + 1, column].Visited)
                visitedCells++;

            // Olha uma coluna a esquerda caso não esteja na coluna 1
            if (column > 0 && mazeCells[row, column - 1].Visited)
                visitedCells++;

            // Olha uma coluna a direita caso não esteja na penultima coluna
            if (column < (columnsCount - 2) && mazeCells[row, column + 1].Visited)
                visitedCells++;

            return visitedCells > 0;
        }

        private void DestroyAdjacentWall(int row, int column)
        {
            bool wallDestroyed = false;

            while (!wallDestroyed) {
                Direction direction = RandomDirectionGenerator.GetDirection();

                if (direction == Direction.North && row > 0 && mazeCells[row - 1, column].Visited)
                {
                    DestroyWallIfItExists(mazeCells[row, column].NorthWall);
                    DestroyWallIfItExists(mazeCells[row - 1, column].SouthWall);
                    wallDestroyed = true;
                }

                else if (direction == Direction.South && row < (rowsCount - 2) && mazeCells[row + 1, column].Visited)
                {
                    DestroyWallIfItExists(mazeCells[row, column].NorthWall);
                    DestroyWallIfItExists(mazeCells[row + 1, column].SouthWall);
                    wallDestroyed = true;
                }

                else if (direction == Direction.East && column > 0 && mazeCells[row, column - 1].Visited)
                {
                    DestroyWallIfItExists(mazeCells[row, column].WeastWall);
                    DestroyWallIfItExists(mazeCells[row, column - 1].EastWall);
                    wallDestroyed = true;
                }
                else if (direction == Direction.Weast && column < (columnsCount - 2) && mazeCells[row, column + 1].Visited)
                {
                    DestroyWallIfItExists(mazeCells[row, column].EastWall);
                    DestroyWallIfItExists(mazeCells[row, column + 1].WeastWall);
                    wallDestroyed = true;
                }
                    
            }
        }

        private void DestroyWallIfItExists(GameObject wall)
        {
            if (wall != null)
            {
                Debug.Log("Wall " + wall.name + " destroyed.");
                GameObject.Destroy(wall);
            }

            Debug.LogWarning("Wall does not exists.");
        }

        private bool CellIsAvaliable(int row, int column)
        {
            if (row >= 0 && row < rowsCount && column >= 0 && column < columnsCount && !mazeCells[row, column].Visited)
                return true;
            else
                return false;
        }

        private void Kill()
        {
            if (RouteStillAvaliable(currentRow, currentColumn))
            {
                Direction direction = RandomDirectionGenerator.GetDirection();

                if (direction == Direction.North && CellIsAvaliable(currentRow - 1, currentColumn))
                {
                    DestroyWallIfItExists(mazeCells[currentRow, currentColumn].NorthWall);
                    DestroyWallIfItExists(mazeCells[currentRow - 1, currentColumn].SouthWall);
                    currentRow--;
                }
                else if (direction == Direction.South && CellIsAvaliable(currentRow + 1, currentColumn))
                {
                    DestroyWallIfItExists(mazeCells[currentRow, currentColumn].SouthWall);
                    DestroyWallIfItExists(mazeCells[currentRow + 1, currentColumn].NorthWall);
                    currentRow++;
                }
                else if (direction == Direction.East && CellIsAvaliable(currentRow, currentColumn + 1))
                {
                    DestroyWallIfItExists(mazeCells[currentRow, currentColumn].EastWall);
                    DestroyWallIfItExists(mazeCells[currentRow, currentColumn + 1].WeastWall);
                    currentColumn++;
                }
                else if (direction == Direction.Weast && CellIsAvaliable(currentRow, currentColumn - 1))
                {
                    DestroyWallIfItExists(mazeCells[currentRow, currentColumn].WeastWall);
                    DestroyWallIfItExists(mazeCells[currentRow, currentColumn - 1].EastWall);
                    currentColumn--;
                }

                mazeCells[currentRow, currentColumn].Visited = true;
            }
        }

        // Verifica se ainda existe algum caminho norte, sul, leste, oeste que não foi visitado a partir de [r, c]
        private bool RouteStillAvaliable(int row, int column)
        {
            int avaliableRoutes = 0;

            // Verifica se a linha anterior não foi visistada
            if (row > 0 && !mazeCells[row - 1, column].Visited)
                avaliableRoutes++;

            // Verifica se a linha superior não foi visitada
            if (row < (rowsCount - 1) && !mazeCells[row + 1, column].Visited)
                avaliableRoutes++;

            // Verifica se a culuna da esquerda não foi visitada
            if (column > 0 && !mazeCells[row, column - 1].Visited)
                avaliableRoutes++;

            // Verifica se a coluna da direita não foi visitada
            if (column < (columnsCount - 1) && !mazeCells[row, column + 1].Visited)
                avaliableRoutes++;

            return avaliableRoutes > 0;
        }
    }
}