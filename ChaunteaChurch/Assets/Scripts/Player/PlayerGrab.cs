using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerGrab : MonoBehaviour {

    // Raycast variables
    public LayerMask m_interactLayerMask;
    public float m_rayCheckLength = 4f;
    public TextMeshProUGUI m_interactiveText;

    private PlayerController m_controller;
    private PlayerCameraController m_camera;
    private Transform m_heldObject;

    private Transform m_rayHitTarget;
    private bool m_hasCandle;

    private void Awake()
    {
        m_controller = GetComponent<PlayerController>();
        m_camera = GetComponent<PlayerCameraController>();
        m_heldObject = null;

        m_hasCandle = false;
    }

    private void Update()
    {
        // Check if anything in player's view to play with
        RayCastLogic();

        // Player trying to interact
        if (Input.GetButtonDown("Fire1"))
            InteractLogic();
    }

    void InteractLogic()
    {

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
            if (hit.transform != m_rayHitTarget)
                ProcessNewInteractable(hit.transform);
        }
        else
        {
            m_rayHitTarget = null;
            m_interactiveText.text = "";
        }
    }

    void ProcessNewInteractable(Transform _t)
    {
        var interactable = _t.GetComponent<Interactable>();

        if (interactable != null)
        {
            m_rayHitTarget = _t;
            m_interactiveText.text = "INTERACT";
        }
        else
        {
            m_rayHitTarget = null;
            m_interactiveText.text = "";
        }

    }
  
}
