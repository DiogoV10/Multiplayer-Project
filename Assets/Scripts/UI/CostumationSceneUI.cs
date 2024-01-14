using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] Button nextWeaponSkinButton;
        [SerializeField] Button previousWeaponSkinButton;
        [SerializeField] Button applyButton;

        [SerializeField] Texture2D[] weaponTexture;
        [SerializeField] Mesh[] characterMesh;

        [SerializeField] Texture2D choosedWeaponTexture;
        [SerializeField] Mesh choosedCharacterMesh;

        [SerializeField] GameObject testWeapon;
        [SerializeField] GameObject testCharacter;

        int textureIndex = 0, meshIndex = 0;

        private void Awake()
        {
            backButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.LobbyScene);
            });

            nextWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin();
            });

            previousWeaponSkinButton.onClick.AddListener(() =>
            {
                ChangeWeaponSkin(-1);
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

        void ChangeWeaponSkin(int direction = 1)
        {
            if(direction == 1)
            {
                if (textureIndex == weaponTexture.Length - 1) textureIndex = 0;
                else textureIndex = textureIndex + direction;
            }
            else
            {
                if (textureIndex == 0) textureIndex = weaponTexture.Length - 1;
                else textureIndex = textureIndex + direction;
            }
            
            testWeapon.gameObject.GetComponent<Renderer>().material.SetTexture("_BaseMap", weaponTexture[textureIndex]);
        }

        void ChangeCharacterSkin(int direction = 1)
        {
            if (direction == 1)
            {
                if (meshIndex == characterMesh.Length - 1) meshIndex = 0;
                else meshIndex = meshIndex + direction;
            }
            else
            {
                if (meshIndex == 0) meshIndex = characterMesh.Length - 1;
                else meshIndex = meshIndex + direction;
            }

            testCharacter.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = (characterMesh[meshIndex]);
        }

        void ApplyChanges()
        {
            choosedCharacterMesh = characterMesh[meshIndex];
            choosedWeaponTexture = weaponTexture[textureIndex];
        }

        public Texture2D GetTexture() => choosedWeaponTexture;
        public Mesh GetMesh() => choosedCharacterMesh;
    }
}
