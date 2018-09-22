using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    //Item refers to the stuff we interact with in the world that isn't a) a structure or b) an enemy.
    //-1 is the default state when spawned, and is altered based on what item it is in. In order from 0 afterwards, they are:
    //Wood (Tree), Stone, Metal (Ore), Water, Rune A, Rune B, Rune C, Rune D.
    public int itemType = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
