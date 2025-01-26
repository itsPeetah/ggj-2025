using SPNK.Game.Events;
using UnityEngine;

// This should be an interface lmao
public class Damageable : MonoBehaviour
{
    public int maxHp;
    public int currentHp;

    [Header("Broadcast to")]
    public IntEventChannelSO onHealthChange;
    public VoidEventChannelSO onHealthDepleted;
    public AudioClipEventChannelSO playSfxChannel;

    [Header("Data")]
    public AudioClip damageSfx;
    public AudioClip deadSfx;

    public bool invincible = false;

    private void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage();
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            invincible = !invincible;
        }
    }

    public void TakeDamage()
    {
        if (invincible)
        {
            return;
        }

        currentHp -= 1;
        onHealthChange.RaiseEvent(currentHp);

        if (currentHp <= 0)
        {
            playSfxChannel.RaiseEvent(deadSfx);
            onHealthDepleted.RaiseEvent();
        }
        else
        {
            playSfxChannel.RaiseEvent(damageSfx);
        }
    }
}
