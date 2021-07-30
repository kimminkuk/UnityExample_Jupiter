using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectForMobile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EffectPrefab;
    float spawnsTime;
    public float defaultTime = 0.05f;
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && spawnsTime >= defaultTime )
        {
            StartCreate();
            spawnsTime = 0;
        }
        spawnsTime += Time.deltaTime;
    }
    void StartCreate()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        Instantiate(EffectPrefab, pos, Quaternion.identity);
    }
}
