using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace V10
{
    public class PlayerWeapons : MonoBehaviour
    {
        public List<GameObject> weaponsList;
        public List<Gun> weapons;

        public KeyCode firstWeapon, secondWeapon, thirdWeapon;

        public TextMeshPro firstWeaponName, secondWeaponName, thirdWeaponName;

        public float switchTime;

        private int selectedWeapon;
        private float timeSinceLastSwitch = 0f;

        private void Awake()
        {
            weapons[0].gameObject.SetActive(true);
            weapons[1].gameObject.SetActive(false);
            weapons[2].gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            firstWeaponName.text = weapons[0].name;
            secondWeaponName.text = weapons[1].name;
            thirdWeaponName.text = weapons[2].name;
        }

        public void Update()
        {

        }

        private void SetWeapons()
        {

        }

        private void SelectWeapon()
        {
            
        }
    }
}
