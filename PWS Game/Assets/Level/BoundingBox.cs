using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public Bounds box;
    public GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        //building area
        box = new Bounds(new Vector3(0, 7, 0), new Vector3(15, 16, 15));
        Instantiate(platform, new Vector3(0, 0.1f, 0), new Quaternion(0,0,0,0));
        platform.gameObject.transform.localScale = new Vector3(15.0f,0.1f,15.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
