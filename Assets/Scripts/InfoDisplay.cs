using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour {
    public static InfoDisplay id;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void upgradeScreen()
    {
        //Bring up the UI. From there, what should happen is as follows:
        //Print out on that UI the two options are 'Upgrade' or 'Experiment'.
        //If the option selected is 'Upgrade', show the upgradeReqs values and prompt next.
        //If it's 'Experiment', prompt to select a material that the player has discovered.
    }
}
