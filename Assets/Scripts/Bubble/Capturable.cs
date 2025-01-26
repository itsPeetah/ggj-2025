using System.Collections.Generic;
using UnityEngine;

public class Capturable : MonoBehaviour
{
    public List<MonoBehaviour> m_DisableOnCapture = new List<MonoBehaviour>();
    private GameObject m_CapturedBy;
    public int m_MinBubbleSizeToCapture = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            var bubble = collision.gameObject.GetComponent<Bubble>();
            if (CanBeCapturedBy(bubble))
            {
                bubble.Capture(this);
            }
            else if (collision.gameObject != m_CapturedBy)
            {
                bubble.Pop();
            }
        }
    }

    private bool CanBeCapturedBy(Bubble bubble)
    {
        return m_CapturedBy == null && bubble.GetGrowth() >= m_MinBubbleSizeToCapture;
    }

    public void Capture(GameObject capturer)
    {
        m_CapturedBy = capturer;
        foreach (var b in m_DisableOnCapture)
        {
            if (b != null)
            {
                b.enabled = false;
            }
        }

        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D r))
        {
            r.linearVelocity = Vector2.zero;
            r.gravityScale = 0;
        }
    }

    public void Release()
    {
        m_CapturedBy = null;
        foreach (var b in m_DisableOnCapture)
        {
            if (b != null)
            {
                b.enabled = true;
            }
        }
    }
}
