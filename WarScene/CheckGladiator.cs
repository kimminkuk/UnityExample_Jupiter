using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum E_SceneState_New_WarScene
{ 
    idle,
    WinLose
}

public class CheckGladiator : MonoBehaviour
{
    [Header("Check On/Off Gladiator")]
    public GameObject[] On_ASite_Gladiators;
    public Transform[] On_ASite_Gladiators_transform;
    public BoolValue[] Check_ASite_On_Gladiators;
    public GameObject[] On_BSite_Gladiators;
    public BoolValue[] Check_BSite_On_Gladiators;
    public Vector3 A_Site;
    public Vector3 B_Site;

    [Header("Win/Lose Animator")]
    public Animator WinLoseScene;
    public float transitionTime = 1f;

    public E_SceneState_New sceneState;

    [Header("Gladiator Scene State")]
    public IntValue[] Check_ASite_Scene_Gladiators;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CheckGladiator in War Scene\n");
        sceneState = E_SceneState_New.idle;
        if (On_ASite_Gladiators.Length != Check_ASite_On_Gladiators.Length ||
            On_BSite_Gladiators.Length != Check_BSite_On_Gladiators.Length)
        {
            Debug.Log("Out!! Error");
            return;
        }
        
        for (int i = 0; i < On_ASite_Gladiators.Length; i++)
        {
            if (Check_ASite_On_Gladiators[i].RuntimeValue)
            {
                // //A Site
                // Create_A_Site_Gladiator(On_ASite_Gladiators[i], i);
                A_Site.y += i * 5f;
                Debug.Log("pos:" + A_Site.y);
                //A Site Gladiator Position Re-Setting
                Pos_A_Site_Gladiator(On_ASite_Gladiators[i], i, A_Site);
            }
        }

        for (int i = 0; i < On_BSite_Gladiators.Length; i++)
        {
            if (Check_BSite_On_Gladiators[i].RuntimeValue)
            {
                //A Site
                Create_B_Site_Gladiator(On_BSite_Gladiators[i], i );

                //Move Position
                B_Site.y += 2;
            }
        }
    }

    private void Update()
    {
        if (sceneState == E_SceneState_New.idle)
        {
            WinLoseCheck();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(sceneState == E_SceneState_New.WinLose)
            {
                for (int i = 0; i < Check_ASite_Scene_Gladiators.Length; i++)
                {
                    Check_ASite_Scene_Gladiators[i].RuntimeValue = 0; //War Scene
                }
                StartCoroutine(WinLoseSceneInit());
                StartCoroutine(TrainingRoomSceneLoad(SceneManager.GetActiveScene().buildIndex -1));
            }
        }
    }

    private void WinLoseCheck()
    {
        int Lose = 0;
        int Win = 0;
        for (int i = 0; i < On_ASite_Gladiators.Length; i++)
        {
            if (!Check_ASite_On_Gladiators[i].RuntimeValue)
            {
                Lose++;
            }
        }

        for (int i = 0; i < On_BSite_Gladiators.Length; i++)
        {
            if (!Check_BSite_On_Gladiators[i].RuntimeValue)
            {
                Win++;
            }
        }

        if (Lose == On_ASite_Gladiators.Length)
        {
            StartCoroutine(LoseSceneLoad());
            sceneState = E_SceneState_New.WinLose;
        }
        else if(Win == On_BSite_Gladiators.Length)
        {
            //Win Scene Load
            StartCoroutine(WinSceneLoad());
            sceneState = E_SceneState_New.WinLose;
        }
    }

    IEnumerator WinSceneLoad()
    {
        //Lose Scene Load
        WinLoseScene.SetBool("WinScene", true);
        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator LoseSceneLoad()
    {
        //Lose Scene Load
        WinLoseScene.SetBool("LoseScene", true);
        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator WinLoseSceneInit()
    {
        sceneState = E_SceneState_New.idle;
        WinLoseScene.SetBool("WinScene", false);
        WinLoseScene.SetBool("LoseScene", false);
        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator TrainingRoomSceneLoad(int sceneIndex)
    {
        //Training Scene Change
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log("War -> Training: " + progress);
            yield return null;
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

    private void Pos_A_Site_Gladiator(GameObject this_Object, int i, Vector3 vector3)
    {
        Debug.Log("Pos_A_Site_Gladiator() Call\n");
        //this_Object.transform.position = A_Site;
        //this_Object.transform.localPosition = A_Site;
        Debug.Log("test vector3: " + vector3);
        this_Object.transform.position = vector3;
        
        //this_Object.GetComponent<NewGladiator>().GladiatorPositionRenewal(vector3);
    }

}
