using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pamux
{
  public enum Difficulties
  {
    Easy,
    Hard,
    Impossible
  }

  public enum ZodiacSigns
  {
    Aries = 1,
    Taurus = 2,
    Gemini = 3,
    Cancer = 4,
    Leo = 5,
    Virgo = 6,
    Libra = 7,
    Scorpio = 8,
    Sagittarius = 9,
    Capricorn = 10,
    Aquarius = 11,
    Pisces = 12
  }

  public enum PathVariants
  {
    None,
    Horizontal,
    Vertical,
    Center,
    Reverse,
    RCenter,
    RVertical,
    RHorizontal,
    Max
  }
}
