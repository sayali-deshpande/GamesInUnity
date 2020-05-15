using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0;
    public int damage = 10;
    public LayerMask whatToHit;
    public Transform bulletTrailPrefab;
    public Transform muzzelFlashPrefab;
    public float spawnEffectRate = 10;

    private float timeToSpawnEffect = 0;
    private float timeToFire = 0;
    private Transform firePoint;
    // Start is called before the first frame update
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("FirePoint is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fireRate == 0)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if(Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1/fireRate;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        // To show infinite or long line we just multiplied it by 100, giving 2nd parameter as a direction
        // to drawLine from start point
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) *100, Color.cyan);
        if(Time.time >= timeToSpawnEffect )
        {
            timeToSpawnEffect = Time.time + 1 / spawnEffectRate;
            //Spawn bullet and muzzle flash
            StartCoroutine("Effect"); 
        }

        if(hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " Damage.");

            Enemy e = hit.collider.GetComponent<Enemy>();
            if(e != null)
            {
                e.DamageEnemy(damage);
                Debug.Log("We hit " + hit.collider.name + " and did " + damage + " Damage.");
            }
        }
    }

    IEnumerator Effect()
    {
        //spawn Bullet
        Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);

        //spawn MuzzleFlash
        Transform clone = Instantiate(muzzelFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.SetParent(firePoint);
        float size = Random.Range(0.6f, 0.8f);
        clone.localScale = new Vector3(size, size, size);
        yield return 0; //skip for 1 frame
        Destroy(clone.gameObject);
    }
}
