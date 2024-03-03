using UnityEngine;
using System.Collections;


namespace Pamux
{
  public class Planet : MonoBehaviour
  {
      internal PlanetStyleInfo styleInfo;
  
      void Start()
      {
          Styleable stylable = this.gameObject.GetComponent<Styleable>();
  
          string id = Random.Range(1, stylable.stylesRoot.childCount).ToString("D2");
          //Debug.Log(Id);
          stylable.SetStyle("Planet_" + id);
          styleInfo = stylable.styleInfo as PlanetStyleInfo;
          if (styleInfo == null)
          {
              return;
          }
          GetComponent<Renderer>().material = styleInfo.material;
          //transform.rotation = Quaternion.Euler(this.styleInfo.initialRotation);
  
          //transform.position = this.styleInfo.initialTransform.position;
          transform.rotation = this.styleInfo.initialTransform.rotation;
          transform.localScale = this.styleInfo.initialTransform.localScale;
  
      }
  
  }
}
