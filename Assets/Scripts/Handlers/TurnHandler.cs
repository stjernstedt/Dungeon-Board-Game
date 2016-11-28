using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnHandler : MonoBehaviour
{
	public Text turnText;
	CharacterHandler charHandler;
	int turnNr = 1;

	// Use this for initialization
	void Awake()
	{
		charHandler = GetComponent<CharacterHandler>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void EndTurn()
	{
		foreach (Character character in charHandler.characters)
		{
			character.movesLeft = character.moves;
		}

		charHandler.SelectUnit(charHandler.characters[0]);
		turnNr += 1;
		turnText.text = "Turn " + turnNr;
		turnText.gameObject.SetActive(true);
	}
}