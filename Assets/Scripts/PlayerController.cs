using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController pc;
    //Need HP.
    public int HP;

    public bool attacking;

    public float attackTimer, attackCD;

    public Rigidbody2D hitbox;

    public int mvtSpd;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveChar();
        getItem();
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
}
