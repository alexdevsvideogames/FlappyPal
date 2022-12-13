using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public bool GameOver; 
    public bool DeathRotate;
    public bool GetReady;
    public Text GameOverText;
    public Text ScoreText;
    public Text HiScoreText;
    public Text GetReadyText;
    public Text CreditsText;
    public int score;
    private float RotationSpeed = 200.0f;
    private float fallSpeed = 140.0f;
    public float FlapDuration = 0.01f;
    private float GrowDuration = 5f;
    private bool growBird = false;
    private bool shrinkBird = false;
    private bool addBonus = false;
    float pipe1Y = -200; //220
    float pipe2Y = 190; //200
    IEnumerator birdGrow;
    //IEnumerator growBird;
    //IEnumerator shrinkBird;

    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    bool ob1Reset = true;
    bool ob2Reset = true;
    bool ob3Reset = true;
    public GameObject obstacle12;
    public GameObject obstacle22;
    public GameObject obstacle32;
    public GameObject food;

    public AudioSource punchSFX;
    public AudioSource flapSFX;
    public AudioSource munchSFX;
    public AudioSource growSFX;
    public AudioSource shrinkSFX;

    public SpriteRenderer spriteRenderer;
    public Sprite birdSprite;
    public Sprite birdFlapSprite;

    void Awake ()
    {
        HiScoreText.text = PlayerPrefs.GetInt("HiScore", 0).ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        GameOver = false;
        DeathRotate = false;
        GetReady = true;
        score = 0;
        ScoreText.text = score.ToString();
        HiScoreText.enabled = false;
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
        spriteRenderer.sprite = birdSprite; 
        transform.position = new Vector3(0f,0f,-1.1f);
        rb2D.velocity = new Vector3(0, 0, 0);
        rb2D.mass = 100;
        transform.localScale = new Vector3(17.0f,20.0f,1.0f);

        ob1Reset = true;
        ob2Reset = true;
        ob3Reset = true;
        growBird = false;
        shrinkBird = false;
        addBonus = false;

        resetPipes();
        birdGrow = GrowBirdCoroutine(GrowDuration);
        StopCoroutine(birdGrow);
        food.transform.position = new Vector3(food.transform.position[0], 300, food.transform.position[2]);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetReady & Input.GetMouseButtonDown((0))) {
            StartGame();
        }

        if (Input.GetMouseButtonDown((0)) & (GameOver==false)) {
            BirdFlap();
        }

        updateText();

        if (GameOver & Input.GetMouseButtonDown((0))) {
            resetGame();
        }

        if (GetReady) {
            transform.position = new Vector3(0f,0f,-1.1f);
        }

        if (DeathRotate) {
            DeathRotation();
        }

        if ((GameOver == false) & (GetReady == false)) {
            transform.Rotate( new Vector3(0,0,1) * ( -fallSpeed * Time.deltaTime ));
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

        if (growBird) {
            transform.localScale += new Vector3(0.25f, 0.25f, 0f);
            rb2D.mass += 50.0f;
        }

        if (shrinkBird) {
            transform.localScale -= new Vector3(0.25f, 0.25f, 0f);
            rb2D.mass -= 50.0f;
        }

        if (addBonus) {
            score = bonusScore(score);
            addBonus = false;
        }

        if(score>PlayerPrefs.GetInt("HiScore", 0))
        {
            PlayerPrefs.SetInt("HiScore", score);
            HiScoreText.text = score.ToString();
        }
    }

    void StartGame() {
        Debug.Log("START");
        GetReady = false;
        GetReadyText.enabled = false;
    }

    void BirdFlap() {
        spriteRenderer.sprite = birdSprite;  
        // the cube is going to move upwards in 10 units per second
        rb2D.velocity = new Vector3(0, 350, 0);
        transform.rotation = Quaternion.Euler(0,0,60);
        flapSFX.Play();
        spriteRenderer.sprite = birdFlapSprite;  
        StartCoroutine(FlapCoroutine());
    }

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.tag);
        if (col.tag == "pipe") {
            if (GameOver==false) {
                GameOver = true;
                rb2D.velocity = new Vector3(0, 200, 0);
                DeathRotate = true;
                punchSFX.Play();
            }
        }
        if (col.tag == "food") {
            munchSFX.Play();
            addBonus = true;
            food.transform.position = new Vector3(food.transform.position[0], 300, food.transform.position[2]);
            //growBird = true;
            StartCoroutine(birdGrow);
        }
    }

    // reset game same as start()
    void resetGame() {
        Debug.Log("RESET");
        GameOver = false;
        DeathRotate = false;
        GetReady = true;
        GetReadyText.enabled = true;
        score = 0;
        ScoreText.text = score.ToString();
        HiScoreText.enabled = false;
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
        spriteRenderer.sprite = birdSprite; 
        transform.position = new Vector3(0f,0f,-1.1f);
        rb2D.velocity = new Vector3(0, 0, 0);
        rb2D.mass = 100;
        transform.localScale = new Vector3(17.0f,20.0f,1.0f);

        ob1Reset = true;
        ob2Reset = true;
        ob3Reset = true;
        //growBird = false;
        //shrinkBird = false;
        addBonus = false;

        resetPipes();
        StopCoroutine(birdGrow);
        birdGrow = GrowBirdCoroutine(GrowDuration);
        food.transform.position = new Vector3(food.transform.position[0], 300, food.transform.position[2]);
    }

    void DeathRotation() {
        transform.Rotate( new Vector3(0,0,1) * ( RotationSpeed * Time.deltaTime ));
    }

    void updateText() {
        if (GameOver) {
            GameOverText.enabled = true;
            CreditsText.enabled = true;
            int HiScore = PlayerPrefs.GetInt("HiScore", 0);
            if (HiScore == score) {
                HiScoreText.text = "NEW HIGH SCORE!!";
            } else {
                HiScoreText.text = "High Score: " + PlayerPrefs.GetInt("HiScore", 0).ToString();
            }
            HiScoreText.enabled = true;
        } else {
            GameOverText.enabled = false;
            CreditsText.enabled = false;
            HiScoreText.enabled = false;
        }
    }

    int increaseScore(int score) {
        score += 1;
        ScoreText.text = score.ToString();
        return score;
    }

    int bonusScore(int score) {
        score += 3;
        ScoreText.text = score.ToString();
        return score;
    }

    public void resetPipes(){
        float RandStrength = 70.0f;
        float gap = Random.Range(-RandStrength, RandStrength);
        float shift = Random.Range(-RandStrength, RandStrength);

        obstacle1.transform.position = new Vector3(550.0f,pipe1Y+shift+gap/2,obstacle1.transform.position[2]);
        obstacle12.transform.position = new Vector3(550.0f,pipe2Y+shift+gap/2,obstacle12.transform.position[2]);

        obstacle2.transform.position = new Vector3(900.0f,pipe1Y+shift+gap/2,obstacle2.transform.position[2]);
        obstacle22.transform.position = new Vector3(900.0f,pipe2Y+shift+gap/2,obstacle22.transform.position[2]);

        obstacle3.transform.position = new Vector3(1250.0f,pipe1Y+shift+gap/2,obstacle3.transform.position[2]);
        obstacle32.transform.position = new Vector3(1250.0f,pipe2Y+shift+gap/2,obstacle32.transform.position[2]);
    }

    IEnumerator FlapCoroutine() {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = birdSprite; 
    }

    IEnumerator GrowBirdCoroutine(float GrowDuration) {
        //growBird = growBirdCoRoutine(GrowDuration/8);
        //shrinkBird = shrinkBirdCoRoutine(GrowDuration/8);
        yield return new WaitForSeconds(0.2f);
        Debug.Log("growing...");
        growBird = true;
        growSFX.Play();
        //StartCoroutine(growBird);
        yield return new WaitForSeconds(GrowDuration/8);
        //StopCoroutine(growBird);
        growBird = false;
        Debug.Log("steady...");
        yield return new WaitForSeconds(GrowDuration/4*3);
        shrinkBird = true;
        Debug.Log("shrinking...");
        shrinkSFX.Play();
        //StartCoroutine(shrinkBird);
        yield return new WaitForSeconds(GrowDuration/8);
        //StopCoroutine(shrinkBird);
        shrinkBird = false;
    }

    /*IEnumerator growBirdCoRoutine(float Duration) {
        transform.localScale += new Vector3(0.02f, 0.02f, 0f);
        rb2D.mass += 4.0f;
        yield return new WaitForSeconds(Duration);
    }

    IEnumerator shrinkBirdCoRoutine(float Duration) {
        transform.localScale -= new Vector3(0.02f, 0.02f, 0f);
        rb2D.mass -= 4.0f;
        yield return new WaitForSeconds(Duration);
    }*/

}
