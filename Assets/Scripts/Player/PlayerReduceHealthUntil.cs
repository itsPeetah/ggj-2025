using SPNK.Game.Events;
using UnityEngine;

public class PlayerReduceHealthUntil : MonoBehaviour
{
    [Header("Listen to")]
    public BoolEventChannelSO childPickedUpEvent;

    [Header("data")]
    public int desiredHealth = 1;
    private Damageable damageable;

    private void Start()
    {
        damageable = GetComponent<Damageable>();
    }

    private void OnEnable()
    {
        childPickedUpEvent.OnEventRaised += HandleChildPickedUpEvent;
    }

    private void OnDisable()
    {
        childPickedUpEvent.OnEventRaised -= HandleChildPickedUpEvent;
    }

    private void HandleChildPickedUpEvent(bool pickedUp)
    {
        if (!pickedUp) return;

        while (damageable.currentHp > desiredHealth)
        {
            damageable.TakeDamage();
        }
    }
}
