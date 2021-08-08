using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum E_SceneState
{
    idle,
    pass,
    fail
}
*/
public class SceneState_Test : MonoBehaviour
{
    public int SceneState_current = 0;
    public void SceneIdle()
    {
        SceneState_current = 0;
        return;
    }

    public void ScenePass()
    {
        SceneState_current = 1;
        return;
    }

    public void SceneFail()
    { 
        SceneState_current = 2;
        return;
    }
}
