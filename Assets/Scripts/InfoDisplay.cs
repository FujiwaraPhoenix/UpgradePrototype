using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour {
    public static InfoDisplay id;
    public Text displayTxt;
    public Image img;
    public bool UIActive = false;
    public Structure currentStructure;
    //UI states: 0 is default (inactive), 1 is upgradeScreen, 2 is upgradeWeapon, 3 is combineItems.
    public int UIstate = 0;
    public int UISelector = 0;
    public bool upgrade = true;
    public bool moveNext = false;
    public bool moveNext1 = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!UIActive)
        {
            displayTxt.gameObject.SetActive(false);
            img.gameObject.SetActive(false);
        }
        else
        {
            displayTxt.gameObject.SetActive(true);
            img.gameObject.SetActive(true);
        }
        if (UIstate == 1)
        {
            upgradeScreen(currentStructure);
            UIstate = 0;
            currentStructure = null;
        }
	}

    public void upgradeScreen(Structure str)
    {
        //Bring up the UI. From there, what should happen is as follows:
        //Print out on that UI the two options are 'Upgrade' or 'Experiment'.
        //If the option selected is 'Upgrade', show the upgradeReqs values and prompt next.
        //If it's 'Experiment', prompt to select a material that the player has discovered.
        moveNext = false;
        displayTxt.text = "Upgrade\tExperiment";
        while (!moveNext)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                upgrade = !upgrade;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (upgrade)
                {
                    displayTxt.text = "To upgrade this, you need the following materials:\nWood: " + str.upgradeReqs[0] + "\tStone: " + str.upgradeReqs[1] + "\nMetal: " + str.upgradeReqs[2] + "\tWater: " + str.upgradeReqs[3] + "\nWould you like to proceed?\nYes\tNo";
                    moveNext1 = false;
                    while (!moveNext1)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            upgrade = !upgrade;
                        }
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            if (upgrade)
                            {
                                if ((Controller.Instance.woodCount >= str.upgradeReqs[0]) && (Controller.Instance.stoneCount >= str.upgradeReqs[1]) && (Controller.Instance.metalCount >= str.upgradeReqs[2]) && (Controller.Instance.waterCount >= str.upgradeReqs[3]))
                                {
                                    Controller.Instance.woodCount -= str.upgradeReqs[0];
                                    Controller.Instance.stoneCount -= str.upgradeReqs[1];
                                    Controller.Instance.metalCount -= str.upgradeReqs[2];
                                    Controller.Instance.waterCount -= str.upgradeReqs[3];
                                    //Upgrade the structure.
                                }
                                else
                                {
                                    //Spit out an error message.
                                }
                            }
                            else
                            {
                                //Return to last menu.
                            }
                        }
                    }
                }
                else
                {
                    //This is the 'Experiment' interface.
                }
                moveNext = !moveNext;
            }
        }
    }

    public void upgradeWeapon(Structure str)
    {
        //Bring up the UI. From there, what should happen is as follows:
        //Print out on that UI to select either the Sword or Bow/Arrow.
        //If the furnace has the freezing upgrade, apply that effect to the weapon.
        //Else, treat it like default upgrade for structures.
    }

    public void combineItems()
    {
        //In order: Select the rune you want to test, then the material you want to test it on, then the quantity.
        //If the first two match with the table's values, add the third value to the corresponding index on the Controller.

    }
}
