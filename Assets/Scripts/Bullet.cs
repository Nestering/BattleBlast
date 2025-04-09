using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float force, forceConst;
    [SerializeField] private float minDamageSpeed, maxDamageSpeed, maxDamageMultiplier;
    public float baseDamage;
    public float range = 3f;

    private Rigidbody _rb;
    private Collider _collider;
    private Vector3 _previousPosition;
    private float _currentSpeed;

    public List<GameObject> effects;
    public GameObject effectExploid;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _previousPosition = transform.position;
        _rb.AddForce(transform.forward * 100 * force, ForceMode.Impulse);
        Invoke("Death", 1.5f);
    }

    public void ActiveAudio(bool activeAudioExplotion)
    {
        effectExploid.GetComponent<AudioSource>().volume = activeAudioExplotion ? 1 : 0 ;
    }
    private void FixedUpdate()
    {
        _rb.AddForce(Vector3.forward * forceConst, ForceMode.Force);
        _currentSpeed = (transform.position - _previousPosition).magnitude / Time.fixedDeltaTime;
        _previousPosition = transform.position;
        transform.Rotate(new Vector3(1.5f, 1.3f, -1.7f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentSpeed < minDamageSpeed)
        {
            Death();
            return;
        }

        float speedFactor = Mathf.Clamp01((_currentSpeed - minDamageSpeed) / (maxDamageSpeed - minDamageSpeed));
        int damage = (int)(baseDamage * (1 + speedFactor * (maxDamageMultiplier - 1)));

        // Прямой урон по объекту, если он Cube
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            cube.TakeDamage(damage);
        }

        // Взрывной урон по всем вокруг
        Explode(transform.position, damage);

        Death();
    }

    private void Explode(Vector3 position, int explosionDamage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, range*1.5f);

        foreach (Collider col in hitColliders)
        {
            if (col.TryGetComponent(out Cube cube))
            {
                cube.TakeDamage(explosionDamage);
            }
        }
        Instantiate(effectExploid,position,Quaternion.identity);
    }

    private void Death()
    {
        _collider.enabled = false;

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ChangeObject(transform.localScale * 0.3f, 0.5f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ChangeObject(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        Destroy(gameObject);
    }
}
