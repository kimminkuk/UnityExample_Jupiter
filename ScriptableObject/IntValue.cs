using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class IntValue : ScriptableObject
{
    public int initialValue;
    public int RuntimeValue;
    // Start is called before the first frame update
    public void Start()
    {
        RuntimeValue = initialValue;
    }
}
