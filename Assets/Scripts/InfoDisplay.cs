using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour {
    public static InfoDisplay id;
    public Text displayTxt;
    public Text[] itemListing = new Text[4];
    public Text feedbackMsg;
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
    public float fadeoutTime;

    int tempRuneVal = 0;

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
            p.gameObject.SetActive(false);
        }
        else
        {
            displayTxt.gameObject.SetActive(true);
            img.gameObject.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                itemListing[i].gameObject.SetActive(true);
            }
            p.gameObject.SetActive(true);
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
        if (feedbackMsg.IsActive())
        {
            if (fadeoutTime > 0)
            {
                fadeoutTime -= Time.deltaTime;
            }
            else
            {
                feedbackMsg.gameObject.SetActive(false);
            }
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
        if (currentScreen == 0)
        {
            displayTxt.text = "Upgrade\t\tExperiment";
            if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.RightArrow)))
            {
                upgrade = !upgrade;
                p.pPos++;
                if (p.pPos > 1)
                {
                    p.pPos = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (upgrade)
                {
                    currentScreen = 1;
                    p.pPos = 0;
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
                    p.pPos = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                //End it all.
                UIstate = 0;
                UIActive = false;
                currentStructure = null;
                Controller.Instance.UIActive = false;
            }
        }
        if (currentScreen == 1)
        {
            displayTxt.text = "To upgrade this, you need the\nfollowing materials:\nWood: " + str.upgradeReqs[0] + "\tStone: " + str.upgradeReqs[1] + "\nMetal: " + str.upgradeReqs[2] + "\tWater: " + str.upgradeReqs[3] + "\nWould you like to proceed?\nYes\t\tNo";
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                upgrade = !upgrade;
                p.pPos++;
                if (p.pPos > 1)
                {
                    p.pPos = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.Z))
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
                        feedbackMsg.gameObject.SetActive(true);
                        feedbackMsg.text = "Success!";
                        UIstate = 0;
                        UIActive = false;
                        currentStructure = null;
                        Controller.Instance.UIActive = false;
                    }
                    else
                    {
                        feedbackMsg.gameObject.SetActive(true);
                        feedbackMsg.text = "You don't have enough resources for that.";
                        fadeoutTime = 4f;
                        UIstate = 0;
                        UIActive = false;
                        currentStructure = null;
                        Controller.Instance.UIActive = false;
                    }
                }
                else
                {
                    //Return to last menu.
                    currentScreen = 0;
                    p.pPos = 0;
                }
                if (Input.GetKeyUp(KeyCode.X))
                {
                    //Return to last menu.
                    currentScreen = 0;
                    p.pPos = 0;
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                //Return to last menu.
                currentScreen = 0;
                p.pPos = 0;
            }
        }
        if (currentScreen == 2)
        {
            for (int i = 0; i < itemListing.Length; i++)
            {
                itemListing[i].text = itemsShown[i + minMenuItem];
            }
            move1D();

            if (Input.GetKeyUp(KeyCode.Z))
            {
                //Using UIselector, check this versus the structureID on the table that the struct has. If success, apply upgrade. If fail, well... Yeah.
                for (int i = 0; i < currentStructure.viableUpgrades.Length; i++)
                {
                    if (currentStructure.viableUpgrades[i] == UISelector)
                    {
                        currentStructure.upgradeID = UISelector;
                        feedbackMsg.gameObject.SetActive(true);
                        feedbackMsg.text = "Success!";
                        UIstate = 0;
                        UIActive = false;
                        currentStructure = null;
                        Controller.Instance.UIActive = false;
                    }
                    else
                    {
                        feedbackMsg.gameObject.SetActive(true);
                        feedbackMsg.text = "Experimentation failed!";
                        UIstate = 0;
                        UIActive = false;
                        currentStructure = null;
                        Controller.Instance.UIActive = false;
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                //Return to last menu.
                currentScreen = 0;
                p.pPos = 0;
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
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                swordSelected = !swordSelected;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (swordSelected)
                {
                    displayTxt.text = "To upgrade this, you need " + Controller.Instance.swordDetails[2] + " metal. Proceed?";
                    
                    upgrade = true;
                    if (currentScreen == 0)
                    {
                        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            upgrade = !upgrade;
                        }
                        if (Input.GetKeyUp(KeyCode.Z))
                        {
                            if (upgrade)
                            {
                                if (Controller.Instance.metalCount >= Controller.Instance.swordDetails[2])
                                {
                                    Controller.Instance.metalCount -= Controller.Instance.swordDetails[2];
                                    //Upgrade the weapon.
                                    //Bump the cost up.
                                    feedbackMsg.gameObject.SetActive(true);
                                    feedbackMsg.text = "Success!";
                                    UIstate = 0;
                                    UIActive = false;
                                    currentStructure = null;
                                    Controller.Instance.UIActive = false;
                                }
                                else
                                {
                                    feedbackMsg.gameObject.SetActive(true);
                                    feedbackMsg.text = "Not enough metal.";
                                    UIstate = 0;
                                    UIActive = false;
                                    currentStructure = null;
                                    Controller.Instance.UIActive = false;
                                }
                            }
                            else
                            {
                                //Return to last menu.
                                currentScreen = 0;
                            }
                        }
                        if (Input.GetKeyUp(KeyCode.X))
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
                        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            upgrade = !upgrade;
                        }
                        if (Input.GetKeyUp(KeyCode.Z))
                        {
                            if (upgrade)
                            {
                                if (Controller.Instance.woodCount >= Controller.Instance.bowDetails[2])
                                {
                                    Controller.Instance.woodCount -= Controller.Instance.bowDetails[2];
                                    //Upgrade the weapon.
                                    //Bump the cost up.
                                    feedbackMsg.gameObject.SetActive(true);
                                    feedbackMsg.text = "Success!";
                                    UIstate = 0;
                                    UIActive = false;
                                    currentStructure = null;
                                    Controller.Instance.UIActive = false;
                                }
                                else
                                {
                                    feedbackMsg.gameObject.SetActive(true);
                                    feedbackMsg.text = "Not enough wood. Doesn't seem like anything changed.";
                                    UIstate = 0;
                                    UIActive = false;
                                    currentStructure = null;
                                    Controller.Instance.UIActive = false;
                                }
                            }
                            else
                            {
                                //Return to last menu.
                                currentScreen = 0;
                            }
                        }
                        if (Input.GetKeyUp(KeyCode.X))
                        {
                            //Return to last menu.
                            currentScreen = 0;
                        }
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                //Return to last menu.
                currentScreen = 0;
            }
        }
    }

    public void combineItems()
    {
        //In order: Select the rune you want to test, then the material you want to test it on.
        //If the first two match with the table's values, add the third value to the corresponding index on the Controller.
        displayTxt.text = "Which rune would you like to use?";
        if (Controller.Instance.runesOwned[0])
        {
            itemListing[0].text = "Se Rune";
        }
        else
        {
            itemListing[0].text = "???";
        }
        if (Controller.Instance.runesOwned[1])
        {
            itemListing[1].text = "Au Rune";
        }
        else
        {
            itemListing[1].text = "???";
        }
        if (Controller.Instance.runesOwned[2])
        {
            itemListing[2].text = "Ni Rune";
        }
        else
        {
            itemListing[2].text = "???";
        }
        if (Controller.Instance.runesOwned[3])
        {
            itemListing[3].text = "Ka Rune";
        }
        else
        {
            itemListing[3].text = "???";
        }
        itemsDisplayed = 3;
        minMenuItem = 0;
        maxMenuItem = 3;
        menuSize = 3;
        move1D();
        //Set the text to be rotated through as an array of strs.
        if (currentScreen == 0)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (UISelector == 0 && Controller.Instance.runesOwned[0])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                    UISelector = 0;
                }
                else if (UISelector == 1 && Controller.Instance.runesOwned[1])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                    UISelector = 0;
                }
                else if (UISelector == 2 && Controller.Instance.runesOwned[2])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                    UISelector = 0;
                }
                else if (UISelector == 3 && Controller.Instance.runesOwned[3])
                {
                    currentScreen = 1;
                    tempRuneVal = UISelector;
                    UISelector = 0;
                }
                else
                {
                    Debug.Log("You don't own that rune!");
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                //End it all.
                UIstate = 0;
                UIActive = false;
                currentStructure = null;
                Controller.Instance.UIActive = false;
            }
        }
        //Set the text to be rotated through as an array of strs.
        if (currentScreen == 1)
        {
            displayTxt.text = "Which material would you like to use?";
            itemListing[0].text = "Wood";
            itemListing[1].text = "Stone";
            itemListing[2].text = "Metal";
            itemListing[3].text = "Water";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((UISelector == 0 && Controller.Instance.woodCount > 0) || (UISelector == 1 && Controller.Instance.stoneCount > 0) || (UISelector == 2 && Controller.Instance.metalCount > 0) || (UISelector == 3 && Controller.Instance.waterCount > 3))
                {
                    currentScreen = 2;
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                //Back 1.
                currentScreen = 0;
            }
        }
        if (currentScreen == 2)
        {
            //Run through the list in the parser; if the rune value and UISelector line up, succeed. Else, fail.
            for (int i = 0; i < 10; i++)
            {
                if (tempRuneVal == TextFileParser.tfp.convertedItemList[i, 0] && UISelector == TextFileParser.tfp.convertedItemList[i, 1])
                {
                    if (Controller.Instance.specialItemList[tempRuneVal, UISelector] == -1)
                    {
                        Controller.Instance.specialItemList[tempRuneVal, UISelector] = 0;
                    }
                    Controller.Instance.specialItemList[tempRuneVal, UISelector] += 1;
                    fadeoutTime = 4f;
                    feedbackMsg.text = "Success! You've made a new item!";
                    break;
                }
                else
                {
                    //Spit out an error message.
                    if (i == 9)
                    {
                        fadeoutTime = 4f;
                        feedbackMsg.text = "Sorry! No dice!";
                    }
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

            UIstate = 0;
            currentStructure = null;
            UIActive = false;
            feedbackMsg.gameObject.SetActive(true);
            Controller.Instance.UIActive = false;
        }
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
