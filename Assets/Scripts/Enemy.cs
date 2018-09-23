using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int hp;
    public int dmg;
    public float mvtSpd;

    public Rigidbody2D rb;

	public GameObject controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        chase();
		death();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Controller.Instance.invulnTimer <= 0)
            {
                PlayerController.pc.HP -= dmg;
                Controller.Instance.invulnTimer = 2f;
            }
        }
        else if (collision.gameObject.GetComponent<Structure>() != null)
        {
            if (collision.gameObject.GetComponent<Structure>().structureID == 0)
            {
                collision.gameObject.GetComponent<Structure>().currentHP -= dmg;
                Controller.Instance.invulnTimer = 2f;
            }
        }

		else if (collision.gameObject.tag == "rangedAtk") {
			Debug.Log ("hit");

			hp = hp - Controller.Instance.bowDetails [0];

			Destroy (collision.gameObject);

		}

    }

    public void chase()
    {
        Vector3 distToPlayer = new Vector2(PlayerController.pc.transform.position.x - transform.position.x, PlayerController.pc.transform.position.y - transform.position.y);
        if (distToPlayer.magnitude < 10f)
        {
            distToPlayer = distToPlayer.normalized;
            transform.position += distToPlayer * mvtSpd * Time.deltaTime;
        }
    }

	public void death(){

		if (hp <= 0) {
			Destroy (this.gameObject);
		}

	}
		
}
