using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satania.QuickWrapper
{
    //[CreateAssetMenu(menuName = "QuickWrapper/Manifest Batcher", fileName = "New Batch", order = 1000)]
    public class QuickWrapperBatch : ScriptableObject
    {
        public List<QuickWrapperManifest> manifests;
    }
}