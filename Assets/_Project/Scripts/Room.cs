using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Directions
    {
        TOP,
        RIGHT,
        BOTTOM,
        LEFT
    }

    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWall;

    public Vector2Int Index { get; set; }
    public bool visited = false;

    private Dictionary<Directions, GameObject> walls;

    private void Awake()
    {
        walls = new Dictionary<Directions, GameObject>
        {
            { Directions.TOP, topWall },
            { Directions.RIGHT, rightWall },
            { Directions.BOTTOM, bottomWall },
            { Directions.LEFT, leftWall }
        };
    }

    public void SetDirFlag(Directions direction, bool active)
    {
        walls[direction].SetActive(active);
    }
}