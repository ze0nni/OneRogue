using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerCameraWeapon : MonoBehaviour
{
    public Canvas PlayerHandsCanvas;
    
    Weapon weapon;
    GameObject leftRoot;
    GameObject rightRoot;

    GameObject currentWeapon;
    Image currentWeaponImage;

    void Start()
    {
        this.weapon = GetComponent<Weapon>();
        this.leftRoot = PlayerHandsCanvas.transform.Find("leftRoot").gameObject;
        this.rightRoot = PlayerHandsCanvas.transform.Find("rightRoot").gameObject;
    }

    void Update()
    {
        
    }
    
    void OnWeaponChanged(WeaponData weapon) {
        if (null != currentWeapon) {
            DestroyObject(currentWeapon);
        }

        currentWeapon = new GameObject(weapon.Name);
        currentWeaponImage = currentWeapon.AddComponent<Image>();
        currentWeaponImage.sprite = weapon.Image;
        currentWeaponImage.rectTransform.pivot = weapon.Image.pivot / weapon.Image.rect.size;

        currentWeapon.transform.SetParent(rightRoot.transform, false);
    }

    void OnWeaponHitTask(Weapon.WeaponHitTask task) {
        if (null != currentWeaponImage) {
            currentWeaponImage.rectTransform.localPosition = new Vector3(
                0,
                task.ratioYOffset() * task.weapon.Image.rect.height,
                0
            );

            currentWeaponImage.rectTransform.rotation = Quaternion.Euler(
                0,
                0,
                -task.ratioZRotation() * 2 * Mathf.PI * Mathf.Rad2Deg
            );
        }
    }

    void OnWeaponHit(WeaponData weapon) {

    }
}
