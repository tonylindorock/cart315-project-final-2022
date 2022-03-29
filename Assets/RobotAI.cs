using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target;
    private bool eyeOnTarget = false;

    public GameObject eye;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (eyeOnTarget){
            if (target != null){
                LookAt();
            }
        }else{
            LookAround();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            target = other.gameObject;
            eyeOnTarget = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == target){
            target = null;
            eyeOnTarget = false;
        }
    }

    private void LookAt(){
        Transform targetTrans = target.GetComponent<Transform>();
        Vector3 targetPos = new Vector3(targetTrans.position.x, this.transform.position.y, targetTrans.position.z) ;
        eye.GetComponent<Transform>().LookAt(targetPos);
    }

    private void LookAround(){

    }

    public void setTarget(GameObject t){
        target = t;
    }
}
