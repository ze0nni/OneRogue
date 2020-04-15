using Data;
using Monsters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnDamageableTouched : UnityEvent<Damageable>
{
}


public class Weapon : MonoBehaviour
{
    public WeaponDataList weaponList;
    public OnDamageableTouched OnDamageableTouched;

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

    private void ApplyHit(WeaponData weapon) {
        var hits = Physics.OverlapSphere(
            transform.position + transform.forward * weapon.Range * 0.5f,
            weapon.Range / 2
        );
    
        foreach (var h in hits) {
            var rigid = h.gameObject.GetComponent<Rigidbody>();
            
            if (null != rigid) {
                var f = h.gameObject.transform.position - transform.position;
                rigid.AddForce(f.normalized * weapon.Mass, ForceMode.Impulse);
            }
            
            var damagable = h.gameObject.GetComponent<Damageable>();
            if (null == damagable) {
                continue;
            }

            damagable.Hit(currentWeapon.Damage);
            OnDamageableTouched.Invoke(damagable);
        }
    }

    private void OnDrawGizmos() {
        if (null == currentWeapon) {
            return;
        }
        Gizmos.DrawSphere(
            transform.position + transform.forward * currentWeapon.Range * 0.5f,
            currentWeapon.Range / 2
        );
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
            yield return new WaitForEndOfFrame();
            wait -= Time.deltaTime;
        }
        #endregion

        yield return new WaitWhile(() => isTriggered);

        yield return HitPaseTask(
            WeaponHitTaskPhase.Hit, msg, currentWeapon.Release,
            //If player not prepare
            1 - (1-msg.ratio),
        2);

        ApplyHit(currentWeapon);

        SendMessage("OnWeaponHit", currentWeapon, SendMessageOptions.DontRequireReceiver);

        yield return HitPaseTask(WeaponHitTaskPhase.Reload, msg, currentWeapon.Reload, 2, 3);

        isHitTaskInProgress = false;
    }

    IEnumerator HitPaseTask(WeaponHitTaskPhase phase, WeaponHitTask msg, float waitMs, float min, float max) {
        var wait = waitMs / 1000f;
        var range = max - min;

        SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
        yield return new WaitForEndOfFrame();
        wait -= Time.deltaTime;
        while (wait > 0)
        {
            msg.update(
                min + (1f - (wait / waitMs * 1000f)) * range, 
                phase
            );

            SendMessage("OnWeaponHitTask", msg, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForEndOfFrame();
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
