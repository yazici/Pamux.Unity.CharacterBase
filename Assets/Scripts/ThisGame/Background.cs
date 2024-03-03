using UnityEngine;
using System.Collections;

namespace Pamux
{
  namespace Zodiac
  {

    public class Background : MonoBehaviour
    {
      public float farScrollSpeed;
      public float tileSizeZ;

      public float nearSpawnPeriod = 20.0f;
      public float nearScrollSpeedMin;
      public float nearScrollSpeedMax;
      public GameObject prefabNear;

      private float lastNearSpawnTime = 0;

      private Vector3 startPosition;

      void Start()
      {
        startPosition = transform.position;
      }

      void Update()
      {
        if (Time.time - lastNearSpawnTime > nearSpawnPeriod)
        {
          Vector3 scale = Random.Range(0.2f, 1.0f) * Vector3.one;
          Vector3 position = new Vector3(Random.Range(GameArea2D.INSTANCE.left + scale.x, GameArea2D.INSTANCE.right - scale.x),
                                                      -Random.Range(30.0f, 40.0f),
                                                      GameArea2D.INSTANCE.top + 20f);
          Quaternion rotation = Quaternion.identity;
          rotation.eulerAngles = new Vector3(90, 0, 0);


          GameObject newPlanet = GameController.INSTANCE.planetPool.Acquire(true, position, rotation, scale);


          newPlanet.GetComponent<SimpleMover>().velocity = Vector3.back * Random.Range(nearScrollSpeedMin, nearScrollSpeedMax);
          lastNearSpawnTime = Time.time;
        }

        float newPosition = Mathf.Repeat(-Time.time * farScrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
      }
    }
  }
}