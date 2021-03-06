﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInitializer : MonoBehaviour
{
	Prefabs prefabs;
	CharacterHandler charHandler;
	DepthFirst depthFirst;
	CharacterFactory charFactory;
	UIHandler uiHandler;
	TurnHandler turnHandler;

	// Use this for initialization
	void Awake()
	{
		prefabs = GetComponent<Prefabs>();
		charHandler = GetComponent<CharacterHandler>();
		depthFirst = GetComponent<DepthFirst>();
		charFactory = GetComponent<CharacterFactory>();
		uiHandler = GetComponent<UIHandler>();
		turnHandler = GetComponent<TurnHandler>();

		MapGenerator mapGen = GetComponent<MapGenerator>();
		mapGen.CreateMap();
		depthFirst.Init();
		charHandler.Init();
		charFactory.Init();
		uiHandler.Init();
		turnHandler.Init();

		GameObject startRoom = GameObject.Find("Room0");
		Vector3 pos = startRoom.GetComponent<Room>().GetCenter();
		GameObject player = charFactory.CreateCharacter(prefabs.player, pos.x, pos.z);
		charHandler.SelectUnit(player.GetComponent<Character>());
		Camera.main.transform.position = new Vector3(player.transform.position.x, Camera.main.transform.position.y, player.transform.position.z - 4);

		Vector3 goblinRoom = GameObject.Find("Room1").GetComponent<Room>().GetCenter();
		GameObject goblin = charFactory.CreateCharacter(prefabs.goblin, goblinRoom.x, goblinRoom.z);

		charHandler.characters.Add(player.GetComponent<Character>());
		charHandler.enemies.Add(goblin.GetComponent<Character>());

		player.AddComponent<MeleeAttack>();
		uiHandler.PopulatePanel(player.GetComponent<Character>());
		turnHandler.FirstTurn();
	}
}