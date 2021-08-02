using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    private bool OnOff;
    public GameObject SceneReadyPanel;

    private void Start()
    {
        OnOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //LoadNextLevel();

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;

            var pos1 = transform.position.x - 0.5f;
            var pos2 = transform.position.x + 0.5f;
            var pos3 = transform.position.y - 0.5f;
            var pos4 = transform.position.y + 0.5f;

            if (pos.x >= pos1 && pos.x <= pos2 && pos.y <= pos4 && pos.y >= pos3)
            {
                 //Panel Open
                 ReadyPanelOpen();

                //LoadNextLevel();
            }
        }
    }

    private void ReadyPanelOpen()
    {
        OnOff = !OnOff;
        if(OnOff)
        {
            SceneReadyPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            SceneReadyPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LoadNextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //load scene
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log("LoadAsynchronously: "+ progress);
            yield return null;
        }
    }


    public void LoadNextLevelSence()
    {
        Time.timeScale = 1f;
        LoadNextLevel();
        //Time.timeScale = 1f;
    }

    public void ReturnButton()
    {
        SceneReadyPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
