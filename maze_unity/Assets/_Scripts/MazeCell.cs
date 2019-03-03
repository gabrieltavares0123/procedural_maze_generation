using UnityEngine;

public class MazeCell 
{
    private bool visited = false;
    public bool Visited { get => visited; set => visited = value; }

    private GameObject northWall, southWall, eastWall, weastWall, floor;
    public GameObject NorthWall { get => northWall; set => northWall = value; }
    public GameObject SouthWall { get => southWall; set => southWall = value; }
    public GameObject EastWall { get => eastWall; set => eastWall = value; }
    public GameObject WeastWall { get => weastWall; set => weastWall = value; }
    public GameObject Floor { get => floor; set => floor = value; }
}
