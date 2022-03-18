using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
        if (slider == null){
            Debug.Log("WARNING: " + gameObject.name + " ProgressBar slider is null!");
        }
    }

    public void SetValue(float val){
        slider.value = val;
    }

    public void AddValue(float val){
        slider.value += val;
    }

    public void Reset(){
        slider.value = slider.maxValue;
    }
}
