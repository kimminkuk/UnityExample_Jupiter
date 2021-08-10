using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGladiator : MonoBehaviour
{
    [Header("Check On/Off Gladiator")]
    public GameObject[] On_ASite_Gladiators;
    public BoolValue[] Check_ASite_On_Gladiators;
    public GameObject[] On_BSite_Gladiators;
    public BoolValue[] Check_BSite_On_Gladiators;
    public Vector3 A_Site;
    public Vector3 B_Site;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CheckGladiator in War Scene\n");
        if(On_ASite_Gladiators.Length != Check_ASite_On_Gladiators.Length ||
            On_BSite_Gladiators.Length != Check_BSite_On_Gladiators.Length)
        {
            Debug.Log("Out!! Error");
            return;
        }
        
        for (int i = 0; i < On_ASite_Gladiators.Length; i++)
        { 
            if(Check_ASite_On_Gladiators[i])
            {
                //A Site
                Create_A_Site_Gladiator(On_ASite_Gladiators[i], i);

                //Move Position
                A_Site.y += 2;
            }
        }

        for (int i = 0; i < On_BSite_Gladiators.Length; i++)
        {
            if (Check_BSite_On_Gladiators[i])
            {
                //A Site
                Create_B_Site_Gladiator(On_BSite_Gladiators[i], i );

                //Move Position
                B_Site.y += 2;
            }
        }
    }

    private void Create_B_Site_Gladiator(GameObject this_Object, int num)
    {
        B_Site.z = 0;
        On_BSite_Gladiators[num] = (GameObject)Instantiate(this_Object, B_Site, Quaternion.identity);
    }

    private void Create_A_Site_Gladiator(GameObject this_Object, int num)
    {
        A_Site.z = 0;
        On_ASite_Gladiators[num] = (GameObject)Instantiate(this_Object, A_Site, Quaternion.identity);
    }
}
