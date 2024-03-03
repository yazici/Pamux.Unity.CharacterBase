#if USE_CREST_WATER

using Crest;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Pamux.Lib.Unity.Commons
{
    public class UnderCrestWaterDetector : MonoBehaviour
    {
        [Tooltip("When player is over this threshold above the water, it is considered not to swim anymore.")]
        public float maxHeightAboveWater = 1.5f;

        // We need one sample height helper for each query of a single frame. 
        // The helper detects the exact height of the wave on a queried position.
        // To allow multiple queries we cache 3 helpers.
        // Note: if more are required, they will be created at runtime and cached.
        private readonly List<SampleHeightHelper> sampleWaterHeightList = new List<SampleHeightHelper>()
        {
            new SampleHeightHelper(),
            new SampleHeightHelper(),
            new SampleHeightHelper()
        };

        /// <summary>
        /// Monitors the number of queries processed within one frame.
        /// </summary>
        public int QueryRequestedCount {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return _queryRequested; }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set { _queryRequested = value; }
        }

        private static int _queryRequested;
        private static int lastFrame = -1;

        private void FixedUpdate()
        {
            // Reset requested queries if we are on the next frame.
            if (lastFrame != Time.frameCount) 
            {
                QueryRequestedCount = 0;
                lastFrame = Time.frameCount;
            }
        }

        public static UnderCrestWaterDetector Master {
            get {
                if (!_master)
                {
                    Debug.LogError("No UnderCrestWaterDetector master instance was found in the scene. Please put one in the scene");
                }
                return _master;
            }
        }

        private static UnderCrestWaterDetector _master;

        public void Awake()
        {
            if (!_master)
            {
                _master = this;
            }
            else
            {
                Debug.LogError("More than one UnderCrestWaterDetector master instance was found in the scene. Please put only one in the scene.");
            }
        }

        private void IncrementQueryCount()
        {
            QueryRequestedCount += 1;
            while (QueryRequestedCount > sampleWaterHeightList.Count-1)
            {
                // Create on the fly a new sampler and add it to the list,
                // so that we can still serve this query despite it exceeded
                // out expected requests count.
                sampleWaterHeightList.Add(new SampleHeightHelper());
            }
        }

        public float GetCurrentWaterHeight(Transform transform, float playerWidth)
        {
            if (OceanRenderer.Instance)
            {
                IncrementQueryCount();
                sampleWaterHeightList[QueryRequestedCount].Init(transform.position, playerWidth);
                sampleWaterHeightList[QueryRequestedCount].Sample(out float waterHeight);
                return waterHeight;
            }
            return 0;
        }

        public bool GetDisplacementNormalSurfaceVel(float playerWidth, out Vector3 o_displacementToPoint, out Vector3 o_normal, out Vector3 o_surfaceVel)
        {
            IncrementQueryCount();
            sampleWaterHeightList[QueryRequestedCount].Init(transform.position, playerWidth);
            return sampleWaterHeightList[QueryRequestedCount].Sample(out o_displacementToPoint, out o_normal, out o_surfaceVel);
        }
    }
}

#endif