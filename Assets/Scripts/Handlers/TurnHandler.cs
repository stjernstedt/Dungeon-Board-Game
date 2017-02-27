using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TurnHandler : MonoBehaviour, IInitializeable
{
	public Text turnText;
	CharacterHandler charHandler;
	int turnNr = 0;

	public void Init()
	{
		charHandler = GetComponent<CharacterHandler>();
	}

	public void FirstTurn()
	{
		charHandler.SelectUnit(charHandler.characters[0]);
		turnNr += 1;
		turnText.text = "Turn " + turnNr;
		turnText.gameObject.SetActive(true);
	}

	public void EndTurn()
	{
		foreach (Character character in charHandler.characters)
		{
			character.movesLeft = character.moves;
		}

		foreach (Character character in charHandler.enemies)
		{
			character.GetComponent<MonsterAI>().DoActions();
			character.movesLeft = character.moves;
		}

		charHandler.SelectUnit(charHandler.characters[0]);
		turnNr += 1;
		turnText.text = "Turn " + turnNr;
		turnText.gameObject.SetActive(true);
	}

}