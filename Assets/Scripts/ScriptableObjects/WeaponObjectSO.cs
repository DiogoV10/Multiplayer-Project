using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    [CreateAssetMenu(menuName = "Weapon/Weapon Object")]
    public class WeaponObjectSO : ScriptableObject
    {

        public Transform prefab;
        public Sprite sprite;
        public string objectName;

    }
}
