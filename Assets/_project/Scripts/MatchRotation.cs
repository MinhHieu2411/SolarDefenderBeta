using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{

    [SerializeField] Transform _target;
    private void LateUpdate()
    {
        transform.rotation = _target.rotation;
    }
}
