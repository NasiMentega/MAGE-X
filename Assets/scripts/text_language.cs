using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class text_language : MonoBehaviour
{
    [Header("dimskin ke TextMeshPro yg gk akan diganti2 (kek UI gt)")]
    [TextArea]
    [SerializeField] private string textIDN;
    [TextArea]
    [SerializeField] private string textENG;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(PlayerPrefs.GetString("language") == "english")
        {
            text.text = textENG;
        }
        else
        {
            text.text = textIDN;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("language") == "english")
        {
            text.text = textENG;
        }
        else
        {
            text.text = textIDN;
        }
    }
}
