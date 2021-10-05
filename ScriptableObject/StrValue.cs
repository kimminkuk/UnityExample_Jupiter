using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class StrValue : ScriptableObject
{
    public string initialValue;
    public string RuntimeValue;
    // Start is called before the first frame update
    public void Start()
    {
        RuntimeValue = initialValue;
    }
}
