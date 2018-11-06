using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour {

    // Raycast variables
    public LayerMask m_interactLayerMask;
    public float m_rayCheckLength = 4f;

    private PlayerController m_controller;
    private PlayerCameraController m_camera;
    private Transform m_heldObject;
    private Interactable m_focusedInteract;

    public bool m_canMove = true;

    private void Awake()
    {
        m_controller = GetComponent<PlayerController>();
        m_camera = GetComponent<PlayerCameraController>();
        m_heldObject = null;
    }

    private void Update()
    {
        if (!m_canMove)
            return;

        // Check if anything in player's view to play with
        RayCastLogic();

        // Player trying to interact
        if (Input.GetButtonDown("Fire1"))
            InteractLogic();
    }

    void InteractLogic()
    {
        if (m_focusedInteract)
        {
            m_focusedInteract.Interact(m_controller);
            PlayerUI.instance.m_interactText.text = "";
        }
    }

    private void RayCastLogic()
    {
        // Constantly raycasting for objects
        Vector3 rayDir = m_camera.GetCameraTransform().forward;
        Vector3 rayStart = m_camera.GetCameraTransform().position;
        RaycastHit hit;

        // Only keep targets that are Interactible
        if (Physics.Raycast(rayStart, rayDir, out hit, m_rayCheckLength, m_interactLayerMask))
        {
            // Hitting same object, don't do anything
            if (hit.transform != m_focusedInteract)
                ProcessNewInteractable(hit.transform);
        }
        else
        {
            m_focusedInteract = null;
            PlayerUI.instance.m_interactText.text = "";
        }
    }

    void ProcessNewInteractable(Transform _t)
    {
        var interactable = _t.GetComponent<Interactable>();

        if (interactable != null)
        {
            m_focusedInteract = interactable;
            PlayerUI.instance.m_interactText.text = interactable.m_interactText;
        }
        else
        {
            m_focusedInteract = null;
            PlayerUI.instance.m_interactText.text = "";
        }

    }
  
}
