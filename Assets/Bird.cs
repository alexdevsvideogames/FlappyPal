using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private bool GameOver; 
    private bool DeathRotate;
    public Text GameOverText;
    public Text ScoreText;
    public int score;
    private float RotationSpeed = 200.0f;

    public AudioSource gameaudio;
    public AudioSource punchSFX;
    public AudioClip gameAudioClip;
    public AudioClip punchClip;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        GameOver = false;
        DeathRotate = false;
        score = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        
        gameaudio = GetComponent<AudioSource>();
        gameaudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") & (GameOver==false)) {
            // the cube is going to move upwards in 10 units per second
            rb2D.velocity = new Vector3(0, 100, 0);
        }

        updateText();

        if (GameOver & (Input.GetKeyDown("escape"))) {
            resetGame();
        }

        if (DeathRotate) {
            DeathRotation();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        if (GameOver==false) {
            GameOver = true;
            rb2D.velocity = new Vector3(0, 200, 0);
            DeathRotate = true;
            punchSFX.Play();
        }
    }

    void resetGame() {
        GameOver = false;
        score = 0;
        transform.position = new Vector3(0f,0f,-1.1f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb2D.velocity = new Vector3(0, 0, 0);
        DeathRotate = false;
    }

    void DeathRotation() {
        transform.Rotate( new Vector3(0,0,1) * ( RotationSpeed * Time.deltaTime ));
    }

    void updateText() {
        if (GameOver) {
            GameOverText.enabled = true;
        } else {
            GameOverText.enabled = false;
        }
    }
}
