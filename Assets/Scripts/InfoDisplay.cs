using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour {
    public static InfoDisplay id;
    public Text displayTxt;
    public Text[] itemListing = new Text[4];
    string[] itemsShown = new string[9];
    public Image img;
    public bool UIActive = false;
    public Structure currentStructure;
    //UI states: 0 is default (inactive), 1 is upgradeScreen, 2 is upgradeWeapon, 3 is combineItems.
    public int UIstate = 0;
    public int UISelector = 0;
    public bool upgrade = true;
    public int currentScreen = 0;
    public bool swordSelected = true;

    //In order: Determines current menu value, last menu value, amount of items in the set, and how many items will be displayed.
    public int currentPointer, lastPointer, menuSize, itemsDisplayed;

    //Affected by menuSize, this determines which menu options are available.
    public int maxMenuItem, minMenuItem;

    public Pointer p;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Controller.Instance.specialItemList[i, j] = -1;
            }
        }
        generateItemList();
	}
	
	// Update is called once per frame
	void Update () {
        if (!UIActive)
        {
            displayTxt.gameObject.SetActive(false);
            img.gameObject.SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                itemListing[i].gameObject.SetActive(false);
            }
        }
        else
        {
            displayTxt.gameObject.SetActive(true);
            img.gameObject.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                itemListing[i].gameObject.SetActive(true);
            }
        }
        if (UIstate == 1)
        {
            upgradeScreen(currentStructure);
        }
        if (UIstate == 2)
        {
            upgradeWeapon(currentStructure);
        }
        if (UIstate == 3)
        {
            combineItems();
        }
	}

    void Awake()
    {
        if (id == null)
        {
            DontDestroyOnLoad(gameObject);
            id = this;
        }
        else if (id != this)
        {
            Destroy(gameObject);
        }
    }

    public void upgradeScreen(Structure str)
    {
        //Bring up the UI. From there, what should happen is as follows:
        //Print out on that UI the two options are 'Upgrade' or 'Experiment'.
        //If the option selected is 'Upgrade', show the upgradeReqs values and prompt next.
        //If it's 'Experiment', prompt to select a material that the player has discovered.
        
        displayTxt.text = "Upgrade\t\tExperiment";
        if (currentScreen == 0)
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
                    upgrade = true;
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
									displayTxt.text = "That didn't work.";
                                }
                            }
                            else
                            {
                                //Return to last menu.
                                moveNext1 = !moveNext1;
                            }
                            if (Input.GetKeyDown(KeyCode.X))
                            {
                                //Return to last menu.
                                moveNext1 = !moveNext1;
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            //Return to last menu.
                            moveNext1 = !moveNext1;
                        }
                    }
                }
                else
                {
                    currentScreen = 2;
                    //This is the 'Experiment' interface.
                    generateItemList();
                    displayTxt.text = "Which item would you like to use?";
                    itemsDisplayed = 3;
                    minMenuItem = 0;
                    maxMenuItem = 3;
                    menuSize = 8;
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Return to last menu.
                currentScreen = 0;
            }
        }
        if (currentScreen == 1)
        {
            displayTxt.text = "To upgrade this, you need the following materials:\nWood: " + str.upgradeReqs[0] + "\tStone: " + str.upgradeReqs[1] + "\nMetal: " + str.upgradeReqs[2] + "\tWater: " + str.upgradeReqs[3] + "\nWould you like to proceed?\nYes\tNo";
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
                        //Print a success message
                        UIstate = 0;
                        UIActive = false;
                        currentStructure = null;
                    }
                    else
                    {
                        //Spit out an error message.
                    }
                }
                else
                {
                    //Return to last menu.
                    currentScreen = 0;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    //Return to last menu.
                    currentScreen = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Return to last menu.
                currentScreen = 0;
            }
        }
        if (currentScreen == 2)
        {
            for (int i = 0; i < itemListing.Length; i++)
            {
                itemListing[i].text = itemsShown[i + minMenuItem];
            }
            move1D();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //Using UIselector, check this versus the structureID on the table that the struct has. If success, apply upgrade. If fail, well... Yeah.
                for (int i = 0; i < currentStructure.viableUpgrades.Length; i++)
                {
                    if (currentStructure.viableUpgrades[i] == UISelector)
                    {
                        currentStructure.upgradeID = UISelector;
                        UIstate = 0;
                        UIActive = false;
                        currentStructure = null;
                    }
                    else
                    {
                        //Spit out an error message.
                    }
                }
            }
        }
    }

    public void upgradeWeapon(Structure str)
    {
        //Bring up the UI. From there, what should happen is as follows:
        //Print out on that UI to select either the Sword or Bow/Arrow.
        //If the furnace has the freezing upgrade, apply that effect to the weapon.
        //Else, treat it like default upgrade for structures.
        displayTxt.text = "Sword\tBow and Arrow";
        swordSelected = true;
        if (currentScreen == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                swordSelected = !swordSelected;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (swordSelected)
                {
                    displayTxt.text = "To upgrade this, you need " + Controller.Instance.swordDetails[2] + " metal. Proceed?";
                    
                    upgrade = true;
                    if (currentScreen == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            upgrade = !upgrade;
                        }
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            if (upgrade)
                            {
                                if (Controller.Instance.metalCount >= Controller.Instance.swordDetails[2])
                                {
                                    Controller.Instance.metalCount -= Controller.Instance.swordDetails[2];
                                    //Upgrade the weapon.
                                    //Bump the cost up.
                                }
                                else
                                {
									displayTxt.text = "Doesn't seem like anything changed...";
                                }
                            }
                            else
                            {
                                //Return to last menu.
                                currentScreen = 0;
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            //Return to last menu.
                            currentScreen = 0;
                        }
                    }
                }
                else
                {
                    //Here, it's the bow. Thus, wood.
                    displayTxt.text = "To upgrade this, you need " + Controller.Instance.bowDetails[2] + " wood. Proceed?";
                    
                    upgrade = true;
                    if (currentScreen == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            upgrade = !upgrade;
                        }
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            if (upgrade)
                            {
                                if (Controller.Instance.woodCount >= Controller.Instance.bowDetails[2])
                                {
                                    Controller.Instance.woodCount -= Controller.Instance.bowDetails[2];
                                    //Upgrade the weapon.
                                    //Bump the cost up.
                                }
                                else
                                {
									displayTxt.text = "The bow doesn't look any different.";
                                }
                            }
                            else
                            {
                                //Return to last menu.
                                currentScreen = 0;
                            }
                        }
                        if (Input.GetKeyDown(KeyCode.X))
                        {
                            //Return to last menu.
                            currentScreen = 0;
                        }
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Return to last menu.
                currentScreen = 0;
            }
        }
    }

    public void combineItems()
    {
        //In order: Select the rune you want to test, then the material you want to test it on, then the quantity.
        //If the first two match with the table's values, add the third value to the corresponding index on the Controller.
        UISelector = 0;
        displayTxt.text = "Which rune would you like to use?\n";
        int tempRuneVal = -1;
        if (Controller.Instance.runesOwned[0])
        {
            displayTxt.text += "Se Rune\n";
        }
        else
        {
            displayTxt.text += "???\n";
        }
        if (Controller.Instance.runesOwned[1])
        {
            displayTxt.text += "Au Rune\n";
        }
        else
        {
            displayTxt.text += "???\n";
        }
        if (Controller.Instance.runesOwned[2])
        {
            displayTxt.text += "Ni Rune\n";
        }
        else
        {
            displayTxt.text += "???\n";
        }
        if (Controller.Instance.runesOwned[3])
        {
            displayTxt.text += "Ka Rune\n";
        }
        else
        {
            displayTxt.text += "???";
        }
        itemsDisplayed = 3;
        minMenuItem = 0;
        maxMenuItem = 3;
        menuSize = 3;
        //Set the text to be rotated through as an array of strs.
        if (currentScreen == 0)
        {
            move1D();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (UISelector == 0 && Controller.Instance.runesOwned[0])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                }
                else if (UISelector == 1 && Controller.Instance.runesOwned[1])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                }
                else if (UISelector == 2 && Controller.Instance.runesOwned[2])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                }
                else if (UISelector == 3 && Controller.Instance.runesOwned[3])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                }
                else
                {
					displayTxt.text = "You do not have this Rune in your inventory.";
                }
            }
        }
        minMenuItem = 0;
        maxMenuItem = 3;
        menuSize = 3;
        //Set the text to be rotated through as an array of strs.
        if (currentScreen == 1)
        {
            move1D();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                //Run through the list in the parser; if the rune value and UISelector line up, succeed. Else, fail.
                for (int i = 0; i < TextFileParser.tfp.convertedItemList.Length; i++)
                {
                    if (tempRuneVal == TextFileParser.tfp.convertedItemList[i,0] && UISelector == TextFileParser.tfp.convertedItemList[i, 1])
                    {
                        Controller.Instance.specialItemList[tempRuneVal, UISelector] += 1;
                        break;
                    }
                    else
                    {
                        //Spit out an error message.
                    }
                }
                if (UISelector == 0)
                {
                    Controller.Instance.woodCount--;
                }
                if (UISelector == 1)
                {
                    Controller.Instance.stoneCount--;
                }
                if (UISelector == 2)
                {
                    Controller.Instance.metalCount--;
                }
                if (UISelector == 3)
                {
                    Controller.Instance.waterCount--;
                }
            }
        }
        UIstate = 0;
        currentStructure = null;
        UIActive = false;
    }

    public void generateItemList()
    {
        //Now we cycle through the items and generate a text string to show to the player. Or, more accurately, a list of strings to show.
        int[] itemListFull = new int[9];
        for (int i = 0; i < 9; i++)
        {
            itemListFull[i] = Controller.Instance.specialItemList[TextFileParser.tfp.convertedItemList[i, 0], TextFileParser.tfp.convertedItemList[i, 1]];
        }

        string[] items = Controller.Instance.sItemListNames.Split(new string[] { "\t" }, System.StringSplitOptions.None);

        for (int i = 0; i < 9; i++)
        {
            if (itemListFull[i] > -1)
            {
                itemsShown[i] = items[i] + " x" + itemListFull[i];
            }
            else
            {
                itemsShown[i] = "???";
            }
        }
    }

    public void move1D()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (UISelector == 0)
            {
                UISelector = menuSize;
                //If there are more items in the menu than are present, change the items presented on screen.
                //Example: if there are 4 items available and 3 is the max shown, then this moves to item 4, sets that as the bottom item, and 
                //turns max to 3, min to 1. 0 counts as an item, and this also makes it easier to pull the index in the array.
                if (menuSize > maxMenuItem)
                {
                    maxMenuItem = menuSize;
                    minMenuItem = menuSize - itemsDisplayed;
                }
                //Otherwise, just show what items exist here and jump to the position of the aforementioned index. It updates on frame, anyhow.
            }
            else
            {
                //Check if we're at the top of the menu, and update the options if we are.
                if (UISelector == minMenuItem)
                {
                    minMenuItem--;
                    maxMenuItem--;
                }
                UISelector--;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (UISelector == menuSize)
            {
                UISelector = 0;
                //As in the first half, just check the menu size and fiddle with this accordingly.
                if (menuSize == maxMenuItem)
                {
                    maxMenuItem = itemsDisplayed;
                    minMenuItem = 0;
                }
            }
            else
            {
                //Check if we're at the bottom of the menu, and update the options if we are.
                if (UISelector == maxMenuItem)
                {
                    minMenuItem++;
                    maxMenuItem++;
                }
                UISelector++;
            }
        }
        p.pPos = UISelector - minMenuItem;
        //And finally, we update the pointer's Y coordinate to line up with that of the given index.
    }
}
