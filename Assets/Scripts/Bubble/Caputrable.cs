using System.Collections.Generic;
using UnityEngine;

public class Caputrable : MonoBehaviour
{
    public List<MonoBehaviour> m_DisableOnCapture = new List<MonoBehaviour>();
    private bool m_IsCaptured = false;

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
            if (CanCapture())
            {
                m_IsCaptured = true;
                collision.gameObject.GetComponent<Bubble>().Capture(gameObject);
                foreach (var b in m_DisableOnCapture)
                {
                    if (b != null)
                    {
                        b.enabled = false;
                    }
                }
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
}
