using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    private GameObject owner; // who picks it
    private GameObject followPoint;

    private Rigidbody body;

    public string uniqueName = "Throwable";

    public float damage = 10f;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (owner != null){
            //Vector3 newPosition = Vector3.MoveTowards(transform.position, followPoint.transform.position, speed * Time.deltaTime);
            //body.MovePosition(newPosition);

            Vector3 direction = followPoint.transform.position - transform.position;
            body.velocity = direction * speed;
        }
    }

    public void PickUp(GameObject newOwner, GameObject target){
        //body.isKinematic = true;
        body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        owner = newOwner;
        followPoint = target;
    }

    public void Drop(){
        //body.isKinematic = false;
        body.constraints = RigidbodyConstraints.None;
        owner = null;
    }

    public void Throw(float force){
        // get looking dir
        //Vector3 lookDir = owner.GetComponent<FPSController>().playerCamera.transform.forward;
        // get first collision position
        Vector3 targetPos = owner.GetComponent<FPSRaycast>().GetFirstCollisionPoint();
        // get direction between holding point and collision point
        Vector3 dir = targetPos - followPoint.transform.position;
        Drop();
        body.AddForce(dir.normalized * force);
    }

    public string GetName(){
        return uniqueName;
    }
}
