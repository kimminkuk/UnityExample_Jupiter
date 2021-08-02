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
    //public Transform ClickTarget;

    private void Start()
    {
        isPaused = false;
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

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;

            var pos1 = transform.position.x - 0.5f;
            var pos2 = transform.position.x + 0.5f;
            var pos3 = transform.position.y - 0.5f;
            var pos4 = transform.position.y + 0.5f;

            if (pos.x >= pos1 && pos.x <= pos2 && pos.y <= pos4 && pos.y >= pos3)
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
