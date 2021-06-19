using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    [Header("Unity Handles")]
    public GameObject bullet;
    public Camera fpsCam;
    public Transform attackpoint;
    public Rigidbody player;

    [Header("Graphics/Particles")]
    public GameObject muzzle;
    public TextMeshPro ammo;

    [Header("Floats")] //Gun Stats & Force
    public float force, upwardForce;
    public float timeBetweenShots, spread, reloadTime, timeBetweenShooting;
    public float recoilForce;

    [Header("Integers")]
    int magazineSize, bulletsPerTap;
    int bulletsLeft, bulletsShot;

    [Header("Booleans")]
    bool shooting, readyToShoot, reloading, allowedButtonHold;
    public bool allowInvoke = true; //bugs

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        if (ammo != null)
            ammo.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);

    }

    void PlayerInput()
    {
        //Are you allowed to hold down?
        if (allowedButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if (Input.GetKey(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        //AutoReload
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
            Reload();

        //shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //set
            bulletsShot = 0;

            Shooting();
        }
    }

    void Shooting()
    {
        readyToShoot = false;

        //Where to hit?
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //did I hit?
        Vector3 target;
        if (Physics.Raycast(ray, out hit))
            target = hit.point;
        else
            target = ray.GetPoint(75); // far from player

        //Calculations 
        Vector3 directionWithNoSpread = target - attackpoint.position;

        //spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculations WIth Spread
        Vector3 directionWithSpread = directionWithNoSpread + new Vector3(x, y, 0);

        //Instantiate
        GameObject currenBullet = Instantiate(bullet, attackpoint.position, Quaternion.identity);
        currenBullet.transform.forward = directionWithSpread.normalized;

        //forces
        currenBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * force, ForceMode.Impulse);
        currenBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

       //Graphics
        if (muzzle != null)
            Instantiate(muzzle, attackpoint.position, Quaternion.identity);


        //Bullet Math
        bulletsLeft--;
        bulletsShot++;

        if(allowInvoke)
        {
            Invoke(nameof(ResetShots), timeBetweenShooting);
            allowInvoke = false;

            //recoil
            player.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);

        }

        //multiple shots ber tap
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke(nameof(Shooting), timeBetweenShooting);
    }

    void ResetShots()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    void Reload()
    {
        reloading = true;
        Invoke(nameof(ReloadingFinished), reloadTime);
    }

    void ReloadingFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
