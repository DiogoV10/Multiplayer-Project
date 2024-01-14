using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace V10
{
    public class CostumizationSceneUI : MonoBehaviour
    {

        [SerializeField] Button backButton;
        [SerializeField] Button nextCharacterSkinButton;
        [SerializeField] Button previousCharacterSkinButton;
        [SerializeField] Button nextPrimaryWeaponSkinButton;
        [SerializeField] Button previousPrimaryWeaponSkinButton;
        [SerializeField] Button nextSecondaryWeaponSkinButton;
        [SerializeField] Button previousSecondaryWeaponSkinButton;
        [SerializeField] Button nextSpecialWeaponSkinButton;
        [SerializeField] Button previousSpecialWeaponSkinButton;
        [SerializeField] Button applyButton;

        [SerializeField] Texture2D[] weaponTexture;
        [SerializeField] Mesh[] characterMesh;

        [SerializeField] Texture2D primaryWeaponTexture, secondaryWeaponTexture, specialWeaponTexture;
        [SerializeField] Mesh choosedCharacterMesh;

        [SerializeField] GameObject primaryTestWeapon, secondaryTestWeapon, SpecialTestWeapon;
        [SerializeField] GameObject testCharacter;

        DirectoryInfo dir;

        [SerializeField] int primaryTextureIndex = 0, secondaryTextureIndex = 0, specialTextureIndex = 0, meshIndex = 0;

        private void Awake()
        {
            backButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.LobbyScene);
            });

            nextPrimaryWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin(1, ref primaryTextureIndex, primaryTestWeapon);
            });

            previousPrimaryWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin(-1, ref primaryTextureIndex, primaryTestWeapon);
            });

            nextSecondaryWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin(1, ref secondaryTextureIndex, secondaryTestWeapon);
            });

            previousSecondaryWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin(-1, ref secondaryTextureIndex, secondaryTestWeapon);
            });

            nextSpecialWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin(1, ref specialTextureIndex, SpecialTestWeapon);
            });

            previousSpecialWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin(-1, ref specialTextureIndex, SpecialTestWeapon);
            });

            nextCharacterSkinButton.onClick.AddListener(() =>
            {
                ChangeCharacterSkin();
            });

            previousCharacterSkinButton.onClick.AddListener(() =>
            {
                ChangeCharacterSkin(-1);
            });

            applyButton.onClick.AddListener(() =>
            {
                ApplyChanges();
                Loader.Load(Loader.Scene.LobbyScene);
            });
        }

        private void Start()
        {
            dir = new DirectoryInfo("Assets/Packages/PolygonBattleRoyale/Textures/Weapons");
        }

        void ChangeWeaponSkin(int direction, ref int textureIndex, GameObject weapon)
        {
            if(direction == 1) textureIndex = (textureIndex + direction) % weaponTexture.Length;
            else
            {
                if (textureIndex == 0) textureIndex = weaponTexture.Length - 1;
                else textureIndex = textureIndex + direction;
                //textureIndex = weaponTexture.Length - (weaponTexture.Length - Mathf.Abs((textureIndex + direction) % weaponTexture.Length));
            }

            weapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", weaponTexture[textureIndex]);
            //foreach (Transform obj in weapon.transform)
            //{
            //    obj.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", weaponTexture[textureIndex]);
            //}
        }

        void ChangeCharacterSkin(int direction = 1)
        {
            if (direction == 1) meshIndex = (meshIndex + direction) % characterMesh.Length;
            else
            {
                if (meshIndex == 0) meshIndex = characterMesh.Length - 1;
                else meshIndex = meshIndex + direction;
            }

            testCharacter.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = characterMesh[meshIndex];
        }

        void ApplyChanges()
        {
            PlayerPrefs.SetString("Mesh", characterMesh[meshIndex].name);
            PlayerPrefs.SetString("PrimaryWeapon", weaponTexture[primaryTextureIndex].name);
            PlayerPrefs.SetString("SecondaryWeapon", weaponTexture[secondaryTextureIndex].name);
            PlayerPrefs.SetString("SpecialWeapon", weaponTexture[specialTextureIndex].name);
        }

        //public Texture2D GetTexture() => choosedWeaponTexture;
        public Mesh GetMesh() => choosedCharacterMesh;
    }
}
