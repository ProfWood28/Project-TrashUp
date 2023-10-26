using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailScroll : MonoBehaviour
{
    float scrollIndex;
    float scrollspeed = 5;
    float maxscroll;
    float initialPos;

    // Start is called before the first frame update
    void Start()
    {
        maxscroll = (GameObject.Find("EmailCode").GetComponent<EmailCreator>().nEmails)*152.5f -1080 + 152.5f/2 - 20;
        initialPos = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        mailScroll();
    }

    public void mailScroll()
        {
            scrollIndex = Input.GetAxis("Mouse ScrollWheel");

            if(scrollIndex > 0 && this.transform.position.y > initialPos)
                {
                    this.transform.position -= new Vector3(0,10*scrollspeed,0);
                }

            if(scrollIndex < 0 && this.transform.position.y < initialPos + maxscroll)
                {
                    this.transform.position += new Vector3(0,10*scrollspeed,0);
                }
        }
}
