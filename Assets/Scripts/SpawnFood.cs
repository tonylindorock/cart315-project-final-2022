using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public float waitTime = 5f;
    float timer = 0f;

    public GameObject[] food;

    GameObject[] spawnPoints = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        timer = waitTime;

        for(int i = 0; i < 4; i ++){
            spawnPoints[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f){
            Spawn();
            timer = waitTime;
        }
    }

    void Spawn(){
        for(int i = 0; i < 4; i ++){
            float r = Random.Range(0f, 1f);
            if (r > 0.2f){
                ChooseFood(i);
            }
        }
    }

    void ChooseFood(int pos){
        int r = Random.Range(0, food.Length);
        spawnPoints[pos].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        GameObject foodObj = Instantiate(food[r], spawnPoints[pos].transform.position, food[r].transform.rotation);
    }
}
