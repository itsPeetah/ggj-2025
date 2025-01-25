using System.Collections;
using SPNK.Game.Events;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Listen to")]
    public IntEventChannelSO onHpChange;

    [Header("Components")]
    public Image[] hpImages;

    [Header("Data")]
    public Sprite bubbleSprite;
    public Sprite bubblePopSprite;
    public float bubblePopDuration;


    private void OnEnable()
    {
        onHpChange.OnEventRaised += HandleHPChange;
    }

    private void OnDisable()
    {

        onHpChange.OnEventRaised -= HandleHPChange;
    }

    private void HandleHPChange(int hp)
    {
        if (hp < 0)
            return;

        for (int i = 0; i < hpImages.Length; i++)
        {
            bool previous = hpImages[i].enabled;
            bool current = hp >= i + 1;

            if (current)
            {
                hpImages[i].sprite = bubbleSprite;
                hpImages[i].enabled = true;
            }
            else if (previous)
            {
                StartCoroutine(DoBubblePop(i));
            }
        }

    }

    private IEnumerator DoBubblePop(int index)
    {
        hpImages[index].sprite = bubblePopSprite;
        yield return new WaitForSeconds(bubblePopDuration);
        hpImages[index].enabled = false;
    }
}
