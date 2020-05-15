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
    public Transform bulletHitPrefab;
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

        if (Time.time >= timeToSpawnEffect)
        {
            timeToSpawnEffect = Time.time + 1 / spawnEffectRate;
            //Spawn bullet and muzzle flash
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null) //if it didnt hit anything
            {
                hitPos = (mousePosition - firePointPosition) * 30f;
                //We cannot check if Vector3 is null or not
                // to check this we can assign x,y,z with some crazy value to ensure
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hitPos;
            }
            StartCoroutine(Effect(hitPos, hitNormal));
        }
    }

    IEnumerator Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        //spawn Bullet
        Transform trail = Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        if(lr!= null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.04f);

        if(hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitParticle = Instantiate(bulletHitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(hitParticle.gameObject, 1f);
        }

        //spawn MuzzleFlash
        Transform clone = Instantiate(muzzelFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.SetParent(firePoint);
        float size = Random.Range(0.6f, 0.8f);
        clone.localScale = new Vector3(size, size, size);
        yield return 0; //skip for 1 frame
        Destroy(clone.gameObject);
    }
}
