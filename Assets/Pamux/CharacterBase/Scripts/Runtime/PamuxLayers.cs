using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pamux.Lib.Unity.Commons
{
    public static class PamuxLayer
    {
        public static class Names
        {   
            public static readonly string Interactables = "Interactables";
        }

        public static class Ids
        {   
            public static readonly int Interactables = LayerMask.NameToLayer(Names.Interactables);
        }


        public static class Masks
        {   
            public static readonly int Interactables = LayerMask.GetMask(new string[] { Names.Interactables });
        }
    }
}