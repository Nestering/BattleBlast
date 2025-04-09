using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{
    public int healthMaxCube;
    [SerializeField] private int healthCube;
    [SerializeField] private int force;
    [SerializeField] private bool isDie;

    private Material _startMaterial;
    private Material _runtimeMaterial;
    private Rigidbody _rb;
    private BoxCollider _collider;

    private void Awake()
    {
        _runtimeMaterial = new Material(GetComponent<Renderer>().material);
        _startMaterial = new Material(GetComponent<Renderer>().material);
        GetComponent<Renderer>().material = _runtimeMaterial;
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
    }
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;
    private bool isInitialized = false;

    private void Start()
    {
        if (!isInitialized)
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            startScale = transform.localScale;
            isInitialized = true;
        }
    }
    public void SpawnThis()
    {
        if (isInitialized)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            transform.localScale = startScale;
        }
        if(_runtimeMaterial == null)
        {
            _runtimeMaterial = new Material(GetComponent<Renderer>().material);
            _startMaterial = new Material(GetComponent<Renderer>().material);
            GetComponent<Renderer>().material = _runtimeMaterial;
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
        }
        else
        {
            _runtimeMaterial = new Material(_startMaterial);
        }
        GetComponent<Renderer>().material = _runtimeMaterial;
        healthCube = healthMaxCube;
        _rb.isKinematic = true;
        _rb.useGravity = false;
        _collider.enabled = true;
        isDie = false;
    }
    public void TakeDamage(int damage)
    {
        healthCube = Mathf.Max(healthCube - damage, 0);
        ChangeMaterial();
        if (healthCube <= 0 && !isDie)
        {
            Death();
        }
    }

    private void ChangeMaterial()
    {
        float healthPercent = (float)healthCube / healthMaxCube;
        float darknessFactor = 0.5f + 0.5f * healthPercent;

        Color originalColor = _runtimeMaterial.color;
        Color newColor = originalColor * darknessFactor;
        newColor.a = originalColor.a;

        _runtimeMaterial.color = newColor;
    }

    private void Death()
    {
        LevelController.Instance.CubeDied();
        _rb.isKinematic = false;
        _rb.useGravity = true;
        isDie = true;
        transform.localScale *= 0.9f;
        Vector3 randomDirection = Random.onUnitSphere;
        _rb.AddForce(randomDirection * force, ForceMode.Impulse);

        StartCoroutine(ChangeObject(transform.localScale * 0.3f, 0.5f));
    }

    private IEnumerator ChangeObject(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        yield return new WaitForSeconds(0.5f);
        _collider.enabled = false;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        gameObject.SetActive(false);
    }
}
