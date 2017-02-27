using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
	public LayerMask layerMask;
	public int visibilityRange;
	CharacterHandler charHandler;
	List<Character> viableTargets = new List<Character>();

	// Use this for initialization
	void Start()
	{
		charHandler = GameObject.Find("Core").GetComponent<CharacterHandler>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void DoActions()
	{
		GetViableTargets();
	}

	void GetViableTargets()
	{
		//TODO fix get targets
		foreach (Character character in charHandler.characters)
		{
			Ray ray = new Ray(transform.position, character.transform.position - transform.position);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, visibilityRange, layerMask))
			{
				viableTargets.Add(hit.collider.GetComponent<Character>());
			}
		}
	}
}
