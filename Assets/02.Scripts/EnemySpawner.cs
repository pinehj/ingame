using Unity.FPS.AI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyPrefab;

    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _spawnDuration;
    [SerializeField] private int _maxCount;
    [SerializeField] private PatrolPath _linkedPatrolPath;

    private EnemyManager m_EnemyManager;

    private void Awake()
    {
         m_EnemyManager = FindFirstObjectByType<EnemyManager>();
        _linkedPatrolPath = GetComponentInChildren<PatrolPath>();
    }
    private void Update()
    {
        Debug.Log(m_EnemyManager.Enemies.Count);
        if(_spawnTimer <= 0 && m_EnemyManager.Enemies.Count < _maxCount)
        {
            _spawnTimer = _spawnDuration;
            EnemyController newEnemy = Instantiate(_enemyPrefab, this.transform);
            newEnemy.PatrolPath = _linkedPatrolPath;
        }
        else
        {
            _spawnTimer -= Time.deltaTime;
        }
    }
}
