using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRaycast : MonoBehaviour
{
    private float range = 100f;
    public Transform fpsCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float GetRange(){
        return range;
    }

    public void SetRange(float val){
        range = val;
    }

    public void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.position, fpsCamera.forward, out hit, range)){
            Debug.Log(hit.transform.name);
        }
    }
}
