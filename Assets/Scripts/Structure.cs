using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour {
    //Structures are as follows: Walls, Furnace, Bed, Chest, Rune Circle. 0-4 in order.
    //To note: The weird materials are as follows: Wood B/D. Stone B/D, Water A/B/C, Metal A/C.
    //In order for upgrade ID, they are 0-8.
    public int structureID, upgradeID;
    public int currentHP;
    public int maxHP;
    public float invulnTimer = 0;
    public int upgradeLv;
    public int maxUpgradeLv;
    //In order: Wood, Stone, Metal, Water.
    public int[] upgradeReqs = new int[4];
    public Sprite[] possibleSprites = new Sprite[6];
    //True is use normally, false is upgrade.
    public bool optionSelect;
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
        updateSprites();
	}

    public void structureAction()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            //Wall
            if (structureID == 0)
            {
                promptUpgrade();
            }
            if (structureID == 1)
            {
                chooseOption();
            }
            if (structureID == 2)
            {
                chooseOption();
            }
            if (structureID == 3)
            {
                    promptUpgrade();
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
            Controller.Instance.UIActive = true;
            InfoDisplay.id.currentStructure = this;
            InfoDisplay.id.UIActive = true;
            InfoDisplay.id.currentScreen = 0;
            InfoDisplay.id.UIstate = 4;
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
            InfoDisplay.id.UISelector = 0;
            InfoDisplay.id.p.pPos = 0;
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

    public void updateSprites()
    {
        //Wall
        if (structureID == 0)
        {
            if (upgradeLv == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[0];
            }
            if (upgradeLv == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[1];
            }
            if (upgradeLv == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[2];
            }
        }

        //Furnace
        if (structureID == 1)
        {
            if (upgradeLv == 1)
            {
                if (upgradeID == -1)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[0];
                }
                if (upgradeID == 6 || upgradeID == 8)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[1];
                }
                if (upgradeID == 5)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[2];
                }
            }
            if (upgradeLv == 2)
            {
                if (upgradeID == -1)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[3];
                }
                if (upgradeID == 6 || upgradeID == 8)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[4];
                }
                if (upgradeID == 5)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[5];
                }
            }
        }
        //Bed
        if (structureID == 2)
        {
            if (upgradeLv == 1)
            {
                if (upgradeID == -1)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[0];
                }
                if (upgradeID == 3)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[1];
                }
            }
            if (upgradeLv == 2)
            {
                if (upgradeID == -1)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[2];
                }
                if (upgradeID == 3)
                {
                    this.GetComponent<SpriteRenderer>().sprite = possibleSprites[3];
                }
            }
        }
        //Chest
        if (structureID == 3)
        {
            if (upgradeLv == 1)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[0];
            }
            if (upgradeLv == 2)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[1];
            }
            if (upgradeLv == 3)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[2];
            }
            if (upgradeID == 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[3];
            }
            if (upgradeID == 5)
            {
                this.GetComponent<SpriteRenderer>().sprite = possibleSprites[4];
            }
        }
    }
    
}
