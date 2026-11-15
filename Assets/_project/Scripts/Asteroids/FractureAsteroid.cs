using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureAsteroid : MonoBehaviour
{
    [Range(1f,30f)] [SerializeField] float _duration = 10f;

    private void OnEnable()
    {
        Destroy(gameObject, _duration);
    }
}
