using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class instruct_help : MonoBehaviour
{
    [SerializeField] AudioClip[] sfxCall;
    AudioSource audioSource;
    [SerializeField] private AudioClip soundsGibberish;
    [SerializeField] private AudioClip soundsIDN0;
    [SerializeField] private AudioClip soundsIDN;
    [SerializeField] private AudioClip soundsENG0;
    [SerializeField] private AudioClip soundsENG;
    [SerializeField] private TextMeshProUGUI[] textReset;
    [SerializeField] private Button[] tengah;
    [SerializeField] private Button[] calls;
    [SerializeField] private AudioClip[] callsIDN;
    [SerializeField] private AudioClip[] callsENG;
    NPC_manager npc_manager;
    GameManager gm;
    public GameObject guide = null;
    // Start is called before the first frame update
    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        audioSource = GameObject.Find("suaraNPC").GetComponent<AudioSource>();
        foreach (Button call in calls)
        {
            call.interactable = false;
        }
        npc_manager = FindObjectOfType<NPC_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!guide || isclicked)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }
    public void awal()
    {
        audioSource.Stop();
        audioSource.volume = 1;
        if (PlayerPrefs.GetString("language") == "english")
        {
            audioSource.clip = soundsENG0;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = soundsIDN0;
            audioSource.Play();
        }
    }

    public void clicked()
    {
        if (!isclicked)
        {
            //gm.timing = false;
            gm.instructed = true;
            gm.timer[npc_manager.currTotal] = 11f;
            isclicked = true;
            audioSource.Stop();
            audioSource.volume = 1;
            if (PlayerPrefs.GetString("language") == "english")
            {
                audioSource.clip = soundsENG;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = soundsIDN;
                audioSource.Play();
            }
            foreach (Button call in calls)
            {
                call.interactable = true;
            }
            foreach (Button call in tengah)
            {
                call.interactable = false;
            }
            if (PlayerPrefs.GetString("language") == "english")
            {
                Invoke("giberish", 2.7f);
            }
            else
            { 
                Invoke("giberish", 3.9f);            
            }

            //Invoke("end", 10f);
        }
    }
    void giberish()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.volume = 0.3f;
            audioSource.clip = soundsGibberish;
            audioSource.Play();
        }

    }
    [SerializeField] bool callClick = false;
    [SerializeField] bool[] extraCall = {false, false, false};
    string nama;
    public void callClicked(GameObject name)
    {
        audioSource.Stop();
        AudioSource aud = GameObject.Find("suaraSFX(loop)").GetComponent<AudioSource>();
        aud.Stop();

        nama = name.name;
        if (nama == "police")
        {
            //callClick = !callClick;
            extraCall[0] = !extraCall[0];
            callClick = extraCall[0];
        }
        else if (nama == "firefighter")
        {
            //callClick = !callClick;
            extraCall[1] = !extraCall[1];
            callClick = extraCall[1];
        }
        else if (nama == "paramedic")
        {
            //callClick = !callClick;
            extraCall[2] = !extraCall[2];
            callClick = extraCall[2];
        }
        if (SceneManager.GetActiveScene().name != "tutorial" || !npc_manager.npc_tutor)
        {
            if (callClick)
            {
                //callClick = false;
                //audioSource.Stop();
                audioSource.volume = 1;
                //npc_manager.npc.curr_call = name.name;
                if(!npc_manager.npc.curr_call.Contains(name.name))
                {
                    npc_manager.npc.curr_call.Add(name.name);
                }

                for (int i=0;i<calls.Length;i++)
                {
                    if (calls[i].gameObject.name == name.name)
                    {
                        aud.clip = sfxCall[i];
                        aud.Play();
                        calls[i].GetComponent<Image>().color = Color.gray;
                        if (PlayerPrefs.GetString("language") == "english")
                        {
                            audioSource.clip = callsENG[i];
                            audioSource.Play();
                        }
                        else
                        {
                            audioSource.clip = callsIDN[i];
                            audioSource.Play();
                        }
                    }
/*                    else
                    {
                        calls[i].GetComponent<Image>().color = Color.white;
                    }*/
                }
            }
            else
            {
                //callClick = true;
                aud.Stop();
                audioSource.Stop();
                //npc_manager.npc.curr_call = "";
                if (npc_manager.npc.curr_call.Contains(name.name))
                {
                    npc_manager.npc.curr_call.Remove(name.name);
                }
                foreach (Button call in calls)
                {
                    if (call.gameObject.name == name.name)
                    {
                        call.GetComponent<Image>().color = Color.white;
                    }
                }
                //EventSystem.current.SetSelectedGameObject(null);
            }
        }
        else
        {
            if (callClick)
            {
                //callClick = false;
                audioSource.Stop();
                audioSource.volume = 1;
                npc_manager.npc_tutor.curr_call = name.name;
                for (int i = 0; i < calls.Length; i++)
                {
                    if (calls[i].gameObject.name == name.name)
                    {
                        aud.clip = sfxCall[i];
                        aud.Play();
                        calls[i].GetComponent<Image>().color = Color.gray;
                        if (PlayerPrefs.GetString("language") == "english")
                        {
                            audioSource.clip = callsENG[i];
                            audioSource.Play();
                        }
                        else
                        {
                            audioSource.clip = callsIDN[i];
                            audioSource.Play();
                        }
                    }
/*                    else
                    {
                        calls[i].GetComponent<Image>().color = Color.white;
                    }*/
                }
            }
            else
            {
                //callClick = true;
                aud.Stop();
                audioSource.Stop();
                npc_manager.npc_tutor.curr_call = "";
                foreach (Button call in calls)
                {
                    if(call.gameObject.name == name.name)
                    {
                        call.GetComponent<Image>().color = Color.white;
                    }
                }
                //EventSystem.current.SetSelectedGameObject(null);
            }
        }
        callClick = !callClick;
        if (nama == "police")
        {
            //callClick = !callClick;
            extraCall[0] = !callClick;
        }
        else if (nama == "firefighter")
        {
            //callClick = !callClick;
            extraCall[1] = !callClick;
        }
        else if (nama == "paramedic")
        {
            //callClick = !callClick;
            extraCall[2] = !callClick;
        }
    }
    public bool isclicked = false;
    public void end()
    {
        foreach (Button call in calls)
        {
            call.GetComponent<Image>().color = Color.white;
            call.interactable = false;
        }
        if (SceneManager.GetActiveScene().name != "tutorial" || !npc_manager.npc_tutor)
        {
            npc_manager.npc.check(guide);
        }
        else
        {
            npc_manager.npc_tutor.check(guide);
        }
    }
    [SerializeField] private Image image;
    public void resetVar()
    {
        extraCall[0] = false;
        extraCall[1] = false;
        extraCall[2] = false;
        AudioSource aud = GameObject.Find("suaraSFX(loop)").GetComponent<AudioSource>();
        aud.Stop();
        foreach(TextMeshProUGUI text in textReset)
        {
            text.text = "...";
        }

        image.color = new Color32(0x7B,0x99,0xC7,0xff);
        image.sprite = null;
        isclicked = false;
        callClick = false;
        guide = null;
    }
}
