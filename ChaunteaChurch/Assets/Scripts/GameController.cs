using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance
    { get; private set; }

    [HideInInspector]
    public bool m_playerHasCandle = false;
    public GameObject m_candleUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_candleUI.SetActive(false);
    }

    public void ToggleCandleUI(bool _toggle)
    {
        m_candleUI.SetActive(_toggle);
    }
}
