using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvInGameUI : MonoBehaviour
{
    public float transitionTime = 1f;
    private bool OnOff;
    private bool[] LvComplete = new bool[9];
    [Header("Lv Panel")]
    public GameObject SceneLvInGamePanel;
    public BoolValue Touch_BoolValue;
    public BoolValue Touch_BoolValue_UI;

    [Header("Lv Road Map Sprite Image Render")]
    public Image LvCurrentImageLoad;
    public Sprite[] ChangeImage;
    public Text[] EraseText;

    // Start is called before the first frame update
    void Start()
    {
        OnOff = false;
        for(int i = 0; i < LvComplete.Length; i++)
        {
            LvComplete[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;

            var pos1 = transform.position.x - 0.5f;
            var pos2 = transform.position.x + 0.5f;
            var pos3 = transform.position.y - 0.5f;
            var pos4 = transform.position.y + 0.5f;

            if (pos.x >= pos1 && pos.x <= pos2 && pos.y <= pos4 && pos.y >= pos3)
            {
                //Panel Open
                if (SceneLvInGamePanel != null && Touch_BoolValue.RuntimeValue && Touch_BoolValue_UI.RuntimeValue)
                {
                    SceneLvInGamePanelOpen();
                }
                //LoadNextLevel();
            }
        }
    }

    private void SceneLvInGamePanelOpen()
    {
        OnOff = !OnOff;
        if (OnOff)
        {
            SceneLvInGamePanel.SetActive(true);
            Touch_BoolValue_UI.RuntimeValue = false;
            Time.timeScale = 0f;
        }
        else
        {
            SceneLvInGamePanel.SetActive(false);
            Touch_BoolValue_UI.RuntimeValue = true;
            Time.timeScale = 1f;
        }
    }

    public void Lv1_State()
    {
        //1. Basic Information
        //  where, Enemy Number, Level..
        Debug.Log("Lv1_State() Call");

        //if Clear? for Test Clear
        LvComplete[0] = true;
        LvCurrentImageLoad.sprite = ChangeImage[1];
        EraseText[0].text = string.Empty;
    }

    public void Lv2_State()
    {
        if(LvComplete[0] == true)
        {
            LvCurrentImageLoad.sprite = ChangeImage[2];
            LvComplete[1] = true;
            EraseText[1].text = string.Empty;
        }
        else
        {
            return;
        }
    }

    public void Lv3_State()
    {

    }

    public void Lv4_State()
    {

    }

    public void Lv5_State()
    {

    }

    public void Lv6_State()
    {

    }

    public void Lv7_State()
    {

    }
    public void Lv8_State()
    {

    }

    public void Lv9_State()
    {

    }

    public void CloseSceneLvInGamePanel()
    {
        SceneLvInGamePanel.SetActive(false);
        OnOff = false;
        Time.timeScale = 1f;
        Touch_BoolValue_UI.RuntimeValue = true;
    }
}
