using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public static Controller Instance;

    public float invulnTimer;

    public int woodCount = 0;
    public int stoneCount = 0;
    public int metalCount = 0;
    public int waterCount = 0;
    //Tools: Empty Hands, Sword, Bow&Arrow, in that order.
    public int toolNo = 0;

    //Sword info: Base atk, upgrade applied.
    public int[] swordDetails = new int[2];
    //Bow info: Base atk, upgrade applied.
    public int[] bowDetails = new int[2];

    //Checks to see if we have discovered any of the new materials yet and, if so, which ones?
    //First dimension is item type, second is rune type.
    public int[,] specialItemList = new int[4, 4];

    //Rune list
    public bool[] runesOwned = new bool[4];

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (invulnTimer > 0)
        {
            invulnTimer -= Time.deltaTime;
        }
	}
}
