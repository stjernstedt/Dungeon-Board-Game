using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour
{
	protected CharacterHandler charHandler;
	protected TargetingHandler targetingHandler;
	public string abilityName;
	public int damage;
	public int range;

	public abstract void Execute();
	public abstract void FireAt(GameObject target);

	void Start()
	{
		charHandler = GameObject.Find("World Data").GetComponent<CharacterHandler>();
		targetingHandler = GameObject.Find("World Data").GetComponent<TargetingHandler>();
	}

}