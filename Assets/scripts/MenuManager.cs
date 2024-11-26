using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] no_settings;
    [SerializeField] private GameObject[] settings;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject selectInd;
    [SerializeField] private GameObject selectEng;
    [SerializeField] private Slider bgm_slider;
    [SerializeField] private Slider sfx_slider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject kreditObj;
    static bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        sureReset.SetActive(false);
        kreditObj.SetActive(false);
        if(started)
        {
            anim.Play("stay");
        }
        foreach (GameObject set in settings)
        {
            set.SetActive(bukaSetting);
        }
        if(!PlayerPrefs.HasKey("language"))
        {
            PlayerPrefs.SetString("language", "english");
        }
        if(!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 0);
        }
        //startButton.SetActive(false);
        if (PlayerPrefs.GetString("language") == "english")
        {
            selectEng.SetActive(true);
            selectInd.SetActive(false);
        }
        else
        {
            selectEng.SetActive(false);
            selectInd.SetActive(true);
        }
        if (!PlayerPrefs.HasKey("bgm") || !PlayerPrefs.HasKey("sfx"))
        {
            PlayerPrefs.SetFloat("bgm", 1);
            PlayerPrefs.SetFloat("sfx", 1);
        }
        bgm_slider.value = PlayerPrefs.GetFloat("bgm");
        sfx_slider.value = PlayerPrefs.GetFloat("sfx");
        Time.timeScale = 1;


    }
    bool krediting = false;
    public void kredit()
    {
        krediting = true;
        kreditObj.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("language") == "english")
        {
            selectEng.SetActive(true);
            selectInd.SetActive(false);
        }
        else
        {
            selectEng.SetActive(false);
            selectInd.SetActive(true);
        }

        PlayerPrefs.SetFloat("bgm",bgm_slider.value);
        PlayerPrefs.SetFloat("sfx",sfx_slider.value);
        mixer.SetFloat("bgm", Mathf.Log10(PlayerPrefs.GetFloat("bgm")) * 20);
        mixer.SetFloat("sfx", Mathf.Log10(PlayerPrefs.GetFloat("sfx")) * 20);
        if(Input.GetMouseButtonDown(0))
        {
            AudioSource click = GameObject.Find("SFX").GetComponent<AudioSource>();
            click.Stop();
            click.Play();
            if(krediting)
            {
                krediting = false;
                kreditObj.SetActive(false);
            }
        }

        if(Input.anyKeyDown)
        {
            count++;
            CancelInvoke("resetCount");
            Invoke("resetCount", 0.5f);
            if(count > 2)
            {
                started = true;
                anim.Play("stay");
            }
        }
    }
    int count = 0;
    void resetCount()
    {
        count = 0;
    }
    public void start()
    {
        started = true;
        if(PlayerPrefs.GetInt("level") == 0)
        {
            SceneManager.LoadScene("tutorial");
        }
        else
        {
            SceneManager.LoadScene("gameplay");
        }
    }
    public void resetlvl()
    {
        PlayerPrefs.SetInt("level",0);
        sure(false);
        //start();
    }
    [SerializeField] private GameObject sureReset;
    bool sureBool = false;
    public void sure(bool yesno)
    {
        sureBool = !sureBool;
        sureReset.SetActive(sureBool);
        if(yesno)
        {
            resetlvl();
        }
    }
    public void exitGame()
    {
        Application.Quit();
    }
    bool bukaSetting = false;
    public void setting()
    {
        CancelInvoke("setting");
        bukaSetting = !bukaSetting;
        foreach(GameObject set in settings)
        {
            set.SetActive(bukaSetting);
        }
        foreach(GameObject set in no_settings)
        {
            set.SetActive(!bukaSetting);
        }
        if(bukaSetting)
        {
            Invoke("setting", 5f);
        }
    }
    public void language(GameObject language)
    {
/*        GameObject.Find("english").SetActive(false);
        GameObject.Find("indonesia").SetActive(false);
        startButton.SetActive(true);*/
        PlayerPrefs.SetString("language",language.name);
    }
}
