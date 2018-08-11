using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    bool crashed = false;
    Color startColor;

    private void Start()
    {
        startColor = GetComponent<ParticleSystem>().startColor;
    }

    void Update() {
        float step = moveSpeed * Time.deltaTime;

        if (crashed == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 np = new Vector3(transform.position.x + (horizontal * 10), transform.position.y + (vertical * 10), transform.position.z + 10);
            transform.position = Vector3.MoveTowards(transform.position, np, step);
            Quaternion rotation = Quaternion.Euler(vertical * 25, 180, horizontal * 30);
            transform.rotation = rotation;
        } else {
            Vector3 np = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
            transform.position = Vector3.MoveTowards(transform.position, np, step);
            if (moveSpeed > 0) {
                moveSpeed -= 0.1f;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall") {
            transform.GetComponent<Rigidbody>().useGravity = true;
            crashed = true;
            WallCompressor compressor = GameObject.Find("GameObject").GetComponent<WallCompressor>();
            compressor.shouldMove = false;
            Invoke("GameOver", 2);
        } if (collision.gameObject.tag == "ring" && collision.collider is BoxCollider) {
            GetComponent<ParticleSystem>().startColor = Color.green;
            Invoke("RevertParticleColor", 2);
            transform.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 0, 100), new Vector3(0, 0, 0), ForceMode.Impulse);
        }
    }

    void RevertParticleColor() {
        GetComponent<ParticleSystem>().startColor = startColor;
    }

    void GameOver() {
    }
}
