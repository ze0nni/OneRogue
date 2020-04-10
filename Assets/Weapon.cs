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
        Switch();
    }

    internal void Switch()
    {
        currentWeapon = weaponList.weapons[Random.Range(0, weaponList.weapons.Length)];

        SendMessage("OnWeaponChanged", currentWeapon, SendMessageOptions.DontRequireReceiver);
    }

    void Update()
    {
        
    }

#region Triggere

    bool isTriggered;
    bool isHitTaskInProgress;

    internal void Trigger(bool value)
    {
        isTriggered = value;

        if (value && false == isHitTaskInProgress)
        {
            StartCoroutine(HitTask(currentWeapon));
        }
    }

    public enum WeaponHitTaskPhase {
        Prepare,
        Hit,
        Reload
    }

    sealed public class WeaponHitTask
    {
        public WeaponData weapon { get; }
        public WeaponHitTaskPhase phase { get; private set; }
        public float ratio { get; private set; }

        internal WeaponHitTask(WeaponData weapon)
        {
            this.weapon = weapon;
        }

        internal void update(float ratio, WeaponHitTaskPhase phase)
        {
            this.ratio = ratio;
            this.phase = phase;
        }

        public float ratioYOffset()
        {
            return weapon.PrepareCurveYOffset.Evaluate(ratio);
        }

        public float ratioZRotation()
        {
            return weapon.PrepareCurveZRotation.Evaluate(ratio);
        }
    }

    IEnumerator HitTask(WeaponData currentWeapon) {
        isHitTaskInProgress = true;

        var wait = currentWeapon.Prepare / 1000f;

        var msg = new WeaponHitTask(currentWeapon);

        #region Prepare
        while (wait > 0 && isTriggered)
        {
            msg.update(1f - (wait / currentWeapon.Prepare * 1000f), WeaponHitTaskPhase.Prepare);

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForFixedUpdate();
            wait -= Time.deltaTime;
        }
        #endregion

        yield return new WaitWhile(() => isTriggered);

        yield return HitPaseTask(
            WeaponHitTaskPhase.Hit, msg, currentWeapon.Release,
            //If player not prepare
            1 - (1-msg.ratio),
        2);

        SendMessage("OnWeaponHit", currentWeapon, SendMessageOptions.DontRequireReceiver);

        yield return HitPaseTask(WeaponHitTaskPhase.Reload, msg, currentWeapon.Reload, 2, 3);

        isHitTaskInProgress = false;
    }

    IEnumerator HitPaseTask(WeaponHitTaskPhase phase, WeaponHitTask msg, float waitMs, float min, float max) {
        var wait = waitMs / 1000f;
        var range = max - min;

        SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
        yield return new WaitForFixedUpdate();
        wait -= Time.deltaTime;
        while (wait > 0)
        {
            msg.update(
                min + (1f - (wait / waitMs * 1000f)) * range, 
                phase
            );

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForFixedUpdate();
            wait -= Time.deltaTime;
        }

        msg.update(
            max,
            phase
        );

        SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
    }

    #endregion
}
