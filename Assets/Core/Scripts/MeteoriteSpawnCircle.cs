using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteSpawnCircle : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _target;

    [SerializeField] private int _poolSize = 500;
    [SerializeField] private float _spawnRate = 1f;
    [SerializeField] private float _spawnDistance = 5f;
    [SerializeField] private float _spawnWidth = 2f;

    private Queue<GameObject> _pool;
    private int _currentIndex = 0;

    private void Start()
    {
        // �������������� ��� ��������
        _pool = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab);
            obj.SetActive(false);
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

        // ��������� ��������� ���� � ��������
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float offset = Random.Range(-_spawnWidth / 2, _spawnWidth / 2);

        // ��������� ������� ������
        Vector2 spawnPosition = (Vector2)_target.position + new Vector2(
            Mathf.Cos(angle) * (_spawnDistance + offset),
            Mathf.Sin(angle) * (_spawnDistance + offset)
        );

        // ���� ������ �� ���� � ���������� ���
        GameObject obj = _pool.Dequeue();
        obj.transform.position = spawnPosition;
        obj.SetActive(true);

        // ���������� � ����� �������
        _pool.Enqueue(obj);
    }
}
