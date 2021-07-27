using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WarScene : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;

    private bool isPaused;
    public GameObject WarScenePanel;
    public GameObject text;
    public Text placeText;
    public Text placeText2;
    private void Start()
    {
        isPaused = false;
        text.SetActive(true);
    }

    private void Update()
    {
        //for(int i = 0; i < Input.touchCount; i++)
        //{
        //    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
        //    Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
        //}

        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector2 vector2 = Input.mousePosition;

            Debug.Log("vector2 Always x: " + vector2.x+ " y: " + vector2.y);
            placeText.text = vector2.x.ToString();
            placeText2.text = vector2.y.ToString();
            if (vector2.x >= 170 && vector2.x <= 230 && vector2.y <= 750 && vector2.y >= 700)
            {
                Debug.Log("vector2 ChangeWarScenePanel\n");
                ChangeWarScenePanel();
            }
            //LoadWarScene();
        }

        
    }

    public void LoadWarScene()
    {
        ResetCameraBounds();
        WarScenePanel.SetActive(false);
        Time.timeScale = 1f;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
    }

    public void ChangeWarScenePanel()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            WarScenePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            WarScenePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    public void ReturnButton()
    {
        WarScenePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ResetCameraBounds()
    {
        return;
    }
}
