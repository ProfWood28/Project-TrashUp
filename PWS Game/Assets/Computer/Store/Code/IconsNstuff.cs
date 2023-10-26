using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class IconsNstuff : MonoBehaviour
{
    public GameObject InvSys;
    private InvSystem IS;

    public RenderTexture RT;
    public GameObject ObjectPositioner;
    public Camera iconCam;

    public List<Sprite> icons;
    public Sprite tempIcon;
    public GameObject IconPrefab;
    public GameObject IconFolder;
    public GameObject TempFolder;

    int iconIndex = 0;
    int genTimer = 0;

    private bool doGen = true;

    void Awake()
        {
            IS = InvSys.GetComponent<InvSystem>();
        }

    // Start is called before the first frame update
    void Start()
    {
        iconCam.targetTexture = RT;
        
        for (int a = 0; a < IS.menuItems.Count; a++)
        {
            icons.Add(tempIcon);
        }

        SpawnItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(genTimer < IS.menuItems.Count*5)
            {
                GenerateTextures(iconIndex);
                iconIndex++;

                if(iconIndex == IS.menuItems.Count)
                    {
                        iconIndex = 0;
                    }

                genTimer++;
            }

        if(doGen)
            {
                for (int v = 0; v < IS.menuItems.Count; v++)
                    {
                        GameObject placedIcon = Instantiate(IconPrefab, new Vector3(150+50 - 300*v, 0, -550), new Quaternion(0,0,0,0), TempFolder.transform);
                        placedIcon.transform.SetParent(IconFolder.transform, false);
                        placedIcon.transform.position = new Vector3(IS.menuItems.Count*-200, 540-190 , -550);
                        placedIcon.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = icons[v];
                        placedIcon.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = IS.finalItems[v].name.Replace("Final", "");
                        placedIcon.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().text = ("€" + IS.itemPrices[v]);
                        placedIcon.name = "Icon Nr: " + v;
                    }
                
                doGen = false;
            }

        for (int a = 0; a < IS.menuItems.Count; a++)
            {
                GameObject.Find("Icon Nr: " + a).transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = icons[a];
            }
    }

    public void GenIcons()
        {
            for (int i = 0; i < IS.menuItems.Count; i++)
            {
                GameObject objectToIcon = IS.menuItems[i];
                Texture2D icon = PrefabUtility.GetIconForGameObject(objectToIcon);
                
                Sprite iconSprite = Sprite.Create(icon,  new Rect(0, 0, icon.width, icon.height), new Vector2(0.5f, 0.5f));
                icons.Add(iconSprite);
            }
        }

    public Texture2D toTexture2D(RenderTexture rTex)
        {
            Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, -1, false);
            // ReadPixels looks at the active RenderTexture.
            RenderTexture.active = rTex;
            tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            tex.Apply();
            return tex;
        }

    public void GenerateTextures(int loopIndex)
        {
            iconCam.transform.position = ObjectPositioner.transform.position + new Vector3(10*(loopIndex+1) - 2.0f, 1.03004f, -2.0f);

            RenderTexture.active = RT;
            Texture2D icon = toTexture2D(RT);

            Sprite iconSprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), new Vector2(0.5f, 0.5f));
            icons[loopIndex] = iconSprite;
        }

    public void SpawnItems()
        {
            for (int c = 0; c < IS.finalItems.Count; c++)
            {
                int index = c;

                if(index == 0)
                    {
                        index = IS.finalItems.Count;
                    }

                Instantiate(IS.finalItems[c], ObjectPositioner.transform.position + new Vector3(10*index,0,0), new Quaternion(0,0,0,0), ObjectPositioner.transform);
            }
        }
}
