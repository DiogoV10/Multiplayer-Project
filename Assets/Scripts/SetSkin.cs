using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

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
            applyMesh = (Mesh)AssetDatabase.LoadAssetAtPath(path + choosedMesh + ".mesh", typeof(Mesh));
            gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh = applyMesh;
        }
    }
}
