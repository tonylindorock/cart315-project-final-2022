using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOrb : MonoBehaviour
{
    public GameObject orb;
    public float maxDistance = 50f;

    Vector3 lastPoint = Vector3.zero;

    BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        SpawnNewOrb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNewOrb(){
        Vector3 p = GetNextPoint();

        GameObject orbObj = Instantiate(orb, p, Quaternion.identity);
        orbObj.GetComponent<Collectable>().setTrigger(this.gameObject);
        GetComponent<AudioSource>().Play();
    }

    private Vector3 GetNextPoint(){
        Vector3 point = RandPointInBox(box.bounds);
        if (lastPoint == Vector3.zero){
            return point;
        }
        while (Vector3.Distance(point, lastPoint) > maxDistance){
            lastPoint = point;
            point = RandPointInBox(box.bounds);
        }
        return point;
    }

    public Vector3 RandPointInBox(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
}
}
