using System.Collections;
using UnityEngine;

public class trapTrigger : MonoBehaviour

 {
public float triggerDuration = 10f;
private bool isTriggered = false;
 private void OnTriggerEnter2D(Collider2D collision)
 {
    if (!isTriggered)
    {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    {
        StartCoroutine(TrapRoutine());
    }
    if (collision.gameObject.layer == LayerMask.NameToLayer("Bubble"))
    {
        StartCoroutine(TrapRoutine());
    }
    }
}
private IEnumerator TrapRoutine()
{
    isTriggered = true;
    Debug.Log("Trap triggered");
    yield return new WaitForSeconds(triggerDuration);
    isTriggered = false;
    Debug.Log("Trap reset");
}
}