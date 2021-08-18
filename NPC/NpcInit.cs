using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcInit : MonoBehaviour
{
    [Header("Open Character Image")]
    protected bool isOpend;
    public GameObject NPCPanel;

    [Header("TouchOnOff")]
    public BoolValue Touch_BoolValue_ST;

    [Header("Master Resource List")]
    public IntValue MasterMoney;

    protected float CheckDistance_ = 0.5f;
    protected string[] WarningList = { "Not enough Money", "Buy Success", "Skill Select plz" };

    protected bool MasterMoneyCalResult(int needful)
    {
        if(MasterMoney.RuntimeValue >= needful)
        {
            MasterMoney.RuntimeValue -= needful;
            return true;
        }
        else
        {
            //Waring
            return false;
        }
    }
}
