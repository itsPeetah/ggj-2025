using System.Collections;
using SPNK.Game.Events;
using UnityEngine;

public class FadeInWhenEventFires : MonoBehaviour
{
    private new SpriteRenderer renderer;
    public float fadeDuration = 1;
    private bool wasTriggered = false;


    [Header("Listen to")]
    public BoolEventChannelSO triggerChannel;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Color c = renderer.color;
        c.a = 0;
        renderer.color = c;
    }

    private void OnEnable()
    {
        triggerChannel.OnEventRaised += HandleEvent;
    }

    private void OnDisable()
    {
        triggerChannel.OnEventRaised -= HandleEvent;
    }

    private void HandleEvent(bool _)
    {
        if (wasTriggered) return;
        wasTriggered = true;
        StartCoroutine(DoFade());
    }

    private IEnumerator DoFade()
    {
        float elapsed = 0;
        Color c = renderer.color;
        while (elapsed < fadeDuration)
        {
            c.a = elapsed / fadeDuration;
            renderer.color = c;
            elapsed += Time.deltaTime;
            yield return null;
        }
        c.a = 1;
        renderer.color = c;
    }
}
