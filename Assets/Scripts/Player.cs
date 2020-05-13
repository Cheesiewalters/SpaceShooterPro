using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.2f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _ShieldVisualiser;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _RightEngine;

    private bool _IsTripleShotActive;
    private bool _isSpeedPowerUpActive;
    private bool _isShieldPowerUpActive;
    private SpawnManager _spawnManager;
    private Enemy _enemy;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    public AudioSource _LaserSound;
    [SerializeField]
    public AudioClip _LaserAudioClip;
    [SerializeField]
    public AudioClip _PowerupClip;


    void Start()
    {

        _LaserSound = GetComponent<AudioSource>();
        _LaserSound.clip = _LaserAudioClip;
        transform.position = new Vector3(0,-3,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _ShieldVisualiser.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        ControlUserInput();
        ControlBoundsOfPlayer();
        Firelaser();
    }

    private void Firelaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            if (_IsTripleShotActive == false)
            {
                _nextFire = Time.time + _fireRate;
                Instantiate(_laserPrefab,
                    transform.position + new Vector3(0, 0.5f, 0),
                    Quaternion.identity);
            }
            else
            {
                _nextFire = Time.time + _fireRate;
                Instantiate(_TripleShotPrefab,
                    transform.position + new Vector3(0, 0.5f, 0),
                    Quaternion.identity);
            }
            _LaserSound.Play();
        }
    }

    void ControlUserInput()
    {
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        MovePlayer(horizontalinput, verticalInput);
    }

    void MovePlayer(float horizontalinput, float verticalInput)
    {
        if (_isSpeedPowerUpActive == true)
        {
            transform.Translate(new Vector3(horizontalinput, verticalInput, 0) * _speed * 2 * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(horizontalinput, verticalInput, 0) * _speed * Time.deltaTime);
        }
    }

    void ControlBoundsOfPlayer()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4, 3.4f), 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y, 0);
    }

    public void Damage()
    {
        if (_isShieldPowerUpActive == false)
        {
            _lives--;
            if(_lives == 2)
            {
                _leftEngine.SetActive(true);
            }
            else if(_lives == 1)
            {
                _RightEngine.SetActive(true);
            }
            else
            {
                if (_spawnManager != null)
                {
                    _spawnManager.OnPlayerDeath();
                }
                Destroy(this.gameObject);
            }
        }
        else
        {
            return;
        }
    }

    public void TripleShopActive()
    {
        _LaserSound.clip = _PowerupClip;
        _LaserSound.Play();
        _IsTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedPowerUpActive()
    {
        _LaserSound.clip = _PowerupClip;
        _LaserSound.Play();
        _isSpeedPowerUpActive = true;
        StartCoroutine(SpeedPowerDownRoutine());

    }

    public void ShieldPowerUpActive()
    {
        _LaserSound.clip = _PowerupClip;
        _LaserSound.Play();
        _isShieldPowerUpActive = true;
        _ShieldVisualiser.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _ShieldVisualiser.SetActive(true);
        _isShieldPowerUpActive = false;  
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerUpActive = false;
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _IsTripleShotActive = false;
    }

    public void AddScore()
    {
        _score += 10;
    }

    public int GetScore()
    {
        return _score;
    }

    public int GetLives()
    {
        return _lives;
    }
}
