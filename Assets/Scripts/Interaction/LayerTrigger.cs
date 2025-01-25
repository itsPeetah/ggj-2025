using System;
using UnityEngine;

public class LayerTrigger : MonoBehaviour
{

    public LayerMask layersToDetect;

    public event Action<Collider2D> onTriggerEnter;
    public event Action<Collider2D> onTriggerExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMatchesMask(other.gameObject)) onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMatchesMask(other.gameObject)) onTriggerExit?.Invoke(other);
    }

    private bool LayerMatchesMask(GameObject other)
    {
        int othersMask = 1 << other.layer;
        return (othersMask & (int)layersToDetect) == othersMask;
    }
}
