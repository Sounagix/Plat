using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform gunExit;

    [SerializeField]
    private float shotCD;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletPool;

    [SerializeField]
    private string enemyTag;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float gunPower;

    private bool canShot = true;

    public bool CanShot()
    {
        return canShot;
    }

    public void Shoot()
    {
        CancelInvoke();
        canShot = false;
        BulletBehaviour currentBullet = Instantiate(bulletPrefab).GetComponent<BulletBehaviour>();
        currentBullet.gameObject.transform.position = gunExit.position;
        currentBullet.SetUp(enemyTag, damage, transform.parent.transform.right, gunPower);
        Invoke(nameof(BackCD), shotCD);
    }

    private void BackCD()
    {
        canShot = true;
    }
}
