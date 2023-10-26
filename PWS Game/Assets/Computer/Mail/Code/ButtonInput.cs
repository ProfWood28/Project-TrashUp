using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonInput : MonoBehaviour
{
    EmailDisplay ED;
    EmailCreator EC;

    public int currentEmailIDCopy = 0;

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

    public void OnAccept()
        {
            Debug.Log("Accepted the commision :D");
            GameObject.Find("Accept").GetComponent<Image>().color = Color.green;
            GameObject.Find("Accept").GetComponent<Button>().interactable = false;
            GameObject.Find("ButtonText").GetComponent<TMPro.TextMeshProUGUI>().text = "Accepted";
            ED.acceptState = true;

            PlayerPrefs.SetInt("MailIndex", currentEmailIDCopy);
            Debug.Log(PlayerPrefs.GetInt("MailIndex"));

            int acceptTrue = 0;

            if(ED.acceptState)
                {
                    acceptTrue = 1;
                }

            else
                {
                    acceptTrue = 0;
                }


            PlayerPrefs.SetInt("Accept", acceptTrue);

            PlayerPrefs.Save();
        }

    public void OnBack()
        {
            SceneManager.LoadScene("Computer", LoadSceneMode.Single);
        }
}
