using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class WeaponFireStyleInfo : PhysicsStyleInfo
  {
      // Rigidbody acting forces
      public float impulse;
      public float continuousForce;
      public float randomTorque;
  
      // Effects
      public GameObject muzzleEffect;
      public GameObject explosionEffect;
      public GameObject impactEffect;
  
      // Behavior
      public GameObject[] detachOnDeath;
  
      public bool targetSeeker = false;
  
  }
}
