using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHandler : MonoBehaviour, IInitializeable
{
	public List<Character> characters = new List<Character>();
	public List<Character> enemies = new List<Character>();
	public Character selected;
	DepthFirst depthFirst;
	Dictionary<Vector3, Node> tiles;

	public bool abilityRunning = false;

	public void Init()
	{
		depthFirst = GetComponent<DepthFirst>();
		tiles = GetComponent<MapGenerator>().tiles;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Tiles"))
				{
					List<Node> path = depthFirst.GetPath(selected.GetCurrentCell().coord, hit.collider.GetComponent<Node>().coord);
					if (path != null)
					{
						StartCoroutine(MoveCharacter(path));
					}
				}
			}
		}
	}

	IEnumerator MoveCharacter(List<Node> path)
	{
		selected.walking = true;

		for (int i = path.Count - 1; i >= 0; i--)
		{
			Node node = path[i];
			float posY = tiles[node.coord].GetComponent<MeshRenderer>().bounds.max.y + selected.GetComponent<MeshRenderer>().bounds.extents.y
			+ selected.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size.y; ;
			selected.transform.position = node.transform.position + new Vector3(0, posY, 0);
			yield return new WaitForSeconds(selected.waitBetweenWalk);
		}

		selected.movesLeft -= depthFirst.costSoFar[path[0].coord];
		depthFirst.Reset();
		depthFirst.GetGrid(selected.GetCurrentCell().coord, selected.movesLeft);
		depthFirst.ColorGrid();
		path.Clear();
		selected.walking = false;
	}

	public void SelectUnit(Character character)
	{
		selected = character;
		depthFirst.Reset();
		depthFirst.GetGrid(selected.GetCurrentCell().coord, selected.movesLeft);
	}

}
