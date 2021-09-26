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
    private int ImageCntMax = 4;
    public IntValue GetSelectNum;
    public IntValue GetSelectName;
    private int SlaveSortNum;
    private int SlaveSortName;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SlaveObjectList Call()");
    }

    public void ClickSlaveList()
    {
        GetSelectNum.RuntimeValue = SlaveSortNum;
        GetSelectName.RuntimeValue = SlaveSortName;
    }

    public void Init(int ListSortNum, int ListSortGlaName)
    {
        int RanNum = Random.Range(0, ImageCntMax);
        int RanName = Random.Range(0, 10);
        if (RanNum < 3)
        {
            Lv = 0;
        }
        else
        {
            Lv = 1;
        }
        for(int i = 0; i < ImageCntMax; i++)
        {
            if(i != RanNum)
            {
                RandomImage[i].SetActive(false);
            }
            else
            {
                RandomImage[i].SetActive(true);
            }
        }
        RandomName.text = GladiatorNameList[ListSortGlaName];
        RandomLv.text = "Lv: " + Lv.ToString();

        SlaveSortNum = ListSortNum;
        SlaveSortName = ListSortGlaName;
    }

    private void OnDisable()
    {
        GetSelectNum.RuntimeValue = 0;
        GetSelectName.RuntimeValue = 0;
    }
}
