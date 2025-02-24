using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class SpaceBomb : MonoBehaviour
{
    [SerializeField] private bool _exloded;

    [SerializeField] private float _explosionPower;
    [SerializeField] private float _explosionRadius;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private VisualEffect _explosionEffect;

    public void Explosion()
    {
        _exloded = true;

        //Instantiate(_explosionParticle, transform.position, Quaternion.identity);
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);

        //_rb.AddExplosionForce(_explosionPower, transform.position, _explosionRadius);

        // Находим все объекты в радиусе
        Collider[] objects = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(_explosionPower, transform.position, _explosionRadius);

            /*if (obj.gameObject.CompareTag("SpaceBomb"))
            {
                SpaceBomb spaceBomb = obj.GetComponent<SpaceBomb>();

                if (spaceBomb._exloded)
                    return;

                spaceBomb.Explosion();
            }*/
        }

        DOTween.Sequence()
            .Append(gameObject.transform.DOScale(0, 0.5f))
            .OnComplete(() => 
            { 
                Destroy(gameObject);
            });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Explosion();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 1f);
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
