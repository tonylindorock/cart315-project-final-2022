using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide(){
        GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    public void Show(){
        GetComponent<CanvasRenderer>().SetAlpha(1f);
    }
}
