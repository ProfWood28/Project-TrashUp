using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCamFollow : MonoBehaviour
{
    public Camera cam;
    public float distance;
    public float minDistance;
    public bool placed;
    public bool final;

    private BoundingBox b;
    private GameObject go;
    private bool inBounds;

    private GameObject inv;
    private InvSystem cs;

    public GameObject objectToSpawn;

    public List<GameObject> spawnItems;  
    private float index;
    public GameObject currentObject; 
    private int listLength;

    private ObjectSpawner os;
    private GameObject player;
    private PlayerController pc;

    public int cancelIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        inv = GameObject.Find("Inventory system");
        cs = inv.GetComponent<InvSystem>();

        spawnItems = cs.finalItems;

        cam = Camera.main;
        distance = 4.0f;
        minDistance = 1.0f;
        placed = false;
        final = false;

        go = GameObject.Find("Level");
        b = go.GetComponent<BoundingBox>();

        index = Mathf.FloorToInt(cs.index);

        os = player.GetComponent<ObjectSpawner>();
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distance + Input.mouseScrollDelta.y/10 > minDistance)
            {
                distance += Input.mouseScrollDelta.y/10;
            }

        inBounds = b.box.Contains(this.transform.position);

        if(Input.GetAxis("Cancel") == 1 || !inBounds)
            {
                cancelIndex = cs.menuItemNames.IndexOf(this.gameObject.name);
                cs.itemCounts[cancelIndex] += 1;
                os.pressed = false;
                pc.placed = false;
                pc.final = false;
                Destroy(this.gameObject);
            }

        index = Mathf.FloorToInt(cs.index);

        if (Input.GetMouseButtonDown(0) && placed == true && final == false && inBounds)   
            {
                if(player.GetComponent<ObjectClick>().hitPlace)
                    {
                        index = player.GetComponent<ObjectClick>().hitPlaceIndex;
                    }

                player.GetComponent<ObjectClick>().hitPlace = false;

                final = true;
                GameObject newObject = Instantiate(spawnItems[Mathf.FloorToInt(index)], this.transform.position, transform.rotation);
                newObject.name = spawnItems[Mathf.FloorToInt(index)].name;
                os.pressed = false;
                pc.placed = false;
                pc.final = false;
                Destroy(this.gameObject);
            }

        if (Input.GetMouseButtonDown(0) && placed == false && final == false && inBounds)
            {
                placed = true;
            }

        if (final == false && placed == false)
            {
                transform.position = (cam.transform.forward* distance + cam.transform.position) ;
                transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
            }
        
        if (placed == true && final == false)
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                float rotate = Input.GetAxis("Rotation");

                transform.rotation = Quaternion.Euler(rotate + this.transform.eulerAngles.x, horizontal + this.transform.eulerAngles.y, vertical + this.transform.eulerAngles.z);

            }
        
    }
}
