using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowToPlayer : MonoBehaviour {

    public Material m_materialToGlow;
    public Color m_emissionColor;
    public float m_lowEnd = 0.01f;
    public float m_upperEnd = 2.5f;
    public float m_disToGlow = 4.0f;
    public float m_moveSpd = 1.5f;
    Transform m_player;
    float m_currentVal;

    private void Awake()
    {
        m_player = GameObject.Find("Player").transform;;
        m_currentVal = m_lowEnd;
    }

    private void Update()
    {
        float target = m_lowEnd;
        if ((transform.position - m_player.position).sqrMagnitude < m_disToGlow * m_disToGlow)
            target = m_upperEnd;

        m_currentVal = Mathf.MoveTowards(m_currentVal, target, m_moveSpd * Time.deltaTime);
        
        Color finalColor = m_emissionColor * Mathf.LinearToGammaSpace(m_currentVal);
        m_materialToGlow.SetColor("_EmissionColor", finalColor);
    }
}
