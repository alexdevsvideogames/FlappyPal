using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public GameObject pipe1;
    public GameObject pipe2;
    public Bird bird = new Bird();

    float speed;
    //private float obstacleDist = 400f;
    float pipe1Y = -200; //220
    float pipe2Y = 190; //200
    float shiftamount = 1050f;
    float RandStrength = 70.0f;
    float sineStrength = 0.15f;
    float angle = 0.0f;
    public float resetX;

    // Start is called before the first frame update
    void Start()
    {
        randomisePipes(0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.GameOver == false) {
            if (bird.score >= 10) {
                pipe1.transform.Translate((Vector3.left + new Vector3 (0,sineStrength*Mathf.Sin(angle),0)) * Time.deltaTime * speed);
                pipe2.transform.Translate((Vector3.right + new Vector3 (0,-sineStrength*Mathf.Sin(angle),0)) * Time.deltaTime * speed);
            } else {
                pipe1.transform.Translate(Vector3.left * Time.deltaTime * speed);
                pipe2.transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
        }

        angle += 0.02f;

        if (bird.GetReady == true) {
            speed = 0;
        } else {
            speed = 100f;
        }

        //todo respawn when too far left
        if(pipe1.transform.position[0] < -520) {
            randomisePipes(shiftamount);
        }

    }

    void randomisePipes(float shiftamount) {
        float gap = Random.Range(-RandStrength, RandStrength);
        float shift = Random.Range(-RandStrength, RandStrength);
        pipe1.transform.position = new Vector3(pipe1.transform.position[0]+shiftamount,pipe1Y+shift+gap/2,pipe1.transform.position[2]);
        pipe2.transform.position = new Vector3(pipe2.transform.position[0]+shiftamount,pipe2Y+shift+gap/2,pipe2.transform.position[2]);
    }
}
