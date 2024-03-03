namespace Pamux
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    using Object = System.Object;

    public class ObjectPool
    {
        private static Object poolLock = new Object();

        private static Dictionary<GameObject, ObjectPool> inUse = new Dictionary<GameObject, ObjectPool>();

        private Dictionary<GameObject, ObjectPool> toBeReset = new Dictionary<GameObject, ObjectPool>();

        private List<GameObject> available = new List<GameObject>();

        public static ObjectPool GetPool(GameObject go)
        {
            return inUse[go];
        }

        private int startingCount;

        private int minCount;

        private int incrementBy;

        private int maxCount;

        private int currentCount;

        public delegate GameObject CreatePooledObject();

        private CreatePooledObject factory;

        public ObjectPool(int startingCount, int minCount, int maxCount, int incrementBy, CreatePooledObject factory)
        {
            this.currentCount = 0;
            this.factory = factory;
#if USE_POOLING
              this.startingCount = startingCount;
              this.minCount = minCount;
              this.maxCount = maxCount;
              this.incrementBy = incrementBy;
  
              CreateObjects(Math.Max(this.startingCount, this.minCount));
#endif
        }

        private void CreateObjects(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject go = factory();
                available.Add(go);
                currentCount++;
            }
        }

        public GameObject Acquire()
        {
#if USE_POOLING
          lock (poolLock)
          {
              if (!EnsureEnoughAvailableObjects())
              {
                  return null;
              }
  
              GameObject obj = available[0];
              available.RemoveAt(0);
              inUse.Add(obj, this);
              return obj;
          }
#else
            return factory();
#endif
        }

        private static void Reset(Object g)
        {
            GameObject go = g as GameObject;

            Rigidbody rb = go.GetComponentInChildren<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }

        public static void Release(GameObject go)
        {
            //Debug.Log("RELEASE: " + go.name);
#if USE_POOLING
          lock (poolLock)
          {
              if (!inUse.ContainsKey(go))
              {
                  UnityEngine.Object.Destroy(go);
                  return;
              }
  
              //Dictionary<GameObject, ObjectPool> toBeReset = inUse[go].toBeReset;
  
              inUse.Remove(go);
              go.SetActive(false);
          }
  
          Thread thread = new System.Threading.Thread(Reset);
          thread.Start(go);
#else
            UnityEngine.Object.Destroy(go);
#endif
        }

        internal static IEnumerator Release(GameObject gameObject, float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            Release(gameObject);
        }

        private bool EnsureEnoughAvailableObjects()
        {
            //Debug.Log(available.Count + "A I" + inUse.Count + "CC " + currentCount);
            if (available.Count > 1)
            {
                return true;
            }
            //Debug.Log("AC " + available.Count);
            if (currentCount >= maxCount)
            {
                throw new Exception("TOO MANY OBJECTS");
            }

            CreateObjects(incrementBy);
            //Debug.Log("CC " + currentCount);
            return true;
        }

        internal GameObject Acquire(bool activate, Vector3 position, Quaternion rotation, Vector3 localScale)
        {
            GameObject go = Acquire();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.transform.localScale = localScale;
            if (activate)
            {
                go.SetActive(true);
            }
            return go;
        }
    }
}