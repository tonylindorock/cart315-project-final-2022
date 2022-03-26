using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{   
    public string triggerId;
    public string eventId;
    public GameObject triggerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Triggered(string id = ""){
        if (triggerId == id){
            TriggerEvent[] events = triggerObject.GetComponents<TriggerEvent>();
            for (int i = 0; i < events.Length; i++){
                events[i].StartEvent(eventId);
            }
        }
    }
}
