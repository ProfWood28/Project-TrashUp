using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EmaiInput : MonoBehaviour
{
    int mailID;
    GameObject mailObject;
    EmailCreator EC;
    EmailDisplay ED;
    
    // Start is called before the first frame update
    void Start()
    {
        ED = GameObject.Find("EmailCode").GetComponent<EmailDisplay>();
        EC = GameObject.Find("EmailCode").GetComponent<EmailCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEmail()
        {
            mailObject = EventSystem.current.currentSelectedGameObject;
            mailID = mailObject.transform.parent.gameObject.GetInstanceID();

            ED.displayEmail(EC.mailIDList.IndexOf(mailID));

            PlayerPrefs.SetString("Opened" + EC.mailIDList.IndexOf(mailID), "true");

            mailObject.transform.parent.transform.GetChild(7).gameObject.SetActive(false);
        } 
}
