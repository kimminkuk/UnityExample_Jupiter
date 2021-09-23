using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSlaveList : MonoBehaviour
{
    public GameObject SlaveList;
    private GameObject[] Copy_SlaveList = new GameObject[9];
    private int GetLength;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("TradeSlaveList Call()");
        InvokeRepeating("UpdateList", 0f, 0.5f);
        int randomList = Random.Range(1, 4);

        for(int i = 0; i < randomList; i++)
        {
            Copy_SlaveList[i] = Instantiate(SlaveList, transform);
            
        }
        GetLength = randomList;
    }

    void UpdateList()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetSlaveList()
    {
        for (int i = 0; i < GetLength; i++)
        {
            Destroy(Copy_SlaveList[i]);
        }
        int randomList = Random.Range(1, 4);
        for (int i = 0; i < randomList; i++)
        {
            Copy_SlaveList[i] = Instantiate(SlaveList, transform);
        }
        GetLength = randomList;
    }
}
