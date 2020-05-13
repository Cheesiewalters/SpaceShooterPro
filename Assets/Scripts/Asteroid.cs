using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50.0f;
    [SerializeField]
    private GameObject Explosion;
    private SpawnManager _spawnmanager;
    [SerializeField]
    public AudioSource _ExplosionSource;
    [SerializeField]
    public AudioClip _ExplosionAudioClip;
    // Start is called before the first frame update
    void Start()
    {
        _ExplosionSource = GetComponent<AudioSource>();
        _ExplosionSource.clip = _ExplosionAudioClip;
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(Explosion,
            this.gameObject.transform.localPosition,
            Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            _ExplosionSource.Play();
            _spawnmanager.StartSpawning();
        }
    }
}
