using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float _rotationSpeed = 3;

    [SerializeField]
    GameObject _explosionPrefab;

    SpawnManager _sm;

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        _sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_sm == null)
        {
            Debug.LogError("Spawn Manager is NULL.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            _sm.StartSpawning();
            Destroy(gameObject, .25f);
        }
    }
}
