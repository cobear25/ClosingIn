using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform target;
    float xdif;
    float ydif;
    float zdif;

    private void Start()
    {
        xdif = target.position.x - transform.position.x;
        ydif = target.position.y - transform.position.y;
        zdif = target.position.z - transform.position.z;
    }

    void Update() {
        //transform.position = Vector3.MoveTowards(transform.position, newPos, step);
    }

    private void FixedUpdate()
    {
    }

    private void LateUpdate()
    {
        
        float step = 100 * Time.deltaTime;

        transform.position = new Vector3(target.position.x - xdif, target.position.y - ydif, target.position.z - zdif);
    }
}
