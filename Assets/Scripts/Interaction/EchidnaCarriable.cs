using System;
using UnityEngine;

public class EchidnaCarriable : MonoBehaviour
{
    public LayerTrigger playerDetector;
    public MonoBehaviour[] behavioursToDeactivate;

    private void OnEnable()
    {
        playerDetector.onTriggerEnter += HandlePlayerEnter;
    }

    private void OnDisable()
    {
        playerDetector.onTriggerEnter -= HandlePlayerEnter;
    }

    private void HandlePlayerEnter(Collider2D player)
    {
        for (int i = 0; i < behavioursToDeactivate.Length; i++)
        {
            behavioursToDeactivate[i].enabled = false;
        }

        if (player.TryGetComponent<PlayerCarry>(out PlayerCarry carry))
        {
            carry.PickUp(this.gameObject);
        }
    }
}
