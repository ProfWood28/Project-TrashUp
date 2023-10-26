using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
    public int slot;
    public string currentButton;

    private GameObject Sl;
    private LoadToSlot lts;
    private SaveToFile stf;

    private ObjectSaver os;
    private GameObject go;

    public GameObject field;
    public InputField typeSpace;

    public GameObject FilePicker;
    public TMPro.TMP_Dropdown dropdown;

    public GameObject FilePickerSave;
    public TMPro.TMP_Dropdown dropdownSave;
    public GameObject confirmSave;

    GameObject load;
    GameObject save;
    GameObject CNew;
    GameObject NewSF;
    GameObject COld;

    // Start is called before the first frame update
    void Start()
    {
        slot = 1;
        currentButton = "Slot 1";

        Sl = GameObject.Find("Slot loader");
        lts = Sl.GetComponent<LoadToSlot>();

        go = GameObject.Find("ObjectSaver");
        os = go.GetComponent<ObjectSaver>();
        stf = go.GetComponent<SaveToFile>();

        dropdown = FilePicker.GetComponent<TMPro.TMP_Dropdown>();
        dropdownSave = FilePickerSave.GetComponent<TMPro.TMP_Dropdown>();

        load = GameObject.Find("File Picker");
        save = GameObject.Find("Saving Stuff");
        CNew = GameObject.Find("Confirm New");
        NewSF = GameObject.Find("New SaveFile");
        COld = GameObject.Find("Confirm Old");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()   
    {
        currentButton = EventSystem.current.currentSelectedGameObject.name;
        
        if(currentButton == "Editor")
            {
                slot = 0;
            }

        if(currentButton == "Slot 1")
            {
                slot = 1;
            }

        if(currentButton == "Slot 2")
            {
                slot = 2;
            }
        
        if(currentButton == "Load" && !save.activeSelf)
            {
                stf.generateFileNames("/ArtWorks/");

                //still have to disable this upon startup
                dropdown.ClearOptions();
                dropdown.AddOptions(stf.fileNames);

                if(stf.fileNames.Count == 0)
                    {
                        List<string> noItems = new List<string>();
                        noItems.Add("No saveFiles are available");
                        dropdown.AddOptions(noItems);
                    }

                FilePicker.SetActive(true);
            }

        if(currentButton == "Save" && !load.activeSelf)
            {
                save.SetActive(true);
                field.SetActive(false);
                CNew.SetActive(false);
                COld.SetActive(true);
                NewSF.SetActive(true);
                FilePickerSave.SetActive(true);

                stf.generateFileNames("/ArtWorks/");

                //still have to disable this upon startup
                dropdownSave.ClearOptions();
                dropdownSave.AddOptions(stf.fileNames);

                if(stf.fileNames.Count == 0)
                    {
                        List<string> noItems = new List<string>();
                        noItems.Add("No saveFiles are available");
                        dropdownSave.AddOptions(noItems);
                    }
            }
    }
    
    public void OnNameSubmit()
        {
            os.saveToList(typeSpace.text);
            typeSpace.text = "";
            field.SetActive(false);
            CNew.SetActive(false);
            save.SetActive(false);
        }

    public void OnLoadSubmit()
        {
            string fileName = stf.fileNames[dropdown.value];
            lts.loadSlot(slot, fileName);
            FilePicker.SetActive(false);
        }

    public void onDeleteSubmit()
        {
            string fileName = "/ArtWorks/" + stf.fileNames[dropdown.value];
            System.IO.File.Delete(Application.persistentDataPath + fileName);
            FilePicker.SetActive(false);
        }

    public void onNewSaveSubmit()
        {
            GameObject.Find("New SaveFile").SetActive(false);

            FilePickerSave.SetActive(false);
            COld.SetActive(false);
            confirmSave.SetActive(true);
            field.SetActive(true);
        }
    
    public void onOldSaveSubmit()
        {
            string fileName = stf.fileNames[dropdownSave.value];
            os.saveToList(fileName);
            save.SetActive(false);
        }
}
