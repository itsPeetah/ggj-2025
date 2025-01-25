using System;
using SPNK.Game.Events;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public LayerTrigger playerDetector;

    [Header("Listen to")]
    public BoolEventChannelSO childWasPickedUpChannel; // what the fuck kinda variable name is this LMAO

    [Header("Broadcast to")]
    public VoidEventChannelSO winLevel;

    private bool canFinishLevel;

    private void OnEnable()
    {
        playerDetector.onTriggerEnter += HandlePlayerEnter;
        childWasPickedUpChannel.OnEventRaised += HandleChildPickedUp;
    }

    private void OnDisable()
    {
        playerDetector.onTriggerEnter -= HandlePlayerEnter;
        childWasPickedUpChannel.OnEventRaised -= HandleChildPickedUp;
    }

    private void HandlePlayerEnter(Collider2D player)
    {
        if (!canFinishLevel) return;
        winLevel.RaiseEvent();
    }

    private void HandleChildPickedUp(bool val)
    {
        if (val) canFinishLevel = true;
    }

}
