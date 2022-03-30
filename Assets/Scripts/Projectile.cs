using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 25f;
    public float hitForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter(Collider other) {
       if (other.gameObject.tag == "Player"){
           other.gameObject.GetComponent<PushPlayer>().AddForce(GetComponent<Rigidbody>().velocity * hitForce);
       }
       GetComponent<DestroySelf>().DestroyWithEffect();
   }
}
