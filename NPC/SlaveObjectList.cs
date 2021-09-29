using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaveObjectList : MonoBehaviour
{
    public GameObject[] RandomImage;
    public Text RandomName;
    public Text RandomLv;
    private string[] GladiatorNameList = { "Mark", "Trers", "Obius", "Rendolf", "Duex", "Durant", "James", "Rblon-A", "Rbion-B", "Mk333" };
    private int Lv;
    private int ImageCntMax = 2;
    public IntValue GetSelectNum;
    public IntValue GetSelectName;
    public IntValue GetSelectLv;
    public IntValue GetSelectAni;
    public BoolValue BuyPassFail;
    private int SlaveSortNum;
    private int SlaveSortName;
    private int SlaveSortLv;
    private int SlaveSortAni;
    private TradeSlaveList DestroySlaveList;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SlaveObjectList Call()");
    }

    public void ClickSlaveList()
    {
        GetSelectNum.RuntimeValue = SlaveSortNum;
        GetSelectName.RuntimeValue = SlaveSortName;
        GetSelectLv.RuntimeValue = SlaveSortLv;
        GetSelectAni.RuntimeValue = SlaveSortAni;
    }

    public void Init(int ListSortNum, int ListSortGlaName, int ListSortLv, int ListSortAni)
    {
        for (int i = 0; i < ImageCntMax; i++)
        {
            if (i != ListSortAni)
            {
                RandomImage[i].SetActive(false);
            }
            else
            {
                RandomImage[i].SetActive(true);
            }
        }

        RandomName.text = GladiatorNameList[ListSortGlaName];
        RandomLv.text = "Lv: " + ListSortLv.ToString();

        SlaveSortNum = ListSortNum;
        SlaveSortName = ListSortGlaName;
        SlaveSortLv = ListSortLv;
        SlaveSortAni = ListSortAni;
    }

    private void OnDisable()
    {
        GetSelectNum.RuntimeValue = 0;
        GetSelectName.RuntimeValue = 0;
        GetSelectLv.RuntimeValue = 0;
        GetSelectAni.RuntimeValue = 0;
    }
    
    public void ClieckBuyBtn()
    {
        if (BuyPassFail.RuntimeValue)
        {
            DestroySlaveList.SlaveBuyBtn_ObjectList(GetSelectNum.RuntimeValue);
        }
    }
}
