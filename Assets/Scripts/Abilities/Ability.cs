using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour, IAction
{
	protected CharacterHandler charHandler;
	public string abilityName;
	public int damage;

	public abstract void Execute();
	public abstract IEnumerator Target();

	void Start()
	{
		charHandler = GameObject.Find("World Data").GetComponent<CharacterHandler>();
	}
}