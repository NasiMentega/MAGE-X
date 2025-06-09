using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject nxtLvl;
    [SerializeField] private GameObject[] locations;
    [SerializeField] private Button[] aktifTiming;
    [SerializeField] private GameObject map;
    instruct_help help;
    GameObject guide_parent;
    [SerializeField] private AudioClip winSFX;
    //[SerializeField] private AudioClip winVoice;
    [SerializeField] private AudioClip loseSFX;
    [SerializeField] private AudioClip[] loseVoice;
    //[SerializeField] private GameObject[] guide_highlights;
    //[SerializeField] private int[] guide_highlightsIdx;
    [SerializeField] private GameObject guideParent;
    [SerializeField] private GameObject[] guides;
    [SerializeField] private List<string> guidesEmergency;

     [SerializeField] private List<GameObject> guidesClone = new List<GameObject>();
     [SerializeField] private List<GameObject> index = new List<GameObject>();
     [SerializeField] private List<int> index2 = new List<int>();

    [SerializeField] private TextMeshProUGUI waktuRes;
    public float waktuResTemp = 0;
    public List<float> waktuRestotal;
    [SerializeField] private Slider accuracyBar;
    [SerializeField] private GameObject caseReport;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI timerText;
    public float[] timer;
    NPC_manager npc_manager;
    [SerializeField] private int level;
    public bool isPaused = false;
    public int accuracy;
    // Start is called before the first frame update
    void Awake()
    {
        guidesEmergency = new List<string>();
        index = new List<GameObject>();
        index2 = new List<int>();
        /*        ColorBlock location = GameObject.Find("LOCATION").GetComponent<Button>().colors;
                location.normalColor = new Color(1, 0.97f, 0.68f, 1);
                GameObject.Find("LOCATION").GetComponent<Button>().colors = location;*/
        GameObject.Find("LOCATION").GetComponent<Image>().color = new Color(1, 0.97f, 0.68f, 1);
        foreach (Button obj in aktifTiming)
        {
            obj.interactable =false;
        }
        map.SetActive(false);
        //caseReport.SetActive(false);
        caseReport.GetComponent<CanvasGroup>().interactable = false;
        caseReport.GetComponent<CanvasGroup>().alpha = 0;
        caseReport.GetComponent<CanvasGroup>().blocksRaycasts = false;
        guide_parent = GameObject.Find("Content");
        help = FindObjectOfType<instruct_help>();
        accuracyBar.value = 100;
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        npc_manager = FindObjectOfType<NPC_manager>();
        level = PlayerPrefs.GetInt("level");
        if (level == 0)
        {
            npc_manager.maxTotal = 1;
            timer = new float[npc_manager.maxTotal];
            //timer[npc_manager.currTotal] = 0;
            //GameObject tutor = FindObjectOfType<NPC_Tutor>().gameObject;
            //tutor.SetActive(true);
        }
        else
        {
            //GameObject tutor = FindObjectOfType<NPC_Tutor>().gameObject;
            //tutor.SetActive(false);

            //demo
            if (level == 1)
            {
                npc_manager.maxTotal = 3;
                timer = new float[npc_manager.maxTotal];
                guidesEmergency.Add("epilepsy");
                guidesEmergency.Add("hit and run");
                guidesEmergency.Add("heart attack");
            }
            //
            /*if (level == 1)
            {
                npc_manager.maxTotal = 3;
                timer = new float[npc_manager.maxTotal];
                guidesEmergency.Add("breathing difficulty");
                guidesEmergency.Add("allergic reactions");
                guidesEmergency.Add("dog bites");
            }*/
            else if (level == 2)
            {
                npc_manager.maxTotal = 5;
                timer = new float[npc_manager.maxTotal];
                guidesEmergency.Add("burnt hand");
                guidesEmergency.Add("stung by a bee");
                guidesEmergency.Add("basketball injury");
                guidesEmergency.Add("epilepsy");
                guidesEmergency.Add("hit and run");
            }
            else if (level == 3)
            {
                npc_manager.maxTotal = 7;
                timer = new float[npc_manager.maxTotal];
                guidesEmergency.Add("drunk");
                guidesEmergency.Add("domestic violence");
                guidesEmergency.Add("food poisoning");
                guidesEmergency.Add("swallowed a toy");
                guidesEmergency.Add("heart attack");
                guidesEmergency.Add("electric failure");
                guidesEmergency.Add("sudden death");
            }
            spawnGuides();
            nextCase();
        }
    }
    public void callKiky()
    {
        //guidesClone[3].transform.SetAsFirstSibling();
        foreach(GameObject gui in guidesClone)
        {
            if(gui.tag == npc_manager.npc.current_emergency)
            {
                AudioSource audioSource = GameObject.Find("suaraNPC").GetComponent<AudioSource>();
                audioSource.Stop();
                audioSource.volume = 1;
                if (PlayerPrefs.GetString("language") == "english")
                {
                    audioSource.clip = kikyENG;
                    audioSource.Play();
                }
                else
                {
                    audioSource.clip = kikyIND;
                    audioSource.Play();
                }
                gui.transform.SetAsFirstSibling();
                //ColorBlock color = gui.GetComponent<Button>().colors;
                //color.normalColor = new Color(1, 0.97f, 0.68f, 1);
                //gui.GetComponent<Button>().colors = color;
                gui.GetComponent<Image>().color = new Color(1, 0.97f, 0.68f, 1);
            }
        }
    }
    public void locationInactive()
    {
        foreach (Button obj in aktifTiming)
        {
            obj.interactable = false;
        }
    }
    void spawnGuides()
    {
        /*        for(int i = 0;i< guide_highlightsIdx.Length; i++)
                {
                    GameObject clone = Instantiate(guides[guide_highlightsIdx[i]]);
                    clone.name = guide_highlightsIdx[i].ToString();
                    clone.transform.parent = guide_parent.transform;
                }*/

        int idx = 0;
        for (int i = 0; i < guidesEmergency.Count; i++)
        {
            for(int j = 0; j < guides.Length;j++)
            {
                if (guides[j].tag == guidesEmergency[i])
                {
                    index.Add(guides[j]);
                    index2.Add(idx);
                    idx++;
                }
            }
        }
        for (int i = 0; i < index.Count; i++)
        {
            int random = Random.Range(0, index2.Count);
            GameObject clone = Instantiate(index[index2[random]], guideParent.transform);
            //clone.transform.SetParent(guideParent.transform);
            guidesClone.Add(clone);
            index.Remove(clone);
            index2.RemoveAt(random);
            //clone.name = guidesIdx[i].ToString();
            clone.transform.SetParent(guide_parent.transform, false);
        }
    }
    [SerializeField] private AudioClip tandaiLokasiENG;
    [SerializeField] private AudioClip kikyENG;
    [SerializeField] private AudioClip tandaiLokasiIND;
    [SerializeField] private AudioClip kikyIND;
    bool mapping = false;
    public bool locationMarked = false;
    public void munculinMap(bool count)
    {
        locationMarked = true;
        if(!count)
        {
            AudioSource audioSource = GameObject.Find("suaraNPC").GetComponent<AudioSource>();
            audioSource.Stop();
            audioSource.volume = 1;
            if (PlayerPrefs.GetString("language") == "english")
            {
                audioSource.clip = tandaiLokasiENG;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = tandaiLokasiIND;
                audioSource.Play();
            }
        }
/*        ColorBlock location = GameObject.Find("LOCATION").GetComponent<Button>().colors;
        location.normalColor = Color.white;
        GameObject.Find("LOCATION").GetComponent<Button>().colors = location;*/
        GameObject.Find("LOCATION").GetComponent<Image>().color = Color.white;


        /*        foreach (Button obj in aktifTiming)
                {
                    obj.interactable = false;
                }*/
        mapping = count;
        map.SetActive(mapping);
    }
    public bool timing = false;
    bool sudahAktif = false;
    // Update is called once per frame
    void Update()
    {
        if (!sudahAktif && timing)
        {
            sudahAktif = true;
            foreach (Button obj in aktifTiming)
            {
                obj.interactable = true;
            }            
        }

        if (npc_manager.npc)
        {
            foreach(GameObject lokasi in locations)
            {
                if(lokasi.name == npc_manager.npc.location[npc_manager.npc.idx])
                {
                    lokasi.SetActive(true);
                }
                else
                {
                    lokasi.SetActive(false);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            AudioSource click = GameObject.Find("SFX").GetComponent<AudioSource>();
            click.Stop();
            click.Play();
        }
        if (accuracy > 100)
        {
            accuracy = 100;
        }
        else if(accuracy < 0)
        {
            accuracy = 0;
        }
        accuracyBar.value = accuracy;
        accuracyText.text = accuracy.ToString() + "%";
        //pauseUI.SetActive(isPaused);
/*        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused();
        }*/

        /*        if(SceneManager.GetActiveScene().name != "tutorial")
                {
                    if (npc_manager.currTotal > 0 && npc_manager.currTotal < npc_manager.maxTotal && timer[npc_manager.currTotal-1] > 0 && !winning && timing)
                    {
                        timer[npc_manager.currTotal-1] -= Time.deltaTime;
                        waktuResTemp += Time.deltaTime;
                    }
                    else if(npc_manager.currTotal > 0 && npc_manager.currTotal < npc_manager.maxTotal && timer[npc_manager.currTotal-1] <= 0 && !winning && timing)
                    {
                        if (SceneManager.GetActiveScene().name != "tutorial" && !npc_manager.npc_tutor)
                        {
                            npc_manager.npc.check(null);
                        }
                        else
                        {
                            npc_manager.npc_tutor.check(null);
                        }
                        timer[npc_manager.currTotal - 1] = 0;
                    }
                    else if(npc_manager.currTotal > 0 && npc_manager.currTotal < npc_manager.maxTotal && winning)
                    {
                        timer[npc_manager.currTotal - 1] = 0;
                    }

                    if(npc_manager.currTotal > 0 && npc_manager.currTotal < npc_manager.maxTotal)
                    {
                        int menit = Mathf.FloorToInt(timer[npc_manager.currTotal - 1] / 60);
                        int detik = Mathf.FloorToInt(timer[npc_manager.currTotal - 1] % 60);
                        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
                    }
                    else
                    {
                        int menit = 0;
                        int detik = 0;
                        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
                    }
                }
                else
                {
                    if (npc_manager.currTotal < npc_manager.maxTotal && timer[npc_manager.currTotal] > 0 && !winning && timing)
                    {
                        timer[npc_manager.currTotal] -= Time.deltaTime;
                        waktuResTemp += Time.deltaTime;
                    }
                    else if (npc_manager.currTotal < npc_manager.maxTotal && timer[npc_manager.currTotal] <= 0 && !winning && timing)
                    {
                        if (SceneManager.GetActiveScene().name != "tutorial" && !npc_manager.npc_tutor)
                        {
                            npc_manager.npc.check(null);
                        }
                        else
                        {
                            npc_manager.npc_tutor.check(null);
                        }
                        timer[npc_manager.currTotal] = 0;
                    }
                    else if (npc_manager.currTotal < npc_manager.maxTotal && winning)
                    {
                        timer[npc_manager.currTotal] = 0;
                    }

                    if (npc_manager.currTotal < npc_manager.maxTotal)
                    {
                        int menit = Mathf.FloorToInt(timer[npc_manager.currTotal] / 60);
                        int detik = Mathf.FloorToInt(timer[npc_manager.currTotal] % 60);
                        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
                    }
                    else
                    {
                        int menit = 0;
                        int detik = 0;
                        timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
                    }
                }*/
        if (npc_manager.currTotal < npc_manager.maxTotal && timer[npc_manager.currTotal] > 0 && !winning && timing)
        {
            timer[npc_manager.currTotal] -= Time.deltaTime;
            if (!instructed)
            {
                waktuResTemp += Time.deltaTime;
            }
        }
        else if (npc_manager.currTotal < npc_manager.maxTotal && timer[npc_manager.currTotal] <= 0 && !winning && timing)
        {
            if (instructed)
            {
                instructed = false;
                instruct_help help = FindObjectOfType<instruct_help>();
                help.end();
            }
            else
            {
                if (SceneManager.GetActiveScene().name != "tutorial" && !npc_manager.npc_tutor)
                {
                    npc_manager.npc.check(null);
                }
                else
                {
                    npc_manager.npc_tutor.check(null);
                }
            }
            timer[npc_manager.currTotal] = 0;
        }
        else if (npc_manager.currTotal < npc_manager.maxTotal && winning)
        {
            timer[npc_manager.currTotal] = 0;
        }

        if (npc_manager.currTotal < npc_manager.maxTotal)
        {
            int menit = Mathf.FloorToInt(timer[npc_manager.currTotal] / 60);
            int detik = Mathf.FloorToInt(timer[npc_manager.currTotal] % 60);
            timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
        }
        else
        {
            int menit = 0;
            int detik = 0;
            timerText.text = string.Format("{0:00}:{1:00}", menit, detik);
        }
    }
    public bool instructed = false;
    public bool winning = false;
    public void paused()
    {
        if(!winning)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    [SerializeField] private bool hasPaused = false;
    [SerializeField] private AudioClip pauseIDN;
    [SerializeField] private AudioClip pauseENG;
    public void pauseLife()
    {
        if (!winning && !hasPaused)
        {
            AudioSource audioSource = GameObject.Find("suaraNPC").GetComponent<AudioSource>();
            audioSource.Stop();
            if (PlayerPrefs.GetString("language") == "english")
            {
                audioSource.clip = pauseENG;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = pauseIDN;
                audioSource.Play();
            }
            hasPaused = true;
            timing = isPaused;
            isPaused = !isPaused;
            if (isPaused)
            {
                Invoke("pauseLife", 10);
                //Time.timeScale = 0;
            }
            else
            {
                //Time.timeScale = 1;
            }
        }
    }
    public void nextCase()
    {
        if (npc_manager.currTotal <= npc_manager.maxTotal)
        {
            foreach (GameObject gui in guidesClone)
            {
                gui.transform.SetSiblingIndex(Random.Range(0,guidesClone.Count-1));
                gui.GetComponent<Image>().color = Color.white;
/*                ColorBlock color = gui.GetComponent<Button>().colors;
                color.normalColor = Color.white;
                gui.GetComponent<Button>().colors = color;*/
            }
            sudahAktif = false;
            timer[npc_manager.currTotal] = 0;
            winning = true;
            //waktuRestotal.Add(waktuResTemp);
            //npc_manager.currTotal++;
            Invoke("spawn", 3f);
        }
    }
    public void spawn()
    {

        AudioSource aud = GameObject.Find("suaraSFX(loop)").GetComponent<AudioSource>();
        aud.Stop();
        /*        ColorBlock location = GameObject.Find("LOCATION").GetComponent<Button>().colors;
                location.normalColor = new Color(1, 0.97f, 0.68f, 1);
                GameObject.Find("LOCATION").GetComponent<Button>().colors = location;*/
        locationMarked = false;
        GameObject.Find("LOCATION").GetComponent<Image>().color = new Color(1, 0.97f, 0.68f, 1);
        //help.resetVar();
        winning = false;
        npc_manager.spawn();
    }
    public void addTime()
    {
        waktuRestotal.Add(waktuResTemp);
        waktuResTemp = 0f;
    }
    AudioClip clipNPC = null;
    void playNPC()
    {
        AudioSource aud = GameObject.Find("suaraNPC").GetComponent<AudioSource>();
        aud.Stop();
        aud.clip = clipNPC;
        if(clipNPC) aud.Play();
    }
    public void win()
    {
        winning = true;
        AudioSource aud = GameObject.Find("suaraSFX").GetComponent<AudioSource>();
        aud.Stop();
        //caseReport.SetActive(true);
        caseReport.GetComponent<CanvasGroup>().interactable = true;
        caseReport.GetComponent<CanvasGroup>().alpha = 1;
        caseReport.GetComponent<CanvasGroup>().blocksRaycasts = true;
        caseReport.GetComponent<Animator>().Play("in");
        if (accuracy > 60)
        {
            aud.clip = winSFX;
            caseReport.transform.Find("Kiky_senang").gameObject.SetActive(true);
            caseReport.transform.Find("Kiky_marah").gameObject.SetActive(false);
            nxtLvl.SetActive(true);
            //GameObject.Find("Kiky_senang").SetActive(true);
            //GameObject.Find("Kiky_marah").SetActive(false);
            //clipNPC = winVoice;
        }
        else
        {
            //if(PlayerPrefs.GetInt("level") < 3) nxtLvl.SetActive(false);
            aud.clip = loseSFX;
            caseReport.transform.Find("Kiky_senang").gameObject.SetActive(false);
            caseReport.transform.Find("Kiky_marah").gameObject.SetActive(true);
            nxtLvl.SetActive(false);
            //GameObject.Find("Kiky_senang").SetActive(false);
            //GameObject.Find("Kiky_marah").SetActive(true);
            if (PlayerPrefs.GetString("language") == "english")
            {
                clipNPC = loseVoice[0];
            }
            else
            {
                clipNPC = loseVoice[1];
            }
        }
        aud.Play();
        Invoke("playNPC", 1.5f);

        help.resetVar();
        //waktuRestotal.Add(waktuResTemp);
        float waktuTotal = 0;
        for (int i = 0; i < waktuRestotal.Count; i++)
        {
            waktuTotal += waktuRestotal[i];
        }
        waktuTotal /= waktuRestotal.Count;

        int menit = Mathf.FloorToInt(waktuTotal / 60);
        int detik = Mathf.FloorToInt(waktuTotal % 60);
        waktuRes.text = string.Format("{0:00}:{1:00}", menit, detik);
        //isPaused = true;
        //Time.timeScale = 0;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void backHome()
    {
        SceneManager.LoadScene("main_menu");
    }
    public void nextLvl()
    {
        level = PlayerPrefs.GetInt("level");
        //demo
        if (level >= 1)
        {
            PlayerPrefs.SetInt("level", 0);
            SceneManager.LoadScene("ending");
        }
        //
        else if(level < 3)
        {
            level++;
            PlayerPrefs.SetInt("level",level);
            SceneManager.LoadScene("gameplay");        
        }
        else
        {
            PlayerPrefs.SetInt("level", 0);
            SceneManager.LoadScene("ending");
        }
    }

}
