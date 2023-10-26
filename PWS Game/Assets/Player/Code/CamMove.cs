using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    //Horizontal
    public float hSpeed = 1f;
    //Vertical
    public float vSpeed = 1f;
    public float xRot = 0.0f;
    private float yRot = 0.0f;
    private Camera cam;

    private float rotOffset = 180;

    public GameObject Menu;    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Menu = GameObject.Find("MenuUI");

        if(cam.transform.localEulerAngles.x != 0.0f)
            {
                xRot = cam.transform.localEulerAngles.x;
            }

        if(cam.transform.localEulerAngles.y != 0.0f)
            {
                yRot = cam.transform.localEulerAngles.y;
            }

        GameObject.Find("ObjectSaver").GetComponent<SaveToFile>().loadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Menu.activeInHierarchy)
        {
                float mouseX = Input.GetAxis("Mouse X") * hSpeed;
                float mouseY = Input.GetAxis("Mouse Y") * vSpeed;

                yRot += mouseX;
                xRot -= mouseY;
                float xRotClamped = xRot + rotOffset;

                xRot = Mathf.Clamp(xRotClamped, -90 + rotOffset, 90 + rotOffset);
                xRot -= rotOffset;
        
                cam.transform.eulerAngles = new Vector3(xRot, yRot, 0.0f);
        }
    }
}
