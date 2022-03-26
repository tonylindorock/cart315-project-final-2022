using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenText : MonoBehaviour
{
    private int id = 0;
    public GameObject text;

    public string[] hints;
    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<TextMeshProUGUI>().text = GetCurrentText();
    }

    public void UpdateHint(){
        text.GetComponent<TextMeshProUGUI>().text = Advance();
    }

    public string GetCurrentText(){
        return hints[id].Replace("\\n", "\n");
    }

    public string Advance(){
        if (id < hints.Length - 1){
            id += 1;
        }else{
            id = 0;
        }
        return hints[id].Replace("\\n", "\n");
    }
}
