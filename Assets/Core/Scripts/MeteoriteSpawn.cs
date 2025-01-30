using System.Collections;
using UnityEngine;

public class MeteoriteSpawn : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _spawning;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private int _maxObjects = 10;
    [SerializeField] private Vector3 _spawnArea = new Vector3(10, 10, 0); 

    [Header("Randomization")]
    [SerializeField] private bool _randomRotation = true;
    [SerializeField] private Vector2 _randomScaleRange = new Vector2(0.5f, 2);

    [SerializeField] private int _currentObjects = 0;

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

    public void StartSpawn()
    {
        _spawning = true;
    }

    public void StopSpawn()
    {
        _spawning = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, _spawnArea);
    }

    private IEnumerator SpawnMeteorites()
    {
        while (_spawning)
        {
            if (_currentObjects >= _maxObjects)
                yield return null;

            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-_spawnArea.x / 2, _spawnArea.x / 2),
                Random.Range(-_spawnArea.y / 2, _spawnArea.y / 2),
                Random.Range(-_spawnArea.z / 2, _spawnArea.z / 2)
            );

            GameObject newObject = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            newObject.GetComponent<Meteorite>().Init();

            if (_randomRotation)
                newObject.transform.rotation = Random.rotation;

            float randomScale = Random.Range(_randomScaleRange.x, _randomScaleRange.y);
            newObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            _currentObjects++;

            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }
}
