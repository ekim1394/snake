using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

	// Food Prefab
	public GameObject foodPrefab;

	//Borders
	public Transform wallTop;
	public Transform wallBottom;
	public Transform wallRight;
	public Transform wallLeft;

	// Spawn Food
	void Spawn() {
		// Pick X Y within borders
		int x = (int)Random.Range (wallLeft.position.x, wallRight.position.x);
		int y = (int)Random.Range (wallTop.position.y, wallBottom.position.y);

		Instantiate (foodPrefab,
			new Vector2 (x, y),
			Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		// Spawn food every four seconds
		InvokeRepeating ("Spawn", 3, 4);
	}

}
