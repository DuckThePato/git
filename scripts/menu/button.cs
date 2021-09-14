using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    void TaskOnClick()
    {
        Status stt = new Status();
        stt.LoadDataJson();


    }
}
