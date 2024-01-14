using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace V10
{
    public class SetSkin : MonoBehaviour
    {
        int choosedMesh;
        [SerializeField] Mesh[] characterMesh;


        // Start is called before the first frame update
        void Start()
        {
            choosedMesh = PlayerPrefs.GetInt("Mesh");
            gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = characterMesh[choosedMesh];
        }
    }
}
