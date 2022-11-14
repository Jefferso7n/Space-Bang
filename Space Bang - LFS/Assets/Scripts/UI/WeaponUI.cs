using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] Image image;

    public void UpdateWeaponSpriteOnUI(Sprite weaponSprite){
        image.sprite = weaponSprite;
    }

}
