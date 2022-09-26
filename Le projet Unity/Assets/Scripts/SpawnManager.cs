using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private int _testTimerRemoveMonster;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject monster;
    [SerializeField] private float spawnOffsetFromCamera;
    [SerializeField] private int numberOfMonstersInWave;
    [SerializeField] private float delayBetweenWavesInMinutes;
    [SerializeField] private float delayBetweenSpawnsInSeconds;

    public List<GameObject> monsterList;

    private float _timer;
    private int _nextWave;
    private int _nextSpawn;
    private float _halfHeightCamera;
    private float _halfWidthCamera;
    private float _spawnRange;
    private Vector2 _newPos;

    private void Start()
    {
        _halfHeightCamera = mainCamera.orthographicSize;
        _halfWidthCamera = mainCamera.aspect * _halfHeightCamera;
        _spawnRange = new Vector2(_halfWidthCamera+spawnOffsetFromCamera, _halfHeightCamera+spawnOffsetFromCamera).magnitude;
        Debug.Log(_spawnRange);
    }

    private void Update()
    {
        if (_timer > _nextWave * delayBetweenWavesInMinutes * 60)
        {
            for (int i = 0; i < numberOfMonstersInWave; i++)
            {
                var rand = Random.Range(0, Mathf.PI * 2);
                _newPos.Set(Mathf.Cos(rand) * _spawnRange, Mathf.Sin(rand) * _spawnRange);
                monsterList.Add(Instantiate(monster, _newPos, quaternion.identity));
            }
            
            _nextWave++;
        }
        else
        {
            if (_timer > _nextSpawn * delayBetweenSpawnsInSeconds)
            {
                var rand = Random.Range(0, Mathf.PI * 2);
                _newPos.Set(Mathf.Cos(rand) * _spawnRange, Mathf.Sin(rand) * _spawnRange);
                monsterList.Add(Instantiate(monster, _newPos, quaternion.identity));
                _nextSpawn++;
            }
        }

        if (_timer > _testTimerRemoveMonster*1)
        {
            foreach (var monster in monsterList)
            {
                Destroy(monster);
            }
            monsterList.Clear();
            _testTimerRemoveMonster++;
        }
        
        _timer += Time.deltaTime;
    }
}
