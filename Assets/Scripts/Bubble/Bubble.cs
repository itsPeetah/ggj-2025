using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : PoolableObject
{
    public float m_Lifetime = 5.0f;
    private float m_LifetimeLeft;

    public Sprite m_SpriteNormal;
    public Sprite m_SpritePop;

    private Vector3 m_Direction = Vector3.zero;
    private Capturable m_Captured;
    private Rigidbody2D m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void Enable()
    {
        base.Enable();

        GetComponent<SpriteRenderer>().sprite = m_SpriteNormal;
        m_LifetimeLeft = m_Lifetime;
        SetEnableColliders(true);
        SetTriggerColliders(true);
        m_Captured = null;
    }

    public override void Disable()
    {
        base.Disable();
        if (m_Captured != null)
        {
            m_Captured.Release();
        }
        m_Captured = null;
    }

    // Update is called once per frame
    void Update()
    {
        m_LifetimeLeft -= Time.deltaTime;
        if (m_LifetimeLeft < 0)
        {
            Disable();
            return;
        }

        m_Rigidbody.linearVelocity = m_Direction;

        if (m_Captured != null)
        {
            m_Captured.transform.position = transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble")) // ???
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
            //SetTriggerColliders(false);
        }
    }

    public void Shoot(Vector3 direction)
    {
        SetTriggerColliders(false);
        m_Direction = direction;
    }

    private void Absorb(GameObject obj)
    {
        var growBy = obj.transform.localScale *= 0.3f;
        transform.localScale += growBy;

        m_Direction.x *= Mathf.Clamp(1.0f - growBy.x / 1.5f, 0.9f, 1.0f);
        ExtendLifetime(m_Lifetime / 3.0f);
    }

    public void ExtendLifetime(float by)
    {
        m_LifetimeLeft += by;
        m_LifetimeLeft = Mathf.Min(m_LifetimeLeft, m_Lifetime);
    }

    public void ExtendLifetime()
    {
        m_LifetimeLeft = m_Lifetime;
    }

    public void Capture(Capturable obj)
    {
        if (m_Captured != null) { return; }

        m_Direction = new Vector3(m_Direction.x * 0.3f, Mathf.Abs(m_Direction.x) * 0.1f, 0.0f);
        //SetEnableColliders(false);
        ExtendLifetime();
        m_Captured = obj;
        m_Captured.Capture(gameObject);
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

    public void Pop()
    {
        SetEnableColliders(false);
        m_Direction = Vector3.zero;

        if (m_Captured != null)
        {
            m_Captured.Release();
        }
        m_Captured = null;
        m_LifetimeLeft = 0.6f;
        GetComponent<SpriteRenderer>().sprite = m_SpritePop;
    }
}
