using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    public enum LockAxis {NONE, X, Y}
    public LockAxis lockAxis;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null){
            /*
            Vector3 targetPos = target.position - transform.position;
            Vector3 newPos;
            transform.LookAt(new Vector3(target.position.x, target.position.y, target.position.z));*/

            // look at target with smooth movement
            Quaternion targetRot = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, speed * Time.deltaTime);

            // reset locked axises
            if (lockAxis == LockAxis.X){
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, 0f);
            }else if (lockAxis == LockAxis.Y){
                transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, 0f);
            }
        }else{
            Debug.Log("WARNING: No target object assigned!");
        }
    }
}
