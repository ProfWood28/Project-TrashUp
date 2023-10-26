using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed =10;
    public float Gravity = 9.8f;
    private float velocity = 0;
    public Camera cam;
    private Vector3 movement;
    private Rigidbody rb;
    private bool canMove;

    private GameObject currentObject;
    private ObjectCamFollow script;

    private GameObject inv;
    private InvSystem cs;

    public bool placed;
    public bool final;

    public GameObject Menu;

    private void Start()
    {
        //set names for object (aspects)
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        
        inv = GameObject.Find("Inventory system");
        cs = inv.GetComponent<InvSystem>();
        currentObject = cs.currentObject;

        Menu = GameObject.Find("MenuUI");

        GameObject.Find("ObjectSaver").GetComponent<SaveToFile>().loadPlayerData();
    }
 
    void Update()
    {
        if(!Menu.activeInHierarchy)
            {
                currentObject = cs.currentObject;

                if(GameObject.FindGameObjectsWithTag("Place").Length > 0)
                    {
                        placed = GameObject.FindWithTag("Place").GetComponent<ObjectCamFollow>().placed;
                        final = GameObject.FindWithTag("Place").GetComponent<ObjectCamFollow>().final;
                    }

                if(final == false && placed == false)
                    {
                        canMove = true;
                    }

                else
                    {
                        canMove = false;
                    }

                if (canMove == false)
                    {
                        MovementSpeed = 0;
                    }

                if (canMove == true)
                    {
                        //if left shift (run) is pressed
                        if (Input.GetKey(KeyCode.LeftShift))
                            {
                                //reduce movementspeed to 3
                                MovementSpeed = 15;
                            }

                        //if left control (slow movement key) is pressed
                        if (Input.GetKey(KeyCode.LeftControl))
                            {
                                //reduce movementspeed to 3
                                MovementSpeed = 3;
                            }
                        //if not
                        if (!(Input.GetKey(KeyCode.LeftShift)) && !(Input.GetKey(KeyCode.LeftControl)))
                            {
                                //set movementspeed to normal
                                MovementSpeed = 10;
                            }
                    }

                // player movement - forward, backward, left, right
                float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
                float vertical = Input.GetAxis("Vertical") * MovementSpeed;

                //Rotate the BEAN to the camera rotation
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

                //set variable movement to a Vector3 of the input directions * camera left/right and inputs * the forward direction of the BEAN * forwards/backwards
                //and all this * dt bc you do this every frame
                movement = ((cam.transform.right * horizontal + this.transform.forward * vertical) * Time.deltaTime);

                //move the object using the character controller component
                characterController.Move(movement);
        
                // Gravity (and maybe used for jumping if I add it)
                //if on the floor
                if(characterController.isGrounded)
                    {
                        //vertical speed = 0
                        velocity = 0;
                    }
                //if in the air
                else
                    {
                        //vertical speed = the gravitational constant * dt bc every frame
                        velocity -= Gravity * Time.deltaTime;
                        //move using the chracter controller component
                        characterController.Move(new Vector3(0, velocity, 0));
                    }

                Vector3 vel = rb.velocity;
            }
    }
}