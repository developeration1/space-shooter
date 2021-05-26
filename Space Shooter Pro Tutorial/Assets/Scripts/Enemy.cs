using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed = 4;
    
    Player _player;

    Animator _an;
    AudioSource _as;

    [SerializeField]
    GameObject _laserPrefab;
    [SerializeField]
    float _fireRate = 3;
    float _canFire = -1;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _an = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();

        if(_player == null)
        {
            Debug.LogError("Player is NULL.");
        }

        if (_an == null)
        {
            Debug.LogError("Animator is NULL.");
        }

        if (_as == null)
        {
            Debug.LogError("Audio Source in Explosion is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.5f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _an.SetTrigger("EnemyDeath");
            _speed = 0;
            Destroy(gameObject.GetComponent<Collider2D>());
            Destroy(gameObject, 2.5f);
            _as.Play();
        }

        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _an.SetTrigger("EnemyDeath");
            _speed = 0;
            Destroy(gameObject.GetComponent<Collider2D>());
            Destroy(gameObject, 2.5f);
            _as.Play();
        }
    }
}
