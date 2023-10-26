using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveToFile : MonoBehaviour
{
    public List<string> fileNames;

     public void WriteString(List<float> SaveData, string filePath, string fileName)
        {
            if(!fileName.Contains(".txt"))
                {
                    fileName = filePath + fileName + ".txt";
                }
            
            else
                {
                    fileName = filePath + fileName;
                }

            ;

            string path = Application.persistentDataPath + fileName;

            if (!File.Exists(path)) 
                {
                    File.WriteAllText(path, "");
                }

            //Write some text to the test.txt file

            StreamWriter clearing = new StreamWriter(path, false);
            clearing.Close();
            
            StreamWriter writer = new StreamWriter(path, true);

            for(int i = 0; i < SaveData.Count; i++)
                {
                    writer.WriteLine(SaveData[i]);
                }        
            writer.Close();
        }
        
    public List<float> ReadString(string pathName, string fileName)
        {
            if(!fileName.Contains(".txt"))
                {
                    fileName = fileName + ".txt";
                }

            string path = Application.persistentDataPath + pathName + fileName;
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);

            string[] linesStr = System.IO.File.ReadAllLines(path);
            List<float> linesFloat = new List<float>();

            for (var i = 0; i < linesStr.Length; i++)
                {   
                    linesFloat.Add(float.Parse(linesStr[i]));
                }
            reader.Close();

            return linesFloat;  
        }

    public void generateFileNames(string pathAdd)
        {
            fileNames.Clear();

            DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + pathAdd);
            FileInfo[] info = dir.GetFiles("*.*");
            foreach (FileInfo f in info)
                { 
                    fileNames.Add(f.Name);
                }
        }

    public void generateFolders()
        {
             if (!Directory.Exists(Application.persistentDataPath + "/ArtWorks/"))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/ArtWorks/");
                }

            if (!Directory.Exists(Application.persistentDataPath + "/PlayerData/"))
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/PlayerData/");
                }
        }

    public void savePlayerData()
        {
            GameObject player = GameObject.Find("Player"); 
            List<float> playerFloats = new List<float>();
            playerFloats.Add(player.transform.position.x);
            playerFloats.Add(player.transform.position.y);
            playerFloats.Add(player.transform.position.z);
            playerFloats.Add(Camera.main.transform.localEulerAngles.x);
            playerFloats.Add(player.transform.localEulerAngles.y);
            playerFloats.Add(Camera.main.transform.localEulerAngles.z);
            
            WriteString(playerFloats, "/PlayerData/", "playerData");
        }

    public void loadPlayerData()
        {
            List<float> playerLoadFloats = new List<float>();
            playerLoadFloats = ReadString("/PlayerData/", "playerData");

            GameObject player = GameObject.Find("Player");

            Vector3 pos = new Vector3();
            Vector3 rot = new Vector3();

            pos.x = playerLoadFloats[0];
            pos.y = playerLoadFloats[1];
            pos.z = playerLoadFloats[2];

            rot.x = playerLoadFloats[3];
            rot.y = playerLoadFloats[4];
            rot.z = playerLoadFloats[5];

            player.transform.position = pos;
            player.transform.localEulerAngles = new Vector3(0.0f, rot.y, 0.0f);
            Camera.main.transform.localEulerAngles = new Vector3(0.0f, rot.y, rot.z);

            if(rot.x > 270)
                {
                    Camera.main.transform.eulerAngles = new Vector3(0.0f, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
                    player.GetComponent<CamMove>().xRot = rot.x-360;
                }

            else
                {    
                    Camera.main.transform.eulerAngles = new Vector3(rot.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
                }
        }

    // Start is called before the first frame update
    void Start()
    {
        generateFileNames("/ArtWorks/");
        generateFolders();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
