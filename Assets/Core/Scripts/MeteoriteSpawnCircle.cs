using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawnCircle : MonoBehaviour
{
    [SerializeField] private Meteorite _prefab;
    [SerializeField] private Transform _target;

    [SerializeField] private int _poolSize = 500;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private float _spawnDistance = 5f;
    [SerializeField] private float _spawnWidth = 2f;

    private Queue<Meteorite> _pool;
    private List<Meteorite> _activeMeteorites = new List<Meteorite>();

    private void Start()
    {
        // Инициализируем пул объектов
        _pool = new Queue<Meteorite>();

        for (int i = 0; i < _poolSize; i++)
        {
            Meteorite obj = Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        StartCoroutine(SpawnRoutine());
        StartCoroutine(ActiveMeteoritesCotorutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(1f / _spawnRate);
        }
    }

    /*private void FixedUpdate()
    {
        // Проверяем, какие метеориты вышли за границу
        for (int i = _activeMeteorites.Count - 1; i >= 0; i--)
        {
            Meteorite obj = _activeMeteorites[i];

            if (Vector2.Distance(obj.transform.position, _target.position) > _spawnDistance * 1.5f)
            {
                // Деактивируем, убираем из активного списка и возвращаем в пул
                obj.gameObject.SetActive(false);
                _activeMeteorites.RemoveAt(i);
                _pool.Enqueue(obj);
            }
        }
    }*/

    private IEnumerator ActiveMeteoritesCotorutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);

            for (int i = _activeMeteorites.Count - 1; i >= 0; i--)
            {
                Meteorite obj = _activeMeteorites[i];

                if (Vector2.Distance(obj.transform.position, _target.position) > _spawnDistance * 1.5f)
                {
                    // Деактивируем, убираем из активного списка и возвращаем в пул
                    obj.gameObject.SetActive(false);
                    _activeMeteorites.RemoveAt(i);
                    _pool.Enqueue(obj);
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
        Meteorite obj = _pool.Dequeue();
        obj.transform.position = spawnPosition;
        obj.gameObject.SetActive(true);
        obj.Init();

        // Добавляем в активный список
        _activeMeteorites.Add(obj);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnDistance);
        Gizmos.DrawWireSphere(transform.position, _spawnDistance - _spawnWidth);
        Gizmos.DrawWireSphere(transform.position, _spawnDistance + _spawnWidth);
    }
}
