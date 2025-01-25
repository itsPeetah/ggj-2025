using SPNK.Game.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad = "";

    public VoidEventChannelSO playButtonPressed;
    public VoidEventChannelSO quitButtonPressed;

    private void OnEnable()
    {
        playButtonPressed.OnEventRaised += HandlePlayButtonPressed;
        quitButtonPressed.OnEventRaised += HandleQuitButtonPressed;
    }



    private void OnDisable()
    {
        playButtonPressed.OnEventRaised -= HandlePlayButtonPressed;
        quitButtonPressed.OnEventRaised -= HandleQuitButtonPressed;
    }

    private void HandlePlayButtonPressed()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(sceneToLoad); // TODO: Make a nice fade transition :)
    }

    private void HandleQuitButtonPressed()
    {
        Application.Quit();
    }
}
