using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public int moves = 5;
	public int movesLeft;
	public float waitBetweenWalk = 0.2f;

	public bool walking = false;
	public LayerMask ignoreLayers;

	GameObject worldData;

	// Use this for initialization
	void Start()
	{
		worldData = GameObject.Find("World Data");
	}

	public Node GetCurrentCell()
	{
		Node node = null;
		Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100f, ignoreLayers))
		{
			node = hit.collider.GetComponent<Node>();
		}

		return node;
	}

	public void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			worldData.GetComponent<CharacterHandler>().SelectUnit(this);
		}
	}
}
