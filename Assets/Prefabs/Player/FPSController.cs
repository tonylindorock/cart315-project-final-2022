using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float walkSpeed = 7.5f;
    public float runSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float acceleration = 0.6f;

    float speed = 0f;
    public float airSpeed = 4f;

    public float jumpHeight = 3f;
    public float gravity = -9.8f;
    public float gravityMultipler = 4f;
    public Camera playerCamera;
    public Camera weaponCamera;
    public float lookSpeed = 10f;
    public float lookXLimit = 90f;

    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    Vector3 velocity;

    private FPSRaycast lookRaycast;
    public GameObject hand;

    private GameObject holdingObj;
    public float throwForce = 1000f;

    private GameObject crosshairSolid;
    private GameObject crosshairHollow;

    bool isGrounded = false;

    public bool canPlay = true;
    public bool canLook = true;
    public bool canMove = true;
    public bool canAct = true;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lookRaycast = GetComponent<FPSRaycast>();
        
        crosshairSolid = GameObject.Find("/FPSController/Canvas/Crosshair");
        crosshairHollow = GameObject.Find("/FPSController/Canvas/Crosshair_1");

        SetPlay(canPlay);
    }


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
        // update directions
        float x = 0f;
        float z = 0f;
        if(canMove){
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        Vector3 move = (transform.right * x + transform.forward * z) * walkSpeed;

        velocity.x = move.x;
        velocity.z = move.z;

        // if on ground
        if (isGrounded && velocity.y < 0f) {
            if (canPlay){
                canMove = true;
            }
            velocity.y = gravity;
        }

        // handle jump
        if(canMove){
            if (Input.GetButtonDown("Jump") && isGrounded){
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityMultipler);
                canMove = false;
            }
        }

        // apply gravity
        velocity.y += gravity * gravityMultipler * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        HandleLook();
        HandleInteract();
        //HandleFire();
    }

    private void HandleLook(){
        // Player and Camera rotation
        if (canLook){
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void HandleFire(){
        if (canAct){
            if(Input.GetButtonDown("Fire1")){
                GetComponent<FPSRaycast>().Shoot();
            }
        }
    }

    private void HandleInteract(){
        if (canPlay){
             if (holdingObj != null){
                if(Input.GetButtonDown("Fire1")){
                    holdingObj.GetComponent<Throwable>().Throw(throwForce);
                    holdingObj = null;
                }
            }
            lookRaycast.Shoot();
        }
    }

    public void SetPlay(bool val){
        canPlay = val;
        canLook = val;
        canMove = val;
        canAct = val;
        if (canPlay){
            Cursor.lockState = CursorLockMode.Locked;
        }else{
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void UpdateCrosshair(int id){
        if (id == 0){
            crosshairSolid.GetComponent<CanvasRenderer>().SetAlpha(1f);
            crosshairHollow.GetComponent<CanvasRenderer>().SetAlpha(0f);
        }else{
            crosshairSolid.GetComponent<CanvasRenderer>().SetAlpha(0f);
            crosshairHollow.GetComponent<CanvasRenderer>().SetAlpha(1f);
        }
    }

    public void SetHoldingObj(GameObject obj){
        holdingObj = obj;
    }
}