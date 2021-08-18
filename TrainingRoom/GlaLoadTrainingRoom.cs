using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlaLoadTrainingRoom : MonoBehaviour
{
    [Header("Check On/Off Gladiator")]
    public GameObject[] On_ASite_Gladiators;
    public BoolValue[] Check_ASite_On_Gladiators;

    //1) 10, 0
    //2) 8, -4
    //3) 6,  0 ..
    private const int A_Site_Len = 9;
    private Vector3[] A_Site = new Vector3[A_Site_Len];
    // Start is called before the first frame update
    void Start()
    {
        InitVector3();
        Debug.Log("GladiatorLoad in TrainingRoom Scene\n");
        if (On_ASite_Gladiators.Length != Check_ASite_On_Gladiators.Length)
        {
            Debug.Log("Out!! Error");
            return;
        }

        for (int i = 0; i < On_ASite_Gladiators.Length; i++)
        {
            if (Check_ASite_On_Gladiators[i].RuntimeValue)
            {
                //A Site
                Create_A_Site_Gladiator(On_ASite_Gladiators[i], i);
            }
        }
    }

    private void InitVector3()
    {
        for(int i = 0; i < A_Site_Len; i++)
        {
            A_Site[i].x = 10 - i * -2;
            if( i % 2 == 0)
            {
                A_Site[i].y = 0;
            }
            else
            {
                A_Site[i].y = -4;
            }
            A_Site[i].z = 0;
        }
    }

    private void Create_A_Site_Gladiator(GameObject this_Object, int num)
    {
        On_ASite_Gladiators[num] = (GameObject)Instantiate(this_Object, A_Site[num], Quaternion.identity);
    }
}
