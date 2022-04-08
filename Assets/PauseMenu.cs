using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            show = !show;
            if (show){
                 GetComponent<CanvasRenderer>().SetAlpha(1f);
            }else{
                GetComponent<CanvasRenderer>().SetAlpha(0f);
            }
        }
    }
}
