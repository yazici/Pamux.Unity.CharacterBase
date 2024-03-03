using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class Usable : MonoBehaviour
  {
      public float duration = 5.0f;
      public Energy energy;
      public Gauge gauge;
  
      void Awake()
      {
          energy = this.gameObject.GetComponent<Energy>();
      }
  
      internal void SetEnergy(float amount)
      {
          energy.amount = amount;
      }
  
      internal IEnumerator DisableLater(float duration)
      {
          yield return new WaitForSeconds(duration);
          if (GetComponent<AudioSource>() != null && GetComponent<AudioSource>().isPlaying)
          {
              GetComponent<AudioSource>().Stop();
          }
          this.transform.gameObject.SetActive(false);
      }
  
      internal void Use()
      {
  
          this.transform.gameObject.SetActive(true);
          if (this.gauge != null)
          {
              this.gauge.Show(duration);
          }
          if (GetComponent<AudioSource>() != null)
          {
              GetComponent<AudioSource>().Play();
          }
          StartCoroutine(DisableLater(duration));
      }
  }
}
