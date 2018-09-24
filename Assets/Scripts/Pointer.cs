using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public int pPos = 0;
    public float xOffset, topOffsetA, topOffsetB;
    public int[] newXpos = new int[10];

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (InfoDisplay.id.UIstate == 1)
        {
            if (InfoDisplay.id.currentScreen == 0)
            {
                transform.localPosition = new Vector2(newXpos[pPos], InfoDisplay.id.displayTxt.transform.localPosition.y - 2.5f);
            }
            if (InfoDisplay.id.currentScreen == 1)
            {
                transform.localPosition = new Vector2(newXpos[pPos+2], InfoDisplay.id.displayTxt.transform.localPosition.y - 212.5f);
            }
            if (InfoDisplay.id.currentScreen == 2)
            {
                transform.localPosition = new Vector2(xOffset, InfoDisplay.id.itemListing[pPos].transform.localPosition.y - 2.5f);
            }
        }
        if (InfoDisplay.id.UIstate == 2)
        {
            if (InfoDisplay.id.currentScreen == 0)
            {
                transform.localPosition = new Vector2(newXpos[pPos + 4], InfoDisplay.id.displayTxt.transform.localPosition.y - 42.5f);
            }
            if (InfoDisplay.id.currentScreen == 1 || InfoDisplay.id.currentScreen == 2)
            {
                transform.localPosition = new Vector2(newXpos[pPos + 8], InfoDisplay.id.displayTxt.transform.localPosition.y - 80f);
            }
        }
		if (InfoDisplay.id.UIstate == 3)
        {
            transform.localPosition = new Vector2(xOffset, InfoDisplay.id.itemListing[pPos].transform.localPosition.y - 2.5f);
        }
        if (InfoDisplay.id.UIstate == 4)
        {
            transform.localPosition = new Vector2(newXpos[pPos+6], InfoDisplay.id.displayTxt.transform.localPosition.y -42.5f);
        }
    }
}
