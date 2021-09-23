using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_SkillTree
{
    Strike,
    DoubleAttack,
    Swoop
}

public class SlaveTrainer : NpcInit
{
    private Animator TrainerAni;

    [Header("NPC TextList")]
    public Text SkillTreeText;
    public Text WarningText;
    public Text Skill_price;

    [Header("Skill Sprite Image Render")]
    public Image SkillImageLoad;
    public Sprite[] SkillImage;

    public Image[] SkillImage_Color;

    [Header("Slave Active SkillList")]
    public BoolValue[] ActiveSkillList;

    private E_SkillTree e_SkillTree;
    private string[] SkillTreeTextList = { "ù ���ݽ�, ��Ÿ�մϴ�.", "���� Ȯ���� �������� 2�谡 �˴ϴ�.", "���� �޽�"};
    private int[] SkillTreePrice = { 100, 200, 200 };
    private int TechGold;
    private int TechNumber = 99;
    private bool FirstSkillOnOff = false;
    private bool[] ASiteSkillOnOff = new bool[5];
    private bool[] BSiteSkillOnOff = new bool[5];
    private const int MaxNumber = 3;

    private int Trainer_Loop_1_Time = 10;
    private int Trainer_Loop_1_Time_const = 10;
    private bool can_Trainer_Loop_1 = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SlaveTriner Start() Call");
        TrainerAni = GetComponent<Animator>();
        InvokeRepeating("UpdateTrainerAction", 0f, 0.5f);

        for (int i = 0; i < ActiveSkillList.Length; i++)
        {
            if(ActiveSkillList[i].RuntimeValue)
            {
                SkillImage_Color[i] = ChangeAlpha(SkillImage_Color[i], 1f);
            }
        }
    }

    void UpdateTrainerAction()
    {
        if (can_Trainer_Loop_1)
        {
            TrainerAni.SetBool("Loop_1", false);
            can_Trainer_Loop_1 = false;
        }
        Trainer_Loop_1_Time -= 1;
        if (Trainer_Loop_1_Time <= 0)
        {
            Trainer_Loop_1_Time = Trainer_Loop_1_Time_const;
            SetLoop_1_Action();
            can_Trainer_Loop_1 = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;

            var pos1 = transform.position.x - CheckDistance_;
            var pos2 = transform.position.x + CheckDistance_;
            var pos3 = transform.position.y - CheckDistance_;
            var pos4 = transform.position.y + CheckDistance_;

            if (pos.x >= pos1 && pos.x <= pos2 && pos.y <= pos4 && pos.y >= pos3)
            {
                Debug.Log("pos1 " + pos1 + "pos2 " + pos2 + "pos3 " + pos3 + "pos4 " + pos4);
                if (Touch_BoolValue_ST.RuntimeValue && Touch_BoolValue_UI.RuntimeValue) //How to SceneState state share??? make class?
                {
                    OpenSlaveTrainerPanel();
                }
            }
            else
            {
                TextInitFunc();
            }
        }
    }

    private void OpenSlaveTrainerPanel()
    {
        isOpend = !isOpend;

        if (isOpend)
        {
            NPCPanel.SetActive(true);
            Touch_BoolValue_UI.RuntimeValue = false;
        }
        else
        {
            NPCPanel.SetActive(false);
            Touch_BoolValue_UI.RuntimeValue = true;
            TextInitFunc();
        }
    }

    public void CloseSlaveTrainerPanel()
    {
        NPCPanel.SetActive(false);
        isOpend = false;
        Touch_BoolValue_UI.RuntimeValue = true;
        TextInitFunc();
    }

    public void SlaveTechBuyBtn()
    {
        int temp = 0;
        Debug.Log("SlaveTechBuyBtn Call() " + TechNumber);
        switch(TechNumber)
        {
            case 0:
            case 1:
            case 2:
                temp = SkillTreePrice[TechNumber];
                if (ActiveSkillList[TechNumber].RuntimeValue)
                {
                    return;
                }
                break;
            default:
                //Skill Select Error
                WarningText.text = WarningList[2];
                return;
        }

        bool ifmoney = MasterMoneyCalResult(temp);
        if(ifmoney)
        {
            ActiveSkillList[TechNumber].RuntimeValue = true;
            SkillImage_Color[TechNumber] = ChangeAlpha(SkillImage_Color[TechNumber], 1f);
            //Tech apply
            WarningText.text = WarningList[1];
        }
        else
        {
            //no money
            WarningText.text = WarningList[0];
        }
    }

    public void SkillTree_1_StrikeAttack()
    {
        TextInitFunc();
        TechNumber = 0;
        SkillImageLoad.sprite = SkillImage[TechNumber];
        Skill_price.text = SkillTreePrice[(int)E_SkillTree.Strike].ToString() + "G";
        SkillTreeText.text = SkillTreeTextList[0];
    }

    public void SkillTree_2_DoubleAttack()
    {
        TextInitFunc();
        TechNumber = 1;
        SkillImageLoad.sprite = SkillImage[TechNumber];
        Skill_price.text = SkillTreePrice[(int)E_SkillTree.DoubleAttack].ToString() + "G";
        SkillTreeText.text = SkillTreeTextList[1];
    }

    public void SkillTree_3_Swoop()
    {
        TextInitFunc();
        TechNumber = 2;
        SkillImageLoad.sprite = SkillImage[TechNumber];
        Skill_price.text = SkillTreePrice[(int)E_SkillTree.Swoop].ToString() + "G";
        SkillTreeText.text = SkillTreeTextList[2];
    }

    private void TextInitFunc()
    {
        SkillImageLoad.sprite = SkillImage[3];
        SkillTreeText.text = string.Empty;
        Skill_price.text = string.Empty;
        WarningText.text = string.Empty;
    }

    private Image ChangeAlpha(Image image, float v)
    {
        var color = image.color;
        color.a = v;
        image.color = color;
        return image;
    }

    public void SetLoop_1_Action()
    {
        Debug.Log("SetLoop_1_Action Call()");
        StartCoroutine(Action_1());
    }

    private IEnumerator Action_1()
    {
        TrainerAni.SetBool("Loop_1", true);
        yield return new WaitForSeconds(0.35f); 
    }

}
