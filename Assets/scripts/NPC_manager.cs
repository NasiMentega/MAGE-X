using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NPC_manager : MonoBehaviour
{
    //public int maxIdx;
    //public int minIdx;
    [SerializeField] private scroll_map map;
    GameManager gm;
    public NPC npc;
    public NPC_Tutor npc_tutor;
    [SerializeField] private GameObject npcParent;
    [SerializeField] private GameObject npcPos;
    [SerializeField] private GameObject npcPrefab;
    public List<string> emergency;
    public int maxTotal;
    public int currTotal = 0;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //npc_tutor = FindObjectOfType<NPC_Tutor>();
        //demo
        if (PlayerPrefs.GetInt("level") == 1)
        {
            emergency.Add("epilepsy");
            emergency.Add("hit and run");
            emergency.Add("heart attack");
        }
        //
/*        if (PlayerPrefs.GetInt("level") == 1)
        {
            emergency.Add("breathing difficulty");
            emergency.Add("allergic reactions");
            emergency.Add("dog bites");
        }*/
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            emergency.Add("burnt hand");
            emergency.Add("stung by a bee");
            emergency.Add("basketball injury");
            emergency.Add("epilepsy");
            emergency.Add("hit and run");
        }
        else if (PlayerPrefs.GetInt("level") == 3)
        {
            emergency.Add("drunk");
            emergency.Add("domestic violence");
            emergency.Add("food poisoning");
            emergency.Add("swallowed a toy");
            emergency.Add("heart attack");
            emergency.Add("electric failure");
            emergency.Add("sudden death");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Random.Range(0,emergency.Count));
        if (currTotal == maxTotal && !gm.winning)
        {
            gm.win();
            AudioSource aud = GameObject.Find("suaraSFX(loop)").GetComponent<AudioSource>();
            aud.Stop();
        }
    }
    public void spawn()
    {
        if(currTotal < maxTotal)
        {
            map.resetTargetPos();
            instruct_help help = FindObjectOfType<instruct_help>();
            help.isclicked = false;
            if (SceneManager.GetActiveScene().name != "tutorial" || !npc_tutor)
            {
                //currTotal++;
                npc = Instantiate(npcPrefab).GetComponent<NPC>();
                //npc.transform.parent = npcParent.transform;
                npc.transform.SetParent(npcParent.transform, false);
                //npc.idx = Random.Range(minIdx,maxIdx);
                int idx = Random.Range(0, emergency.Count);
                npc.current_emergency = emergency[idx];
                emergency.RemoveAt(idx);
                //currTotal++;
            }
        }
    }
}
