using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    private string _sceneName = "GameScene";
    public new void OnClick()
    {     
        SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
    }
}
