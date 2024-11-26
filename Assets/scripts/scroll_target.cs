using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scroll_target : MonoBehaviour
{
    [SerializeField] private bool isTarget = false;
    NPC_manager npc_manager;
    [SerializeField] private GameObject target;
    [SerializeField] private bool collide = false;
    // Start is called before the first frame update
    void Start()
    {
        npc_manager = FindObjectOfType<NPC_manager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isTarget)
        {
            Debug.Log(collide);
            if(!npc_manager) npc_manager = FindObjectOfType<NPC_manager>();
            if (SceneManager.GetActiveScene().name == "tutorial" || npc_manager.npc_tutor)
            {
                npc_manager.npc_tutor.mapBener = collide;
            }
            else
            {
                npc_manager.npc.mapBener = collide;
            }
        }
        //transform.position = new Vector3(Camera.main.ScreenToWorldPoint(target.transform.position).x, Camera.main.ScreenToWorldPoint(target.transform.position).y, transform.position.z);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "spriteMap" && isTarget)
        {
            collide = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "spriteMap" && isTarget)
        {
            collide = false;
        }
    }
}
