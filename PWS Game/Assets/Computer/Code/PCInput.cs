using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PCInput : MonoBehaviour
{
    public string currentApp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            {
                onCommisionCOmplete();
            }

        if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                PlayerPrefs.DeleteAll();
            }
    }

    public void onIconClick()
        {
            currentApp = EventSystem.current.currentSelectedGameObject.name;
            Debug.Log(currentApp);

            if (currentApp == "Mail")
                {
                    onMailSelect();
                }
        }

    public void onMailSelect()
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("Mail", LoadSceneMode.Single);
        }
    
    public void onPower()
        {
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }

    public void onCommisionCOmplete()
        {
            PlayerPrefs.SetInt("Accept", 0);

            PlayerPrefs.DeleteKey("Titles" + PlayerPrefs.GetInt("MailIndex"));
            PlayerPrefs.DeleteKey("KeyWords" + PlayerPrefs.GetInt("MailIndex"));
            PlayerPrefs.DeleteKey("Names" + PlayerPrefs.GetInt("MailIndex"));
            PlayerPrefs.DeleteKey("Adresses" + PlayerPrefs.GetInt("MailIndex"));
            PlayerPrefs.DeleteKey("Prices" + PlayerPrefs.GetInt("MailIndex"));
            PlayerPrefs.DeleteKey("Opened" + PlayerPrefs.GetInt("MailIndex"));

            orderEmails(PlayerPrefs.GetInt("MailIndex"));
        }

    public void orderEmails(int deleteIndex)
        {
            for (int i = deleteIndex - 1; i >= 0; i--)
                {
                    // Retrieve the contents of the email at index i
                    string keyWords = PlayerPrefs.GetString("KeyWords" + i);
                    string title = PlayerPrefs.GetString("Titles" + i);
                    string name = PlayerPrefs.GetString("Names" + i);
                    string address = PlayerPrefs.GetString("Adresses" + i);
                    int price = PlayerPrefs.GetInt("Prices" + i);
                    string open = PlayerPrefs.GetString("Opened" + i);

                    // Save the contents to the new index (i + 1)
                    PlayerPrefs.SetString("KeyWords" + (i + 1), keyWords);
                    PlayerPrefs.SetString("Titles" + (i + 1), title);
                    PlayerPrefs.SetString("Names" + (i + 1), name);
                    PlayerPrefs.SetString("Adresses" + (i + 1), address);
                    PlayerPrefs.SetInt("Prices" + (i + 1), price);
                    PlayerPrefs.SetString("Opened" + (i + 1), open);
                }

            //delete index 0 email
            PlayerPrefs.DeleteKey("KeyWords0");
            PlayerPrefs.DeleteKey("Titles0");
            PlayerPrefs.DeleteKey("Names0");
            PlayerPrefs.DeleteKey("Adresses0");
            PlayerPrefs.DeleteKey("Prices0");
            PlayerPrefs.DeleteKey("Opened0");
        }
}
