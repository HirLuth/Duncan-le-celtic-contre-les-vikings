using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject bossFight;
    [SerializeField] private float spawnOffsetFromCamera;
    [SerializeField] private int numberOfMonstersInWave;
    [SerializeField] private int monsterWaveLimit;
    [SerializeField] private int wavesUntilSpecialMonster;
    [SerializeField] private float delayBetweenWavesInMinutes;
    [SerializeField] private float delayBetweenSpawnsInSeconds;
    [SerializeField] private float startMinuteOfBossFight;
    [SerializeField] private int scaleBreakMinute;
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
    private int _wavesAfterSpecialMonster;

    private void Start()
    {
        _halfHeightCamera = CameraController.instance.camera.orthographicSize;
        _halfWidthCamera = CameraController.instance.camera.aspect * _halfHeightCamera;
        _spawnRange = new Vector2(_halfWidthCamera+spawnOffsetFromCamera, _halfHeightCamera+spawnOffsetFromCamera).magnitude;
    }

    private void FixedUpdate()
    {
        if (TimerController.instance.timer >= startMinuteOfBossFight*60)
        {
            StartBossFight();
        }
        
        _timer += Time.deltaTime;
        if (_timer > (_nextWave+1) * delayBetweenWavesInMinutes * 60)
        {
            ChangeMonsterColor();
            var monsterToSpawn = numberOfMonstersInWave * (_nextWave + 1);
            if (monsterToSpawn > monsterWaveLimit) monsterToSpawn = monsterWaveLimit;
            for (var i = 0; i < monsterToSpawn; i++)
            {
                SummonMonsterOnRandomSpot();
            }
            
            _wavesAfterSpecialMonster++;
            if (_wavesAfterSpecialMonster == wavesUntilSpecialMonster)
            {
                SummonSpecialMonster();
                _wavesAfterSpecialMonster = 0;
            }
            _nextWave++;
        }
        else
        {
            if (_timer > (_nextSpawn+1) * delayBetweenSpawnsInSeconds)
            {
                for (var i = 0; i < _nextWave+1; i++)
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
    }

    private void StartBossFight()
    {
        bossFight.SetActive(true);
        
        for (int i = 1; i < ListeMonstres.instance.ennemyList.Count; i++)
        {
            ListeMonstres.instance.ennemyList.RemoveAt(i);
        }
        
        foreach (GameObject t in monsterList)
        {
            Destroy(t);
        }

       
        Destroy(this);
    }

    private void SummonMonsterOnRandomSpot()
    {
        var rand = Random.Range(0, Mathf.PI * 2);
        var cameraPos = CameraController.instance.transform.position;
        var y = Mathf.Clamp(Mathf.Sin(rand)*_spawnRange+cameraPos.y,-_halfHeightCamera-spawnOffsetFromCamera+cameraPos.y,_halfHeightCamera+spawnOffsetFromCamera+cameraPos.y);
        _newPos.Set(Mathf.Cos(rand) * _spawnRange + cameraPos.x,y);
        var currentMonster = Instantiate(monster, _newPos, quaternion.identity);
        var monsterStat = currentMonster.GetComponent<IAMonstre1>();
        if (_nextWave<scaleBreakMinute)
        {
            monsterStat.health += _nextWave * healthAugmentationBetweenWaves;
        }
        else
        {
            monsterStat.health += Mathf.RoundToInt(4.5f*Mathf.Pow(_nextWave,1.38f));
        }
        monsterStat.Damages += _nextWave * damageAugmentationBetweenWaves;
        monsterStat.speed += _nextWave * speedAugmentationBetweenWaves;
        
        //tests
        monsterList.Add(currentMonster);
    }
    
    private void SummonSpecialMonster()
    {
        var rand = Random.Range(0, Mathf.PI * 2);
        var cameraPos = CameraController.instance.transform.position;
        var y = Mathf.Clamp(Mathf.Sin(rand)*_spawnRange+cameraPos.y,
            -_halfHeightCamera-spawnOffsetFromCamera+cameraPos.y,_halfHeightCamera+spawnOffsetFromCamera+cameraPos.y);
        _newPos.Set(Mathf.Cos(rand) * _spawnRange + cameraPos.x,y);
        var currentMonster = Instantiate(specialMonster, _newPos, quaternion.identity);
        var monsterStat = currentMonster.GetComponent<IAMonstre1>();
        if (_nextWave<scaleBreakMinute)
        {
            monsterStat.health += _nextWave * healthAugmentationBetweenWaves;
        }
        else
        {
            monsterStat.health += Mathf.RoundToInt(4.5f*Mathf.Pow(_nextWave,1.38f));
        }
        monsterStat.Damages += _nextWave * specialMonsterDamageAugment;
        monsterStat.speed += _nextWave * specialMonsterSpeedAugment;
        
        //tests
        monsterList.Add(currentMonster);
    }

    private void ChangeMonsterColor()
    {
        var r = Random.Range(0f, 1f);
        var g = Random.Range(0f, 1f);
        var b = Random.Range(0f, 1f);
        monster.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
    }
}
