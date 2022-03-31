using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRaycast : MonoBehaviour
{
    public float range = 100f;
    public Transform fpsCamera;

    private FPSController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<FPSController>();
    }

    public float GetRange(){
        return range;
    }

    public void SetRange(float val){
        range = val;
    }

    public float GetFirstCollisionDist(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.position, fpsCamera.forward, out hit)){
            return hit.distance;
        }
        return 0f;
    }

    public Vector3 GetFirstCollisionPoint(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.position, fpsCamera.forward, out hit)){
            return hit.point;
        }
        return new Vector3();
    }

    public void Shoot(){
        player.UpdateCrosshair(0);

        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.position, fpsCamera.forward, out hit, range)){
            if(hit.collider.gameObject.tag == "Throwable"){
                if(Input.GetButtonDown("Fire1")){
                    hit.collider.gameObject.GetComponent<Throwable>().PickUp(this.gameObject, player.hand);
                    player.SetHoldingObj(hit.collider.gameObject); 
                }
                player.UpdateCrosshair(1);
            }
        }
    }
}
