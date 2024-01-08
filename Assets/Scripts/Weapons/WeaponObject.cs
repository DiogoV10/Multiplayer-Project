using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class WeaponObject : MonoBehaviour
    {


        [SerializeField] private WeaponObjectSO weaponObjectSO;


        public WeaponObjectSO GetWeaponObjectSO()
        {
            return weaponObjectSO;
        }


    }
}
