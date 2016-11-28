using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour
{
	Vector3 _coord;
	public Vector3 coord
	{
		get
		{
			return _coord;
		}
		set
		{
			_coord = value;
			CalculateNeighbours();
		}
	}
	public int cost = 1;
	public int costSoFar = 0;
	public bool walkable = true;
	public List<Vector3> neighbours = new List<Vector3>();

	//MapGenerator mapGenerator;
	Vector3[] directions = { new Vector3(+1, 0, 0), new Vector3(0, 0, +1), new Vector3(-1, 0, 0), new Vector3(0, 0, -1) };

	void Start()
	{
		//mapGenerator = FindObjectOfType<MapGenerator>();
		//mapGenerator.tiles.Add(coord, this);
	}

	void CalculateNeighbours()
	{
		foreach (Vector3 direction in directions)
		{
			neighbours.Add(coord + direction);
		}
	}

	public float GetTop()
	{
		float top;
		top = GetComponent<MeshRenderer>().bounds.max.y;

		return top;
	}
}
