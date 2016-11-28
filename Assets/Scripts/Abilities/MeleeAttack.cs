using UnityEngine;
using System.Collections;
using System;

public class MeleeAttack : Ability
{
	void Awake()
	{
		abilityName = "Melee Attack";
	}

	public override void Execute()
	{
		charHandler.abilityRunning = true;
		StartCoroutine(Target());
	}

	public override IEnumerator Target()
	{
		while (charHandler.abilityRunning == true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 1000))
				{
					Debug.Log(hit.collider.name);
					charHandler.abilityRunning = false;
				}
			}
			yield return null;
		}
	}
}