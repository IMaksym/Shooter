using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipGuns : MonoBehaviour
{
    public GameObject inventory;
    public GameObject[] weapons;
    GameObject currentGun;

    void Start()
    {
        inventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeSelf); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleWeapon(2);
        }
    }

    public void SelectWeapon(int choice)
    {
        if (currentGun != null)
        {
            currentGun.SetActive(false);
        }

        currentGun = weapons[choice];
        currentGun.SetActive(true);
    }

    public void ToggleWeapon(int choice)
    {
        if (currentGun != null && currentGun == weapons[choice])
        {
            currentGun.SetActive(false);
            currentGun = null;
        }
        else
        {
            SelectWeapon(choice);
        }
    }
}