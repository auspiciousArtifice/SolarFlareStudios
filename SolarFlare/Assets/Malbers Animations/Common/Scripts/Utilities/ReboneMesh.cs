using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MalbersAnimations.Utilities
{
    public class ReboneMesh : MonoBehaviour
    {

        public SkinnedMeshRenderer defaultMesh;
        public SkinnedMeshRenderer newMesh;

         
        public void TransferBones()
        {
            newMesh.transform.parent = defaultMesh.transform.parent;
            newMesh.bones = defaultMesh.bones;
            newMesh.rootBone = defaultMesh.rootBone;

            Debug.Log("Mesh: " + newMesh.name + "has the Bones from "+ defaultMesh.name);
        }

    }
}