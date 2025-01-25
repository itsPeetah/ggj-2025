using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform m_BubbleSpawn;
    public GameObjectPool m_BubblePool;
    public PlayerMovement m_Movement;

    public float m_ShootCd = 1.0f;
    private float m_ShootCdRemain = 0.0f;

    public int m_MaxGrows = 15;
    public float m_GrowCd = 0.05f;
    private float m_GrowCdRemain = 0.0f;

    public float m_BubbleSpeed = 0.08f;

    private Bubble m_Bubble;
    private int m_Grown = 0;

    // Made this compatible with the "new" input system
    // m_keyIsDown is set through HandleShootInput() which is invoked by the PlayerInput component
    private bool m_keyIsDown = false;
    private bool m_KeyWasDown = false;

    void Update()
    {
        m_GrowCdRemain -= Time.deltaTime;
        m_ShootCdRemain -= Time.deltaTime;
        if (m_ShootCdRemain >= 0) { return; }

        if (!m_KeyWasDown && m_keyIsDown)
        {
            if (m_Bubble == null)
            {
                SpawnBubble();
            }
        }
        else if (m_KeyWasDown && m_keyIsDown)
        {
            if (m_GrowCdRemain <= 0)
            {
                GrowBubble();
            }
        }
        else if (m_KeyWasDown && !m_keyIsDown)
        {
            ShootBubble();
        }

        m_KeyWasDown = m_keyIsDown;
    }

    public void SpawnBubble()
    {
        m_Bubble = m_BubblePool.GetNext() as Bubble; // instead of instantiating a new one it takes the first available in a pool
        m_Bubble.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void GrowBubble()
    {
        if (m_Bubble == null) { return; }
        if (m_Grown >= m_MaxGrows) { return; }

        m_Grown += 1;
        m_GrowCdRemain = m_GrowCd;

        var scale = 0.1f + m_Grown * 0.1f;
        m_Bubble.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void ShootBubble()
    {
        m_ShootCdRemain = m_ShootCd;
        m_GrowCdRemain = m_GrowCd;
        if (m_Bubble == null) { return; }

        float adjustSpeedBySize = 1.0f - ((float)m_Grown / (m_MaxGrows + 1));

        bool left = m_Movement.FacingLeft;
        Vector3 direction = new Vector3(left ? -1 : 1, 0, 0);
        direction *= m_BubbleSpeed;
        direction *= adjustSpeedBySize;
        m_Bubble.Init(m_BubbleSpawn.position, direction); // setting position in init
        m_Bubble.Enable(); // this turns the object on

        m_Grown = 0;
        m_Bubble = null;
    }

    public void HandleShootInput(InputAction.CallbackContext ctx)
    {
        m_keyIsDown = ctx.performed;
    }
}
