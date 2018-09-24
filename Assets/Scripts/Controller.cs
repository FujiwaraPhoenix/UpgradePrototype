using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public static Controller Instance;

    public float invulnTimer;

    public bool UIActive = false;

    public int woodCount = 0;
    public int stoneCount = 0;
    public int metalCount = 0;
    public int waterCount = 0;
    //Tools: Empty Hands, Sword, Bow&Arrow, in that order.
    public int toolNo = 0;

    //Sword info: Base atk, upgrade applied, metal needed.
    public int[] swordDetails = new int[3];
    //Bow info: Base atk, upgrade applied, wood needed.
    public int[] bowDetails = new int[3];

    //Checks to see if we have discovered any of the new materials yet and, if so, which ones?
    //First dimension is item type, second is rune type.
    public int[,] specialItemList = new int[4, 4];
    public string sItemListNames = "Gel\tElectrified Water\tRubbery Wood\tShining Stone\tPlastic\tFreezing Liquid\tIce-Cold Metal\tEverlasting Flame\tWarm Stone";

    //Rune list
    public bool[] runesOwned = new bool[4];

    public GameObject[] tiles = new GameObject[2];
    public Item[] worldItems = new Item[7];

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
        if (PlayerController.pc.HP <= 0)
        {
            Destroy(PlayerController.pc.gameObject);
            Debug.Log("YOU DIED");
        }
        //spawn a new enemy in the world every so often.
	}

    void initWorld()
    {
        for (int i = -10; i < 10; i++)
        {
            for (int j = -10; j < 10; j++)
            {
                //splatter the world with tiles.
            }
        }
        //Then, splash the world with resources. Liberally.
        //FInally, seed the runes and drop a few enemies into the world.
    }
}
