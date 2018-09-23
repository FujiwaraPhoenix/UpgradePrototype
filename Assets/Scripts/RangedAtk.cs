using UnityEngine;
using System.Collections;

public class RangedAtk : MonoBehaviour {
	public Vector3 direction;
	public float speed;
	float startTime;
	float endTime = 4f;

	public GameObject enemyPrefab;
	public GameObject controller;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += (direction * speed * Time.deltaTime);


		if (Time.time > startTime + endTime) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "enemy") {
			Debug.Log ("hit");

			enemyPrefab.gameObject.GetComponent <Enemy> ().hp = 
				enemyPrefab.gameObject.GetComponent <Enemy> ().hp - Controller.Instance.bowDetails [0];

			Destroy (this.gameObject);

		}

	}
}
