using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("SceneName")]
    public string sceneName;
    // Start is called before the first frame update
    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
