using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    [Range(100, 1000)] [SerializeField]  int _asteroidNum = 300;
    [Range(100f, 2000f)][SerializeField] float _radius = 300f;
    [Range(1f, 10f)][SerializeField] float _maxScale = 10f;
    [SerializeField]  List<GameObject> _asteroidPrefabs;
    Transform _transform;

    public float Radius => _radius;

     void Awake()
    {
        _transform = transform;
    }

     void OnEnable()
    {
        SpawnAsteroid();
    }
    void SpawnAsteroid()
    {
        for(int i= 0; i < _asteroidNum; ++i)
        {
            GameObject asteroid = Instantiate(_asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Count)], _transform.position, Random.rotation);
            float scale = Random.Range(0.2f, _maxScale);
            asteroid.transform.localScale = new Vector3(scale, scale, scale);
            asteroid.transform.position += Random.insideUnitSphere * _radius;
            //asteroid.transform.rotation = Random.rotation;
            asteroid.GetComponent<Rigidbody>()?.AddTorque(Random.insideUnitCircle * Random.Range(0f, 50f),ForceMode.Impulse);
        }
    }
}
