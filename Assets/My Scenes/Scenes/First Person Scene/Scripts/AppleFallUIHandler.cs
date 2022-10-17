using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AppleFallUIHandler : MonoBehaviour
{

    private void Start()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuScene()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        SceneManager.LoadScene(0);
    }
    
}


