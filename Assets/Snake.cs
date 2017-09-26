using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour {

	public GameObject tailPrefab;
	public float speed;
	//Default moves up
	Vector2 dir = Vector2.up;

	bool ate = false;
	bool lose = false;

	// Keep track of tail
	List<Transform> tail = new List<Transform>();

	// Use this for initialization
	void Start () {
		// Move Snake every 300ms
		InvokeRepeating("Move", speed, speed);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.DownArrow) && dir != Vector2.up)
			dir = Vector2.down;
		else if (Input.GetKey (KeyCode.RightArrow) && dir != Vector2.left)
			dir = Vector2.right;
		else if (Input.GetKey (KeyCode.LeftArrow)  && dir != Vector2.right)
			dir = Vector2.left;
		else if (Input.GetKey (KeyCode.UpArrow)  && dir != Vector2.down)
			dir = Vector2.up;

	}

	void Move(){
		// Save Current Position
		Vector2 v = transform.position;

		// Moving head
		if (!lose){
			transform.Translate (dir);
		}else {
			Vector3 reset = new Vector3 (0, 15, 0);
			transform.position = reset;
			lose = false;
			dir = Vector2.up;
		}
		// Increase tail
		if (ate) {
			GameObject g = (GameObject)Instantiate (tailPrefab,
				               v,
				               Quaternion.identity);
			g.tag = "tail";

			tail.Insert (0, g.transform);
			ate = false;
		}

		if (tail.Count > 0) {
			tail.Last ().position = v;

			tail.Insert (0, tail.Last ());
			tail.RemoveAt (tail.Count - 1);
		}

	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.name.StartsWith ("FoodPrefab")) {
			ate = true;

			Destroy (coll.gameObject);
		} else {
		// Game Over
			lose=true;
			tail.Clear();
			foreach (GameObject item in GameObject.FindGameObjectsWithTag("tail")) {
				Destroy (item);
			}
		}
	}
}
