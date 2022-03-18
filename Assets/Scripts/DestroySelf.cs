using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float waitTime = 1f;
    public GameObject despawnParticle;

    float timeLeft = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = waitTime;
    }

    void SpawnParticle(){
        if (despawnParticle != null){
            GameObject particle = Instantiate(despawnParticle, this.gameObject.transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0f){
            SpawnParticle();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "DestroyArea"){
            Destroy(this.gameObject);
        }
    }
}
