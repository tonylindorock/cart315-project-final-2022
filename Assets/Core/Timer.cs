using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float waitTime = 1f;
    public bool auto = false;
    public bool repeat = false;

    private bool running = false;
    private bool done = false;

    public string func;

    float time = 0f;

    void Start()
    {
        time = waitTime;
        running = auto;
    }

    // Update is called once per frame
    void Update()
    {
        if(running){
            if (time > 0f){
                time -= Time.deltaTime;
            }else{
                if (!done){
                    done = true;
                    if (!repeat){
                        running = false;
                    }else{
                        done = false;
                        time = waitTime;
                    }
                    CallFunc();
                }
            }
        }
    }

    public void StartTimer(){
        running = true;
    }

    public void StopTimer(){
        running = false;
    }

    public bool IsDone(){
        return done;
    }

    void CallFunc(){
        if (func != null){
            SendMessage(func);
        }
    }
}