using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, iDamageable
{
    [SerializeField] private Color _flashColor = Color.white;
    [Range(0.25f, 1f)][SerializeField] private float _fadeOutTime = 0.3f;
    [SerializeField] float _minIntensity = -10f, _maxItensity = 5f;

    [SerializeField] private int _shieldHealth = 2000;
    [SerializeField] GameObject _explosionPrefab;
    private Renderer _renderer;
    private Color _baseColor;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private int _health;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _health = _shieldHealth;
        _baseColor = _renderer.material.color;
    }

    public void TakeDamage(int Damage, Vector3 hitPosition)
    {
        _health -= Damage;
        if (_health <= 0)
        {
            DestroyShield();
            return;
        }
        StartCoroutine(FlashAndFadeShield());
    }

    private void DestroyShield()
    {
        StopAllCoroutines();
        if(_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }
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
