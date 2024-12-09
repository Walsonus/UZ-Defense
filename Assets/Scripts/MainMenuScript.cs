using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        // Uruchom korutynę, aby odczekać przed zmianą sceny
        StartCoroutine(ChangeSceneWithDelay(scene));
    }

    private IEnumerator ChangeSceneWithDelay(string scene)
    {
        // Odczekaj 2 sekundy w czasie rzeczywistym
        yield return new WaitForSecondsRealtime(0.5f);

        // Zmień scenę
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Wyłącza edytor
        #else
        Application.Quit(); // Wyłącza grę na urządzeniu
        #endif
    }
}
