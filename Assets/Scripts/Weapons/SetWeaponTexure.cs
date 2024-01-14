using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace V10
{
    public class SetWeaponTexure : MonoBehaviour
    {

        [SerializeField] GameObject[] primaryWeapons;
        [SerializeField] GameObject[] secondaryWeapons;
        [SerializeField] GameObject[] specialWeapons;

        [SerializeField] Texture2D[] weaponTexture;

        int primaryTexture, secondaryTexture, specialTexture;

        private void Start()
        {
            primaryTexture = PlayerPrefs.GetInt("PrimaryWeapon");
            secondaryTexture = PlayerPrefs.GetInt("SecondaryWeapon");
            specialTexture = PlayerPrefs.GetInt("SpecialWeapon");

            foreach (GameObject weapon in primaryWeapons)
            {
                weapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", weaponTexture[primaryTexture]);
            }

            foreach (GameObject weapon in secondaryWeapons)
            {
                weapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", weaponTexture[secondaryTexture]);
            }

            foreach (GameObject weapon in specialWeapons)
            {
                weapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", weaponTexture[specialTexture]);
            }
        }

    }
}
