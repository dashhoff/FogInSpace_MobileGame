using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBombSpawnCircle : MonoBehaviour
{
    [SerializeField] private SpaceBomb _prefab;
    [SerializeField] private Transform _target;

    [SerializeField] private int _poolSize = 500;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private float _spawnDistance = 5f;
    [SerializeField] private float _spawnWidth = 2f;

    private Queue<SpaceBomb> _pool;
    private List<SpaceBomb> _activeBombs = new List<SpaceBomb>();

    private void Start()
    {
        // Инициализируем пул объектов
        _pool = new Queue<SpaceBomb>();

        for (int i = 0; i < _poolSize; i++)
        {
            SpaceBomb obj = Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        StartCoroutine(SpawnRoutine());
        StartCoroutine(ActiveSpaceBombCotorutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(1f / _spawnRate);
        }
    }

    private IEnumerator ActiveSpaceBombCotorutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);

            for (int i = _activeBombs.Count - 1; i >= 0; i--)
            {
                SpaceBomb spaceBomb = _activeBombs[i];

                if (Vector2.Distance(spaceBomb.transform.position, _target.position)  > _spawnDistance * 1.5)
                {
                    _pool.Enqueue(spaceBomb);
                    _activeBombs.RemoveAt(i);
                    spaceBomb.gameObject.SetActive(false);
                }
            }
        }
    }

    private void SpawnPrefab()
    {
        if (_target == null || _pool.Count == 0) return;

        // Вычисляем случайный угол и смещение
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float offset = Random.Range(-_spawnWidth / 2, _spawnWidth / 2);

        // Вычисляем позицию спавна
        Vector2 spawnPosition = (Vector2)_target.position + new Vector2(
            Mathf.Cos(angle) * (_spawnDistance + offset),
            Mathf.Sin(angle) * (_spawnDistance + offset)
        );

        // Берём объект из пула и активируем его
        SpaceBomb obj = _pool.Dequeue();
        obj.transform.position = spawnPosition;
        obj.gameObject.SetActive(true);

        // Возвращаем в конец очереди
        _activeBombs.Add(obj);
        //_pool.Enqueue(obj);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, _spawnDistance);
        Gizmos.DrawWireSphere(transform.position, _spawnDistance - _spawnWidth);
        Gizmos.DrawWireSphere(transform.position, _spawnDistance + _spawnWidth);
    }
}
