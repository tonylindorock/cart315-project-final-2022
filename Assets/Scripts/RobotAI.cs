using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target;
    private Transform player;
    private bool playerInSightRange = false;
    private bool playerInAttackRange = false;
    private bool eyeOnTarget = false;
    private bool targetLocked = false;

    private bool died = false;

    public enum EnemyState {PATROL, PURSUE, ATTACK};
    public EnemyState state;

    public GameObject body;
    public GameObject particles;
    public GameObject eye;
    public float eyeSpeed = 5f;
    public float turnSpeed = 2.5f;
    public Material eyeNormal;
    public Material eyeAttack;
    public Material eyeOff;

    public float sightRange = 25f;
    public float attackRange = 10f;
    public LayerMask playerMask;

    public Transform firePos;
    public GameObject projectile;
    public float projectileForce = 100f;
    public float cooldown = 2f;
    private float timer = 0f;
    private bool canAttack = true;

    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        timer = cooldown;
        Alert(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!died){
            if (eyeOnTarget){
                if (player != null){
                    LookAt(Time.deltaTime);
                }
            }else{
                LookAround();
            }
        }
    }

    void Update(){
        if (!died){
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

            if (playerInSightRange){
                Alert(true);
            }else{
                Alert(false);
            }

            if (!canAttack){
                timer -= Time.deltaTime;
                if (timer <= 0f){
                    canAttack = true;
                    timer = cooldown;
                }
            }

            switch(state){
                case EnemyState.PATROL:
                    break;
                case EnemyState.PURSUE:
                    break;
                case EnemyState.ATTACK:
                    if (canAttack && InRange()){
                        Attack();
                    }
                    break;
            }
        }
    }

    // handle throwable collision
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Throwable" && !died){
            Vector3 vel = other.gameObject.GetComponent<Rigidbody>().velocity;
            if (vel.sqrMagnitude > 0.2f){
                GetComponent<Rigidbody>().isKinematic = false;
                Vector3 throwablePos = new Vector3(other.gameObject.transform.position.x, transform.position.y, transform.position.z);
                Vector3 dir = transform.position - throwablePos;

                GetComponent<Rigidbody>().AddForce(dir.normalized * other.gameObject.GetComponent<Throwable>().damage, ForceMode.Impulse);
                Die();
            }
        }
    }

    // Cosmetic purpose
    private void LookAt(float delta){
        Vector3 targetPos = player.position - transform.position;
        Quaternion targetRot = Quaternion.LookRotation(new Vector3(targetPos.x, 0f, targetPos.z));

        float angle = Vector3.Angle(targetPos, eye.GetComponent<Transform>().forward);
        if (angle <= 10f){
            Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
            eye.GetComponent<Transform>().LookAt(lookPos);
        }else{
            
            eye.GetComponent<Transform>().rotation = Quaternion.Slerp(eye.GetComponent<Transform>().rotation, targetRot, eyeSpeed * delta);
        }
        // important, for attacking
        TurnAt(targetRot, delta);
    }

    private void LookAround(){

    }


    // important
    private void TurnAt(Quaternion target, float delta){
        transform.rotation = Quaternion.Slerp(transform.rotation, target, turnSpeed * delta);
        float rotateY = target.y - transform.rotation.y;
    }

    private bool InRange(){
        Vector3 targetDir = player.position - transform.position;
        float angle = Vector3.Angle(targetDir, body.GetComponent<Transform>().forward);

        if (angle < 30f && playerInAttackRange){
            return true;
        }
        return false;
    }

    private void Alert(bool val){
        if (eyeOnTarget != val){
            eyeOnTarget = val;
            Material[] materials = eye.GetComponent<MeshRenderer>().sharedMaterials;
            if (val){
                materials[1] = eyeAttack;
                state = EnemyState.ATTACK;
            }else{
                materials[1] = eyeNormal;
                state = EnemyState.PURSUE;
                canAttack = false;
                timer = cooldown;
            }
            eye.GetComponent<MeshRenderer>().sharedMaterials = materials;
        }
    }

    private void Attack(){
        Transform firePosTrans = firePos.GetComponent<Transform>();
        GameObject bullet = Instantiate(projectile, firePosTrans.position, firePosTrans.rotation);

        Vector3 targetPos = player.position - firePosTrans.position + new Vector3(0f, 1f, 0f);

        bullet.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(targetPos) * projectileForce);

        canAttack = false;
    }

    public void setTarget(GameObject t){
        target = t;
    }

    private void Die(){
        particles.SetActive(false);
        // update material, turn off lights
        Material[] eyeMaterials = eye.GetComponent<MeshRenderer>().sharedMaterials;
        eyeMaterials[1] = eyeOff;
        eye.GetComponent<MeshRenderer>().sharedMaterials = eyeMaterials;
        Material[] bodyMaterials = body.GetComponent<MeshRenderer>().sharedMaterials;
        bodyMaterials[1] = eyeOff;
        body.GetComponent<MeshRenderer>().sharedMaterials = bodyMaterials;

        died = true;
    }
}
