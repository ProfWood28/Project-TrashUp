using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailCreator : MonoBehaviour
{
    public GameObject emailToCreate;
    public GameObject emailParent;

    GameObject newObject;

    public int nEmails = 0;
    public List<int> mailIDList;
    public List<string> mailKeyWords;
    public List<string> mailContents;

    public EmailDisplay ED;

    public string emailTemplateText1;
    public string emailTemplateText2;

    public int nameIndex;
    public string emailName;
    public List<string> emailNames;
    public List<string> emailAdresses;

    //maybe add time of sending (in game time or irl time idc)
    public List<string> emailTitles;
    public int titleIndex;
    public List<string> emailGenTitles;

    public List<int> commissionPrices;

    // Start is called before the first frame update
    void Start()
    {
        ED = this.GetComponent<EmailDisplay>();
        emailTitles = ED.emailTitles;

        emailTemplateText1 = "Dear Artist, \nCould you please make me an artpiece that incorporates these theme(s): \n \n";
        emailTemplateText2 = "\nI would like to pay you X for the piece. \n\nThank you in advance, \n";

        for(int i = 0; i < 4; i++)
            {           
                if(!PlayerPrefs.HasKey("KeyWords" + i))
                    {
                        EmailContentsCreate(i);
                    }

                else
                    {
                        //EmailContentsCreate(i);
                        EmailContentsLoad(i);
                    }
                
                PlayerPrefs.Save();
                EmailInstantiate(i);            
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EmailInstantiate(int forIndex3)
        {
            nEmails += 1;
            newObject = Instantiate(emailToCreate, new Vector3(332.5f,1080/2+477.5f-152.5f*nEmails + 152.5f/2,0), new Quaternion(0,0,0,0), emailParent.transform);
            newObject.layer = 6;
            newObject.transform.Find("Contents").GetComponent<TMPro.TextMeshProUGUI>().maxVisibleLines = 3;

            newObject.transform.Find("Contents").GetComponent<TMPro.TextMeshProUGUI>().text = mailContents[nEmails-1].Replace("X", "€" + commissionPrices[nEmails-1].ToString() + ",-");
            newObject.transform.Find("Title").GetComponent<TMPro.TextMeshProUGUI>().text = emailGenTitles[nEmails-1];

            if(PlayerPrefs.GetString("Opened" + forIndex3) == "true")
                {
                    newObject.transform.GetChild(7).gameObject.SetActive(false);
                }
            
            mailIDList.Add(newObject.GetInstanceID());
        }

    public void EmailContentsCreate(int forIndex)
        {
            int nItems = Random.Range(1,4);
            
            nameIndex = Random.Range(1,7);
            emailName = NVJOBNameGen.Uppercase(NVJOBNameGen.GiveAName(nameIndex));
            emailNames.Add(emailName);

            emailAdresses.Add((emailName.ToLower() + ED.emailSuffix[Random.Range(0, ED.emailSuffix.Count)]).Replace(" ", ""));

            titleIndex = Random.Range(0, emailTitles.Count);
            emailGenTitles.Add(emailTitles[titleIndex]);

            PlayerPrefs.SetString("Titles" + forIndex, emailTitles[titleIndex]);
            Debug.Log(PlayerPrefs.GetString("Titles" + forIndex));

            string newEmailWords = "";

            for(int a = 0; a < nItems; a++)
                {
                    int itemIndex = Random.Range(0, mailKeyWords.Count-1);

                    if(!newEmailWords.Contains(mailKeyWords[itemIndex]))
                        {
                            newEmailWords += " - " + mailKeyWords[itemIndex] + "\n";
                        }

                    else
                        {
                            a -= 1;       
                        }
                }

            PlayerPrefs.SetString("KeyWords" + forIndex, newEmailWords);
            PlayerPrefs.SetString("Names" + forIndex, emailName);
            PlayerPrefs.SetString("Adresses" + forIndex, emailAdresses[forIndex]);

            Debug.Log(PlayerPrefs.GetString("Adresses" + forIndex));

            //Debug.Log(PlayerPrefs.GetString("KeyWords" + forIndex));
            
            mailContents.Add(emailTemplateText1 + newEmailWords + emailTemplateText2 + emailName);

            int price = Random.Range(150,400);

            PlayerPrefs.SetInt("Prices" + forIndex, price);
            commissionPrices.Add(price);

            PlayerPrefs.SetString("Opened" + forIndex, "false");
        }

    public void EmailContentsLoad(int forIndex2)
        {
            emailAdresses.Add(PlayerPrefs.GetString("Adresses" + forIndex2));

            commissionPrices.Add(PlayerPrefs.GetInt("Prices" + forIndex2));

            string oldEmailWords = PlayerPrefs.GetString("KeyWords" + forIndex2);
            string oldEmailName = PlayerPrefs.GetString("Names" + forIndex2);

            mailContents.Add(emailTemplateText1 + oldEmailWords + emailTemplateText2 + oldEmailName);

            emailGenTitles.Add(PlayerPrefs.GetString("Titles" + forIndex2));
            
            //PlayerPrefs.SetString("Opened" + forIndex2, "false");
        }
}
