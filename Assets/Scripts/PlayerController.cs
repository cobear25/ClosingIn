using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    bool crashed = false;
    Color startColor;
    private float startZ;
    private float distanceTraveled = 0;
    public Text distanceText;
    public CanvasRenderer gameOverPanel;
    public Text scoreText;
    public Text bestText;
    public Button tryAgainButton;
    ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule emissionModule;

    private void Awake() {
    gameOverPanel.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        foreach (Text text in gameOverPanel.GetComponentsInChildren<Text>()) {
            text.CrossFadeAlpha(0.0f, 0.0f, false);
        } 
        tryAgainButton.enabled = false;
        tryAgainButton.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        tryAgainButton.GetComponentInChildren<Text>().GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        tryAgainButton.onClick.AddListener(TryAgain);
    }

    private void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        //startColor = GetComponent<ParticleSystem>().startColor;
        startColor = myParticleSystem.startColor;
        emissionModule = myParticleSystem.emission;
        startZ = transform.position.z;
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
            distanceTraveled = transform.position.z - startZ;
        } else {
            Vector3 np = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
            transform.position = Vector3.MoveTowards(transform.position, np, step);
            if (moveSpeed > 0) {
                moveSpeed -= 0.1f;
            }
        }
    }

    private void LateUpdate()
    {
        distanceText.text = "DISTANCE TRAVELED:\n" + distanceTraveled.ToString("f1") + " METERS";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall") {
            if (crashed == false)
            {
                Invoke("GameOver", 2);
            }
            transform.GetComponent<Rigidbody>().useGravity = true;
            crashed = true;
            WallCompressor compressor = GameObject.Find("GameObject").GetComponent<WallCompressor>();
            compressor.shouldMove = false;
        } if (collision.gameObject.tag == "ring" && collision.collider is BoxCollider) {
            myParticleSystem.startColor = Color.green;
            emissionModule.rateOverTime = 400;
            Invoke("RevertParticleColor", 2);
            transform.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 0, 100), new Vector3(0, 0, 0), ForceMode.Impulse);
        }
    }

    void RevertParticleColor() {
        myParticleSystem.startColor = startColor;
        emissionModule.rateOverTime = 200;
    }

    void GameOver() {
        scoreText.text = "Score: " + distanceTraveled.ToString("f1");
        float prevBest = PlayerPrefs.GetFloat("bestScore");
        if (distanceTraveled > prevBest) {
            PlayerPrefs.SetFloat("bestScore", distanceTraveled);
            scoreText.text = "NEW RECORD!";
            bestText.text = distanceTraveled.ToString("f1") + " Meters";
        } else {
            bestText.text = "Best: " + prevBest.ToString("f1");
        }
        PlayerPrefs.Save();

        tryAgainButton.enabled = true;
        gameOverPanel.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        foreach (Text text in gameOverPanel.GetComponentsInChildren<Text>())
        {
            text.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        }
        tryAgainButton.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        tryAgainButton.GetComponentInChildren<Text>().GetComponent<CanvasRenderer>().SetAlpha(1.0f);
    }

    void TryAgain() {
        UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex); 
    }
}
