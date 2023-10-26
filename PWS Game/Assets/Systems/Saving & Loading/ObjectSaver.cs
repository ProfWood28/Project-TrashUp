using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSaver : MonoBehaviour
{
  //List that contains all the floats which all data points are comprised of
  public List<float> saveList = new List<float>(); 
  //List that contains the names of the spawnable objects
  public List<string> objectList = new List<string>();
  //contains the spawnable objects 
  public List<GameObject> spawnList = new List<GameObject>(); 

  //contains the route to the boundingbox
  private BoundingBox b;
  private GameObject go;
  
  //contains the route to the inventory system
  private GameObject inv;
  private InvSystem cs;

    // Start is called before the first frame update
    void Start()
    {
      inv = GameObject.Find("Inventory system");
      cs = inv.GetComponent<InvSystem>();

      //Add the names of the spawnable objects to 'objectList' from invSystem
      objectList = cs.finalItemNames;
      //Add the spawnable objects to 'spawnList' from invSystem
      spawnList = cs.finalItems;

      //add route to boundingbox
      go = GameObject.Find("Level");
      b = go.GetComponent<BoundingBox>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //saving function
    public void saveToList(string fileName)
    {
      //clear the saved data off of the list
      saveList.Clear();

      //find all the objects with the 'final' tag and put them in the array 'objectsToSave'
      GameObject[] objectsToSave = GameObject.FindGameObjectsWithTag("Final");

      //stores the currently-being-saved object's position
      Vector3 pos;
      //stores the currently-being-saved object's rotation in quaternion format
      Quaternion rot;
      //stores the currently-being-saved object
      GameObject currentObject;
      //stores the index of the list-item which matches the name ofthe currently-being-saved object 
      int index = -1;
      //stores the number of to-be-saved objects
      int nObjects = objectsToSave.Length;

      //for every object in 'objectsToSave'
      for(int i = 0; i < nObjects; i++)
        {
          //sets 'currentObject' to the object stored in 'objectsToSave' at position 'i'
          currentObject = objectsToSave[i];
          
          //check if the object is in the building area
          //as to not save objects currently displayed or something
          if (b.box.Contains(currentObject.transform.position))
            {
              //find the name of 'currentObject' in 'objectList' and get it's position in that list for later retrival
              index = objectList.IndexOf(currentObject.name);

              //set 'pos' to the 'currentObject''s postion in Vector3
              pos = currentObject.transform.position;
              //set 'rot' to the 'currentObject''s rotation in Quaternion
              rot = currentObject.transform.rotation;

              //save the listed number as a float to 'saveList'
              //all the data has to be seperated because it has to be stored as a single value class
              //as unity lists do not have multi-class support
              saveList.Add(index); //object index
              saveList.Add(pos.x); //xpos
              saveList.Add(pos.y); //ypos
              saveList.Add(pos.z); //zpos
              saveList.Add(rot.w); //wrot
              saveList.Add(rot.x); //xrot
              saveList.Add(rot.y); //yrot
              saveList.Add(rot.z); //zrot

              //destroy 'currentObject'
              Destroy(currentObject);
            }
        }

      this.gameObject.GetComponent<SaveToFile>().WriteString(saveList, "/ArtWorks/", fileName);

    }

    //loading function
    public void loadFromList(Vector3 offset, string loadFrom)
      {
        
        //stores the to-be-spawned object
        GameObject objectToSpawn;
        //stores the object loaded from memory
        GameObject objectToLoad = new GameObject();
        //stores the to-be-assembled position of the to-be-spawned object
        Vector3 pos = new Vector3();
        //stores the to-be-assembled rotation of the to-be-spawned object
        Quaternion rot = new Quaternion();
        //stores the current value stored at the current position in 'saveList'
        float listPos;

        GameObject checkDestroy;

        //destroys the empty GameObject that gets created as a result of needing the declare 'objectToLoad' is a new Gameobject
        Destroy(GameObject.Find("New Game Object"));


        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Final");
        int nObjects = objectsToDestroy.Length;

        for(int i = 0; i < nObjects; i++)
        {
          checkDestroy = objectsToDestroy[i];
          
          if (b.box.Contains(checkDestroy.transform.position - offset))
            {
              Destroy(checkDestroy);
            }
        }
         
        saveList = this.gameObject.GetComponent<SaveToFile>().ReadString("/ArtWorks/", loadFrom);

        //for every item on 'saveList' divided by 8 (the number of items per object to load)
        for(int i = 0; i < saveList.Count/8; i++)
          {
            //for every 8 items
            for(int a = 0; a < 8; a++)
              {
                //sets the current value stored at the current position in 'saveList'
                listPos = saveList[Mathf.FloorToInt(i*8 + a)];

                //if the current item index is the first of any given object
                if (a == 0)
                  {
                    //set 'objectToLoad' to the GameObject found in 'spawnList' at the index read from 'listPos'
                    objectToLoad = spawnList[Mathf.FloorToInt(listPos)];
                  }

                //if the current item index is the second of any given object
                if (a == 1)
                  {
                    //set the 'x' aspect of the spawn-postion to the value read from 'listPos'
                    pos.x = listPos;
                  }

                //if the current item index is the third of any given object
                if (a == 2)
                  {
                    //set the 'y' aspect of the spawn-postion to the value read from 'listPos'
                    pos.y = listPos;
                  }

                //if the current item index is the fourth of any given object
                if (a == 3)
                  {
                    //set the 'z' aspect of the spawn-postion to the value read from 'listPos'
                    pos.z = listPos;
                  }

                //if the current item index is the fifth of any given object
                if (a == 4)
                  {
                    //set the 'w' aspect of the spawn-rotation to the value read from 'listPos'
                    rot.w = listPos;
                  }

                //if the current item index is the sixth of any given object
                if (a == 5)
                  {
                    //set the 'x' aspect of the spawn-rotation to the value read from 'listPos'
                    rot.x = listPos;
                  }

                //if the current item index is the seventh of any given object
                if (a == 6)
                  {
                    //set the 'y' aspect of the spawn-rotation to the value read from 'listPos'
                    rot.y = listPos;
                  }

                //if the current item index is the eighth of any given object
                if (a == 7)
                  {
                    //set the 'z' aspect of the spawn-rotation to the value read from 'listPos'
                    rot.z = listPos;
                    //spawn the defined object at the defined position with the defined rotation
                    objectToSpawn = Instantiate(objectToLoad, pos + offset, rot);
                    //rename it to the name of the stored object
                    objectToSpawn.name = objectToLoad.name;
                  }
              }
          }
      }
}
