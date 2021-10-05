using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarEnterList : MonoBehaviour
{
    private const int SlaveMaxLength = 9;
    private int cnt = 0;
    private GameObject[] Copy_SlaveList = new GameObject[SlaveMaxLength];
    public GameObject WarEnterSlaveObj;

    public BoolValue[] Alive_BoolValue = new BoolValue[SlaveMaxLength];
    public IntValue[] maxHealth = new IntValue[SlaveMaxLength];
    public IntValue[] Level_IntValue = new IntValue[SlaveMaxLength];

    public StrValue[] GetSlaveName = new StrValue[SlaveMaxLength];


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("WarEnterList Call()\n");
        for(int i = 0; i < SlaveMaxLength; i++)
        {
            if(Alive_BoolValue[i].RuntimeValue)
            {
                Copy_SlaveList[cnt] = Instantiate(WarEnterSlaveObj, transform);
                Copy_SlaveList[cnt].GetComponent<SlaveObjectList>().WarEnterInit
                cnt++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
