using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
    public float minX;
    public float minY;
    public float minZ;
    public float maxX;
    public float maxY;
    public float maxZ;
    public Transform pillarObject;
    public Transform ringObject;
    public Transform boxObject;
    public Material pillarMaterial;
    public Material ringMaterial;
    public Material boxMaterial;

	// Use this for initialization
	void Start () {
        // add pillars
        for (int i = 0; i < 100; i++)
        {
            float xpos = Random.Range(minX, maxX);
            float zpos = Random.Range(minZ, maxZ);
            float rot = Random.Range(0, 360);

            addPillar(xpos, zpos, rot);
        }
        // add rings
        for (int i = 0; i < 60; i++)
        {
            float xpos = Random.Range(minX, maxX);
            float zpos = Random.Range(minZ, maxZ);
            float ypos = Random.Range(minY, maxY);

            addRing(xpos, zpos, ypos);
        }
        // add boxes
        for (int i = 0; i < 80; i++)
        {
            float xpos = Random.Range(minX, maxX);
            float zpos = Random.Range(minZ, maxZ);
            float ypos = Random.Range(minY, maxY);

            addBox(xpos, zpos, ypos);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void addPillar(float x, float z, float rot) {
        Transform pillar = Instantiate(pillarObject, new Vector3(x, 0, z), Quaternion.identity);
        pillar.transform.rotation = Quaternion.AngleAxis(rot, Vector3.up);
        float minScale = 1.0f;
        float maxScale = 3.5f;
        float scale = Random.Range(minScale, maxScale);
        pillar.transform.localScale = new Vector3(scale, 1, scale);
        pillar.GetComponent<Renderer>().material = pillarMaterial;
    }

    void addRing(float x, float z, float y) {
        Transform ring = Instantiate(ringObject, new Vector3(x, y, z), Quaternion.identity);
        ring.GetComponent<Renderer>().material = ringMaterial;
    }

    void addBox(float x, float z, float y)
    {
        Transform box = Instantiate(boxObject, new Vector3(x, y, z), Quaternion.identity);
        float minScale = 12.0f;
        float maxScale = 24.0f;
        float scale = Random.Range(minScale, maxScale);
        box.transform.localScale = new Vector3(scale, scale, scale);
        box.GetComponent<Renderer>().material = boxMaterial;
    }
}
