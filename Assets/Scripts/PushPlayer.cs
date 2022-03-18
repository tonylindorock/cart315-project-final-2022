using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    public float pushPower = 50.0f;

    CharacterController character;

    public float mass = 3.0f; 
    private Vector3 impact = Vector3.zero; // character momentum 
    
    // adapted from https://answers.unity.com/questions/502798/object-push-character-controller.html
    void Start()
    {
        character = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (impact.magnitude > 0.2){ // if momentum > 0.2...
            character.Move(impact * Time.deltaTime);
        }
        // impact vanishes to zero over time
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    public void AddForce(Vector3 force){
        Vector3 dir = force.normalized;
        dir.y = 1f;
        impact += dir.normalized * force.magnitude / mass;

        GetComponent<AudioSource>().Play();
    }
}
