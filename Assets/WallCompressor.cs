using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCompressor : MonoBehaviour {
    public Transform floor;
    public Transform leftWall;
    public Transform rightWall;
    public Transform ceiling;
    public float compressionSpeed;
    public bool shouldMove = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (shouldMove == true)
        {
            float step = compressionSpeed * Time.deltaTime;
            // floor
            Vector3 floorPos = new Vector3(floor.position.x, floor.position.y + 10, floor.position.z);
            floor.position = Vector3.MoveTowards(floor.position, floorPos, step);

            // left
            Vector3 leftPos = new Vector3(leftWall.position.x - 10, leftWall.position.y, leftWall.position.z);
            leftWall.position = Vector3.MoveTowards(leftWall.position, leftPos, step);

            // right
            Vector3 rightPos = new Vector3(rightWall.position.x + 10, rightWall.position.y, rightWall.position.z);
            rightWall.position = Vector3.MoveTowards(rightWall.position, rightPos, step);

            // top
            Vector3 topPos = new Vector3(ceiling.position.x, ceiling.position.y - 10, ceiling.position.z);
            ceiling.position = Vector3.MoveTowards(ceiling.position, topPos, step);
        }
	}
}
