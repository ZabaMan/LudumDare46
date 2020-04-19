using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public AudioSource shootSound;
    public float reloadTime;
    private bool canShoot = false;
    public Transform[] shootPoints;
    public GameObject bulletPrefab;
    public Transform bulletParent;

    void Start()
    {
        StartCoroutine(Reload());
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            foreach(var point in shootPoints)
            {
                var bullet = Instantiate(bulletPrefab, point.position, point.rotation);
                bullet.transform.parent = bulletParent;
            }
            if(shootSound != null)
            shootSound.Play();
            canShoot = false;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}
