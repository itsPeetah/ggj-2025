using SPNK.Game.Events;
using System.Collections.Generic;
using UnityEngine;

public class TintOnDamage : MonoBehaviour
{
    [Header("Listen to")]
    public IntEventChannelSO onHpChange;

    public float m_FadeOverTime = 0.8f;
    public Color m_Tint;

    private float m_FadeTimer;

    private List<SpriteRenderer> m_Renderers = new List<SpriteRenderer>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var r in m_Renderers)
        {
            r.gameObject.SetActive(false);
        }
        m_Renderers.AddRange(GetComponentsInChildren<SpriteRenderer>(true));
    }

    private void OnEnable()
    {
        onHpChange.OnEventRaised += HandleHPChange;
    }

    private void OnDisable()
    {
        onHpChange.OnEventRaised -= HandleHPChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FadeTimer > 0)
        {
            m_FadeTimer -= Time.deltaTime;
            m_FadeTimer = Mathf.Max(0, m_FadeTimer);

            float progress = 1.0f - m_FadeTimer / m_FadeOverTime;
            var tintColor = m_Tint;
            tintColor.a = 1.0f - progress;

            foreach (var r in m_Renderers)
            {
                r.color = tintColor;
            }

            if (m_FadeTimer <= 0)
            {
                foreach (var r in m_Renderers)
                {
                    r.gameObject.SetActive(false);
                }
            }
        }
    }

    private void HandleHPChange(int hp)
    {
        m_FadeTimer = m_FadeOverTime;

        foreach (var r in m_Renderers)
        {
            r.gameObject.SetActive(true);
        }
    }
}
