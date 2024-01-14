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

        string path, primaryTexture, secondaryTexture, specialTexture;
        Texture2D applyPrimaryTexture, applySecondaryTexture, applySpecialTexture;

        private void Start()
        {
            path = "Assets/PLayerPrefs/WeaponTextures/";
            primaryTexture = PlayerPrefs.GetString("PrimaryWeapon");
            secondaryTexture = PlayerPrefs.GetString("SecondaryWeapon");
            specialTexture = PlayerPrefs.GetString("SpecialWeapon");

            Debug.Log(primaryTexture);
            Debug.Log(secondaryTexture);
            Debug.Log(specialTexture);

            applyPrimaryTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path + primaryTexture + ".png", typeof(Texture2D));
            applySecondaryTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path + secondaryTexture + ".png", typeof(Texture2D));
            applySpecialTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(path + specialTexture + ".png", typeof(Texture2D));

            foreach (GameObject weapon in primaryWeapons)
            {
                weapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", applyPrimaryTexture);
            }

            foreach (GameObject weapon in secondaryWeapons)
            {
                weapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", applySecondaryTexture);
            }

            foreach (GameObject weapon in specialWeapons)
            {
                weapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", applySpecialTexture);
            }
        }

    }
}
