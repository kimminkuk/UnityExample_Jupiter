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
    public Transform GladiatorPos;

    [Header("Upgrade button")]
    public Button UpgradeButton;

    private float CheckDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        isOpend = false;
        Debug.Log("ClickCharacter Start()\n");
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;

            var pos1 = transform.position.x - CheckDistance;
            var pos2 = transform.position.x + CheckDistance;
            var pos3 = transform.position.y - CheckDistance;
            var pos4 = transform.position.y + CheckDistance;

            if (pos.x >= pos1 && pos.x <= pos2 && pos.y <= pos4 && pos.y >= pos3)
            {
                Debug.Log("Character Panel Open!");
                OpenCharacterPanel();
            }
            else
            {
                CloseCharacterPanel();
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
        GladiatorStat_Hp.text = maxHealth.initialValue.ToString();
        GladiatorStat_Speed.text = moveSpeed.ToString();
        GladiatorStat_Damage.text = baseAttack.ToString();
    }
}
