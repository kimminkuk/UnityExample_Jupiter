using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

 public enum E_SceneState
 {
     idle,
     pass,
     fail,
     WinLose
 }

public class ClickCharacter : TrainingMove
{
    [Header("Open Character Image")]
    private bool isOpend;
    public GameObject CharacterPanel;
    public GameObject GladiatorImage;
    public IntValue Check_ASite_Scene_Gladiators;

    [Header("Gladiator Stat TextList")]
    public Text GladiatorStat_Name;
    public Text GladiatorStat_Lv;
    public Text GladiatorStat_Hp;
    public Text GladiatorStat_Speed;
    public Text GladiatorStat_Damage;
    public Text GladiatorStat_PassChance;
    public Text GladiatorStat_WeaponSpeed;
    public Text GladiatorStat_ProjectileSpeed;
    public Transform GladiatorPos;

    private float CheckDistance_ = 0.5f;
    private float UpgradeMin = 0f;
    private float UpgradeMax = 100f;
    private float UpgradeResult;
    private int MaxGladiatorLevel = 9;
    //(ex) +1,+2,+3 100%, +4 51%, +5 42%, +6 30%, +7 22%...)
    private float[] UpgradeList = { 100f, 100f, 100f, 51f, 42f, 30f, 22f, 20f, 15f, 0f };
    private string[] GladiatorNameList = { "Mark", "Trers", "Obius", "Rendolf", "Duex", "Durant", "James", "Rblon", "Rbion", "Mk333"};
    private static int InitGladiatorStat_Num = 4;
    private float[] InitGladiatorStat = new float[InitGladiatorStat_Num];

    [Header("Scene Animation")]
    public Animator transition;
    public float transitionTime = 0.5f;
    private bool TouchOnOff = false;

    //private SceneState sceneState;
    public E_SceneState_New sceneState;
    public Text PassFailText;
    private bool CCON;

    private void Awake()
    {
        // Debug.Log("ClickCharacter Awake() Call\n");
        // var objs = FindObjectsOfType<ClickCharacter>();
        // 
        // if (objs.Length <= 2)
        // {
        //     if (this.Alive_BoolValue.RuntimeValue)
        //     {
        //         Debug.Log("this.Alive_BoolValue.RuntimeValue Call\n");
        //         DontDestroyOnLoad(this.gameObject);
        //         if(Check_ASite_Scene_Gladiators.RuntimeValue != 0) // another Place
        //         {
        //             Vector3 temp;
        //             temp.x = 30;
        //             temp.y = 0;
        //             temp.z = 0;
        //             this.gameObject.transform.position = temp;
        //         }
        //         else //Training Room
        //         {
        //             Vector3[] temp = new Vector3[2];
        //             for (int i = 0; i < 2; i++)
        //             {
        //                 temp[i].x = 10 - i * -2;
        //                 if (i % 2 == 0)
        //                 {
        //                     temp[i].y = 0;
        //                 }
        //                 else
        //                 {
        //                     temp[i].y = -4;
        //                 }
        //                 temp[i].z = 0;
        //                 this.gameObject.transform.position = temp[i];
        //             }
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("Destroy Call\n");
        //         Destroy(this.gameObject);
        //     }
        // }
        // else
        // {
        //     Destroy(this.gameObject);
        //     
        // }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        CCON = true;
        InitailizeSetting();
        //InitailizeSetting_New();
        isOpend = false;
        sceneState = E_SceneState_New.idle;
        Touch_BoolValue.RuntimeValue = true;
    }
    public void InitailizeSetting()
    {
        WriteInittime();
        moveSpeed = InitmoveSpeed.RuntimeValue;
        health = maxHealth.RuntimeValue;
        baseAttack = DamageIntValue.RuntimeValue;
        AttackSpeed = WeaponSpeed.RuntimeValue;
        ProjectileSpeed_base = ProjectileSpeed.RuntimeValue;
        Level = Level_IntValue.RuntimeValue;
        //Alive_BoolValue.RuntimeValue = true;
        InitGladiatorStat[0] = Level;
        InitGladiatorStat[1] = health;
        InitGladiatorStat[2] = moveSpeed;
        InitGladiatorStat[3] = baseAttack;

        if (GladiatorStat_Name.text == string.Empty)
        {
            int RandomList = Random.Range(0, 9);
            GladiatorStat_Name.text = gladiatorName.ToString() + GladiatorNameList[RandomList];
            gladiatorName = GladiatorStat_Name.text;
        }
    }
    private void InitailizeSetting_New()
    {
        moveSpeed = InitmoveSpeed.initialValue;
        health = maxHealth.initialValue;
        baseAttack = DamageIntValue.initialValue;
        AttackSpeed = WeaponSpeed.initialValue;
        ProjectileSpeed_base = ProjectileSpeed.initialValue;
        Level = Level_IntValue.initialValue;
        InitGladiatorStat[0] = Level;
        InitGladiatorStat[1] = health;
        InitGladiatorStat[2] = moveSpeed;
        InitGladiatorStat[3] = baseAttack;
    }

    // Update is called once per frame
    public new void Update()
    //protected void FixedUpdate()
    {
        base.Update();
        
        if (Input.GetMouseButtonDown(0) && Check_ASite_Scene_Gladiators.RuntimeValue == 0)
        {
            TouchOnOff = false;
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;

            var pos1 = transform.position.x - CheckDistance_;
            var pos2 = transform.position.x + CheckDistance_;
            var pos3 = transform.position.y - CheckDistance_;
            var pos4 = transform.position.y + CheckDistance_;

            if (pos.x >= pos1 && pos.x <= pos2 && pos.y <= pos4 && pos.y >= pos3)
            {
                if (sceneState == E_SceneState_New.idle)
                {
                    OpenCharacterPanel();
                }
            }
            if(sceneState == E_SceneState_New.pass )
            {
                StartCoroutine(LoadScenePass());     
            } 
            else if (sceneState == E_SceneState_New.fail)
            {
                StartCoroutine(LoadSceneFail());
            }
        }
    }

    public void OpenCharacterPanel()
    {
        isOpend = !isOpend;
        
        if(isOpend)
        {
            CharacterPanel.SetActive(true);
            GladiatorImage.SetActive(true);
            OpenTextGladiatorStat(Level);
            Time.timeScale = 0.5f;
        }
        else
        {
            CharacterPanel.SetActive(false);
            GladiatorImage.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void CloseCharacterPanel()
    {
        CharacterPanel.SetActive(false);
        GladiatorImage.SetActive(false);
        Time.timeScale = 1f;
        isOpend = false;
    }

    private void OpenTextGladiatorStat(int this_Level)
    {
        if (GladiatorStat_Name.text == string.Empty)
        {
            int RandomList = Random.Range(0, 9);
            GladiatorStat_Name.text = gladiatorName.ToString() + GladiatorNameList[RandomList];
        }
        GladiatorStat_Lv.text = Level.ToString();
        GladiatorStat_Hp.text = health.ToString();
        GladiatorStat_Speed.text = moveSpeed.ToString();
        GladiatorStat_Damage.text = baseAttack.ToString();
        if (this_Level + 1 >= MaxGladiatorLevel)
        {
            GladiatorStat_PassChance.text = "최대강화";
        }
        else
        {
            GladiatorStat_PassChance.text = (UpgradeList[this_Level]).ToString() + "%";
        }
        GladiatorStat_WeaponSpeed.text = AttackSpeed.ToString();
        GladiatorStat_ProjectileSpeed.text = ProjectileSpeed_base.ToString();
    }

    private void OpenTextGladiatorStatForUpgrade(int this_Level)
    {
        GladiatorStat_Lv.text = Level.ToString();
        GladiatorStat_Hp.text = health.ToString();
        GladiatorStat_Speed.text = moveSpeed.ToString();
        GladiatorStat_Damage.text = baseAttack.ToString();
        if(this_Level + 1 >= MaxGladiatorLevel)
        {
            GladiatorStat_PassChance.text = "최대강화";
        }
        else
        {
            GladiatorStat_PassChance.text = (UpgradeList[this_Level]).ToString() + "%";
        }
        GladiatorStat_WeaponSpeed.text = AttackSpeed.ToString();
        GladiatorStat_ProjectileSpeed.text = ProjectileSpeed_base.ToString();
    }

    public void UpgradeGladiator()
    {
        UpgradePassOrFail(Level);
    }

    protected void UpgradePassOrFail(int level)
    {
        TouchOnOff = true;
        switch (level)
        {
            case 0:
                UpgradeStat();
                OpenTextGladiatorStatForUpgrade(Level);
                break;
            case 1:
                UpgradeStat();
                OpenTextGladiatorStatForUpgrade(Level);
                break;
            case 2:
                UpgradeStat();
                OpenTextGladiatorStatForUpgrade(Level);
                break;
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                CloseCharacterPanel();
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
                //2) Pass or Fail Scene Load
                ProbabilitySceneLoad(UpgradeResult, Level);
                break;
            default:
                //강화 버튼 비 활성화
                break;
        }
    }

    private void UpgradeStat()
    {
        WriteLevelUp();
        WriteRuntime();
    }
    private void DowngradeStat()
    {
        WriteLevelDown();
        WriteRuntime();
    }

    public void UpgradeTest()
    {
        Debug.Log("렙10업");
        for(int i = 0; i < 10; i++)
        {
            WriteLevelUp();
        }

        WriteRuntime();
    }

    public void UpgradeInitialize()
    {
        Debug.Log("렙1");

        WriteInittime();
        WriteRuntime();
        OpenTextGladiatorStatForUpgrade(Level);
    }

    private void WriteLevelUp()
    {
        Level_IntValue.RuntimeValue += 1;
        maxHealth.RuntimeValue += 2;
        InitmoveSpeed.RuntimeValue += 0.1f;
        DamageIntValue.RuntimeValue += 1;
        WeaponSpeed.RuntimeValue += 0.1f;
        ProjectileSpeed.RuntimeValue += 0.08f;
    }

    private void WriteLevelDown()
    {
        Level_IntValue.RuntimeValue -= 1;
        maxHealth.RuntimeValue -= 2;
        InitmoveSpeed.RuntimeValue -= 0.1f;
        DamageIntValue.RuntimeValue -= 1;
        WeaponSpeed.RuntimeValue -= 0.1f;
        ProjectileSpeed.RuntimeValue -= 0.08f;
    }

    private void WriteInittime()
    {
        maxHealth.RuntimeValue = maxHealth.initialValue;
        InitmoveSpeed.RuntimeValue = InitmoveSpeed.initialValue;
        DamageIntValue.RuntimeValue = DamageIntValue.initialValue;
        ProjectileSpeed.RuntimeValue = ProjectileSpeed.initialValue;
        WeaponSpeed.RuntimeValue = WeaponSpeed.initialValue;
        Level_IntValue.RuntimeValue = Level_IntValue.initialValue;
    }
    private void WriteRuntime()
    {
        health = maxHealth.RuntimeValue;
        moveSpeed = InitmoveSpeed.RuntimeValue;
        baseAttack = DamageIntValue.RuntimeValue;
        ProjectileSpeed_base = ProjectileSpeed.RuntimeValue;
        AttackSpeed = WeaponSpeed.RuntimeValue;
        Level = Level_IntValue.RuntimeValue;
    }

    protected void ProbabilitySceneLoad(float this_Pb, int this_Level)
    {
        Debug.Log("(2) why\n");
        if (this_Pb <= UpgradeList[this_Level])
        {
            Debug.Log("(3) why\n");
            UpgradeStat();
            UpgradeLevel_IntValue.RuntimeValue = Level;
            Debug.Log("강화성공 :" + this_Pb);

            PassUpgradeSceneload();
            OpenTextGladiatorStatForUpgrade(Level);
        }
        else
        {
            Debug.Log("(4) why\n");
            if (this_Level > 3)
            {
                DowngradeStat();
            }
            Debug.Log("강화실패 :" + UpgradeResult);
            UpgradeLevel_IntValue.RuntimeValue = Level;
            FailUpgradeSceneLoad();
            OpenTextGladiatorStatForUpgrade(Level);
        }
    }
    protected void PassUpgradeSceneload()
    {
        Debug.Log("(5) why\n");
        //load Scene
        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
        StartCoroutine(LoadScenePassFailLoop());

        //sceneState Changed
        sceneState = E_SceneState_New.pass;
        Debug.Log("(5-1)" + sceneState);
        Touch_BoolValue.RuntimeValue = false;
    }

    protected void FailUpgradeSceneLoad()
    {
        Debug.Log("(6) why\n");
        //Fail Scene Load
        StartCoroutine(LoadScenePassFailLoop());

        //sceneState Changed
        sceneState = E_SceneState_New.fail;
        Debug.Log("(6-1)" + sceneState);
        Touch_BoolValue.RuntimeValue = false;
    }

    IEnumerator LoadScenePassFailLoop()
    {
        //play animation
        //if transition Start_PassLoop is Loop condition
        transition.SetTrigger("Start_PassFailLoop");

        //wait
        yield return new WaitForSeconds(1f);
    }

    IEnumerator LoadScenePass()
    {
        //Scene Condtion and Text Update
        sceneState = E_SceneState_New.idle;
        Touch_BoolValue.RuntimeValue = true;

        PassFailText.text = "+" + Level.ToString() + " 강화 성공";

        //play animation
        transition.SetTrigger("Start_Pass");
        //wait
        yield return new WaitForSeconds(3f);

        //Reload -> Idle Set but this is so bug..
        transition.SetTrigger("Idle");
    }

    IEnumerator LoadSceneFail()
    {
        //Scene Condtion and Text Update
        sceneState = E_SceneState_New.idle;
        Touch_BoolValue.RuntimeValue = true;
        PassFailText.text = "강화 실패";

        //play animation
        transition.SetTrigger("Start_Fail");
        //wait
        yield return new WaitForSeconds(3f);

        //Reload -> Idle Set but this is so bug..
        transition.SetTrigger("Idle");
    }

    public void OnDisable()
    {
        //Alive_BoolValue.RuntimeValue = false;
        Check_ASite_Scene_Gladiators.RuntimeValue = 0;
        Debug.Log("OnDisable()\n");
    }

    public void OnMyDestroy()
    {
        {
            Debug.Log("OnMyDestroy() Call");
            Destroy(this.gameObject);
        }
    }
}
