using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Pamux.Lib.Unity.Commons.Editor
{
    public class PamuxDontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}