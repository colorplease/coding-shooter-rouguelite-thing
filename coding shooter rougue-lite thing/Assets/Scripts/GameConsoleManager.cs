using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameConsoleManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI[] codeLines;
    [SerializeField]float codeLinesLength;
    [Header("Player Modifiable Variables")]
    public int a;
    [Header("Code Type Colors")]
    public Color varColor;
    public Color numColor;
    public Color operatorColor;
    public Color commentColor; //this is also used for line numbers and ends (;).

    

    //public Color operator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            CompileCode();
        }
    }

    void CompileCode()
    {
        print("Code Compiled!");
        GameObject[] codeLinesGameObjects = GameObject.FindGameObjectsWithTag("CodeLine");

        for(int i=0;i<codeLinesGameObjects.Length;i++)
        {
            codeLinesLength += codeLinesGameObjects[i].transform.childCount;
        }
        codeLinesLength += codeLinesGameObjects.Length;
        //for()
    }
}
