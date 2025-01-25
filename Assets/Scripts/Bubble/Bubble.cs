using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : PoolableObject
{
    public int m_MinGrowthToHoldPlayer = 6;
    public float m_Lifetime = 5.0f;
    private float m_LifetimeLeft;
    private float m_PopIn;

    public Sprite m_SpriteNormal;
    public Sprite m_SpritePop;

    private Vector3 m_Direction = Vector3.zero;
    private Capturable m_Captured;
    private Rigidbody2D m_Rigidbody;
    private int m_Growth;

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
        m_PopIn = 0.0f;
        m_Growth = 0;
        SetEnableColliders(true);
        SetTriggerColliders(true);
        m_Captured = null;

        m_Rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionY;
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
        if (m_PopIn > 0)
        {
            m_PopIn -= Time.deltaTime;
            if (m_PopIn < 0)
            {
                Disable();
            }
            return;
        }

        m_LifetimeLeft -= Time.deltaTime;
        if (m_LifetimeLeft < 0)
        {
            Pop();
            return;
        }

        m_Rigidbody.linearVelocity = m_Direction;

        if (m_Captured != null)
        {
            var attachPos = transform.position;
            attachPos.y -= transform.localScale.x / 2.0f;
            m_Captured.transform.position = attachPos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bubble")) // ???
                                                       // dont question mark me! if it collides with another bubble, merge them instaed
        {
            if (transform.localScale.x < collision.transform.localScale.x)
            {
                Disable();
                return;
            }
            Absorb(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Pop if touching player only from left or right
            
            Func<Vector2, bool> player_on_side = (Vector2 dir) =>
            {
                var hits = Physics2D.RaycastAll(transform.position, dir, 1.5f);
                foreach (var hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        return true;
                    }
                }

                return false;
            };
            
            if (GetGrowth() < m_MinGrowthToHoldPlayer || player_on_side(Vector2.left) || player_on_side(Vector2.right))
            {
                Pop();
            }
        }
        else if (collision.gameObject.CompareTag("Untagged"))
        {
            Pop();
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

    public void Shoot(Vector3 direction, int growth)
    {
        SetTriggerColliders(false);
        m_Direction = direction;
        m_Growth = growth;
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

        m_Direction = new Vector3(m_Direction.x * 0.8f, Mathf.Abs(m_Direction.x) * 0.8f, 0.0f);
        m_Rigidbody.constraints &=  ~RigidbodyConstraints2D.FreezePositionY;
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
        m_Rigidbody.linearVelocity = Vector3.zero;

        if (m_Captured != null)
        {
            m_Captured.Release();
        }
        m_Captured = null;
        m_PopIn = 0.3f;
        GetComponent<SpriteRenderer>().sprite = m_SpritePop;
    }

    public int GetGrowth()
    {
        return m_Growth;
    }
}
