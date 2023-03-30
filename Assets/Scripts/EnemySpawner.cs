using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField] float _nextWaveTime;
    [SerializeField] float _timer;
    [SerializeField] float[] _spawnChance;
    [SerializeField] int _enemyNextWave;
    [SerializeField] int _waveCount; //save - 1, if <0 = 0
    public int WaveCount { get { return _waveCount; } }
    [SerializeField] GameObject[] _enemyArray;

    private void OnEnable()
    {
        SetDefaultSpawnChance();
        if (MainMenu.saveExist)
        {
            Load();
            RecalculateSpawnChance();
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _timer = _nextWaveTime;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemyWave();
    }

    void SpawnEnemyWave()
    {
        _timer += Time.deltaTime;

        if (_timer > _nextWaveTime)
        {   
            StartCoroutine(SpawnEnemy());
            _waveCount++;
            CutsceneManager.Instance.CutsceneCheck(_waveCount);
            _timer = 0;
            _nextWaveTime = 10 + 1 * _waveCount;
            UIManager.Instance.UpdateWaveText();     
        }
    }

    IEnumerator SpawnEnemy()
    {
        int _enemyToSpawn = _enemyNextWave + 5 * _waveCount;
        for (int i = 0; i < _enemyToSpawn;)
        {
            Instantiate(_enemyArray[EnemyToSpawnIndex()], RandomSpawnPos(), Quaternion.identity, this.gameObject.transform);
            yield return new WaitForSeconds(.2f);
            i++;
        }
        SetEnemySpawnChance(_waveCount);
    }

    Vector2 RandomSpawnPos()
    {
        if(Random.value < 0.5f)
        {
            //left screen side
            Vector2 spawnPos = new Vector2(Random.Range(-13f, -10f),Random.Range(-4f, 0f));
            return spawnPos;
        }
        else
        {
            //right screen side
            Vector2 spawnPos = new Vector2(Random.Range(13f, 10f), Random.Range(-4f, 0f));
            return spawnPos;
        }

    }

    int EnemyToSpawnIndex()
    {
        float random = Random.value;
        float totalSpawnChance = _spawnChance[0];
        int index = 0;

        for(int i = 0; i < _enemyArray.Length; i++)
        {
            if (totalSpawnChance >= random && _spawnChance[i] > 0.01f)
            {
                index = i;
                break;
            }
            else
            {
                totalSpawnChance += _spawnChance[i + 1];
            }
        }
        return index;
    }

    void SetEnemySpawnChance(int value)
    {
        bool chanceUpdated = false;
        if (value % 5 == 0)
        {
            for (int i = 0; i < _enemyArray.Length; i++)
            {
                if (!chanceUpdated)
                {
                    for (int j = i + 1; j < _enemyArray.Length; j++)
                    {
                        if (_spawnChance[i] > 0.01f)
                        {
                            if (_spawnChance[j] == 0)
                            {
                                _spawnChance[i] -= 0.1f;
                                _spawnChance[j] += 0.1f;
                                chanceUpdated = true;
                                break;
                            }
                            else
                            {
                                _spawnChance[i] -= 0.1f;
                                _spawnChance[j] += 0.1f;
                                chanceUpdated = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else break;
            }       
        }
    }

    void RecalculateSpawnChance()
    {
        if(_waveCount < 70 && _waveCount > 0)
        {
            for (int a = 1; a <= _waveCount; a++)
            {
                SetEnemySpawnChance(a);
            }
        }
        else
        {
            for(int b = 0; b < _enemyArray.Length; b++)
            {
                if (b == _enemyArray.Length - 1) _spawnChance[b] = 1.0f;
                else _spawnChance[b] = 0.0f;
            }
        } 
    }

    void SetDefaultSpawnChance()
    {
        _spawnChance = new float[_enemyArray.Length];
        for (int i = 0; i < _enemyArray.Length; i++)
        {
            if (i == 0) _spawnChance[i] = 1.0f;
            else _spawnChance[i] = 0.0f;
        }
    }

    void Save()
    {
        SaveSystem.SaveData(nameof(_waveCount), _waveCount - 1);
    }

    void Load()
    {
        _waveCount = SaveSystem.LoadData(nameof(_waveCount), _waveCount);
        if (_waveCount < 0) _waveCount = 0;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
