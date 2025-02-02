using System.Collections;
using UnityEngine;

public class MeteoriteSpawn : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _spawning;

    [SerializeField] private float _spawnDelay = 1f;

    [SerializeField] private int _maxObjects = 10;
    private int _currentObjects = 0;

    [Header("Randomization")]
    [SerializeField] private bool _randomRotation = true;

    [SerializeField] private bool _randomScale = true;
    [SerializeField] private Vector2 _randomScaleRange = new Vector2(0.5f, 2);

    [SerializeField] private bool _randomTorque = true;
    [SerializeField] private Vector2 _randomTorqueRange = new Vector2(-45f, 45f);

    [SerializeField] private bool _randomDirection = true;
    [SerializeField] private Vector2 _randomDirectionRange = new Vector2(-1f, 1f);

    [Header("Other")]
    [SerializeField] private Vector3 _spawnArea = new Vector3(10, 10, 0);

    private Coroutine _spawnCoroutine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(SpawnMeteorites());
    }

    public void StartSpawn() => _spawning = true;
    public void StopSpawn() => _spawning = false;

    private IEnumerator SpawnMeteorites()
    {
        while (_spawning)
        {
            if (_currentObjects >= _maxObjects)
            {
                yield return new WaitForSeconds(_spawnDelay);
                continue;
            }

            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-_spawnArea.x / 2, _spawnArea.x / 2),
                Random.Range(-_spawnArea.y / 2, _spawnArea.y / 2),
                Random.Range(-_spawnArea.z / 2, _spawnArea.z / 2)
            );

            GameObject newMeteorite = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            Meteorite meteorite = newMeteorite.GetComponent<Meteorite>();

            if (_randomRotation)
                newMeteorite.transform.rotation = Random.rotation;

            if (_randomScale)
            {
                float randomScale = Random.Range(_randomScaleRange.x, _randomScaleRange.y);
                newMeteorite.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            }

            if (_randomTorque)
            {
                Vector3 randomTorque = new Vector3(
                    Random.Range(_randomTorqueRange.x, _randomTorqueRange.y),
                    Random.Range(_randomTorqueRange.x, _randomTorqueRange.y),
                    Random.Range(_randomTorqueRange.x, _randomTorqueRange.y)
                );
                meteorite.SetTorque(randomTorque);
            }

            if (_randomDirection)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(_randomDirectionRange.x, _randomDirectionRange.y),
                    Random.Range(_randomDirectionRange.x, _randomDirectionRange.y),
                    0
                );
                meteorite.SetDirection(randomDirection);
            }

            meteorite.Init();

            _currentObjects++;

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _spawnArea);
    }
}
