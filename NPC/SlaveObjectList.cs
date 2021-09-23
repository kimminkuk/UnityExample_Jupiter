using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaveObjectList : MonoBehaviour
{
    public GameObject[] RandomImage;
    public Text RandomName;
    public Text RandomLv;
    private string[] GladiatorNameList = { "Mark", "Trers", "Obius", "Rendolf", "Duex", "Durant", "James", "Rblon", "Rbion", "Mk333" };
    private int Lv;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SlaveObjectList Call()");
        int RanNum = Random.Range(0, 4);
        int RanName = Random.Range(0, 10);
        if(RanNum < 3)
        {
            Lv = 0;
        }
        else
        {
            Lv = 1;
        }
        RandomImage[RanNum].SetActive(true);
        RandomName.text = GladiatorNameList[RanName];
        RandomLv.text = "Lv: " + Lv.ToString();
    }
}
