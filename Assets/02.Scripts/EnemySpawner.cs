using Unity.FPS.AI;
using Unity.FPS.Game;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyPrefab;

    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _spawnDuration;
    [SerializeField] private int _maxCount;
    [SerializeField] private PatrolPath _linkedPatrolPath;

    private EnemyManager m_EnemyManager;

    private StageLevel _stageLevel;

    private void Awake()
    {
         m_EnemyManager = FindFirstObjectByType<EnemyManager>();
        _linkedPatrolPath = GetComponentInChildren<PatrolPath>();
    }

    private void Start()
    {
        Refresh();
        StageManager.Instance.OnDataChanged += Refresh;
    }
    private void Refresh()
    {
        _stageLevel = StageManager.Instance.Stage.CurrentLevel;
    }
    private void Update()
    {
        if(_stageLevel == null)
        {
            return;
        }

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0 && m_EnemyManager.Enemies.Count < _maxCount)
        {
            _spawnTimer = _spawnDuration;

            if (Random.Range(0, 100) < _stageLevel.SpawnRate)
            {

                EnemyController newEnemy = Instantiate(_enemyPrefab, this.transform);
                var health = newEnemy.GetComponent<Health>();
                health.MaxHealth *= _stageLevel.HealthFactor;
                newEnemy.PatrolPath = _linkedPatrolPath;
            }
        }

    }
}
