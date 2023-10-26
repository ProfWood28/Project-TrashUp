using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvSystem : MonoBehaviour
{
    public List<GameObject> menuItems;  
    public List<string> menuItemNames;

    public List<GameObject> finalItems;
    public List<string> finalItemNames;

    public List<int> itemCounts;

    public List<float> itemPrices;

    public float index = 0;
    public GameObject currentObject;
    private int listLength;

    void Awake()
    {
        currentObject = menuItems[0];
        listLength = menuItems.Count;

        for(int i = 0; i < listLength; i++)
            {
                menuItemNames.Add(menuItems[i].name);
                finalItemNames.Add(finalItems[i].name);
                itemCounts.Add(5);
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputMenu = Input.GetAxisRaw("Menus");
        bool axisDown = Input.GetButtonDown("Menus");

        if (axisDown && (GameObject.FindWithTag("Place") == null))
            {
                if(index + inputMenu < 0)
                    {
                        index = listLength;
                    }

                if(index + inputMenu < listLength)
                    {
                        index += inputMenu; 
                    }

                else
                    {
                        index = 0;
                    }
            }

        currentObject = menuItems[Mathf.FloorToInt(index)];
    }
}
