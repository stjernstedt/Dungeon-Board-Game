using UnityEngine;
using System.Collections;

public class ButtonAbility : MonoBehaviour
{

	public Ability action;

	public void Start()
	{
		//action = ScriptableObject.CreateInstance<Attack>();
	}

	public void DoAction()
	{
		action.Execute();
	}
}