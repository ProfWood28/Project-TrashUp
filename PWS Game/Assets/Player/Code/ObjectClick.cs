using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectClick : MonoBehaviour
{
    public List<string> Compare;
    public List<GameObject> Spawn;
    public int index = 0;
    public GameObject menu;

    public SaveToFile stf;

    private GameObject inv;
    private InvSystem cs;

    private BoundingBox bb;

    private bool canClick;
    public bool hitPlace = false;
    public int hitPlaceIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        inv = GameObject.Find("Inventory system");
        cs = inv.GetComponent<InvSystem>();

        Compare = cs.finalItemNames;
        Spawn = cs.menuItems;

        bb = GameObject.Find("Level").GetComponent<BoundingBox>();

        stf = GameObject.Find("ObjectSaver").GetComponent<SaveToFile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray(this.transform.position, Camera.main.transform.forward);
                RaycastHit hitData;

                if (Physics.Raycast(ray, out hitData))
                    {
                        if(hitData.collider.gameObject.tag == "Final" && hitData.distance < 4 && bb.box.Contains(hitData.collider.gameObject.transform.position))
                            {
                                
                                if((GameObject.FindWithTag("Place") != null))
                                    {
                                        canClick = false;
                                    }
                                
                                else
                                    {
                                        canClick = true;
                                    }

                                if (canClick)
                                    {
                                        index = Compare.IndexOf(hitData.collider.gameObject.name);
                                        
                                        GameObject newObject = Instantiate(Spawn[index], hitData.collider.gameObject.transform.position, hitData.collider.gameObject.transform.rotation);
                                        newObject.name = Spawn[index].name;
                                        Destroy(hitData.collider.gameObject);

                                        //currently does not set the spawned active object to its rotation / position
                                        //I mean it does but the active version just resets to its own defaults sadly
                                        
                                        hitPlace = true;
                                        hitPlaceIndex = index;
                                    }
                            }

                        if(hitData.collider.gameObject.tag == "Loader" && hitData.distance < 2)
                            {
                                //Open UI for loading
                                if(!menu.activeSelf)
                                    {
                                        menu.SetActive(true);
                                        if (GameObject.Find("File Name") != null)
                                        {
                                            GameObject.Find("File Name").SetActive(false);
                                        }
                                        
                                        if (GameObject.Find("File Picker") != null)
                                        {
                                            GameObject.Find("File Picker").SetActive(false);
                                        }
                                        
                                    }
                            }

                        if(hitData.collider.gameObject.tag == "PC" && hitData.distance < 2)
                            {
                                stf.savePlayerData();
                                Cursor.lockState = CursorLockMode.Confined;
                                SceneManager.LoadScene("Computer", LoadSceneMode.Single);
                            }
                    }
            }
    }
}