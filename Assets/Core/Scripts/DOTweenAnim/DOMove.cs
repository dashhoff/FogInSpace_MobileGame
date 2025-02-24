using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DOMove : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    [SerializeField] private bool _autoStart;
    [SerializeField] private bool _loop = true;

    [SerializeField] private float _moveDuration = 2f;
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private float _startDelay = 1f;

    [SerializeField] private Ease _ease;

    private void Start()
    {
        if (_autoStart)
            Init();
    }

    public void Init()
    {
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement()
    {
        yield return new WaitForSeconds(_startDelay);
        MoveToNextPoint(0);
    }

    private void MoveToNextPoint(int index)
    {
        if (_points.Length == 0) return;

        Transform targetPoint = _points[index];

        transform.DOMove(targetPoint.position, _moveDuration)
            .SetUpdate(true)
            .SetEase(_ease)
            .OnComplete(() =>
            {
                StartCoroutine(WaitAndMove(index));
            });
    }

    private IEnumerator WaitAndMove(int currentIndex)
    {
        yield return new WaitForSeconds(_waitTime);

        int nextIndex = (currentIndex + 1) % _points.Length;
        if (!_loop && nextIndex == 0) yield break; // Остановка, если не зациклено

        MoveToNextPoint(nextIndex);
    }
}
