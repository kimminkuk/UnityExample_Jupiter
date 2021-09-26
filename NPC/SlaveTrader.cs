using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaveTrader : NpcInit
{
    [Header("NPC status TextList")]
    public Text Slave_price;
    public Text MasterCurrentMoney;
    public Text WarningText;

    [Header("Slave List")]
    public GameObject[] SlaveList; //? this design ok??? hmmmmm
    public BoolValue[] SlaveActive;
    public Transform SlaveTraderPos;

    private int SlavePriceRandom = 50; // Temporary
    private int temp;

    void Start()
    {
        isOpend = false;
        temp = 0;
        Debug.Log("SlaveTrader Start()\n");
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
                    OpenSlaveTraderPanel();
                }
            }
        }
    }
    private void OpenSlaveTraderPanel()
    {
        isOpend = !isOpend;

        if (isOpend)
        {
            NPCPanel.SetActive(true);
            Touch_BoolValue_UI.RuntimeValue = false;
            OpenTextSlaveTraderStat();
        }
        else
        {
            NPCPanel.SetActive(false);
            Touch_BoolValue_UI.RuntimeValue = true;
        }
    }

    public void CloseSlaveTraderPanel()
    {
        NPCPanel.SetActive(false);
        Touch_BoolValue_UI.RuntimeValue = true;
        isOpend = false;
    }

    private void OpenTextSlaveTraderStat()
    {
        // Random Slave List and Slave Price

        // Master Current Resource List..
        MasterCurrentMoney.text = MasterMoney.RuntimeValue.ToString();
    }

    public void SlaveBuyBtn()
    {
        bool ifmoney = MasterMoneyCalResult(SlavePriceRandom);
        if(ifmoney)
        {
            WarningText.text = WarningList[1];
            if (temp < SlaveList.Length)
            {
                CreateNewSlave(temp);
                temp++;
                OpenTextSlaveTraderStat();
            }

        }
        else
        {
            WarningText.text = WarningList[0];
        }
    }

    private void CreateNewSlave(int Number)
    {
        SlaveActive[Number].RuntimeValue = true;

        Vector3 SlavePos = transform.position;
        SlavePos.x = SlavePos.x + 3;
        SlavePos.y = SlavePos.y + 3;
        SlaveList[Number] = Instantiate(SlaveList[Number], SlavePos, Quaternion.identity);
    }
}
