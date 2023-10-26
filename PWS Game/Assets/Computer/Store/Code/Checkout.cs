using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
    public GameObject InvSys;
    private InvSystem IS;

    public List<int> checkoutCounts;

    public GameObject checkoutButton;
    private Vector3 checkoutBPos;
    public GameObject checkOutBG;
    
    public GameObject templateCheckout;

    public bool checkoutOpen = false;

    private float speed = 5.0f;

    private Color initialColor;
    private float fadeSpeed = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        InvSys = GameObject.Find("Inventory system");
        IS = InvSys.GetComponent<InvSystem>();
        
        for (int i = 0; i < IS.menuItems.Count; i++)
        {
            checkoutCounts.Add(0);
        }

        checkOutBG.SetActive(false);
        checkoutBPos = checkoutButton.GetComponent<RectTransform >().position;

        initialColor = checkOutBG.GetComponent<Renderer>().material.color;
        checkOutBG.GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if(checkoutOpen && Vector3.Distance(checkoutBPos - new Vector3(0, 495, 0), checkoutButton.GetComponent<RectTransform >().position) > 0.01f)
            {
                var step = speed * Time.deltaTime; // calculate distance to move
                checkoutButton.GetComponent<RectTransform >().position = Vector3.Lerp(checkoutButton.GetComponent<RectTransform >().position, checkoutBPos - new Vector3(0, 495, 0), step);

                var fade = initialColor.a + (fadeSpeed * Time.deltaTime);
                checkOutBG.GetComponent<Renderer>().material.color = new Color(initialColor.r, initialColor.g, initialColor.b, fade);
            }

        if(!checkoutOpen && Vector3.Distance(checkoutBPos, checkoutButton.GetComponent<RectTransform >().position) > 0.01f)
            {
                Debug.Log("Should be movin back");
                var step = speed * Time.deltaTime; // calculate distance to move
                checkoutButton.GetComponent<RectTransform >().position = Vector3.Lerp(checkoutButton.GetComponent<RectTransform >().position, checkoutBPos, step);
            }
    }

    public void OnCheckoutClick()
        {
            if(!checkoutOpen)
                {
                    float totalPrice = 0;
                    
                    for (int a = 0; a < IS.menuItems.Count; a++)
                    {
                        totalPrice += checkoutCounts[a] * IS.itemPrices[a];
                    }

                    checkoutOpen = true;

                    Debug.Log(totalPrice);
                }

            else
                {
                    checkOutBG.SetActive(false);
                    checkoutOpen = false;

                    Debug.Log("Foek u");
                }
        }
}
