using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingHandler : MonoBehaviour
{
	public GameObject targetingParticles;
	CharacterHandler charHandler;

	// Use this for initialization
	void Start()
	{
		charHandler = GetComponent<CharacterHandler>();
	}

	public void StartTargeting(Ability ability)
	{
		StartCoroutine(Target(ability));
	}

	IEnumerator Target(Ability ability)
	{
		List<GameObject> particlesList = new List<GameObject>();
		List<GameObject> viableTargets = new List<GameObject>();

		Vector3 pos = charHandler.selected.GetCurrentCell().coord;
		for (int x = (int)pos.x - ability.range; x <= pos.x + ability.range; x++)
		{
			for (int z = (int)pos.z - ability.range; z <= pos.z + ability.range; z++)
			{
				//HACK costly? rewrite?
				foreach (Character character in charHandler.characters)
				{
					if (character == charHandler.selected)
					{
						//if selected character do nothing else add particles
					}
					else if (character.transform.position.x == x && character.transform.position.z == z)
					{
						GameObject particles = Instantiate(targetingParticles);
						particles.transform.position = new Vector3(x, 0.1f, z);
						particlesList.Add(particles);
						viableTargets.Add(character.gameObject);
					}
				}

			}
		}
		while (charHandler.abilityRunning == true)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 1000))
				{
					//HACK change from fixed number to half model width
					float offset = 0.5f;
					foreach (Character character in charHandler.characters)
					{
						if (character != charHandler.selected)
						{
							Vector3 hitPos = new Vector3(hit.point.x, 0, hit.point.z);
							Vector3 charPos = new Vector3((int)character.transform.position.x, 0, (int)character.transform.position.z);

							//checks if distance between ray hit and character is less or equals to offset, which should be half of character width
							if (Mathf.Abs(hitPos.x - charPos.x) <= offset && Mathf.Abs(hitPos.z - charPos.z) <= offset && viableTargets.Contains(character.gameObject))
							{
								Debug.Log(character.name);
								ability.FireAt(character.gameObject);
							}
						}
					}
					//Debug.Log(hit.collider.name);
					charHandler.abilityRunning = false;
				}
			}
			yield return null;
		}

		Debug.Log(particlesList.Count);

		foreach (GameObject parts in particlesList)
		{
			parts.GetComponent<ParticleSystem>().Stop();
			Destroy(parts, 2);
		}
	}
}
