using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleHoleInteractable : Interactable
{
    public string m_reaction = "Something need go in here, dog";
    public GameObject m_candleToActivate;

    private void Awake()
    {
        if (m_candleToActivate) m_candleToActivate.SetActive(false);
    }

    public override bool Interact(PlayerController _playerController)
    {
        if (m_candleToActivate == null)
        {
            PlayerUI.instance.ReactText(m_reaction);
            return true;
        }

        if (GameController.instance.m_playerHasCandle)
        {
            if (m_candleToActivate) m_candleToActivate.SetActive(true);
            GameController.instance.ToggleCandleUI(false);
            Destroy(this);
        }
        else
        {
            PlayerUI.instance.ReactText(m_reaction);
        }

        return true;
    }
}
