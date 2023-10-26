using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float distance;
    private GameObject go;
    private InvSystem cs;
    public bool pressed;

    private BoundingBox b;
    private GameObject lvl;
    private bool inBounds;

    // Start is called before the first frame update
    void Start()
    {
        distance = 4.0f;

        go = GameObject.Find("Inventory system");
        cs = go.GetComponent<InvSystem>();
        objectToSpawn = cs.currentObject;

        pressed = false;

        lvl = GameObject.Find("Level");
        b = lvl.GetComponent<BoundingBox>();

    }

    // Update is called once per frame
    void Update()
    {
        inBounds = b.box.Contains(this.transform.forward*distance + transform.position);

        if (Input.GetKeyDown(KeyCode.LeftAlt) && !pressed && inBounds)
            {
                pressed = true;
                objectToSpawn = cs.currentObject;
                SpawnObject();
            }
    }

    void SpawnObject()
    {
        if (cs.itemCounts[(int)cs.index] -1 >= 0)
            {
                cs.itemCounts[(int)cs.index] -= 1;
                GameObject newObject = Instantiate(objectToSpawn, this.transform.forward*distance + transform.position, transform.rotation);
                newObject.name = objectToSpawn.name;
            }
    }
}
