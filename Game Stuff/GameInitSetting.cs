using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitSetting : MonoBehaviour
{
    [Header("TouchOnOff")]
    public BoolValue Touch_BoolValue_ST;
    public BoolValue Touch_BoolValue_UI;

    // Start is called before the first frame update
    void Start()
    {
        Touch_BoolValue_ST.RuntimeValue = true;
        Touch_BoolValue_UI.RuntimeValue = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
