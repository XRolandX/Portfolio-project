using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2UIHandler : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(2);
    }
    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
