using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTrigger(GameObject obj){
        spawn = obj;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            if (spawn != null){
                spawn.GetComponent<TriggerEvent>().StartEvent();
            }
            GameObject.Find("Health").GetComponent<Health>().AddHealth(15f);
            Destroy(this.gameObject);
        }
    }
}
