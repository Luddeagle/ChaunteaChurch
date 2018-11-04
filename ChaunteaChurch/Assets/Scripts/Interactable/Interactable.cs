using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractibleType
{
    HOLDABLE,
    NOT_HOLDABLE
}

public abstract class Interactable : MonoBehaviour {

    public string m_interactText = "INTERACT";
    public abstract bool Interact(PlayerController _playerController);
}
