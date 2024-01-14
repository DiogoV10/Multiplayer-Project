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
        string choosedMesh, path;
        Mesh applyMesh;

        // Start is called before the first frame update
        void Start()
        {
            path = "Assets/PLayerPrefs/Meshes/";
            choosedMesh = PlayerPrefs.GetString("Mesh");

            #if UNITY_EDITOR
            applyMesh = (Mesh)AssetDatabase.LoadAssetAtPath(path + choosedMesh + ".mesh", typeof(Mesh));
            if (applyMesh != null)
            #endif
            {
                gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = applyMesh;
            }
        }
    }
}
