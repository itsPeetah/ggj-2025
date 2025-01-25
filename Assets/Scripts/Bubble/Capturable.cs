using System.Collections.Generic;
using UnityEngine;

public class Capturable : MonoBehaviour
{
    public List<MonoBehaviour> m_DisableOnCapture = new List<MonoBehaviour>();
    private GameObject m_CapturedBy;

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
                collision.gameObject.GetComponent<Bubble>().Capture(this);
            }
            else if (collision.gameObject != m_CapturedBy)
            {
                collision.gameObject.GetComponent<Bubble>().Pop();
            }
        }
    }

    private bool CanCapture()
    {
        return m_CapturedBy == null;
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
