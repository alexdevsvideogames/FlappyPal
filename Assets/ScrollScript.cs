using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public float speed = .1f;
    public Bird bird = new Bird();

    // Update is called once per frame
    void Update() {
        if (bird.GameOver == false) {
            Vector2 offset = new Vector2(Time.time * speed, 0);
            GetComponent<Renderer>().material.mainTextureOffset = offset;
        }
    }

}
