using SPNK.Game.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Listen to")]
    public VoidEventChannelSO reloadLevelChannel;
    public VoidEventChannelSO goToNextLevelChannel;
    public VoidEventChannelSO quitToMenuChannel;

    [Header("Data")]
    public string nextLevelSceneName;

    private void OnEnable()
    {
        reloadLevelChannel.OnEventRaised += HandleReloadLevel;
        goToNextLevelChannel.OnEventRaised += HandleGoToNextLevel;
        quitToMenuChannel.OnEventRaised += HandleQuitToMenu;
    }

    private void OnDisable()
    {
        reloadLevelChannel.OnEventRaised -= HandleReloadLevel;
        goToNextLevelChannel.OnEventRaised += HandleGoToNextLevel;
        quitToMenuChannel.OnEventRaised -= HandleQuitToMenu;
    }

    private void HandleReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleGoToNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
    }

    private void HandleQuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
