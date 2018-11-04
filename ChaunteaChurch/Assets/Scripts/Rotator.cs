using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public Vector3 m_axisVelocity;

    private void Update()
    {
        transform.Rotate(m_axisVelocity * Time.deltaTime);
    }
}
