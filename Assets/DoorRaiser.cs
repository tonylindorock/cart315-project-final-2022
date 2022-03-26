using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRaiser : MonoBehaviour
{
    public enum Axis {X, Y, Z};
    public Axis moveAxis;

    public float finalVal = 0f;
    public float speed = .2f;

    private bool moving = false;
    private Vector3 finalPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving){
            transform.position = Vector3.Lerp(transform.position, finalPos, speed);
            if (transform.position == finalPos){
                moving = false;
            }
        }
    }

    public void Reveal(){
        switch(moveAxis){
            case Axis.X:
                finalPos = new Vector3(finalVal, transform.position.y, transform.position.z);
                break;
            case Axis.Y:
                finalPos = new Vector3(transform.position.x, finalVal, transform.position.z);
                break;
            case Axis.Z:
                finalPos = new Vector3(transform.position.x, transform.position.y, finalVal);
                break;
        }
        moving = true;
    }
}
