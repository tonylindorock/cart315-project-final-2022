using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public Vector3 rotationSpeed = new Vector3(1f, 1f, 1f);
    public bool randInitialRotation = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (randInitialRotation){
            float rX = 0f, rY = 0f, rZ = 0f;
            if (rotationSpeed.x != 0f){
                rX = Random.Range(-90f, 90f);
            }
            if (rotationSpeed.y != 0f){
                rY = Random.Range(-90f, 90f);
            }
            if (rotationSpeed.z != 0f){
                rZ = Random.Range(-90f, 90f);
            }
            GetComponent<Transform>().rotation = Quaternion.Euler(rX, rY, rZ);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Transform>().Rotate(rotationSpeed.x, rotationSpeed.y, rotationSpeed.z, Space.World);
    }
}
