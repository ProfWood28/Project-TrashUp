using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailDisplay : MonoBehaviour
{
    EmailCreator EC;
    ButtonInput BI;
    public GameObject emailToDisplay;
    public GameObject DisplayemailParent;

    public GameObject acceptButton;
    public GameObject acceptButtonParent;
    public bool acceptState = false;

    public List<string> emailSuffix;
    public List<string> emailTitles;

    private int saveID = 0;

    SaveToFile stf; 

    // Start is called before the first frame update
    void Start()
    {
        EC = this.GetComponent<EmailCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayEmail(int currentEmailID)
        {
            if(GameObject.Find("Email Display") != null)
                {
                    DestroyImmediate(GameObject.Find("Email Display"));
                    DestroyImmediate(GameObject.Find("Accept"));
                }

            GameObject newObject = Instantiate(emailToDisplay, new Vector3(330,940,0), new Quaternion(0,0,0,0), DisplayemailParent.transform);
            newObject.name = "Email Display";
            
            GameObject.Find("ContentsText").GetComponent<TMPro.TextMeshProUGUI>().text = EC.mailContents[currentEmailID].Replace("X", "€" + EC.commissionPrices[currentEmailID].ToString() + ",-");
            GameObject.Find("SenderText").GetComponent<TMPro.TextMeshProUGUI>().text = EC.emailGenTitles[currentEmailID];
            GameObject.Find("TitleText").GetComponent<TMPro.TextMeshProUGUI>().text = EC.emailAdresses[currentEmailID];

            GameObject newButton = Instantiate(acceptButton, new Vector3(1920/3*2,100,0), new Quaternion(0,0,0,0), acceptButtonParent.transform);
            newButton.name = "Accept";

            GameObject.Find("Accept").GetComponent<ButtonInput>().currentEmailIDCopy = currentEmailID;

            if((acceptState || PlayerPrefs.GetInt("Accept") == 1) && GameObject.Find("Accept").GetComponent<ButtonInput>().currentEmailIDCopy != PlayerPrefs.GetInt("MailIndex"))
                {
                    GameObject.Find("Accept").GetComponent<Image>().color = Color.gray;
                    GameObject.Find("ButtonText").GetComponent<TMPro.TextMeshProUGUI>().text = "Commission ongoing";
                    GameObject.Find("Accept").GetComponent<Button>().interactable = false;
                    GameObject.Find("Accept").GetComponent<RectTransform>().sizeDelta = new Vector2(160, 60);
                }

            if((acceptState || PlayerPrefs.GetInt("Accept") == 1) && GameObject.Find("Accept").GetComponent<ButtonInput>().currentEmailIDCopy == PlayerPrefs.GetInt("MailIndex"))
                {
                    GameObject.Find("Accept").GetComponent<Image>().color = Color.green;
                    GameObject.Find("Accept").GetComponent<Button>().interactable = false;
                    GameObject.Find("ButtonText").GetComponent<TMPro.TextMeshProUGUI>().text = "Active Commission";
                    GameObject.Find("Accept").GetComponent<RectTransform>().sizeDelta = new Vector2(160, 60);
                }
        }
}
