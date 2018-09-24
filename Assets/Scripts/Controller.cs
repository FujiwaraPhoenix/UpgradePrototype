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
    public Enemy e;

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
        initWorld();
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
        for (float i = -50; i < 50; i += 2.5f)
        {
            for (float j = -50; j < 50; j += 2.5f)
            {
                //splatter the world with tiles.
                if ((Random.Range(0, 100) > 90f) && Mathf.Abs(i) > 7.5f && Mathf.Abs(j) > 7.5f)
                {
                    GameObject tile = Instantiate(tiles[1], new Vector3(i, j, 0), Quaternion.identity);

                }
                else
                {
                    GameObject tile = Instantiate(tiles[0], new Vector3(i, j, 1), Quaternion.identity);
                    //Then, splash the world with resources. Liberally.
                    if (Mathf.Abs(i) > 7.5f || Mathf.Abs(j) > 7.5f)
                    {
                        if (Random.Range(0, 5) < 2)
                        {
                            int itemVal = Random.Range(0, 3);
                            Item resource = Instantiate(worldItems[itemVal], new Vector3(i, j, 0), Quaternion.identity);
                        }
                    }
                }
            }
        }
        //Finally, seed the runes and drop a few enemies into the world.
        for (int i = 0; i < 4; i++)
        {
            Item addRune = Instantiate(worldItems[3 + i], new Vector3(Random.Range(-25f, 25f), Random.Range(-25f, 25f), 0), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            float range = Random.Range(10f, 50f);
            float range2 = Random.Range(10f, 50f);
            int makeNeg = Random.Range(0, 2);
            int makeNeg2 = Random.Range(0, 2);

            if (makeNeg == 1)
            {
                range *= -1;
            }
            if (makeNeg == 1)
            {
                range2 *= -1;
            }

            Enemy spawnE = Instantiate(e, new Vector3(range, range2, 0), Quaternion.identity);
        }
    }
}
