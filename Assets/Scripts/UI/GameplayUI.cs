using SPNK.Game.Events;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [Header("Listen to")]
    public VoidEventChannelSO onPlayerDead;
    public VoidEventChannelSO onLevelComplete;

    [Header("Components")]
    public GameObject gameLostScreen;
    public GameObject gameWonScreen;

    private void OnEnable()
    {
        onPlayerDead.OnEventRaised += HandlePlayerDead;
        onLevelComplete.OnEventRaised += HandleLevelComplete;
    }

    private void OnDisable()
    {
        onPlayerDead.OnEventRaised -= HandlePlayerDead;
        onLevelComplete.OnEventRaised -= HandleLevelComplete;
    }

    private void Start()
    {
        gameLostScreen.SetActive(false);
        gameWonScreen.SetActive(false);
    }

    private void HandlePlayerDead()
    {
        Debug.Log("Player is dead");

        gameLostScreen.SetActive(true);
    }

    private void HandleLevelComplete()
    {
        Debug.Log("Level complete!");
        gameWonScreen.SetActive(true);
    }

}
