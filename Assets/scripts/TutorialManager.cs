using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Button[] solutions;
    [SerializeField] private GameObject NPCtutorial;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject[] child;
    //[SerializeField] private GameObject[] child1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioClip kikyIND;
    [SerializeField] private AudioClip kikyENG;
    [SerializeField] private AudioClip[] voiceIND;
    [SerializeField] private AudioClip[] voiceENG;
    [SerializeField] private string[] dialog;
    [SerializeField] private string[] dialogENG;
    [SerializeField] private TextMeshProUGUI text;
    private void Awake()
    {
        NPCtutorial.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in child)
        {
            obj.SetActive(false);
        }
        child[0].SetActive(true);

/*        foreach (GameObject obj in child1)
        {
            obj.SetActive(false);
        }
        child1[0].SetActive(true);*/
        audioSource.Stop();
        text.maxVisibleCharacters = 0;
        if (PlayerPrefs.GetString("language") == "english")
        {
            audioSource.clip = voiceENG[0];
            text.text = dialogENG[0];
        }
        else
        {
            audioSource.clip = voiceIND[0];
            text.text = dialog[0];
        }
        audioSource.Play();

        StartCoroutine(typing());
    }
    int idx = 0;
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("talking",audioSource.isPlaying);
        if(Input.anyKeyDown && idx < child.Length && tutorial.activeSelf)
        {
            count++;
            if(count >= 2 || text.maxVisibleCharacters == text.text.Length)
            {
                count = 0;
                idx++;
                if (idx == child.Length && tutorial.activeSelf)
                {
                    audioSource.Stop();
                    audioSource.gameObject.SetActive(false);
                    tutorial.SetActive(false);
                    idx = 0;
                }
                else if(tutorial.activeSelf)
                {
                    foreach (GameObject obj in child)
                    {
                        obj.SetActive(false);
                    }
                    child[idx].SetActive(true);
                }


    /*            foreach (GameObject obj in child1)
                {
                    obj.SetActive(false);
                }
                child1[idx].SetActive(true);*/
                StopAllCoroutines();
                text.maxVisibleCharacters = 0;
                audioSource.Stop();
                if (PlayerPrefs.GetString("language") == "english")
                {
                    audioSource.clip = voiceENG[idx];
                    text.text = dialogENG[idx];
                }
                else
                {
                    audioSource.clip = voiceIND[idx];
                    text.text = dialog[idx];
                }

                StartCoroutine(typing());
                audioSource.Play();
            }
            else
            {
                StopAllCoroutines();
                text.maxVisibleCharacters = text.text.Length;
            }
        }
/*        else if(idx == child.Length && tutorial.activeSelf)
        {
            audioSource.Stop();
            tutorial.SetActive(false);
        }*/
        if(!tutorial.activeSelf && NPCtutorial && !NPCtutorial.activeSelf)
        {
            NPCtutorial.SetActive(true);
        }
    }
    public void callKiky()
    {
        if (PlayerPrefs.GetString("language") == "english")
        {
            audioSource2.clip = kikyENG;
            audioSource2.Play();
        }
        else
        {
            audioSource2.clip = kikyIND;
            audioSource2.Play();
        }
        CancelInvoke("balikWarna");
        Invoke("balikWarna", 5f);
        foreach(Button sol in solutions)
        {
            sol.GetComponent<Image>().color = new Color(1, 0.97f, 0.68f, 1);
/*            ColorBlock warna = sol.colors;
            warna.normalColor = new Color(1, 0.97f, 0.68f, 1);
            sol.colors = warna;*/
        }
    }
    void balikWarna()
    {
        foreach (Button sol in solutions)
        {
            sol.GetComponent<Image>().color = Color.white;
/*            ColorBlock warna = sol.colors;
            warna.normalColor = Color.white;
            sol.colors = warna;*/
        }
    }
    IEnumerator typing()
    {
        text.maxVisibleCharacters += 1;
        yield return new WaitForSeconds(0.05f);
        if (text.maxVisibleCharacters < text.text.Length)
        {
            StartCoroutine(typing());
        }
    }
}
