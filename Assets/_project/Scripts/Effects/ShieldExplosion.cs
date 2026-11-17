using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldExplosion : MonoBehaviour
{
    [SerializeField] Light _pointLight;
    [SerializeField] ParticleSystem _particleSystem;

    private void Update()
    {
        if (_pointLight == null) return;
        if (_pointLight.range > 0)
        {
            _pointLight.range -= 5* Time.deltaTime;
        }

        if(_pointLight.intensity > 0)
        {
            _pointLight.intensity -= 2 * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
