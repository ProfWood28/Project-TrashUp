using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadToSlot : MonoBehaviour
{
    public GameObject platform;
    public List<Vector3> spawnPoints;

    private ObjectSaver os;
    private GameObject Sl;

    private InputManager im;
    
    // Start is called before the first frame update
    void Start()
    {
        Sl = GameObject.Find("Slot loader");
        os = Sl.GetComponent<ObjectSaver>();

        im = this.GetComponent<InputManager>();

        spawnPoints.Add(new Vector3(0, 0.1f, 0));

        //slot 1
        Instantiate(platform, new Vector3(-20, 0.1f, 20), new Quaternion(0,0,0,0));
        platform.gameObject.transform.localScale = new Vector3(15.0f,0.1f,15.0f);

        spawnPoints.Add(new Vector3(-20, 0.1f, 20));

        //slot 2
        Instantiate(platform, new Vector3(20, 0.1f, 20), new Quaternion(0,0,0,0));
        platform.gameObject.transform.localScale = new Vector3(15.0f,0.1f,15.0f);

        spawnPoints.Add(new Vector3(20, 0.1f, 20));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadSlot(int slotID, string fileName)
    {
        Sl = GameObject.Find("ObjectSaver");
        os = Sl.GetComponent<ObjectSaver>();

        os.loadFromList(spawnPoints[slotID], fileName);
    }
}
