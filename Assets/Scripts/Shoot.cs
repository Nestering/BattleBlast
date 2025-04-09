using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Bullet projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float maxDistance = 100f;

    public int damage = 1;
    public float cooldown = 1;
    public float range = 1;

    private float timerShoot;

    public bool enabledBoostDamage, enabledBoostCooldown;
    private void Start()
    {
        Load();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && timerShoot <= 0)
        {
            ShootProjectile();
            if (enabledBoostCooldown)
            {
                timerShoot = 0.05f;
            }
            else
            {
                timerShoot = cooldown;
            }
        }
        timerShoot -= Time.deltaTime;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("damage", damage);
        PlayerPrefs.SetFloat("cooldown", cooldown);
        PlayerPrefs.SetFloat("range", range);
    }
    public void Load()
    {
        damage = PlayerPrefs.GetInt("damage", 5);
        cooldown = PlayerPrefs.GetFloat("cooldown", 1);
        range = PlayerPrefs.GetFloat("range", 1);
    }
    private void ShootProjectile()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(maxDistance);
        }

        Bullet bullet = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        bullet.transform.LookAt(targetPoint);
        bullet.range = range;
        if (enabledBoostDamage)
        {
            bullet.baseDamage = int.MaxValue;
        }
        else
        {
            bullet.baseDamage = damage;
        }
    }
}
