using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleSwitchInteractable : Interactable {

    public bool m_mustBeDown;
    public CandlePuzzleMaster m_puzzleMaster;
    Vector3 m_startPos;
    Vector3 m_endPos;
    float m_moveSpeed = 0.02f;
    LayerMask m_interactLayer;
    Coroutine m_moveRoutine;
    bool m_down = false;

    private void Awake()
    {
        m_interactLayer = gameObject.layer;
        m_startPos = transform.localPosition;
        m_endPos = m_startPos;
        m_endPos.y = 0;
    }

    public override bool Interact(PlayerController _playerController)
    {
        if (m_moveRoutine != null)
            return false;

        if (m_moveRoutine != null)
            StopCoroutine(m_moveRoutine);
        StartCoroutine(MoveTowards(m_endPos, true));
        gameObject.layer = 2; // No raycast layer;
        return true;
    }

    public bool IsCorrect()
    {
        return m_mustBeDown == m_down;
    }

    public void OnPuzzleFinished()
    {
        gameObject.layer = 2;
    }

    public bool IsDown()
    {
        return transform.localPosition == m_endPos;    
    }

    public void ResetSwitch()
    {
        if (m_moveRoutine != null)
            StopCoroutine(m_moveRoutine);
        StartCoroutine(MoveTowards(m_startPos, false, 0.5f));
    }

    private void InteratableAgain()
    {
        gameObject.layer = m_interactLayer;
    }

    IEnumerator MoveTowards(Vector3 target, bool _goingDown, float delay = 0)
    {
        if (delay > 0)
        {
            gameObject.layer = 2;
            yield return new WaitForSeconds(delay);
        }

        while (transform.localPosition != target)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, m_moveSpeed * Time.deltaTime);
            yield return null;
        }

        if (_goingDown)
        {
            // Check with puzzle
            m_down = true;
            m_puzzleMaster.CheckPuzzleResults();
        }
        else
        {
            m_down = false;
            InteratableAgain();
        }

        if (delay > 0)
            gameObject.layer = m_interactLayer;

        m_moveRoutine = null;
    }
}
