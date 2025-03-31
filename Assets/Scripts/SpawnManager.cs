using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    bool _playerIsAlive = true;
    [SerializeField]
    private GameObject[] powerups;
   
    
    public void StartSpawning() 
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    void Update()
    {
      
    }

    //spawn gameobjects
    //Create coroutine of type IEnumerator -- Yield Events
    //while loop


    //COROUTÝNE
   IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_playerIsAlive)
        {
            GameObject newEnemy=Instantiate(_enemyPrefab,new Vector3(Random.Range(-9.4f,9.4f),7.5f,0),Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3);
        while (_playerIsAlive)
        {
            int ranPowerup = Random.Range(0, 3);

            Instantiate(powerups[ranPowerup], new Vector3(Random.Range(-9.4f, 9.4f), 7.5f, 0), Quaternion.identity);
           
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }

    public void OnPlayerDeath() {
    _playerIsAlive= false;
    }

}
