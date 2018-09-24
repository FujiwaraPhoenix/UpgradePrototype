using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController pc;
    //Need HP.
    public int HP, maxHP;

    public bool attacking;

    public float attackTimer, attackCD;

    public Rigidbody2D hitbox;

    public int mvtSpd;

	public GameObject rangedAtkPrefab;
	public Transform rangedAtkSpawn;
	public int playerFacing;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Controller.Instance.UIActive)
        {
            moveChar();
            getItem();
            trackFacing();

            if (Input.GetKeyDown(KeyCode.X) && attacking == false)
            {
                rangedAtk();
                attacking = true;
            }
        }
	}

    void Awake()
    {
        if (pc == null)
        {
            DontDestroyOnLoad(gameObject);
            pc = this;
        }
        else if (pc != this)
        {
            Destroy(gameObject);
        }
    }

    public void moveChar()
    {
        Vector3 pDir = new Vector3(0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            pDir += new Vector3(0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pDir += new Vector3(0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pDir += new Vector3(-1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pDir += new Vector3(1, 0);
        }
        transform.position += pDir.normalized * mvtSpd* Time.deltaTime;
    }

    public void getItem()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            Collider2D[] checkForItem = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
            foreach (Collider2D itemCollider in checkForItem)
            {
                if (itemCollider.GetComponent<Item>() != null)
                {
                    Item item = itemCollider.GetComponent<Item>();
                    int itemID = item.itemType;
                    addItem(itemID);
                    Destroy(item.gameObject);
                }
            }
        }
    }

    public void addItem(int itemID)
    {
        if (itemID == 0)
        {
            Controller.Instance.woodCount++;
        }
        if (itemID == 1)
        {
            Controller.Instance.stoneCount++;
        }
        if (itemID == 2)
        {
            Controller.Instance.metalCount++;
        }
        if (itemID == 3)
        {
            Controller.Instance.waterCount++;
        }
        if (itemID == 4)
        {
            Controller.Instance.runesOwned[0] = true;
        }
        if (itemID == 5)
        {
            Controller.Instance.runesOwned[1] = true;
        }
        if (itemID == 6)
        {
            Controller.Instance.runesOwned[2] = true;
        }
        if (itemID == 7)
        {
            Controller.Instance.runesOwned[3] = true;
        }
    }

	public void trackFacing(){
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
	}
		

	public void rangedAtk(){
		
		GameObject Bullet = (GameObject)Instantiate (rangedAtkPrefab,transform.position,Quaternion.identity);
		Vector3 newDirection = new Vector2();
		if (playerFacing == 0) {
			newDirection.x = -1;
			newDirection.y = 0;
		} else if (playerFacing == 1) {
			newDirection.x = 0;
			newDirection.y = 1;
		} else if (playerFacing == 2) {
			newDirection.x = 1;
			newDirection.y = 0;
		} else if (playerFacing == 3) {
			newDirection.x = 0;
			newDirection.y = -1;
		}

		Bullet.GetComponent<RangedAtk>().direction = newDirection;	

		Invoke ("rangedAtkReset", 0.5f);
	}

	public void rangedAtkReset(){

		attacking = false;

	}
}
