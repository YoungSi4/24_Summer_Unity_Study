using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeLuncher : Weapon
{
    public GameObject granade;
    public float explodeRange;



    protected override void Fire()
    {

        var tempGranade = Instantiate(granade, muzzle.position, transform.rotation);
        tempGranade.GetComponent<Granade>().Init(bulletSpeed, bulletDmg, explodeRange);
    }
    protected override void OnRelease() {}

}
