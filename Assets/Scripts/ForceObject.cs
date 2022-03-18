using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObject : MonoBehaviour
{
    public float hitForce = 10f;

    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(0f, 0f, -1f);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PushPlayer>().AddForce(body.velocity * hitForce);
        }
    }
}
