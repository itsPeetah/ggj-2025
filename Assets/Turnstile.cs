using System;
using System.Collections;
using UnityEngine;

public class Turnstile : MonoBehaviour
{
    [Header("Components")]
    public LayerTrigger playerDetector;
    public LayerTrigger bubbleDetector;
    public Transform rotationPivot;

    [Header("Data")]
    public float stayActiveTime = 5;
    [Range(0, 4)] public int activeRotation = 1;
    public bool invertRotation = false;
    public float activationTime = 1f;

    [Header("Active indicator")]
    public Color activeColor;
    public Color busyColor;
    public Color inactiveColor;
    public Color playerDetectedColor;
    public SpriteRenderer activeIndicator;


    // Properties
    public bool TurnstileActive => stayActiveTimeRemaining > 0;

    // State
    private float stayActiveTimeRemaining;
    private bool playerDetected;

    private void OnEnable()
    {
        playerDetector.onTriggerEnter += HandlePlayerEnter;
        playerDetector.onTriggerExit += HandlePlayerExit;

        bubbleDetector.onTriggerEnter += HandleBubbleEnter;
    }

    private void OnDisable()
    {
        playerDetector.onTriggerEnter -= HandlePlayerEnter;
        playerDetector.onTriggerExit -= HandlePlayerExit;

        bubbleDetector.onTriggerEnter -= HandleBubbleEnter;
    }

    public void Start()
    {
        activeIndicator.color = inactiveColor;
    }

    private void Update()
    {
        if (TurnstileActive && !playerDetected)
        {
            stayActiveTimeRemaining -= Time.deltaTime;
            if (stayActiveTimeRemaining <= 0)
            {
                StartCoroutine(DoDeactivation());
            }
        }
    }

    private void HandlePlayerEnter(Collider2D player)
    {
        playerDetected = true;
        if (TurnstileActive) activeIndicator.color = playerDetectedColor;
    }

    private void HandlePlayerExit(Collider2D player)
    {
        playerDetected = false;
        if (TurnstileActive) activeIndicator.color = activeColor;
    }

    private void HandleBubbleEnter(Collider2D bubble)
    {
        if (!TurnstileActive)
        {
            stayActiveTimeRemaining = stayActiveTime + activationTime;
            StartCoroutine(DoActivation());
        }
        else
        {
            stayActiveTimeRemaining = stayActiveTime;
        }

        if (bubble.TryGetComponent<Bubble>(out Bubble bubbleComponent))
        {
            bubbleComponent.Pop();
        }

    }

    private IEnumerator DoActivation()
    {
        Vector3 lea = rotationPivot.localEulerAngles;
        float targetAngle = 90f * activeRotation * (invertRotation ? -1 : 1);
        float startingAngle = lea.z;
        float elapsed = 0;
        activeIndicator.color = busyColor;
        while (elapsed < activationTime)
        {
            lea.z = Mathf.Lerp(startingAngle, targetAngle, elapsed / activationTime);
            rotationPivot.localEulerAngles = lea;
            elapsed += Time.deltaTime;
            yield return null;
        }
        lea.z = Mathf.Lerp(lea.z, targetAngle, 1);
        rotationPivot.localEulerAngles = lea;
        activeIndicator.color = activeColor;
    }

    private IEnumerator DoDeactivation()
    {
        Vector3 lea = rotationPivot.localEulerAngles;
        float targetAngle = 0f;
        float startingAngle = lea.z;
        float elapsed = 0;

        activeIndicator.color = busyColor;
        while (elapsed < activationTime)
        {
            lea.z = Mathf.Lerp(startingAngle, targetAngle, elapsed / activationTime);
            rotationPivot.localEulerAngles = lea;
            elapsed += Time.deltaTime;
            yield return null;
        }
        lea.z = Mathf.Lerp(lea.z, targetAngle, 1);
        rotationPivot.localEulerAngles = lea;

        activeIndicator.color = inactiveColor;
    }
}
