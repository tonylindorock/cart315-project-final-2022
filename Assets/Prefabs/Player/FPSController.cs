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

    private float speed = 0f;
    private bool isRunning = false;

    public float jumpBuffer = 0.2f;
    private float jumpBufferTimer = 0f;

    public float jumpHeight = 3f;
    public float gravity = -9.8f;
    public float gravityMultipler = 4f;
    public Camera playerCamera;
    public Camera weaponCamera;
    private Vector3 orgMainCamPos;
    public Vector2 mainCamOffset;
    public float lookSpeed = 10f;
    public float lookXLimit = 90f;

    public Transform groundCheck;
    public Transform ceilingCheck;
    public float checkDis = 0.4f;
    public LayerMask groundMask;

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    Vector3 velocity;
    Vector3 gVelocity;

    private FPSRaycast lookRaycast;
    public GameObject hand;

    private GameObject holdingObj;
    public float throwForce = 1000f;

    private GameObject crosshairSolid;
    private GameObject crosshairHollow;

    bool isGrounded = false;

    public bool canPlay = true;
    private bool canLook = true;
    private bool canMove = true; // not affecting jumping
    private bool canJump = true;
    private bool canAct = true;

    private bool disableMovement = false;

    private AudioSource audioPlayer;
    public AudioClip SFX_BOUNCE;

    public GameObject soundManager;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        lookRaycast = GetComponent<FPSRaycast>();

        audioPlayer = GetComponent<AudioSource>();
        
        crosshairSolid = GameObject.Find("/FPSController/Canvas/Crosshair");
        crosshairHollow = GameObject.Find("/FPSController/Canvas/Crosshair_1");

        SetPlay(canPlay);

        speed = walkSpeed;
        orgMainCamPos = playerCamera.transform.localPosition;
    }

    private void LateUpdate() {
        playerCamera.transform.localPosition = orgMainCamPos + new Vector3(mainCamOffset.x, mainCamOffset.y, 0f);
        mainCamOffset = Vector2.Lerp(mainCamOffset, Vector2.zero, 10f * Time.deltaTime);
    }

    private void FixedUpdate() {
        velocity = Vector3.Lerp(velocity, moveDirection * speed, acceleration * Time.deltaTime);

        // apply gravity
        gVelocity.y += gravity * gravityMultipler * Time.deltaTime;
        
        controller.Move((velocity + gVelocity) * Time.deltaTime);
    }


    void Update()
    {
        bool isOnGround = Physics.CheckSphere(groundCheck.position, checkDis, groundMask);
        bool isOnCeiling = Physics.CheckSphere(ceilingCheck.position, checkDis, groundMask);
        
        // player lands from air
        if (isOnGround != isGrounded && isOnGround){
            float vol = Mathf.Abs(gVelocity.y/20f);
            soundManager.GetComponent<FPSSoundManager>().PlayJumpSound(vol);

            // lower camera
            if (gVelocity.y <= -9.8f){
                mainCamOffset.y = -0.4f;
            }
        }
        isGrounded = isOnGround;

        soundManager.GetComponent<FPSSoundManager>().ChangeState(0);
        GetMovement();

        if (isGrounded){
            if (moveDirection.x != 0f || moveDirection.z != 0f){
                if (isRunning){
                    soundManager.GetComponent<FPSSoundManager>().SetMoveInterval(0.3f);
                }else{
                    soundManager.GetComponent<FPSSoundManager>().SetMoveInterval(0.5f);
                }
                soundManager.GetComponent<FPSSoundManager>().ChangeState(1);
            }
        }

        // if on ground
        if (isGrounded && gVelocity.y < 0f) {
            jumpBufferTimer = jumpBuffer;
            if (canPlay){
                canMove = true;
                canJump = true;
            }
            gVelocity.y = gravity;
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
        if (Input.GetButtonDown("Jump") && canJump && (isGrounded || jumpBufferTimer > 0)){
            gVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityMultipler);
            canJump = false;
        }

        if (isOnCeiling){ 
            gVelocity.y = 0f;
        }

        if (Input.GetKey(KeyCode.LeftShift)){
            isRunning = true;
            speed = runSpeed;
        }else{
            isRunning = false;
            if (isGrounded){
                speed = walkSpeed;
            } 
        }

        HandleLook();
        HandleInteract();
        //HandleFire();
    }

    private void GetMovement(){
        // update directions
        float x = 0f;
        float z = 0f;
        if(canMove){
            // not pressing any keys, snap
            if (Input.GetAxisRaw("Horizontal") == 0){
                x = Input.GetAxisRaw("Horizontal");
            // if moving, smooth
            }else{
                x = Input.GetAxis("Horizontal");
            }

            if (Input.GetAxisRaw("Vertical") == 0){
                z = Input.GetAxisRaw("Vertical");
            }else{
                z = Input.GetAxis("Vertical");
            }
        }

        moveDirection = (transform.right * x + transform.forward * z);
        moveDirection = moveDirection.normalized;
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
        canJump = val;
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

    public void PlaySound(int id, float volume = 1f){
        if (id == 0){
            audioPlayer.PlayOneShot(SFX_BOUNCE, volume);
        }
    }

    public Vector3 GetVelocity(){
        return velocity;
    }
}