using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public float speed = 100f;
    float shiftamount = 1050f;
    public Bird bird = new Bird();
    float randomY = 300.0f;
    //float randomX = 0.0f;

    public GameObject pipe1;
    public GameObject pipe3;
    public GameObject pipe5;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(350,300,0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((bird.GameOver == false) & (bird.GetReady == false) & (bird.DeathRotate == false)) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        //todo random chance of spawn, then isActive=true, random X,Y (on right half of screen)
        if (transform.position[0] < -500) {
            // random chance to spawn
            float spawnChance = Random.Range(0.0f,100.0f);

            if (spawnChance > 33.0f) {
                randomY = Random.Range(-140.0f,140.0f);
            } else {
                randomY = 300.0f;
            }

            float newX = transform.position[0]+shiftamount*Random.Range(0.95f,1.05f);
            if (((newX - pipe1.transform.position[0]) < 112) & ((randomY - pipe1.transform.position[1]) < 228)) {
                newX += 130;
            }
            if (((newX - pipe3.transform.position[0]) < 112) & ((randomY - pipe3.transform.position[1]) < 228)) {
                newX += 130;
            }
            if (((newX - pipe5.transform.position[0]) < 112) & ((randomY - pipe5.transform.position[1]) < 228)) {
                newX += 130;
            }
            transform.position = new Vector3(newX,randomY,0);
        }
    }

    //void Despawn() {
    //    transform.position = new Vector3(transform.position[0],300,transform.position[2]);
    //}
}
