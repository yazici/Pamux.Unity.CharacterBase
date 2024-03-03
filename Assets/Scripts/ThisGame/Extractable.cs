using UnityEngine;
using System.Collections;


namespace Pamux
{
  namespace Zodiac
  {

    public class Extractable : MonoBehaviour
    {
      internal int extractionCount = 0;
      public int maxExtractionCount = 1;
      public float extractionTime = 3.0f;
      internal float currentExtractionStart = 0.0f;
      public LevelItemData lid;


      void SetStyle(string style)
      {
        lid.stylable.SetStyle(style);

        //transform.position = this.styleInfo.initialTransform.position;
        transform.rotation = (lid.stylable.styleInfo as ExtractableStyleInfo).initialTransform.rotation;
        transform.localScale = (lid.stylable.styleInfo as ExtractableStyleInfo).initialTransform.localScale;
      }

      void OnTriggerEnter(Collider other)
      {
        //Debug.Log(gameObject.name + " :OnTriggerEnter: " + other.gameObject.name);

        if (extractionCount >= maxExtractionCount || !Player.IsAlive() || other.gameObject != Player.INSTANCE.gameObject)
        {
          return;
        }
        GetComponent<AudioSource>().Play();
        currentExtractionStart = Time.time;

        Player.INSTANCE.extractionGauge.transform.parent = this.gameObject.transform;
        Player.INSTANCE.extractionGauge.gameObject.SetActive(true);
        Player.INSTANCE.extractionGauge.transform.localPosition = Vector3.zero;
        Player.INSTANCE.extractionGauge.transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        Player.INSTANCE.extractionGauge.transform.localScale = Vector3.one;
        Player.INSTANCE.extractionGauge.fullValue = extractionTime;
        Player.INSTANCE.extractionGauge.Show();
      }

      void OnTriggerExit(Collider other)
      {
        GetComponent<AudioSource>().Stop();
        if (currentExtractionStart == 0.0f || !Player.IsAlive())
        {
          return;
        }

        currentExtractionStart = 0.0f;

        Player.INSTANCE.extractionGauge.transform.parent = null;
        Player.INSTANCE.extractionGauge.Hide();
      }

      void OnTriggerStay(Collider other)
      {
        if (currentExtractionStart == 0.0f || !Player.IsAlive())
        {
          return;
        }

        Player.INSTANCE.extractionGauge.v = Time.time - currentExtractionStart;
        if (Time.time - currentExtractionStart >= extractionTime)
        {
          ++extractionCount;
          GetComponent<AudioSource>().Stop();
          Player.INSTANCE.extractionGauge.transform.parent = null;
          Player.INSTANCE.extractionGauge.Hide();
          currentExtractionStart = 0.0f;
          if (extractionCount >= maxExtractionCount)
          {
            (lid.stylable.styleInfo as ExtractableStyleInfo).SetExtractable(false);
          }
        }
      }
    }
  }
}