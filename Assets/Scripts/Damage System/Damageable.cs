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
    }

    public void TakeDamage()
    {
        currentHp -= 1;
        onHealthChange.RaiseEvent(currentHp);

        if (currentHp <= 0)
        {
            onHealthDepleted.RaiseEvent();
        }
    }
}
