using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.2f;
    public bool mapBener = false;
    [SerializeField] private AudioClip clipCall;
    [SerializeField] private AudioClip clipHangUp;
    instruct_help help;
    Image faceIMG;
    Animator anim;
    AudioSource audioSource;
    NPC_manager manager;
    GameManager gm;
    [SerializeField] private TextMeshProUGUI text;
    public string[] location;

    [System.Serializable]
    struct faces
    {
        public string kasus;
        //0 = mulut tutup, mata buka
        //1 = mulut tutup, mata tutup
        //2 = mulut buka, mata buka
        //3 = mulut buka, mata tutup
        public List<Sprite> faceAnim;
    }
    [SerializeField] private List<faces> structFace;
    [SerializeField] private faces currFace;
    [SerializeField] private Sprite[] face;
    [SerializeField] private AudioClip[] soundsIDN;
    [SerializeField] private AudioClip[] soundsENG;

    public string[] emergency;
    public string current_emergency;
    
    [TextArea]
    [SerializeField] private string[] dialogIDN;

    [TextArea]
    [SerializeField] private string[] dialogENG;

    [SerializeField] private string solutionName;
    [SerializeField] private GameObject[] solutions;
    [SerializeField] private GameObject solution;
    [SerializeField] private List<string> call;
    public List<string> curr_call;
    public int idx;
    [SerializeField] private float timeNPC = 0;
    // Start is called before the first frame update
    void Start()
    {
        faceIMG = GetComponent<Image>();
        anim = GetComponent<Animator>();
/*        call = new string[3];
        call[0] = "";
        call[1] = "";
        call[2] = "";*/
        mapBener = false;
        playSFX(clipCall);
        help = FindObjectOfType<instruct_help>();
        audioSource = GameObject.Find("suaraNPC").GetComponent<AudioSource>();
        audioSource.Stop();
        text = GameObject.Find("dialog_text").GetComponent<TextMeshProUGUI>();
        manager = FindObjectOfType<NPC_manager>();
        gm = FindObjectOfType<GameManager>();
        //current_emergency = emergency[idx];

        if(PlayerPrefs.GetInt("level") < 3)
        {
            timeNPC = 120;
        }
        else
        {
            timeNPC = 180;
        }
        solutionName = "1";
        if (current_emergency == "stomachache")
        {
            //solutionName = "1";
            idx = 0;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "breathing difficulty")
        {
            call.Add("paramedic");
            idx = 1;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "burnt hand")
        {
            call.Add("paramedic");
            idx = 2;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "allergic reactions")
        {
            call.Add("paramedic");
            idx = 3;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "drunk")
        {
            call.Add("police");
            idx = 4;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "stung by a bee")
        {
            call.Add("paramedic");
            idx = 5;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "basketball injury")
        {
            call.Add("paramedic");
            idx = 6;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "dog bites")
        {
            call.Add("paramedic");
            idx = 7;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "domestic violence")
        {
            call.Add("paramedic");
            call.Add("police");
            idx = 8;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "food poisoning")
        {
            call.Add("paramedic");
            idx = 9;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "swallowed a toy")
        {
            call.Add("paramedic");
            idx = 10;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "epilepsy")
        {
            call.Add("paramedic");
            idx = 11;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "heart attack")
        {
            call.Add("paramedic");
            idx = 12;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "hit and run")
        {
            call.Add("paramedic");
            idx = 13;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "electric failure")
        {
            call.Add("firefighter");
            idx = 14;
            gm.timer[manager.currTotal] = timeNPC;
        }
        else if (current_emergency == "sudden death")
        {
            call.Add("paramedic");
            idx = 15;
            gm.timer[manager.currTotal] = timeNPC;
        }
        text.text = "...";
        manager.npc = this;
        //mod
        currFace = structFace[idx];
        faceIMG.sprite = currFace.faceAnim[0];
        //
        solutions = GameObject.FindGameObjectsWithTag(emergency[idx]);
        foreach (GameObject go in solutions)
        {
            if (go.name.Contains(solutionName))
            {
                solution = go;
                break;
            }
        }
        //help.awal();
    }
    public void awalText()
    {
        if (PlayerPrefs.GetString("language") == "english")
        {
            //audioSource.clip = soundsENG[idx];
            //audioSource.Play();
            text.maxVisibleCharacters = 0;
            text.text = dialogENG[idx];
            StartCoroutine(typing());
        }
        else
        {
            //audioSource.clip = soundsIDN[idx];
            //audioSource.Play();
            text.maxVisibleCharacters = 0;
            text.text = dialogIDN[idx];
            StartCoroutine(typing());
        }
    }
    public void awal()
    {
        help.awal();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("talking", isVoicing);
        if (isVoicing)
        {
            if (!audioSource.isPlaying && audioSource.time <= 0)
            {
                faceIMG.sprite = currFace.faceAnim[0];
                mulaiTimer();
                isVoicing = false;
            }
        }
    }
    private bool hasChecked = false;
    GameObject kiky_npc;
    GameObject kikySenang;
    GameObject kikyMarah;
    public void check(GameObject currentSolution)
    {
        if (!hasChecked)
        {
            gm.locationInactive();
            hasChecked = true;
            kiky_npc = GameObject.Find("kiky_NPC");
            kikySenang = kiky_npc.transform.Find("KikySenang_NPC").gameObject;
            kikyMarah = kiky_npc.transform.Find("KikyMarah_NPC").gameObject;
            kikySenang.SetActive(false);
            kikyMarah.SetActive(false);
            int nilai = Mathf.CeilToInt(100f / (manager.maxTotal * 4));
            //Debug.Log(nilai);

            if (currentSolution == solution)
            {
                Debug.Log("solusi benar");
                kikySenang.SetActive(true);
                kikyMarah.SetActive(false);
                gm.accuracy += nilai;
            }
            else
            {
                kikySenang.SetActive(false);
                kikyMarah.SetActive(true);
                Debug.Log("solusi salah");
               // gm.accuracy -= nilai;
            }
            int callBenar = 0;
            foreach(string a in call)
            {
                foreach(string b in curr_call)
                {
                    if (b == a)
                    {
                        //Debug.Log("benar1");
                        callBenar++;
                        //gm.accuracy += nilai;
                        //break;
                    }
/*                    else
                    {
                        Debug.Log("salah1");
                        //gm.accuracy -= nilai;
                    }*/
                }
            }
            if(callBenar >= call.Count)
            {
                Debug.Log("extra call benar");
                gm.accuracy += nilai;
            }
            else
            {
                Debug.Log("extra call salah");
            }

            /*if (gm.timer[manager.currTotal] > 0)
            {
                Debug.Log("tepat waktu");
                gm.accuracy += nilai;
            }*/
            if (gm.waktuResTemp < timeNPC)
            {
                Debug.Log("tepat waktu");
                gm.accuracy += nilai;
            }
            else
            {
                Debug.Log("waktu kelewatan");
                //gm.accuracy -= nilai;
            }
            if (mapBener)
            {
                Debug.Log("map benar");
                gm.accuracy += nilai;
            }
            else
            {
                Debug.Log("map salah");
                //gm.accuracy -= nilai;
            }
            anim.SetTrigger("end");
            kiky_npc.GetComponent<Animator>().Play("report");
            text.maxVisibleCharacters = text.text.Length;
            text.text = "...";
            //StartCoroutine(typing());
            //Invoke("ending", 3f);        
        }

    }
    public void mulaiTimer()
    {
        gm.timing = true;
        //gm.munculinMap(true);
    }
    public void ending()
    {
        gm.addTime();
/*        text.maxVisibleCharacters = text.text.Length;
        text.text = "...";
        StartCoroutine(typing());*/
        Destroy(gameObject);
    }
    IEnumerator typing()
    {
        yield return new WaitForSeconds(typingSpeed);
        text.maxVisibleCharacters++;
        if (text.maxVisibleCharacters != text.text.Length)
        {
            StartCoroutine(typing());
        }
    }
    bool isVoicing = false;
    public void voiceOver()
    {
        audioSource.Stop();
        //audioSource.volume = 0.7f;
        if (PlayerPrefs.GetString("language") == "english")
        {
            audioSource.clip = soundsENG[idx];
            audioSource.Play();
        }
        else
        {
            audioSource.clip = soundsIDN[idx];
            audioSource.Play();
        }
        isVoicing = true;
    }
    public void playSFX(AudioClip audioClip)
    {
        AudioSource aud = GameObject.Find("suaraSFX").GetComponent<AudioSource>();
        aud.clip = audioClip;
        aud.Play();
    }
    private void OnDestroy()
    {
        gm.timing = false;
        help.resetVar();
        gm.nextCase();
        manager.currTotal++;
    }
    public void hangUp()
    {
        AudioSource aud = GameObject.Find("suaraSFX(loop)").GetComponent<AudioSource>();
        aud.Stop();
        aud.clip = clipHangUp;
        aud.Play();
    }
    public void FaceAnim(int idx)
    {
        //Debug.Log(currFace.faceAnim[idx].name);
        if(currFace.faceAnim.Count > idx) faceIMG.sprite = currFace.faceAnim[idx];
    }
}
