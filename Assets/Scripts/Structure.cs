using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {
    //Structures are as follows: Walls, Furnace, Bed, Chest, Rune Circle. 0-4 in order.
    //To note: The weird materials are as follows: Wood B/D. Stone B/D, Water A/B/C, Metal A/C.
    //In order for upgrade ID, they are 0-9.
    public int structureID, upgradeID;
    public int currentHP;
    public int maxHP;
    public int[] upgradeReqs = new int[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void structureAction()
    {
        if (structureID == 0)
        {
            wall();
        }
        if (structureID == 1)
        {
            furnace();
        }
        if (structureID == 2)
        {
            bed();
        }
        if (structureID == 3)
        {
            chest();
        }
    }

    public void wall()
    {
        //Each wall is unique for the sake of this prototype.
        //You can only upgrade/experiment on it.
    }
    public void furnace()
    {
        //Only one furnace. Upgrade weapon or upgrade/experiment.
    }
    public void bed()
    {
        //Interact and choose: Sleep or upgrade/experiment.
    }
    public void chest()
    {
        //Honestly, this doesn't do anything lol. Give it the upgrade prompt.
    }
    public void runeCircle()
    {
        //Cannot be upgraded. Select material, select quantity, select rune. 
    }

    public void promptUpgrade()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Collider2D[] checkForItem = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
            foreach (Collider2D itemCollider in checkForItem)
            {
                if (itemCollider.GetComponent<PlayerController>() != null)
                {
                    InfoDisplay.id.upgradeScreen();
                }
            }
        }
    }
}
