using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    float health = 100f;
    public float multipler = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health -= Time.deltaTime * multipler;
        health = Mathf.Clamp(health, 0f, 100f);
        GetComponent<Text>().text = "" + (int)health;

        if (health <= 0f){
            GameObject.Find("GameManager").GetComponent<GameManager>().EndGame();
        }
    }

    public void AddHealth(float amount){
        health += amount;
        health = Mathf.Clamp(health, 0f, 100f);
    }
}
