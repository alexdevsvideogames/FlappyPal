using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{

    public GameObject[] levels; // all background elements
    public Camera backCamera;
    private Vector2 screenBounds;

    
    // Start is called before the first frame update
    void Start()
    {
        backCamera = gameObject.GetComponent<Camera>();
        screenBounds = backCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, backCamera.transform.position.z));
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        foreach(GameObject obj in levels) {
            loadChildObjects(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadChildObjects(GameObject obj) {
        float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);
        GameObject clone = Instantiate(obj) as GameObject;
        Debug.Log(objectWidth);
        Debug.Log(obj.transform.position);
        for(int i = 0; i <= childsNeeded; i++){
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth*i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void LateUpdate() {
        foreach(GameObject obj in levels){
            repositionChildObjects(obj);
        }
    }

    void repositionChildObjects(GameObject obj) {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        Debug.Log(children.Length);
        if (children.Length > 0) {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;
            if(transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth) {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth*2, lastChild.transform.position.y, lastChild.transform.position.z);
            }else if(transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth) {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth*2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }
}
