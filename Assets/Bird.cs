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

    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    bool ob1Reset = true;
    bool ob2Reset = true;
    bool ob3Reset = true;
    public GameObject obstacle12;
    public GameObject obstacle22;
    public GameObject obstacle32;

    public AudioSource punchSFX;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        GameOver = false;
        DeathRotate = false;
        score = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
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

        if ((obstacle1.transform.position[0] < 0) & (ob1Reset) & (DeathRotate==false)) {
            ob1Reset = false;
            score = increaseScore(score);
        } else if (obstacle1.transform.position[0] > 500) {
            ob1Reset = true;
        }

        if ((obstacle2.transform.position[0] < 0) & (ob2Reset) & (DeathRotate==false)) {
            ob2Reset = false;
            score = increaseScore(score);
        } else if (obstacle2.transform.position[0] > 500) {
            ob2Reset = true;
        }

        if ((obstacle3.transform.position[0] < 0) & (ob3Reset) & (DeathRotate==false)) {
            ob3Reset = false;
            score = increaseScore(score);
        } else if (obstacle3.transform.position[0] > 500) {
            ob3Reset = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
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
        ScoreText.text = score.ToString();
        transform.position = new Vector3(0f,0f,-1.1f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb2D.velocity = new Vector3(0, 0, 0);
        DeathRotate = false;
        resetPipes();
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

    int increaseScore(int score) {
        score += 1;
        ScoreText.text = score.ToString();
        return score;
    }

    public void resetPipes(){
        obstacle1.transform.position = new Vector3(250.0f,obstacle1.transform.position[1],obstacle1.transform.position[2]);
        obstacle12.transform.position = new Vector3(250.0f,obstacle12.transform.position[1],obstacle12.transform.position[2]);

        obstacle2.transform.position = new Vector3(600.0f,obstacle2.transform.position[1],obstacle2.transform.position[2]);
        obstacle22.transform.position = new Vector3(600.0f,obstacle22.transform.position[1],obstacle22.transform.position[2]);

        obstacle3.transform.position = new Vector3(950.0f,obstacle3.transform.position[1],obstacle3.transform.position[2]);
        obstacle32.transform.position = new Vector3(950.0f,obstacle32.transform.position[1],obstacle32.transform.position[2]);
    }
}
