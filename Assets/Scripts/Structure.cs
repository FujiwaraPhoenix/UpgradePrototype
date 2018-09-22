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
    public float invulnTimer = 0;
    public int upgradeLv;
    //In order: Wood, Stone, Metal, Water.
    public int[] upgradeReqs = new int[4];
    //True is use normally, false is upgrade.
    public bool optionSelect;
    public bool optionChosen = false;

    public int hpHealed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        structureAction();
        if (invulnTimer > 0)
        {
            invulnTimer -= Time.deltaTime;
        }
	}

    public void structureAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (structureID == 0 || structureID == 3)
            {
                promptUpgrade();
            }
            if (structureID == 1 || structureID == 2)
            {
                chooseOption();
            }
            if (structureID == 4)
            {
                promptCombo();
            }
        }
    }

    public void chooseOption()
    {
        //Only one. Interact and choose: Sleep or upgrade/experiment.
        Collider2D[] checkForPC = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size*1.25f, 0);
        foreach (Collider2D itemCollider in checkForPC)
        {
            if (itemCollider.GetComponent<PlayerController>() != null)
            {
                optionChosen = false;
                while (!optionChosen)
                {
                    //All of these have 2 options: Either use it as it is or prompt the upgrade.
                    if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        optionSelect = !optionSelect;
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        if (optionSelect)
                        {
                            useStructure();
                        }
                        else
                        {
                            promptUpgrade();
                        }
                        optionChosen = true;
                    }
                }
            }
        }
    }

    public void promptUpgrade()
    {
        Collider2D[] checkForPC = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size * 1.25f, 0);
        foreach (Collider2D itemCollider in checkForPC)
        {
            if (itemCollider.GetComponent<PlayerController>() != null)
            {
                InfoDisplay.id.UIActive = true;
                InfoDisplay.id.currentStructure = this;
                InfoDisplay.id.UIstate = 1;
            }
        }
    }

    public void promptCombo()
    {

    }

    public void useStructure()
    {
        //Furnace upgrade prompt.
        if (structureID == 1)
        {
            InfoDisplay.id.upgradeWeapon(this);
        }
        //Use the bed.
        if (structureID == 2)
        {
            if (upgradeID == 3)
            {
                PlayerController.pc.HP = PlayerController.pc.maxHP + 2;
            }
            else
            {
                PlayerController.pc.HP = PlayerController.pc.maxHP;
            }
        }
    }

    
}
