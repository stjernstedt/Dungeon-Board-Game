using UnityEngine;
using System.Collections;

public class CharacterFactory : MonoBehaviour, IInitializeable
{
	MapGenerator mapGen;

	// Use this for initialization
	void Awake()
	{

	}

	public void Init()
	{
		mapGen = GetComponent<MapGenerator>();
	}

	public GameObject CreateCharacter(GameObject prefab, float x, float z)
	{
		Vector3 pos = new Vector3(x, 0, z);

		GameObject character = Instantiate(prefab);
		pos.y = mapGen.tiles[pos].GetTop() + character.GetComponent<MeshRenderer>().bounds.extents.y
			+ character.transform.GetChild(0).GetComponent<MeshRenderer>().bounds.size.y;
		character.transform.position = pos;
		character.GetComponent<Character>().movesLeft = character.GetComponent<Character>().moves;

		return character;
	}
}