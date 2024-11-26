using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class guidebook : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    instruct_help help;
    GameManager gm;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textCommon;
    [SerializeField] private TextMeshProUGUI textDanger;
    [SerializeField] private TextMeshProUGUI textEffective;
    [SerializeField] private TextMeshProUGUI textHelp;
    //[SerializeField] private int guide;

    [SerializeField] private Sprite img;
    [SerializeField] private string titleIDN;
    [SerializeField] private string titleENG;
    [TextArea]
    [SerializeField] private string common_symptomsIDN;
    [TextArea]
    [SerializeField] private string common_symptomsENG;

    [TextArea]
    [SerializeField] private string danger_levelIDN;
    [TextArea]
    [SerializeField] private string danger_levelENG;

    [TextArea]
    [SerializeField] private string effectivenessIDN;
    [TextArea]
    [SerializeField] private string effectivenessENG;

    [TextArea]
    [SerializeField] private string help_instructionsIDN;
    [TextArea]
    [SerializeField] private string help_instructionsENG;
    NPC_manager npc_manager;
    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        help = FindObjectOfType<instruct_help>();
        gm = FindObjectOfType<GameManager>();
        image = GameObject.Find("guideImage").GetComponent<Image>();
        textTitle = GameObject.Find("textTitleDesc").GetComponent<TextMeshProUGUI>();
        textCommon = GameObject.Find("textCommonDesc").GetComponent<TextMeshProUGUI>();
        textDanger = GameObject.Find("textDangerDesc").GetComponent<TextMeshProUGUI>();
        textEffective = GameObject.Find("textEffectiveDesc").GetComponent<TextMeshProUGUI>();
        textHelp = GameObject.Find("textHelpDesc").GetComponent<TextMeshProUGUI>();
        npc_manager = FindObjectOfType<NPC_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("language") == "english")
        {
            text.text = titleENG;
        }
        else
        {
            text.text = titleIDN;
        }
        if (!gm.locationMarked || help.isclicked || gm.winning)
        {
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }
    public void clicked()
    {
        instruct_help help = FindObjectOfType<instruct_help>();
        help.guide = this.gameObject;
        image.sprite = img;
        image.color = Color.white;
        if (PlayerPrefs.GetString("language") == "english")
        {
            textTitle.text = titleENG;
            textCommon.text = common_symptomsENG;
            textDanger.text = danger_levelENG;
            textEffective.text = effectivenessENG;
            textHelp.text = help_instructionsENG;
        }
        else
        {
            textTitle.text = titleIDN;
            textCommon.text = common_symptomsIDN;
            textDanger.text = danger_levelIDN;
            textEffective.text = effectivenessIDN;
            textHelp.text = help_instructionsIDN;
        }     
    }
}
