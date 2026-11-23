using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour
{
    [SerializeField] DamageHandler _damageHandler;
    [SerializeField]  Color _flashColor = Color.white;
    [Range(0.25f, 1f)][SerializeField]  float _fadeOutTime = 0.3f;
    [SerializeField] float _minIntensity = -10f, _maxItensity = 5f;
     Renderer _renderer;
     Color _baseColor;
     static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.color;
    }
    public void Init(int shieldStrength)
    {
        _damageHandler.Init(shieldStrength);
    }
    private void OnEnable()
    {
        _damageHandler.HealthChanged.AddListener(OnHealthChange);
        _damageHandler.ObjectDestroyEvent.AddListener(DestroyShield);
    }
    private void OnDisable()
    {
        _damageHandler.HealthChanged.RemoveListener(OnHealthChange);
        _damageHandler.ObjectDestroyEvent.RemoveListener(DestroyShield);

    }

    void OnHealthChange()
    {
        StartCoroutine(FlashAndFadeShield());
    }
    private void DestroyShield()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator FlashAndFadeShield()
    {
        Color shieldColor;
        Color emissionColor = _renderer.material.GetColor(EmissionColor);
        float currentTime = 0f;
        float intensity = _maxItensity;
        while(currentTime < _fadeOutTime)
        {
            shieldColor = Color.Lerp(_flashColor, _baseColor, currentTime / _fadeOutTime);
            intensity = Mathf.Lerp(_maxItensity, _minIntensity, currentTime/ _fadeOutTime);
            ChangeShieldColor(shieldColor, emissionColor * Mathf.Pow(2,intensity));
            currentTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    private void ChangeShieldColor(Color shieldColor, Color emissionColor)
    {
        _renderer.material.color = shieldColor;
        _renderer.material.SetColor(EmissionColor, emissionColor);
    }
}
