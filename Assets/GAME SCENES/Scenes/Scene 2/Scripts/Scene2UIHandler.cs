using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2UIHandler : MonoBehaviour
{
    [SerializeField] GameObject androidOverlay;
    private void Awake()
    {
#if PLATFORM_STANDALONE_WIN
        androidOverlay.SetActive(false);
#endif
#if UNITY_ANDROID
        androidOverlay.SetActive(true);
#endif
    }


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
#if PLATFORM_STANDALONE_WIN
        KeyControl();
#endif
    }

    private void Start()
    {
#if UNITY_STANDALONE_WIN
        Cursor.lockState = CursorLockMode.None;
#endif
    }
}
