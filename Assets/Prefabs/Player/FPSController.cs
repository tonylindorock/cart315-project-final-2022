using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float walkSpeed = 7.5f;
    public float runSpeed = 11.5f;
    public float jumpSpeed = 8.0f;

    private float acceleration = 6f;
    public float normalAcc = 6f;
    public float airAcc = 1f;

    float speed = 0f;

    public float jumpBuffer = 0.2f;
    private float jumpBufferTimer = 0f;

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
    public bool canMove = true; // not affecting jumping
    public bool canAct = true;

    public bool disableMovement = false;
    

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

        Vector3 move = (transform.right * x + transform.forward * z);

        // if on ground
        if (isGrounded && velocity.y < 0f) {
            jumpBufferTimer = jumpBuffer;
            if (canPlay){
                canMove = true;
            }
            velocity.y = gravity;
            acceleration = normalAcc;
            controller.slopeLimit = 45f;
            controller.stepOffset = 0.3f;
        // if not no ground
        }else{
            canMove = false;
            jumpBufferTimer -= Time.deltaTime;
		    acceleration = airAcc;
            controller.slopeLimit = 90f;
            controller.stepOffset = 0f;
        }

        // handle jump
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpBufferTimer > 0)){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityMultipler);
        }

        velocity = Vector3.Lerp(velocity, move * walkSpeed, acceleration * Time.deltaTime);

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