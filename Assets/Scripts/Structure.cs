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
    public int[] viableUpgrades = new int[4];

    public int hpHealed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Controller.Instance.UIActive)
        {
            structureAction();
        }
        if (invulnTimer > 0)
        {
            invulnTimer -= Time.deltaTime;
        }
	}

    public void structureAction()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (structureID == 0 || structureID == 3)
            {

                promptUpgrade();
            }
            if (structureID == 1 || structureID == 2)
            {
                optionChosen = false;
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
        //Only one. Interact and choose: Sleep/upgrade wep or upgrade/experiment.
        Vector3 distToPlayer = new Vector2(PlayerController.pc.transform.position.x - transform.position.x, PlayerController.pc.transform.position.y - transform.position.y);
        if (distToPlayer.magnitude < 1.5f)
        {
            if (!optionChosen)
            {
                //All of these have 2 options: Either use it as it is or prompt the upgrade.
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    optionSelect = !optionSelect;
                }
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    optionChosen = true;
                }
            }
        }
        if (optionChosen)
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

    public void promptUpgrade()
    {
        Vector3 distToPlayer = new Vector2(PlayerController.pc.transform.position.x - transform.position.x, PlayerController.pc.transform.position.y - transform.position.y);
        if (distToPlayer.magnitude < 1.5f)
        {
            Controller.Instance.UIActive = true;
            InfoDisplay.id.UIActive = true;
            InfoDisplay.id.currentStructure = this;
            InfoDisplay.id.UIstate = 1;
        }
    }

    public void promptCombo()
    {
        Vector3 distToPlayer = new Vector2(PlayerController.pc.transform.position.x - transform.position.x, PlayerController.pc.transform.position.y - transform.position.y);
        if (distToPlayer.magnitude < 1.5f)
        {
            Controller.Instance.UIActive = true;
            InfoDisplay.id.UIActive = true;
            InfoDisplay.id.currentScreen = 0;
            InfoDisplay.id.UIstate = 3;
        }
    }

    public void useStructure()
    {
        //Furnace upgrade prompt.
        if (structureID == 1)
        {
            Controller.Instance.UIActive = true;
            InfoDisplay.id.UIActive = true;
            InfoDisplay.id.currentStructure = this;
            InfoDisplay.id.UIstate = 2;
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
