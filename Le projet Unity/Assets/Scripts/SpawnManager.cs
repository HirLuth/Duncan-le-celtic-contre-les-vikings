using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float spawnOffsetFromCamera;
    [SerializeField] private int numberOfMonstersInWave;
    [SerializeField] private float delayBetweenWavesInMinutes;
    [SerializeField] private int numberOfMonstersInSpawn;
    [SerializeField] private float delayBetweenSpawnsInSeconds;
    [Header("MonsterStatAugment")]
    [SerializeField] private GameObject monster;
    [SerializeField] private int damageAugmentationBetweenWaves;
    [SerializeField] private int healthAugmentationBetweenWaves;
    [SerializeField] private float speedAugmentationBetweenWaves;
    [Header("SpecialMonsterStatAugment")]
    [SerializeField] private GameObject specialMonster;
    [SerializeField] private int specialMonsterDamageAugment;
    [SerializeField] private int specialMonsterHealthAugment;
    [SerializeField] private float specialMonsterSpeedAugment;

    public List<GameObject> monsterList;

    [Header("Variable de Test")] [SerializeField]
    private bool monsterClearOn;
    [SerializeField] private float delayBetweenMonsterClearInSeconds;
    [SerializeField] private int numberOfMonsterClear;
    
    private float _timer;
    private int _nextWave;
    private int _nextSpawn;
    private float _halfHeightCamera;
    private float _halfWidthCamera;
    private float _spawnRange;
    private Vector2 _newPos;

    private void Start()
    {
        _halfHeightCamera = CameraController.instance.camera.orthographicSize;
        _halfWidthCamera = CameraController.instance.camera.aspect * _halfHeightCamera;
        _spawnRange = new Vector2(_halfWidthCamera+spawnOffsetFromCamera, _halfHeightCamera+spawnOffsetFromCamera).magnitude;
    }

    private void Update()
    {
        if (_timer > (_nextWave+1) * delayBetweenWavesInMinutes * 60)
        {
            for (var i = 0; i < numberOfMonstersInWave; i++)
            {
                SummonMonsterOnRandomSpot();
            }
            SummonSpecialMonster();
            _nextWave++;
        }
        else
        {
            if (_timer > (_nextSpawn+1) * delayBetweenSpawnsInSeconds)
            {
                for (var i = 0; i < numberOfMonstersInSpawn; i++)
                {
                    SummonMonsterOnRandomSpot();
                }
                _nextSpawn++;
            }
        }

        if (_timer > delayBetweenMonsterClearInSeconds*numberOfMonsterClear)
        {
            if (!monsterClearOn) return;
            foreach (var monster in monsterList)
            {
                Destroy(monster);
            }
            monsterList.Clear();
            numberOfMonsterClear++;
        }
        
        _timer += Time.deltaTime;
    }

    private void SummonMonsterOnRandomSpot()
    {
        var rand = Random.Range(0, Mathf.PI * 2);
        var cameraPos = CameraController.instance.transform.position;
        _newPos.Set(Mathf.Cos(rand) * _spawnRange + cameraPos.x, Mathf.Sin(rand) * _spawnRange + cameraPos.y);
        var currentMonster = Instantiate(monster, _newPos, quaternion.identity);
        var monsterStat = currentMonster.GetComponent<IAMonstre1>();
        monsterStat.health += _nextWave * healthAugmentationBetweenWaves;
        monsterStat.Damages += _nextWave * damageAugmentationBetweenWaves;
        monsterStat.speed += _nextWave * speedAugmentationBetweenWaves;
        
        //tests
        monsterList.Add(currentMonster);
    }
    
    private void SummonSpecialMonster()
    {
        var rand = Random.Range(0, Mathf.PI * 2);
        var cameraPos = CameraController.instance.transform.position;
        _newPos.Set(Mathf.Cos(rand) * _spawnRange + cameraPos.x, Mathf.Sin(rand) * _spawnRange + cameraPos.y);
        var currentMonster = Instantiate(specialMonster, _newPos, quaternion.identity);
        var monsterStat = currentMonster.GetComponent<IAMonstre1>();
        monsterStat.health += _nextWave * specialMonsterHealthAugment;
        monsterStat.Damages += _nextWave * specialMonsterDamageAugment;
        monsterStat.speed += _nextWave * specialMonsterSpeedAugment;
        
        //tests
        monsterList.Add(currentMonster);
    }
}
