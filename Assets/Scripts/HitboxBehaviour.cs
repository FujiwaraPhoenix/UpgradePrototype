using UnityEngine;
using System.Collections;

public class HitboxBehaviour : MonoBehaviour {

	public bool canAttack;
	public bool attacking;
	public int playerFacing;

	public GameObject enemyPrefab;
	public GameObject controller;

	// Use this for initialization
	void Start () {
	
		canAttack = true;
		attacking = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow)) {
			playerFacing = 0;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			playerFacing = 1;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			playerFacing = 2;
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			playerFacing = 3;
		}
		
		if (Input.GetKeyDown (KeyCode.C) && canAttack == true && playerFacing == 0) {

			attacking = true;
			transform.position = new Vector2(transform.position.x - (1f), transform.position.y);
			canAttack = false;
			Invoke ("attackReset", 0.5f);

		}

		if (Input.GetKeyDown (KeyCode.C) && canAttack == true && playerFacing == 1) {

			attacking = true;
			transform.position = new Vector2(transform.position.x, transform.position.y + (1f));
			canAttack = false;
			Invoke ("attackReset", 0.5f);

		}

		if (Input.GetKeyDown (KeyCode.C) && canAttack == true && playerFacing == 2) {
			
			attacking = true;
			transform.position = new Vector2(transform.position.x + (1f), transform.position.y);
			canAttack = false;
			Invoke ("attackReset", 0.5f);

		}
			

		if (Input.GetKeyDown (KeyCode.C) && canAttack == true && playerFacing == 3) {
			
			attacking = true;
			transform.position = new Vector2(transform.position.x, transform.position.y - (1f));
			canAttack = false;
			Invoke ("attackReset", 0.5f);

		}
			

	}
		
	//what it sounds like
	void attackReset (){
		attacking = false;
		canAttack = true;
		transform.localPosition = Vector3.zero;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "enemy" && attacking == true){
		
			enemyPrefab.gameObject.GetComponent <Enemy> ().hp = 
				enemyPrefab.gameObject.GetComponent <Enemy> ().hp - controller.gameObject.GetComponent <Controller> ().swordDetails [0];

		}

	}


}
