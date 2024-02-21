using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene(1);
    }
    public void Scene2()
    {
        SceneManager.LoadScene(2);
    }
    public void Scene3()
    {
        SceneManager.LoadScene(3);
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void LateUpdate()
    {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
        else Screen.orientation = ScreenOrientation.LandscapeRight;
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
