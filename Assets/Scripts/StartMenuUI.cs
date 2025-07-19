using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
