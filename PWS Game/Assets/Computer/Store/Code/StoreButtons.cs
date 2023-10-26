using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoreButtons : MonoBehaviour
{
    public GameObject InvSys;
    private InvSystem IS;

    public GameObject Store;
    private Checkout CO;

    // Start is called before the first frame update
    void Start()
    {
        InvSys = GameObject.Find("Inventory system");
        IS = InvSys.GetComponent<InvSystem>();

        Store = GameObject.Find("Store");
        CO = Store.GetComponent<Checkout>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonClick()
        {
            string[] nameSplit = this.gameObject.name.Split(":");
            int index =  Convert.ToInt32(nameSplit[1]);
            Debug.Log(IS.itemPrices[index]);

            CO.checkoutCounts[index] += 1;
        }
}
