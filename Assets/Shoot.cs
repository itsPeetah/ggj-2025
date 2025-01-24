using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject m_BubblePrefab;
    public float m_ShootCd = 1.0f;
    private float m_ShootCdRemain = 0.0f;

    public int m_MaxGrows = 15;
    public float m_GrowCd = 0.05f;
    private float m_GrowCdRemain = 0.0f;

    public float m_BubbleSpeed = 0.08f;

    private GameObject m_Bubble;
    private int m_Grown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_GrowCdRemain -= Time.deltaTime;
        m_ShootCdRemain -= Time.deltaTime;
        if (m_ShootCdRemain >= 0) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_Bubble == null)
            {
                SpawnBubble();
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (m_GrowCdRemain <= 0)
            {
                GrowBubble();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ShootBubble();
        }
    }

    public void SpawnBubble()
    {
        m_Bubble = Instantiate(m_BubblePrefab, transform);
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

        bool left = false;
        Vector3 direction = new Vector3(left ? -1 : 1, 0, 0);
        direction *= m_BubbleSpeed;
        direction *= adjustSpeedBySize;
        m_Bubble.GetComponent<Bubble>().Init(direction);

        m_Grown = 0;
        m_Bubble = null;
    }
}
