using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameConsoleManager : MonoBehaviour
{
    [Header("Code Stuff")]
    [SerializeField]List<TextMeshProUGUI> codeLines;
    [SerializeField]TextMeshProUGUI[] codeLinesArray;

    [SerializeField]GameObject[] codeLinesLineGameObject;
    [SerializeField]List<GameObject> codeLinesGameObjectArrayList;
    [SerializeField]TextMeshProUGUI[] currentCodeLineSearch;
    [SerializeField]List<TextMeshProUGUI> currentLine;

    [SerializeField]float highestID;
    [SerializeField]float lowestID;
    [Header("Player Modifiable Variables")]
    public float aVar;
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
        codeLines.Clear();
        //gets all code objects
        foreach(GameObject variable in GameObject.FindGameObjectsWithTag("var"))
        {
            codeLines.Add(variable.GetComponent<TextMeshProUGUI>());
        }
        foreach(GameObject variable in GameObject.FindGameObjectsWithTag("num"))
        {
            codeLines.Add(variable.GetComponent<TextMeshProUGUI>());
        }
        foreach(GameObject variable in GameObject.FindGameObjectsWithTag("operator"))
        {
            codeLines.Add(variable.GetComponent<TextMeshProUGUI>());
        }
        foreach(GameObject variable in GameObject.FindGameObjectsWithTag("end"))
        {
            codeLines.Add(variable.GetComponent<TextMeshProUGUI>());
        }
        foreach(GameObject variable in GameObject.FindGameObjectsWithTag("CodeLine"))
        {
            codeLines.Add(variable.GetComponent<TextMeshProUGUI>());
        }
        //set list to array
        codeLinesArray = codeLines.ToArray();
        lowestID = codeLinesArray[0].GetComponent<CodeObject>().id;
        highestID = codeLinesArray[codeLinesArray.Length - 1].GetComponent<CodeObject>().id;
        GameObject[] codeLinesLineGameObjectArray = GameObject.FindGameObjectsWithTag("CodeLine");
        codeLinesGameObjectArrayList.Clear();
        for(int i=0; i<codeLinesLineGameObjectArray.Length; i++)
        {
            for(int e=0; e<codeLinesLineGameObjectArray[i].transform.childCount; e++)
            {
                if(codeLinesLineGameObjectArray[i].GetComponent<TextMeshProUGUI>() != null)
                {
                    codeLinesGameObjectArrayList.Add(codeLinesLineGameObjectArray[i].transform.GetChild(e).gameObject);
                }
            }
        }
        codeLinesLineGameObject = codeLinesGameObjectArrayList.ToArray();
        EvaluateLine();
        //add colors
        for(int i=0;i<codeLinesArray.Length;i++)
        {
            
            switch(codeLinesArray[i].transform.tag)
            {
                case "num":
                codeLinesArray[i].color = numColor;
                break;
                case "var":
                codeLinesArray[i].color = varColor;
                break;
                case "operator":
                codeLinesArray[i].color = operatorColor;
                break;
            }
        }
        print("Code Compiled!");
    }

    void EvaluateLine()
    {
        float currentIDEval = lowestID;
        while(currentIDEval <= highestID)
        {
            currentLine.Clear();
            for(int i=0;i<codeLinesLineGameObject.Length;i++)
            {
                if(codeLinesLineGameObject[i].GetComponent<CodeObject>().id == currentIDEval)
                {
                    currentLine.Add(codeLinesLineGameObject[i].GetComponent<TextMeshProUGUI>());
                }   
            }
            currentCodeLineSearch = currentLine.ToArray();
            switch(currentCodeLineSearch[0].transform.tag) 
            {
                case "var":
                    for(int i=1; i<currentCodeLineSearch.Length; i+=2)
                    {
                        switch(currentCodeLineSearch[i].text)
                        {
                        
                            case "=":
                            float number;
                            if(float.TryParse(currentCodeLineSearch[i+1].text, out number))
                            {
                            
                                switch(currentCodeLineSearch[0].text)
                                {
                                    case "a":
                                    //print(number);
                                    aVar = number;
                                    break;
                                }
                            }
                            else
                            {
                                ErrorFound("Data Type Mismatch! (a must be a number value)");
                            }
                        break;
                    }
                    }
                break;
            }
            currentIDEval++;
        }
        
    }

    void ErrorFound(string errorMessage)
    {
        Debug.LogError(errorMessage);
    }
}
