using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// class that explores first child of every node until it runs out of moves
// then goes to the next child and does the same until no nodes are left
public class DepthFirst : MonoBehaviour, IInitializeable
{
    Dictionary<Vector3, Node> tiles;
    public Dictionary<Vector3, Vector3> cameFrom = new Dictionary<Vector3, Vector3>();
    public Dictionary<Vector3, int> costSoFar = new Dictionary<Vector3, int>();

    //public int maxMoves = 5;

	public void Init()
	{
		tiles = GetComponent<MapGenerator>().tiles;
	}

	public void GetGrid(Vector3 start, int range)
    {
        cameFrom[start] = start;
        costSoFar[start] = 0;
        GoDeeper(start, range);
        ColorGrid();
    }

    void GoDeeper(Vector3 currentCell, int range)
    {
        foreach (Vector3 neighbour in tiles[currentCell].neighbours)
        {
            if (tiles.ContainsKey(neighbour))
            {
                if (tiles[neighbour].walkable)
                {
                    int newCost = costSoFar[currentCell] + Mathf.Clamp(Mathf.Abs(tiles[neighbour].cost + 1 - tiles[currentCell].cost), 1, 1000);
                    if (!costSoFar.ContainsKey(neighbour) || newCost < costSoFar[neighbour])
                    {
                        if (newCost <= range)
                        {
                            cameFrom[neighbour] = currentCell;

                            costSoFar[neighbour] = newCost;
                            tiles[neighbour].costSoFar = newCost;

                            GoDeeper(neighbour, range);
                        }
                    }
                }
            }
        }

    }

    public void Reset()
    {
        foreach (Vector3 hex in cameFrom.Keys)
        {
            tiles[hex].GetComponent<MeshRenderer>().material.color = Color.white;
        }

        costSoFar.Clear();
        cameFrom.Clear();
    }

    // gets a path from the walkable cells by checking cameFrom
    public List<Node> GetPath(Vector3 start, Vector3 dest)
    {
        if (!cameFrom.ContainsKey(dest))
        {
            return null;
        }
        List<Node> path = new List<Node>();
        path.Add(tiles[dest]);
        Stack<Node> pathStack = new Stack<Node>();
        pathStack.Push(tiles[dest]);

        Vector3 current = dest;
        while (current != start)
        {
            path.Add(tiles[cameFrom[current]]);
            current = cameFrom[current];
        }

        return path;
    }

    public void ColorGrid()
    {
        //int movesLeft = selected.movesLeft;
        foreach (Vector3 tile in costSoFar.Keys)
        {
            int costSoFarForTile = costSoFar[tile];
            if (costSoFarForTile != 0)
            {
                tiles[tile].GetComponent<MeshRenderer>().material.color = Color.cyan;
                //float accuracy = selected.CalculateAccuracy(movesLeft - costSoFar, null);
                //if (accuracy < 0.3)
                //	hexes[hex].GetComponent<MeshRenderer>().material.color = Color.red;
                //if (accuracy >= 0.3 && accuracy < 0.6)
                //	hexes[hex].GetComponent<MeshRenderer>().material.color = Color.yellow;
                //if (accuracy >= 0.6)
                //	hexes[hex].GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }

}
