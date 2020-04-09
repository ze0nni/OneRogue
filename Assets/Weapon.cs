using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponDataList weaponList;

    WeaponData currentWeapon;

    void Start()
    {
        currentWeapon = weaponList.weapons[Random.Range(0, weaponList.weapons.Length)];

        SendMessage("OnWeaponChanged", currentWeapon, SendMessageOptions.DontRequireReceiver);
    }

    void Update()
    {
        
    }

    internal void Trigger()
    {
        StartCoroutine(HitTask(currentWeapon));
    }

    public class WeaponHitTask  {
        public WeaponData weapon;
        public float ratio;
    }

    IEnumerator HitTask(WeaponData currentWeapon) {
        var wait = currentWeapon.Reload / 1000f;

        var msg = new WeaponHitTask();

        while (wait > 0) {
            msg.weapon = currentWeapon;
            msg.ratio = 1f - (wait / currentWeapon.Reload * 1000f);

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForFixedUpdate();
            wait -= Time.deltaTime;
        }

        msg.weapon = currentWeapon;
        msg.ratio = 1f;
        SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);


        SendMessage("OnWeaponHit", currentWeapon, SendMessageOptions.DontRequireReceiver);
    }
}
