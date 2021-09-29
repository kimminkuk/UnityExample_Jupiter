using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSlaveList : MonoBehaviour
{
    public GameObject SlaveList;
    private GameObject SlaveList_;
    private GameObject[] Copy_SlaveList = new GameObject[9];

    public IntValue GetSelectNum;
    private int GetLength;
    private int SlaveSortNum;
    private int SlaveSortName;
    private int SlaveSortLv;
    private int ImageCntMax = 2;
    private int SlaveSortAni;
    // Start is called before the first frame update
    void Start()
    {
        //SlaveList_ = SlaveList_.GetComponent<SlaveObjectList>().gameObject;
        Debug.Log("TradeSlaveList Call()");
        int randomList = Random.Range( 1, 5);

        for (int i = 0; i < randomList; i++)
        {
            SlaveSortNum = i + 1;
            SlaveSortAni = Random.Range(0, ImageCntMax);
            SlaveSortName = Random.Range(0, 10);
            SlaveSortLv = Random.Range(0, 10);
            if (SlaveSortLv > 7)
            {
                SlaveSortLv = 1;
            }
            else
            {
                SlaveSortLv = 0;
            }
            Copy_SlaveList[i] = Instantiate(SlaveList, transform);
            Copy_SlaveList[i].GetComponent<SlaveObjectList>().Init(SlaveSortNum, SlaveSortName, SlaveSortLv, SlaveSortAni);
            
        }
        GetLength = randomList;
    }

    public void ResetSlaveList()
    {
        for (int i = 0; i < GetLength; i++)
        {
            Destroy(Copy_SlaveList[i]);
        }
        int randomList = Random.Range(1, 5);

        for (int i = 0; i < randomList; i++)
        {
            SlaveSortNum = i + 1;
            SlaveSortAni = Random.Range(0, ImageCntMax);
            SlaveSortName = Random.Range(0, 10);
            SlaveSortLv = Random.Range(0, 10);
            if (SlaveSortLv > 7)
            {
                SlaveSortLv = 1;
            }
            else
            {
                SlaveSortLv = 0;
            }
            Copy_SlaveList[i] = Instantiate(SlaveList, transform);
            Copy_SlaveList[i].GetComponent<SlaveObjectList>().Init(SlaveSortNum, SlaveSortName, SlaveSortLv, SlaveSortAni);

        }
        GetLength = randomList;
    }

    public void SlaveBuyBtn_ObjectList(int destroyNum)
    {
        Debug.Log("SlaveBuyBtn_ObjectList Call\n");
        Destroy(this.Copy_SlaveList[GetSelectNum.RuntimeValue]);
    }
}
