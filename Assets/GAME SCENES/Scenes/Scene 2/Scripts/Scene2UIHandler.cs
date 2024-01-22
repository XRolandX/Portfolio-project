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
    private void KeyControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        KeyControl();
    }

    private void Start()
    {
#if UNITY_STANDALONE_WIN
        Cursor.lockState = CursorLockMode.None;
#endif
    }
}
