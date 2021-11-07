using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{

    public GameObject[] obstacle1;
    public GameObject[] obstacle2;
    public GameObject[] obstacle3;

    public float speed = 1f;
    private float obstacleDist = 400f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        //todo respawn when too far left
        // todo randomise heights and gap
    }
}
