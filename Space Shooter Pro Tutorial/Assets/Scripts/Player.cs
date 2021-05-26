using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpawnManager _sm;

    [SerializeField]
    float _speed = 3.5f;
    [SerializeField]
    float _speedMultiplier = 2;

    [SerializeField]
    GameObject _laserPrefab;
    [SerializeField]
    float _fireRate = .5f;
    float _canFire = -1;

    [SerializeField]
    [Range(0, 3)]
    int _lives = 3;

    [SerializeField]
    float _shootDownPowerupTime = 5;
    [SerializeField]
    GameObject _tripleShotPrefab;
    bool _isTripleShotActive;
    bool _isShieldActive;
    [SerializeField]
    GameObject _shield;

    [SerializeField]
    int _score;
    UIManager _uim;

    [SerializeField]
    GameObject hurt_left, hurt_right;

    AudioSource _as;
    [SerializeField]
    AudioClip _laserSound;
    [SerializeField]
    AudioClip _explosionSound;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_sm == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        _uim = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uim == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        _as = GetComponent<AudioSource>();

        if (_as == null)
        {
            Debug.LogError("Audio Source in Player is NULL");
        }
        else
        {
            _as.clip = _laserSound;
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.7f, 0), 0);

        if (transform.position.x >= 11.35f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.35f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {
        _canFire = Time.time + _fireRate;
        _as.Play();
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }

        _lives--;

        _uim.UpdateLives(_lives);

        if(_lives == 2)
        {
            hurt_right.SetActive(true);
        }
        else if(_lives == 1)
        {
            hurt_left.SetActive(true);
        }
        else if(_lives <= 0)
        {
            _as.clip = _explosionSound;
            _as.Play();
            _sm.OnPlayerDead();
            Destroy(gameObject);
        }
    }

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(_shootDownPowerupTime);
        _isTripleShotActive = false;
    }

    public void ActivateSpeedBoost()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(_shootDownPowerupTime);
        _speed /= _speedMultiplier;
    }

    public void ActivateShields()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
    }

    public void AddScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        _uim.UpdateScore(_score);
    }
}
