using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour {

    public TextMeshProUGUI m_interactText;
    public TextMeshProUGUI m_reactText;

    public static PlayerUI instance
    { get; private set; }

    float m_reactTimer = 0;

    private void Awake()
    {
        instance = this;
        m_reactText.text = "";
    }

    public void ReactText(string _text)
    {
        m_reactText.text = _text;
        m_reactTimer = 3.0f;
    }

    private void Update()
    {
        if (m_reactTimer > 0)
        {
            m_reactTimer -= Time.deltaTime;

            if (m_reactTimer <= 0)
                m_reactText.text = "";
        }
    }

}
