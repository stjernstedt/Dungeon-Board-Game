using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
	public int width;
	public int height;
	public int numberOfRooms, minRoomWidth, maxRoomWidth, minRoomHeight, maxRoomHeight;
	public Dictionary<Vector3, Node> tiles = new Dictionary<Vector3, Node>();
	public List<Room> rooms = new List<Room>();
	Prefabs prefabs;

	// Use this for initialization
	void Awake()
	{

	}

	public void CreateMap()
	{
		prefabs = GetComponent<Prefabs>();
		CreateRoom(50, 50, 5, 5);
		for (int i = 0; i < numberOfRooms; i++)
		{
			AddFeature();
		}
	}

	bool CreateRoom(int xStart, int zStart, int width, int height)
	{
		bool createdRoom = true;

		for (int x = xStart - 1; x <= width + xStart; x++)
		{
			for (int z = zStart - 1; z <= height + zStart; z++)
			{
				if (tiles.ContainsKey(new Vector3(x, 0, z)))
				{
					createdRoom = false;
				}
			}
		}

		if (createdRoom)
		{
			Transform roomsParent = GameObject.Find("Rooms").transform;
			Transform room = new GameObject().transform;
			room.position = new Vector3(xStart, 0, zStart);
			room.SetParent(roomsParent, true);
			room.gameObject.AddComponent<Room>().Init(xStart, zStart, width, height);
			room.gameObject.name = "Room" + Room.roomNr;
			rooms.Add(room.GetComponent<Room>());

			for (int x = xStart; x < width + xStart; x++)
			{
				for (int z = zStart; z < height + zStart; z++)
				{
					Vector3 pos = new Vector3(x, 0, z);
					Node tile = Instantiate(prefabs.floorTile1).AddComponent<Node>();
					tile.coord = pos;
					tiles[tile.coord] = tile;
					tile.transform.position = pos;
					tile.transform.SetParent(room, true);
				}
			}
			Room.roomNr += 1;
		}
		return createdRoom;
	}

	void AddFeature()
	{
		bool createdRoom = false;
		Room room = rooms[Random.Range(0, rooms.Count)];
		//Room room = rooms[rooms.Count - 1];

		int dir = Random.Range(0, 4);
		int newRoomWidth = Random.Range(minRoomWidth, maxRoomWidth);
		int newRoomHeight = Random.Range(minRoomHeight, maxRoomHeight);

		int x;
		int z;
		switch (dir)
		{
			//north
			case 0:
				x = room.width / 2;
				//if (x % 2 != 0)
				//	x += 1;
				x += room.xStart;
				z = room.zStart + room.height;
				if (CreateRoom(x - newRoomWidth / 2, z + 1, newRoomWidth, newRoomHeight))
				{
					createdRoom = true;
				}
				break;
			//east
			case 1:
				x = room.xStart + room.width;
				z = room.height / 2;
				//if (z % 2 != 0)
				//	z += 1;
				z += room.zStart;
				if (CreateRoom(x + 1, z - newRoomHeight / 2, newRoomWidth, newRoomHeight))
				{
					createdRoom = true;
				}
				break;
			//south
			case 2:
				x = room.width / 2;
				//if (x % 2 != 0)
				//	x += 1;
				x += room.xStart;
				z = room.zStart - 1;
				if (CreateRoom(x - newRoomWidth / 2, z - newRoomHeight, newRoomWidth, newRoomHeight))
				{
					createdRoom = true;
				}
				break;
			//west
			case 3:
				x = room.xStart - 1;
				z = room.height / 2;
				//if (z % 2 != 0)
				//	z += 1;
				z += room.zStart;
				if (CreateRoom(x - newRoomWidth, z - newRoomHeight / 2, newRoomWidth, newRoomHeight))
				{
					createdRoom = true;
				}
				break;

			default:
				x = 0;
				z = 0;
				break;
		}

		if (createdRoom)
		{
			Vector3 doorwayPos = new Vector3(x, 0, z);
			Node doorway = Instantiate(prefabs.floorTile1).AddComponent<Node>();
			doorway.coord = doorwayPos;
			tiles[doorway.coord] = doorway;
			doorway.transform.position = doorwayPos;
		}
		else
		{
			AddFeature();
		}
	}
}

public class Room : MonoBehaviour
{
	public static int roomNr = 0;
	public int xStart, zStart;
	public int width, height;

	public void Init(int xStart, int zStart, int width, int height)
	{
		this.xStart = xStart;
		this.zStart = zStart;
		this.width = width;
		this.height = height;
	}

	public Vector3 GetCenter()
	{
		int startX = 10000;
		int startZ = 10000;
		int endX = 0;
		int endZ = 0;
		foreach (Transform child in transform)
		{
			if (child.position.x < startX)
				startX = (int)child.position.x;
			if (child.position.x > endX)
				endX = (int)child.position.x;
			if (child.position.z < startZ)
				startZ = (int)child.position.z;
			if (child.position.z > endZ)
				endZ = (int)child.position.z;

		}

		int x = (int)Mathf.Round((endX - startX) / 2);
		int z = (int)Mathf.Round((endZ - startZ) / 2);
		Vector3 pos = new Vector3(startX + x, 0, startZ + z);

		return pos;
	}

}