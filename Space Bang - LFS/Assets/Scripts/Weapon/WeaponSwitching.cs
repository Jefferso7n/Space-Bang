using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] bool limitedScroller = true;
    int selectedWeapon = 0;

    [SerializeField] WeaponUI weaponUI;
    public bool canSwitchWeapon = true;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        if(!canSwitchWeapon) return;

        int previousSelectedWeapon = selectedWeapon;

        // Change weapon with mouse scroll
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                if (limitedScroller)    // To stay in the last weapon
                {
                    selectedWeapon = transform.childCount - 1;
                }
                else
                {
                    selectedWeapon = 0;
                }
            }
            else
            {
                selectedWeapon++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                if (limitedScroller)    // To stay in the first weapon
                {
                    selectedWeapon = 0;
                }
                else
                {
                    selectedWeapon = transform.childCount - 1;
                }
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    // Change weapon as selected on mouse scroll
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weaponUI.UpdateWeaponSpriteOnUI(weapon.GetComponent<SpriteRenderer>().sprite);
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public GameObject GetSelectedWeapon(){
        return transform.GetChild(selectedWeapon).gameObject;
    }
}
