using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC_Tutor : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.2f;
    public bool mapBener = false;
    [SerializeField] private AudioClip clipCall;
    [SerializeField] private AudioClip clipHangUp;
    instruct_help help;
    AudioSource audioSource;
    GameManager gm;
    NPC_manager manager;
    [SerializeField] private TextMeshProUGUI text;
    public string location;

    [SerializeField] private string emergency;
    [SerializeField] private Sprite face;
    [SerializeField] private AudioClip soundsIDN;
    [SerializeField] private AudioClip soundsENG;

    [TextArea]
    [SerializeField] private string dialogIDN;

    [TextArea]
    [SerializeField] private string dialogENG;

    [SerializeField] private string solutionName;
    [SerializeField] private GameObject[] solutions;
    [SerializeField] private GameObject solution;
    [SerializeField] private string call = "";
    public string curr_call = "";
    // Start is called before the first frame update
    void Start()
    {
        mapBener = false;
        playSFX(clipCall);
        help = FindObjectOfType<instruct_help>();
        audioSource = GameObject.Find("suaraNPC").GetComponent<AudioSource>();
        audioSource.Stop();
        text = GameObject.Find("dialog_text").GetComponent<TextMeshProUGUI>();
        manager = FindObjectOfType<NPC_manager>();
        gm = FindObjectOfType<GameManager>();
        solutionName = "1";
        text.text = "...";


        solutions = GameObject.FindGameObjectsWithTag(emergency);
        foreach (GameObject go in solutions)
        {
            if (go.name == solutionName)
            {
                solution = go;
                break;
            }
        }
        //manager.currTotal++;
        gm.timer[manager.currTotal] = 120;
        //help.awal();
    }
    public void awalText()
    {
        if (PlayerPrefs.GetString("language") == "english")
        {
            //audioSource.clip = soundsENG;
            //audioSource.Play();
            text.maxVisibleCharacters = 0;
            text.text = dialogENG;
            StartCoroutine(typing());
        }
        else
        {
            //audioSource.clip = soundsIDN;
            //audioSource.Play();
            text.maxVisibleCharacters = 0;
            text.text = dialogIDN;
            StartCoroutine(typing());
        }
    }

    public void awal()
    {
        help.awal();
    }
    public void mulaiTimer()
    {
        gm.timing = true;
        //gm.munculinMap(true);
    }
    public void voiceOver()
    {
        audioSource.Stop();
        //audioSource.volume = 0.5f;
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
        isVoicing = true;
    }
    bool isVoicing = false;

    // Update is called once per frame
    void Update()
    {
        if(isVoicing)
        {
            if (!audioSource.isPlaying && audioSource.time <= 0)
            {
                mulaiTimer();
                isVoicing = false;
            }
        }
    }
    bool hasChecked = false;
    public void check(GameObject currentSolution)
    {
        if (!hasChecked)
        {
            hasChecked = true;
            if (currentSolution == solution)
            {
                Debug.Log("benar");
                gm.accuracy += 25;
            }
            else
            {
                Debug.Log("salah");
                //gm.accuracy -= 34;
            }
            if (call == curr_call)
            {
                Debug.Log("benar1");
                gm.accuracy += 25;
            }
            else
            {
                Debug.Log("salah1");
                //gm.accuracy -= 34;
            }
            if(gm.timer[manager.currTotal] > 0)
            {
                Debug.Log("benar2");
                gm.accuracy += 25;
            }
            else
            {
                Debug.Log("salah2");
                //gm.accuracy -= 34;
            }
            if(mapBener)
            {
                Debug.Log("benar3");
                gm.accuracy += 25;
            }
            else
            {
                Debug.Log("salah3");
                //gm.accuracy -= 25;
            }
            this.gameObject.GetComponent<Animator>().Play("end");
            text.maxVisibleCharacters = 0;
            text.text = "...";
            StartCoroutine(typing());
            //Invoke("ending", 3f);
        }
    }
    public void ending()
    {
        //gm.win();
        gm.addTime();
        text.maxVisibleCharacters = 0;
        text.text = "...";
        StartCoroutine(typing());
        Destroy(gameObject);
    }
    IEnumerator typing()
    {
        yield return new WaitForSeconds(typingSpeed);
        text.maxVisibleCharacters++;
        if(text.maxVisibleCharacters != text.text.Length)
        {
            StartCoroutine(typing());
        }
    }
    public void playSFX(AudioClip audioClip)
    {
        AudioSource aud = GameObject.Find("suaraSFX").GetComponent<AudioSource>();
        aud.clip = audioClip;
        aud.Play();
    }
    private void OnDestroy()
    {
        //gm.nextCase();
        manager.currTotal++;
    }
    public void hangUp()
    {
        AudioSource aud = GameObject.Find("suaraSFX(loop)").GetComponent<AudioSource>();
        aud.Stop();
        aud.clip = clipHangUp;
        aud.Play();
    }
}
