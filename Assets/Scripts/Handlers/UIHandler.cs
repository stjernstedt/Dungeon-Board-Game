using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIHandler : MonoBehaviour, IInitializeable
{
	public GameObject actionsPanel;
	Prefabs prefabs;

	public void Init()
	{
		prefabs = GetComponent<Prefabs>();
	}

	public void PopulatePanel(Character character)
	{
		//foreach (IAction action in character.actions)
		//{
		//	GameObject button = Instantiate(prefabs.actionButton);
		//	button.GetComponentInChildren<Text>().text = action.actionName;
		//	button.transform.SetParent(actionsPanel.transform);
		//}

		foreach (Ability ability in character.abilities)
		{
			GameObject button = Instantiate(prefabs.actionButton);
			button.GetComponentInChildren<Text>().text = ability.abilityName;
			button.GetComponent<ButtonAbility>().action = ability;
			button.transform.SetParent(actionsPanel.transform);
		}

	}
}