using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcActionCount : MonoBehaviour
{
    public GameObject NPC_Trainer;
    public GameObject NPC_Trader;

    private int Trainer_Loop_1_Time = 5;
    private int Trainer_Loop_1_Time_const = 5;
    private bool can_Trainer_Loop_1 = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateNpcAction", 0f, 1f);
    }

    // Update is called once per frame
    void UpdateNpcAction()
    {
        Debug.Log("Call UpdateNpcAction()");
        if(!can_Trainer_Loop_1)
        {
            Trainer_Loop_1_Time -= 1;
        }
        if(Trainer_Loop_1_Time <= 0)
        {
            Debug.Log("Trainer_Loop_1_Time");
            Trainer_Loop_1_Time = Trainer_Loop_1_Time_const;
            NPC_Trainer.GetComponent<SlaveTrainer>().SetLoop_1_Action();
        }
    }
}
