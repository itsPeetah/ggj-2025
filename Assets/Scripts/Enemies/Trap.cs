using System.Collections;
using UnityEngine;

public class trapTrigger : MonoBehaviour

{
    public float triggerDuration = 10f;
    private bool isTriggered = false;

    public LayerTrigger playerDetector;
    public LayerTrigger bubbleDetector;

    private void OnEnable()
    {
        playerDetector.onTriggerEnter += HandlePlayerEnter;
        bubbleDetector.onTriggerEnter += HandleBubbleEnter;
    }

    private void OnDisable()
    {
        playerDetector.onTriggerEnter -= HandlePlayerEnter;
        bubbleDetector.onTriggerEnter -= HandleBubbleEnter;
    }

    private IEnumerator TrapRoutine()
    {
        isTriggered = true;
        Debug.Log("Trap triggered");
        yield return new WaitForSeconds(triggerDuration);
        isTriggered = false;
        Debug.Log("Trap reset");
    }

    private void HandlePlayerEnter(Collider2D player)
    {
        if (isTriggered) return;

        if (player.TryGetComponent(out Damageable dm))
        {
            dm.TakeDamage();
        }

        if (player.TryGetComponent(out KnockbackReceiver kb))
        {
            kb.GiveKnockback(transform.position);
        }
    }

    private void HandleBubbleEnter(Collider2D bubble)
    {
        if (isTriggered) return;

        StartCoroutine(TrapRoutine());
        bubble.GetComponent<Bubble>()?.Disable();
    }
}