using SPNK.Game.Events;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public Transform carryAnchor;

    [Header("Broadcast to")]
    public BoolEventChannelSO onPickUp;

    private bool isCarrying = false;

    public void PickUp(GameObject what)
    {
        if (isCarrying) return;

        what.transform.parent = carryAnchor;
        what.transform.localPosition = Vector3.zero;
        isCarrying = true;

        onPickUp.RaiseEvent(true);
    }
}
