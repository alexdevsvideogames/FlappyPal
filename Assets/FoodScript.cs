using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public float speed = 1f;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //todo random chance of spawn, then isActive=true, random X,Y (on right half of screen)

        if (isActive) {
            transform.Translate(Vector3.left * Time.deltaTime * speed);

            if (transform.position[0] < 600) {
                isActive = false;
            }
        }
    }
}
