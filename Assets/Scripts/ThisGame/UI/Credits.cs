using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Pamux.Zodiac.UI
{
    public sealed class Credits : Abstracts.UI
    {

        public static Credits INSTANCE = null;

        void Awake()
        {
            INSTANCE = this;
        }
    }
}
