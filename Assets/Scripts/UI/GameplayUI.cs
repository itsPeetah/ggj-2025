using System;
using SPNK.Game.Events;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [Header("Listen to")]
    public VoidEventChannelSO onPlayerDead;
    public VoidEventChannelSO onLevelComplete;
    public BoolEventChannelSO onPuggleAcquired;

    [Header("Components")]
    public GameObject gameLostScreen;
    public GameObject gameWonScreen;
    public TMP_Text gameLostText;
    [TextArea] public string noPuggleGameLostMessage;
    [TextArea] public string yesPuggleGameLostMessage;

    private void Start()
    {
        gameLostText.SetText(noPuggleGameLostMessage);
        gameLostScreen.SetActive(false);
        gameWonScreen.SetActive(false);
    }

    private void OnEnable()
    {
        onPlayerDead.OnEventRaised += HandlePlayerDead;
        onLevelComplete.OnEventRaised += HandleLevelComplete;
        onPuggleAcquired.OnEventRaised += HandlePuggleAcquired;
    }

    private void OnDisable()
    {
        onPlayerDead.OnEventRaised -= HandlePlayerDead;
        onLevelComplete.OnEventRaised -= HandleLevelComplete;
        onPuggleAcquired.OnEventRaised += HandlePuggleAcquired;
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

    private void HandlePuggleAcquired(bool value)
    {
        if (value)
        {
            gameLostText.SetText(yesPuggleGameLostMessage);
        }
    }

}
