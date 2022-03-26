using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowReceiver : MonoBehaviour
{
    public GameObject receive;
    public Material lightGreen;
    public GameObject indicator;

    private bool received = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Throwable" && other.gameObject.GetComponent<Throwable>().GetName() == receive.GetComponent<Throwable>().GetName()){
            Debug.Log("Receiver hit!");
            other.gameObject.GetComponent<DestroySelf>().DestoryWithEffect();
            OnReceived();
        }
    }

    private void OnReceived(){
        received = true;
        indicator.GetComponent<MeshRenderer>().material = lightGreen;
        GetComponent<Trigger>().Triggered("OnReceived");
        GetComponent<AudioSource>().Play();
    }
}
