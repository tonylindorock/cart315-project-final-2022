using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDrivenRotation : MonoBehaviour
{

    public bool enabled = true;
    public float lookSpeed = 4f;
    public float lookXLimit = 10f;
    public float lookYLimit = 10f;
    private float rotationXOffset;
    private float rotationYOffset;
    private float rotationX;
    private float rotationY;
    private float rotationZ;
    // Start is called before the first frame update
    void Start()
    {
        rotationX = transform.eulerAngles.x;
        rotationY = transform.eulerAngles.y;
        rotationZ = transform.eulerAngles.z;
    }

    void Update(){
        HandleCursorMovement();
    }

    private void HandleCursorMovement(){
        if (enabled){
            rotationXOffset += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationYOffset += Input.GetAxis("Mouse X") * lookSpeed;

            rotationXOffset = Mathf.Clamp(rotationXOffset, -lookXLimit, lookXLimit);
            rotationYOffset = Mathf.Clamp(rotationYOffset, -lookYLimit, lookYLimit);

            transform.rotation = Quaternion.Euler(rotationX + rotationXOffset, rotationY + rotationYOffset, rotationZ);
        }
    }
}
