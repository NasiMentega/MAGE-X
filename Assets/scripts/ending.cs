using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("level", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
}
