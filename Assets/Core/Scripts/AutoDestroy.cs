using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _delay;

    [SerializeField] private bool _shrink;
    [SerializeField] private float _shrinkDuration = 0.5f;

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(_delay);

        if (_shrink)
        {
            transform.DOScale(Vector3.zero, _shrinkDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => Destroy(gameObject));
        }
        else
            Destroy(gameObject);
    }
}
