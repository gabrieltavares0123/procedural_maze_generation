using UnityEngine;

public class MazeInitializer : MonoBehaviour
{
    [SerializeField] private int rowSize, columnSize;
    [SerializeField] private GameObject wallPrefab, floorPrefab;

    private MazeBoard mazeBoard;
    private PathCreator pathCreator;

    private void Start()
    {
        Debug.Log(rowSize);
        Debug.Log(columnSize);
        Debug.Log(wallPrefab);
        mazeBoard = new MazeBoard(rowSize, columnSize, wallPrefab, floorPrefab);
        pathCreator = new PathCreator(mazeBoard.Board);
        pathCreator.CreatePath();
        Debug.Log(RandomDirectionGenerator.GetRandomDirection());
        Debug.Log(RandomDirectionGenerator.GetRandomDirection());
        Debug.Log(RandomDirectionGenerator.GetRandomDirection());
        Debug.Log(RandomDirectionGenerator.GetRandomDirection());
        Debug.Log(RandomDirectionGenerator.GetRandomDirection());
        Debug.Log(RandomDirectionGenerator.GetRandomDirection());
        Debug.Log(RandomDirectionGenerator.GetRandomDirection());
    }

}
