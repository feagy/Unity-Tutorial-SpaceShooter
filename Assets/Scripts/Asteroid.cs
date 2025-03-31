using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( new Vector3(0,0,1)*Time.deltaTime*_rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
           Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            
            
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject,0.5f);
        }
    }
}
