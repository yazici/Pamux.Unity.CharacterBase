using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Pamux
{
   using Pamux.Zodiac.UI;

  internal class TriFaceToSign
  {
    internal int triangleStart;
    internal Vector3 coordinate;
    internal ZodiacSigns sign;

    static TriFaceToSign()
    {

    }

    internal TriFaceToSign(int triangleStart, float x, float y, float z, ZodiacSigns sign)
    {
      this.triangleStart = triangleStart;
      this.coordinate = new Vector3(x, y, z);
      this.sign = sign;

      DodecahedronMenu.triangleMap[triangleStart] = this;
      DodecahedronMenu.triangleMap[triangleStart + 1] = this;
      DodecahedronMenu.triangleMap[triangleStart + 2] = this;
      DodecahedronMenu.signMap[sign] = this;
    }
  }

  public class DodecahedronMenu : MonoBehaviour
  {
    public float time = 1.0f;
    public EaseType easeType = EaseType.easeOutElastic;
    internal static Dictionary<int, TriFaceToSign> triangleMap = new Dictionary<int, TriFaceToSign>();
    internal static Dictionary<ZodiacSigns, TriFaceToSign> signMap = new Dictionary<ZodiacSigns, TriFaceToSign>();
    public GameObject explosionEffect;
    private AudioSource audioSource;
    public AudioClip explosionSound;
    public AudioClip changeSound;
    internal Transform sparksEffect;
    public MainMenu mainMenu;
    void Awake()
    {
      sparksEffect = transform.GetChild("Sparks");
      r = this.gameObject.transform.rotation;
      s = this.gameObject.transform.localScale;

      if (triangleMap.Count == 0)
      {
        new TriFaceToSign(0, 0.0f, 180.0f, 90.0f, ZodiacSigns.Cancer);
        new TriFaceToSign(3, 15.0f, 120.0f, -30.0f, ZodiacSigns.Pisces);
        new TriFaceToSign(6, 15.0f, -120.0f, -160.0f, ZodiacSigns.Aquarius);
        new TriFaceToSign(9, -45.0f, -120.0f, -45.0f, ZodiacSigns.Gemini);
        new TriFaceToSign(12, 60.0f, -180.0f, -90.0f, ZodiacSigns.Libra);
        new TriFaceToSign(15, 15.0f, -120.0f, 60.0f, ZodiacSigns.Sagittarius);
        new TriFaceToSign(18, -65.0f, 0.0f, 15.0f, ZodiacSigns.Scorpio);
        new TriFaceToSign(21, 45.0f, -50.0f, -145.0f, ZodiacSigns.Leo);
        new TriFaceToSign(24, -15.0f, 69.0f, 81.0f, ZodiacSigns.Taurus);
        new TriFaceToSign(27, 40.0f, 50.0f, 110.0f, ZodiacSigns.Virgo);
        new TriFaceToSign(30, -17.0f, 69.0f, -138.0f, ZodiacSigns.Aries);
        new TriFaceToSign(33, 0.0f, 0.0f, 128.0f, ZodiacSigns.Capricorn);
      }
      audioSource = this.gameObject.AddComponent<AudioSource>();
      Select(App.INSTANCE.selectedLevel);
    }
    /*void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name + "- " + hit.triangleIndex + "-" + transform.rotation);
        }
    }*/

    Quaternion r;
    Vector3 s;
    public Camera rayCastCamera;


    void OnClick()
    {
      var ray = rayCastCamera.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
      {
        if (!triangleMap.ContainsKey(hit.triangleIndex))
        {
          Debug.Log("Mouse " + Input.mousePosition + " " + hit.transform.name + "ray " + ray + " and misses " + hit.triangleIndex + "X" + transform.rotation);
          return;
        }

        audioSource.clip = changeSound;
        audioSource.Play();

        


        App.INSTANCE.selectedLevel = (int) triangleMap[hit.triangleIndex].sign;
        Select(App.INSTANCE.selectedLevel);

        //mainMenu.lblLevel.text = ((ZodiacSigns) App.INSTANCE.selectedLevel).ToString();
        //gameObject.RotateTo(triangleMap[hit.triangleIndex].coordinate, time, 0.0f, easeType);
      }
    }

    internal IEnumerator StartLevel()
    {
      audioSource.clip = explosionSound;
      audioSource.Play();

      mainMenu.FadeOut(2.0f);

      this.gameObject.transform.localScale = s;
      this.gameObject.transform.rotation = r;

      this.gameObject.ScaleBy(Vector3.one * 1.5f, 2.2f, 0.0f);
      this.gameObject.ShakeRotation(Vector3.back * 60f, 2.0f, 0.0f);
      yield return new WaitForSeconds(0.2f);
      sparksEffect.gameObject.SetActive(true);
      yield return new WaitForSeconds(0.2f);
      yield return new WaitForSeconds(2.2f);

      this.gameObject.ScaleTo(Vector3.zero, 0.5f, 0.0f);

      sparksEffect.gameObject.SetActive(false);

      GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.Euler(Vector3.back)) as GameObject;
      yield return new WaitForSeconds(2.5f);

      SceneManager.LoadScene("GamePlay");
    }

    internal void Select(int levelId)
    {
      ZodiacSigns zodiacSign = (ZodiacSigns) levelId;
      mainMenu.lblLevel.text = zodiacSign.ToString() + " - " + levelId.ToString();
      gameObject.RotateTo(signMap[zodiacSign].coordinate, time, 0.0f, easeType);
    }
  }
}
