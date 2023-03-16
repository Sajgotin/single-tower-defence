using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField] private float _nextWaveTime;
    [SerializeField] private float _timer;
    [SerializeField] private int _enemyNextWave;
    [SerializeField] private int _waveCount;
    public int WaveCount { get { return _waveCount; } }
    [SerializeField] private GameObject[] _enemyArray;

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
            _waveCount++;
            _timer = 0;
            UIManager.Instance.UpdateWaveText();

            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {    
        for (int i = 0; i < _enemyNextWave * _waveCount;)
        {
            Instantiate(_enemyArray[0], RandomSpawnPos(), Quaternion.identity, this.gameObject.transform);
            yield return new WaitForSeconds(.2f);
            i++;
        }
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
}
