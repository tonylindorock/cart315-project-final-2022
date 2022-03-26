using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjective : MonoBehaviour
{
    public int objectives = 1;

    private int finished = 0;
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObjectiveComplete(){
        finished += 1;
        if (finished >= objectives){
            done = true;
            GetComponent<Trigger>().Triggered("ObjectiveDone");
        }
        Debug.Log("One objective completed!");
    }
}
