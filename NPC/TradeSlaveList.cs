using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSlaveList : MonoBehaviour
{
    public GameObject SlaveList;
    private GameObject SlaveList_;
    private GameObject[] Copy_SlaveList = new GameObject[9];
    private int GetLength;
    private int SlaveSortNum;
    private int SlaveSortName;
    // Start is called before the first frame update
    void Start()
    {
        //SlaveList_ = SlaveList_.GetComponent<SlaveObjectList>().gameObject;
        Debug.Log("TradeSlaveList Call()");
        int randomList = Random.Range( 1, 5);

        for (int i = 0; i < randomList; i++)
        {
            SlaveSortNum = i + 1;
            SlaveSortName = Random.Range(0, 10);
            Copy_SlaveList[i] = Instantiate(SlaveList, transform);
            Copy_SlaveList[i].GetComponent<SlaveObjectList>().Init(SlaveSortNum, SlaveSortName);
            
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
            SlaveSortName = Random.Range(0, 10);
            Copy_SlaveList[i] = Instantiate(SlaveList, transform);
            Copy_SlaveList[i].GetComponent<SlaveObjectList>().Init(SlaveSortNum, SlaveSortName);

        }
        GetLength = randomList;
    }
}
