using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private float[] UpgradeList = { 49.5f, 60.3f };
    private static int InitGladiatorStat_Num = 4;
    private float[] InitGladiatorStat = new float[InitGladiatorStat_Num];

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
            OpenTextGladiatorStat();
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

    private void OpenTextGladiatorStat()
    {
        GladiatorStat_Name.text = gladiatorName.ToString();
        GladiatorStat_Lv.text = Level.ToString();
        GladiatorStat_Hp.text = health.ToString();
        GladiatorStat_Speed.text = moveSpeed.ToString();
        GladiatorStat_Damage.text = baseAttack.ToString();
        GladiatorStat_PassChance.text = (100 - UpgradeList[0]).ToString();
        GladiatorStat_WeaponSpeed.text = AttackSpeed.ToString();
        GladiatorStat_ProjectileSpeed.text = ProjectileSpeed_base.ToString();
    }

    private void OpenTextGladiatorStatForUpgrade()
    {
        GladiatorStat_Lv.text = Level.ToString();
        GladiatorStat_Hp.text = health.ToString();
        GladiatorStat_Speed.text = moveSpeed.ToString();
        GladiatorStat_Damage.text = baseAttack.ToString();
        GladiatorStat_PassChance.text = (100 - UpgradeList[0]).ToString();
        GladiatorStat_WeaponSpeed.text = AttackSpeed.ToString();
        GladiatorStat_ProjectileSpeed.text = ProjectileSpeed_base.ToString();
    }

    public void UpgradeGladiator()
    {
        UpgradeResult = Random.Range(UpgradeMin, UpgradeMax);
        if(UpgradeResult > UpgradeList[0])
        {
            Debug.Log("강화성공 :" + UpgradeResult);
            UpgradeStat();
            OpenTextGladiatorStatForUpgrade();
        }
        else
        {
            Debug.Log("강화실패 :" + UpgradeResult);
            DowngradeStat();
            if (Level <= 1)
            {
                UpgradeInitialize();
            }
            OpenTextGladiatorStatForUpgrade();
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
        OpenTextGladiatorStatForUpgrade();
    }

    public void UpgradeInitialize()
    {
        Debug.Log("렙1");

        WriteInittime();
        WriteRuntime();
        OpenTextGladiatorStatForUpgrade();
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
}
