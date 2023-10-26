using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndMoment : MonoBehaviour
{
    public GameObject OS;
    public SaveToFile stf;

    // Start is called before the first frame update
    void Start()
    {
        OS = GameObject.Find("ObjectSaver");
        stf = OS.GetComponent<SaveToFile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationQuit()
        {
            stf.savePlayerData();
            Debug.Log("Game Ended");
        }
}
