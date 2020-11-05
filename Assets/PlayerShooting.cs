using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;

    public Transform shootFrom;

    public float shootForce = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }
    void ShootProjectile()
    {
        GameObject thisBullet = Instantiate(bullet, shootFrom.transform.position, shootFrom.transform.rotation);
        thisBullet.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.Impulse);
    }
}
