using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ClickCharacter : TrainingMove
{
    [Header("Open Character Image")]
    private bool isOpend;
    public GameObject CharacterPanel;
    public GameObject GladiatorImage;

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
    //(ex) +1,+2,+3 100%, +4 51%, +5 34%, +6 26%, +7 16%...)
    private float[] UpgradeList = { 100f, 100f, 100f, 100f, 44f, 36f, 24f, 20f, 14f, 0f };
    private static int InitGladiatorStat_Num = 4;
    private float[] InitGladiatorStat = new float[InitGladiatorStat_Num];

    [Header("Scene Animation")]
    public Animator transition;
    public float transitionTime = 0.5f;
    private bool TouchOnOff = false;
    // [Header("Health Bar")]
    // public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        InitailizeSetting();
        isOpend = false;
        Debug.Log("ClickCharacter Start()\n");
    }
    public void InitailizeSetting()
    {
        moveSpeed = InitmoveSpeed.RuntimeValue;
        health = maxHealth.RuntimeValue;
        baseAttack = DamageIntValue.RuntimeValue;
        AttackSpeed = WeaponSpeed.RuntimeValue;
        ProjectileSpeed_base = ProjectileSpeed.RuntimeValue;
        Level = Level_IntValue.RuntimeValue;
        InitGladiatorStat[0] = Level;
        InitGladiatorStat[1] = health;
        InitGladiatorStat[2] = moveSpeed;
        InitGladiatorStat[3] = baseAttack;
    }

    // Update is called once per frame
    //public override void Update()
    public void FixedUpdate()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
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
                Debug.Log("pos1 " + pos1 + "pos2 " + pos2 + "pos3 " + pos3 + "pos4 " + pos4);
                
                OpenCharacterPanel();
            }
            else
            {
                //CloseCharacterPanel();
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
        GladiatorStat_Name.text = gladiatorName.ToString();
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

    private void UpgradePassOrFail(int level)
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
                CloseCharacterPanel();
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);

                // //1) Load Canvas Image Black Out Effect -> Important: Loop Scene Ok
                // //while (TouchOnOff) // um... fine code ?
                // {
                //     StartCoroutine(WaitTimeForUpgradeSceneLoad(1f));
                // }

                //2) Pass or Fail Scene Load
                ProbabilitySceneLoad(UpgradeResult, Level);
                break;
            case 4:
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
                ProbabilityGladiator(UpgradeResult, Level);
                break;
            case 5:
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
                ProbabilityGladiator(UpgradeResult, Level);
                break;
            case 6:
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
                ProbabilityGladiator(UpgradeResult, Level);
                break;
            case 7:
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
                ProbabilityGladiator(UpgradeResult, Level);
                break;
            case 8:
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
                ProbabilityGladiator(UpgradeResult, Level);
                break;
            case 9:
                UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
                ProbabilityGladiator(UpgradeResult, Level);
                break;
            default:
                //강화 버튼 비 활성화
                break;
        }
    }

    private void ProbabilityGladiator(float this_Pb, int this_Level)
    {
        if (this_Pb <= UpgradeList[this_Level])
        {
            Debug.Log("강화성공 :" + this_Pb);
            UpgradeStat();
            OpenTextGladiatorStatForUpgrade(Level);
        }
        else
        {
            Debug.Log("강화실패 :" + UpgradeResult);
            if(this_Level > 3)
            {
                DowngradeStat();
            }
            //if (Level <= 1)
            //{
            //    UpgradeInitialize();
            //}
            OpenTextGladiatorStatForUpgrade(Level);
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
        //OpenTextGladiatorStatForUpgrade();
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

    IEnumerator WaitTimeForUpgradeSceneLoad(float this_time)
    {
        //play animation
        //Black Fade-in
        Debug.Log("Wait!\n");
        transition.SetTrigger("Start_Wait");
        //wait
        yield return new WaitForSeconds(this_time);
        // //wait
        // Debug.Log("(1) " + Time.time);
        // yield return new WaitForSecnods(this_time);
        // Debug.Log("(2) " + Time.time);

    }
    private void ProbabilitySceneLoad(float this_Pb, int this_Level)
    {
        if (this_Pb <= UpgradeList[this_Level])
        {
            UpgradeStat();
            UpgradeLevel_IntValue.RuntimeValue = Level;
            Debug.Log("강화성공 :" + this_Pb);
            PassUpgradeSceneload();
            //OpenTextGladiatorStatForUpgrade(Level);
        }
        else
        {
            if (this_Level > 3)
            {
                DowngradeStat();
            }
            Debug.Log("강화실패 :" + UpgradeResult);
            UpgradeLevel_IntValue.RuntimeValue = Level;
            FailUpgradeSceneLoad();
            //OpenTextGladiatorStatForUpgrade(Level);
        }
    }

    private void PassUpgradeSceneload()
    {
        //load Scene
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
    }

    private void FailUpgradeSceneLoad()
    {
        //load Scene
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 3));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        //Black Fade-in
        //transition.SetTrigger("Start_Wait");
        //wait
        yield return new WaitForSeconds(1f);
        //load scene
        SceneManager.LoadScene(levelIndex);
    }
}
