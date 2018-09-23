using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextFileParser : MonoBehaviour {
    //Disclaimer: In its current state, this should only be used to parse the recipes. Be careful.
    //Static instance: THERE CAN ONLY BE ONE.
    public static TextFileParser tfp;

    //For storing what we read from a string/output for other scripts.
    public string heldData;
    public string[,] itemList = new string[10,2];
    public int[,] convertedItemList = new int[10, 2];

    private void Start()
    {
        readString("Assets/Other/MatTable.txt");
    }

    private void Update()
    {
        
    }

    void Awake()
    {
        if (tfp == null)
        {
            DontDestroyOnLoad(gameObject);
            tfp = this;
        }
        else if (tfp != this)
        {
            Destroy(gameObject);
        }
    }

    //Exactly what it sounds like. We read a string and... Well, turn the output into a list for us to use.
    void readString(string pathName)
    {
        StreamReader reader = new StreamReader(pathName);
        heldData = reader.ReadToEnd();
        reader.Close();
        tStringTo2DArray();
    }

    //This is just to make things more readable. We take the string and split it. Easy.
    void tStringTo2DArray()
    {
        string[] tempItemList = heldData.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        for (int i = 0; i < tempItemList.Length; i++)
        {
            string[] tempStringHolder = tempItemList[i].Split(new string[] { "\t" }, System.StringSplitOptions.None);
            for (int j = 0; j < tempStringHolder.Length; j++)
            {
                itemList[i, j] = tempStringHolder[j];
                if (i > 0)
                {
                    int output;
                    if (System.Int32.TryParse(itemList[i, j], out output)) {
                        convertedItemList[i - 1, j] = System.Int32.Parse(itemList[i, j]);
                    }
                }
                //Note: int.TryParse can be used to turn the values for the recipes into usable values. Use this, genius.
            }
        }
    }

    
}
