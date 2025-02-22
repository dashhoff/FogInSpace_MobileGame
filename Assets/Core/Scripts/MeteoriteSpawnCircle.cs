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
    private int _currentIndex = 0;

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
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(1f / _spawnRate);
        }
    }

    private void SpawnPrefab()
    {
        if (_target == null) return;

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

        // Возвращаем в конец очереди
        _pool.Enqueue(obj);
    }
}
