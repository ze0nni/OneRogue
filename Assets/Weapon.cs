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

    public enum WeaponHitTaskPhase {
        Prepare,
        Reload
    }

    sealed public class WeaponHitTask  {
        public WeaponData weapon;
        public WeaponHitTaskPhase phase;
        public float ratio;

        public float ratioYOffset() {
            switch (phase) {
                case WeaponHitTaskPhase.Prepare:
                    return weapon.PrepareCurveYOffset.Evaluate(ratio);
                case WeaponHitTaskPhase.Reload:
                    return weapon.ReloadCurveYOffset.Evaluate(ratio);
            }
            return 0;
        }

        public float ratioZRotation()
        {
            switch (phase)
            {
                case WeaponHitTaskPhase.Prepare:
                    return weapon.PrepareCurveZRotation.Evaluate(ratio);
                case WeaponHitTaskPhase.Reload:
                    return weapon.ReloadCurveZRotation.Evaluate(ratio);
            }
            return 0;
        }
    }

    IEnumerator HitTask(WeaponData currentWeapon) {
        var wait = currentWeapon.Prepare / 1000f;

        var msg = new WeaponHitTask();

        while (wait > 0)
        {
            msg.weapon = currentWeapon;
            msg.phase = WeaponHitTaskPhase.Prepare;
            msg.ratio = 1f - (wait / currentWeapon.Prepare * 1000f);

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForFixedUpdate();
            wait -= Time.deltaTime;
        }

        SendMessage("OnWeaponHit", currentWeapon, SendMessageOptions.DontRequireReceiver);

        wait = currentWeapon.Reload / 1000f;
        while (wait > 0)
        {
            msg.weapon = currentWeapon;
            msg.phase = WeaponHitTaskPhase.Reload;
            msg.ratio = 1f - (wait / currentWeapon.Reload * 1000f);

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForFixedUpdate();
            wait -= Time.deltaTime;
        }

        msg.weapon = currentWeapon;
        msg.ratio = 1f;
        SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
    }
}
