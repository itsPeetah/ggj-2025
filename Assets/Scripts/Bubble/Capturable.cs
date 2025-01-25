using System.Collections.Generic;
using UnityEngine;

public class Capturable : MonoBehaviour
{
    public List<MonoBehaviour> m_DisableOnCapture = new List<MonoBehaviour>();
    private bool m_IsCaptured = false;
    private bool m_HandledCollision = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_HandledCollision = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_HandledCollision && collision.gameObject.CompareTag("Bubble"))
        {
            m_HandledCollision = true;

            if (CanCapture())
            {
                collision.gameObject.GetComponent<Bubble>().Capture(this);
            }
            else
            {
                collision.gameObject.GetComponent<Bubble>().Disable();
            }
        }
    }

    private bool CanCapture()
    {
        return !m_IsCaptured;
    }

    public void Capture()
    {
        m_IsCaptured = true;
        foreach (var b in m_DisableOnCapture)
        {
            if (b != null)
            {
                b.enabled = false;
            }
        }
    }

    public void Release()
    {
        m_IsCaptured = false;
        foreach (var b in m_DisableOnCapture)
        {
            if (b != null)
            {
                b.enabled = true;
            }
        }
    }    
}
