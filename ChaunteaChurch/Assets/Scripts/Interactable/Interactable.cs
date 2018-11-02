using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractibleType
{
    HOLDABLE,
    NOT_HOLDABLE
}

public class Interactable : MonoBehaviour {

    public string m_interactText = "INTERACT";
    public virtual bool Interact(PlayerController _playerController)
    {
        // For children classes which need to interact with this
        return false;
    }
}
