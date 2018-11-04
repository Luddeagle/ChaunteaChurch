using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePickUp : Interactable
{
    public override bool Interact(PlayerController _playerController)
    {
        GameController.instance.m_playerHasCandle = true;
        GameController.instance.ToggleCandleUI(true);
        Destroy(gameObject);
        return true;
    }
}
