using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : PoolableObject
{
    private Vector3 m_Direction = Vector3.zero;
    private GameObject m_CapturedBy;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Enable()
    {
        base.Enable();

        SetEnableColliders(true);
        SetTriggerColliders(true);
        m_CapturedBy = null;
    }

    public override void Disable()
    {
        base.Disable();
        m_CapturedBy = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CapturedBy == null)
        {
            transform.position += m_Direction;
        }    
        else
        {
            transform.position = m_CapturedBy.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            if (transform.localScale.x < collision.transform.localScale.x)
            {
                Disable();
                return;
            }
            Absorb(collision.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetTriggerColliders(false);
        }
    }

    public void Shoot(Vector3 direction)
    {
        SetTriggerColliders(true);
        m_Direction = direction;
    }

    private void Absorb(GameObject obj)
    {
        var growBy = obj.transform.localScale *= 0.3f;
        transform.localScale += growBy;
    }

    public void Capture(GameObject obj)
    {
        m_Direction = Vector3.zero;
        SetEnableColliders(false);
        m_CapturedBy = obj;
    }

    private void SetEnableColliders(bool enable)
    {
        foreach (var c in GetComponents<Collider2D>())
        {
            c.enabled = enable;
        }
    }
    private void SetTriggerColliders(bool isTrigger)
    {
        foreach (var c in GetComponents<Collider2D>())
        {
            c.isTrigger = isTrigger;
        }
    }
}
