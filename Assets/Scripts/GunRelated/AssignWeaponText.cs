using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace V10
{
    public class AssignWeaponText : MonoBehaviour
    {
        public GameObject gunToGetInfo;

        public TextMeshProUGUI gunName;
        public TextMeshProUGUI gunDamage;
        public TextMeshProUGUI fireRate;
        public TextMeshProUGUI reloadTime;

        private void Start()
        {
            Gun gun = gunToGetInfo.GetComponent<Gun>();


            gunName.text = gunToGetInfo.name;

            gunDamage.text = $"Damage : {gun.gunData.damage}";
            fireRate.text = $"Fire Rate : {gun.gunData.fireRate}";
            reloadTime.text = $"Reload Time : {gun.gunData.reloadTime}";
        }
    }
}
