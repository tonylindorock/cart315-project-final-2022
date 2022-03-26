using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameTimer : MonoBehaviour
{
    public int waitFrame = 60;
    public bool auto = false;
    public bool repeat = false;

    private bool running = false;
    private bool done = false;

    public string func;

    private int frame = 0;
    public int displayFrame = 0;

    void Start()
    {
        frame = Time.frameCount;
        running = auto;
    }

    // Update is called once per frame
    void Update()
    {
        if(running){
            if (Time.frameCount - frame > waitFrame){
                if (!done){
                    done = true;
                    if (!repeat){
                        running = false;
                    }else{
                        done = false;
                        frame = Time.frameCount;
                    }
                    CallFunc();
                }
            }
            displayFrame = Time.frameCount - frame;
        }
    }

    public void StartTimer(){
        frame = Time.frameCount;
        running = true;
        done = false;
    }

    public void StopTimer(){
        running = false;
        done = true;
    }

    public bool IsDone(){
        return done;
    }

    void CallFunc(){
        if (func != null && func != ""){
            SendMessage(func);
        }
    }
}