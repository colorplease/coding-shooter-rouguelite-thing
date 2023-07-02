using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeObject : MonoBehaviour
{
    public int id;
    public bool isLineNumber;
    void OnEnable()
    {
        if(isLineNumber)
        {
            id = Mathf.Abs(Convert.ToInt32(Char.GetNumericValue(GetComponent<TextMeshProUGUI>().text[0])));
        }
        if(!isLineNumber)
        {
            id = Mathf.Abs(Convert.ToInt32(Char.GetNumericValue(transform.parent.GetComponent<TextMeshProUGUI>().text[0])));
        }
    }
}
