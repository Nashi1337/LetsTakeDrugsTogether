using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "bla jel";

    public void OnNewGame()
    {
        if (string.IsNullOrEmpty(gameSceneName))
        {
            Debug.LogError("MainMenu: gameSceneName is not set!");
            return;
        }

        SceneManager.LoadScene(gameSceneName);
    }

    public void OnQuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game requested.");
    }

    public void PlayFrog()
    {
        AudioSource shmaudio = GetComponent<AudioSource>();
        if(!shmaudio) shmaudio = gameObject.AddComponent<AudioSource>();
        shmaudio.Play();
    }
}
