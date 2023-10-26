using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.IO;

public class TimeNday : MonoBehaviour
{
    public GameObject time;
    private TextMeshProUGUI timeText; 
    public GameObject date;
    private TextMeshProUGUI dateText; 
    
    // Start is called before the first frame update
    void Start()
    {
        timeText = time.GetComponent<TextMeshProUGUI>();
        dateText = date.GetComponent<TextMeshProUGUI>();
        InvokeRepeating("timeDayUpdate", 0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void timeDayUpdate()
    {
        timeText.text = System.DateTime.Now.ToString("HH:mm");
        dateText.text = System.DateTime.Now.ToString("dd-MM-yyyy");
    }
}
