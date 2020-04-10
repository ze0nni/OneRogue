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

    #region Triggere
    {
        StartCoroutine(HitTask(currentWeapon));
    }

    public enum WeaponHitTaskPhase {
        Prepare,
        Reload
    }

    sealed public class WeaponHitTask  {
        public WeaponData weapon { get; }
        public WeaponHitTaskPhase phase { get; private set; }
        public float ratio { get; private set; }

        internal WeaponHitTask(WeaponData weapon) {
            this.weapon = weapon;
        }

        internal void update(float ratio, WeaponHitTaskPhase phase)
        {
            this.ratio = ratio;
            this.phase = phase;
        }

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

        var msg = new WeaponHitTask(currentWeapon);

        while (wait > 0)
        {
            msg.update(1f - (wait / currentWeapon.Prepare * 1000f), WeaponHitTaskPhase.Prepare);
            msg.ratio = 1f - (wait / currentWeapon.Prepare * 1000f);

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForFixedUpdate();
            wait -= Time.deltaTime;
        }

        SendMessage("OnWeaponHit", currentWeapon, SendMessageOptions.DontRequireReceiver);

        wait = currentWeapon.Reload / 1000f;
        while (wait > 0)
        {
            msg.update(1f - (wait / currentWeapon.Reload * 1000f), WeaponHitTaskPhase.Reload);

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForFixedUpdate();
            wait -= Time.deltaTime;
        }

        msg.update(1, WeaponHitTaskPhase.Reload);
        msg.ratio = 1f;
        SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
    }

    #endregion
}
