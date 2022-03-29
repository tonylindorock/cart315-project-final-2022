using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter(Collider other) {
       GetComponent<DestroySelf>().DestroyWithEffect();
   }
}
