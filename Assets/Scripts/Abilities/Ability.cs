using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour, IAction
{
	protected CharacterHandler charHandler;
	protected TargetingHandler targetingHandler;
	public string abilityName;
	public int damage;
	public int range;

	public abstract void Execute();

	void Start()
	{
		charHandler = GameObject.Find("World Data").GetComponent<CharacterHandler>();
		targetingHandler = GameObject.Find("World Data").GetComponent<TargetingHandler>();
	}

	//public IEnumerator Target()
	//{
	//	while (charHandler.abilityRunning == true)
	//	{
	//		if (Input.GetMouseButtonDown(0))
	//		{
	//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	//			RaycastHit hit;
	//			if (Physics.Raycast(ray, out hit, 1000))
	//			{
	//				Debug.Log(hit.collider.name);
	//				charHandler.abilityRunning = false;
	//			}
	//		}
	//		yield return null;
	//	}
	//}

}