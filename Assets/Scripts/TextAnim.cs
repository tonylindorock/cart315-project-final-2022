using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    private float a = 1f;
    private int animId = -1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (animId == 0){
            a -= 0.4f * Time.deltaTime;
        }else if (animId == 1){
            a += 0.4f * Time.deltaTime;
        }
        a = Mathf.Clamp(a, 0f, 1f);
        GetComponent<CanvasRenderer>().SetColor(new Color(1f, 1f, 1f, a));
    }

    public void Fade(int id){
        animId = id;
        if (animId == 0){
            a = 1f;
        }else if (animId == 1){
            a = 0f;
        }
    }
}
