using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    public GameObject roomPrefab;

    public int numX = 10;
    public int numY = 10;

    private const float roomSize = 3.5f;

    private Room[,] rooms;

    void Start()
    {
        CreateRooms();
        Generate();
    }

    void CreateRooms()
    {
        rooms = new Room[numX, numY];

        for (int x = 0; x < numX; x++)
        {
            for (int y = 0; y < numY; y++)
            {
                GameObject room = Instantiate(
                    roomPrefab,
                    transform.position + new Vector3(
                        x * roomSize,
                        y * roomSize,
                        0
                    ),
                    Quaternion.identity,
                    transform
                );

                room.name = "Room_" + x + "_" + y;

                rooms[x, y] = room.GetComponent<Room>();

                rooms[x, y].Index = new Vector2Int(x, y);
            }
        }
    }

    void Generate()
    {
        Stack<Room> stack = new Stack<Room>();

        Room startRoom = rooms[0, 0];
        startRoom.visited = true;

        stack.Push(startRoom);

        while (stack.Count > 0)
        {
            Room current = stack.Peek();

            List<Room.Directions> directions = new List<Room.Directions>();

            int x = current.Index.x;
            int y = current.Index.y;

            if (y < numY - 1 && !rooms[x, y + 1].visited)
                directions.Add(Room.Directions.TOP);

            if (x < numX - 1 && !rooms[x + 1, y].visited)
                directions.Add(Room.Directions.RIGHT);

            if (y > 0 && !rooms[x, y - 1].visited)
                directions.Add(Room.Directions.BOTTOM);

            if (x > 0 && !rooms[x - 1, y].visited)
                directions.Add(Room.Directions.LEFT);

            if (directions.Count > 0)
            {
                Room.Directions chosen =
                    directions[Random.Range(0, directions.Count)];

                int nextX = x;
                int nextY = y;

                current.SetDirFlag(chosen, false);

                switch (chosen)
                {
                    case Room.Directions.TOP:
                        nextY++;
                        rooms[nextX, nextY].SetDirFlag(
                            Room.Directions.BOTTOM, false);
                        break;

                    case Room.Directions.RIGHT:
                        nextX++;
                        rooms[nextX, nextY].SetDirFlag(
                            Room.Directions.LEFT, false);
                        break;

                    case Room.Directions.BOTTOM:
                        nextY--;
                        rooms[nextX, nextY].SetDirFlag(
                            Room.Directions.TOP, false);
                        break;

                    case Room.Directions.LEFT:
                        nextX--;
                        rooms[nextX, nextY].SetDirFlag(
                            Room.Directions.RIGHT, false);
                        break;
                }

                Room nextRoom = rooms[nextX, nextY];
                nextRoom.visited = true;

                stack.Push(nextRoom);
            }
            else
            {
                stack.Pop();
            }
        }

        // Entrance and exit
        rooms[0, 0].SetDirFlag(Room.Directions.LEFT, false);

        rooms[numX - 1, numY - 1].SetDirFlag(
            Room.Directions.RIGHT, false);

        Debug.Log("Library maze generated!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        for (int x = 0; x < numX; x++)
        {
            for (int y = 0; y < numY; y++)
            {
                Vector3 center = transform.position
                    + new Vector3(x * roomSize, y * roomSize, 0f);

                Gizmos.DrawWireCube(center, new Vector3(roomSize, roomSize, 0f));
                Gizmos.DrawSphere(center, 0.08f);
            }
        }
    }
}