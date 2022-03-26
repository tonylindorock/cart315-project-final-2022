using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderButton : MonoBehaviour
{
    public Material lightGreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Throwable"){
            Debug.Log("Button hit!");
            OnButtonHit();
        }
    }

    private void OnButtonHit(){
        GetComponent<MeshRenderer>().material = lightGreen;
        GetComponent<Trigger>().Triggered();
        GetComponent<AudioSource>().Play();
    }
}
