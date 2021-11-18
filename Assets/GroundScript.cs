using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public float speed = 1f;
    public Bird bird = new Bird();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.GameOver == false) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        //Debug.Log(bird.GameOver);

        if(transform.position[0] < -1300) {
            transform.position = new Vector3(transform.position[0]+1638*2,transform.position[1],transform.position[2]);
        }
    }
}
