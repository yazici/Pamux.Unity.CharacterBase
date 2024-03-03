using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class EnemyWeapon : Weapon
  {
    public FireRateInfo _fireRate;
    protected override FireRateInfo fireRate { get { return _fireRate; } }

    protected override bool vShouldFireNow() { return true; }
    public float _fireEnergy = 100f;
    public override float fireEnergy { get { return _fireEnergy; } }
    public override int muzzleCount { get { return 1; } }

    void Awake()
    {
      //fireRate.fireRate = 0.5f;   // Time between enemy fires
      //fireRate.fireGroupRate = 3;  // Time between enemy fire groups
      //fireRate.fireGroupCount = 3; // Fires within one enemy fire
    }
  }
}
