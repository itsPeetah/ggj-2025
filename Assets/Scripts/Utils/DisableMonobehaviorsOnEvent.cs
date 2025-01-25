using System;
using SPNK.Game.Events;
using UnityEngine;

public class DisableMonobehaviorsOnEvent : MonoBehaviour
{
    public VoidEventChannelSO listenChannel;

    public MonoBehaviour[] behavioursToDisable;

    private void OnEnable()
    {
        listenChannel.OnEventRaised += HandleEvent;
    }

    private void OnDisable()
    {
        listenChannel.OnEventRaised -= HandleEvent;
    }

    private void HandleEvent()
    {
        for (int i = 0; i < behavioursToDisable.Length; i++)
        {
            behavioursToDisable[i].enabled = false;
        }
    }
}
