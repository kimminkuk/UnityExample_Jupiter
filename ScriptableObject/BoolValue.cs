using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject
{
    public bool initialValue;
    public bool RuntimeValue;
    // Start is called before the first frame update
    public void Start()
    {
        RuntimeValue = initialValue;
        //Debug.Log("New BoolValue\n");
    }
}
