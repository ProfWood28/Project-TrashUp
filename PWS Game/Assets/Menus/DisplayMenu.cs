using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    GameObject menu;
    GameObject load;
    GameObject save;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menu = GameObject.Find("MenuUI");
        load = GameObject.Find("File Picker");
        save = GameObject.Find("Saving Stuff");
        save.SetActive(false);
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(menu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

        if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(menu.activeSelf && !load.activeSelf && !save.activeSelf)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        menu.SetActive(false);
                    }
                
                if(load.activeSelf)
                    {
                        load.SetActive(false);
                    }

                if(save.activeSelf)
                    {
                        save.SetActive(false);
                    }
            }
    }
}
