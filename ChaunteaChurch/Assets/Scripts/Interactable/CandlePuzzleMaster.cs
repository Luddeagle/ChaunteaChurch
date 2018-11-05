using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePuzzleMaster : MonoBehaviour {

    public CandleSwitchInteractable[] m_puzzleSlots;
    public Transform m_thingToMove;
    public Vector3 m_moveAmount;

    int m_numDownBeforeReset = 2;

    public void CheckPuzzleResults()
    {
        bool success = true;
        int numDown = 0;

        foreach (CandleSwitchInteractable candle in m_puzzleSlots)
        {
            if (!candle.gameObject.activeInHierarchy)
            {
                success = false;
                continue;
            }

            if (!candle.IsCorrect()) success = false;
            if (candle.IsDown()) numDown += 1;
        }


        if (success)
            OnPuzzleComplete();
        else if (numDown >= m_numDownBeforeReset)
            ResetAllCandles();
    }

    void ResetAllCandles()
    {
        foreach (CandleSwitchInteractable candle in m_puzzleSlots)
        {
            if (!candle.gameObject.activeInHierarchy)
                continue;
            candle.ResetSwitch();
        }
    }

    void OnPuzzleComplete()
    {
        foreach (CandleSwitchInteractable candle in m_puzzleSlots)
        {
            if (!candle.gameObject.activeInHierarchy)
                continue;
            candle.OnPuzzleFinished();
        }

        StartCoroutine(MoveThing());
    }

    IEnumerator MoveThing()
    {
        const float moveTime = 3.0f;
        float startTime = Time.time;
        float lerpVal = 0;
        Vector3 startPos = m_thingToMove.position;
        while (Time.time - startTime < moveTime)
        {
            lerpVal = Mathf.Clamp01(lerpVal + Time.deltaTime / moveTime);
            m_thingToMove.position = Vector3.Lerp(startPos, startPos + m_moveAmount, lerpVal);
            yield return null;
        }
    }
}
