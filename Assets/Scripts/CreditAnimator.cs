using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CreditAnimator : MonoBehaviour
{
    private Timer timerScript;
    private TextMeshProUGUI creditText;
    private Trigger triggerScript;

    private int id = 0;

    public string[] credits;
    
    // Start is called before the first frame update
    void Start()
    {
        timerScript = GetComponent<Timer>();
        creditText = GetComponent<TextMeshProUGUI>();

        triggerScript = GetComponent<Trigger>();

        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetText(){
        creditText.text = credits[id];
    }

    private void Advance(){
        if (id < credits.Length - 1){
            id += 1;
            SetText();
        }else{
            if (triggerScript != null){
                triggerScript.Triggered();
            }
        }
    }
}
