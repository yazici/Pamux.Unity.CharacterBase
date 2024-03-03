using UnityEngine;
using System.Collections;

namespace Pamux
{
  public class PickableStyleInfo : StyleInfo
  {
      public Sprite sprite;
      public Color tint;
      public PickableTypes type;
  
      internal bool IsFunds
      {
          get { return FundCount != 0; }
      }
  
      internal static PickableTypes GetTypeFromCount(ref int count)
      {
          if (count >= 10)
          {
              count -= 10;
              return PickableTypes.FundsX10;
          }
          if (count >= 5)
          {
              count -= 5;
              return PickableTypes.FundsX5;
          }
          if (count >= 3)
          {
              count -= 3;
              return PickableTypes.FundsX3;
          }
          if (count >= 1)
          {
              count -= 1;
              return PickableTypes.Funds;
          }
          return PickableTypes.None;
      }
  
      internal int FundCount
      {
          get
          {
              switch (type)
              {
                  case PickableTypes.Funds:
                      return 1;
                  case PickableTypes.FundsX3:
                      return 3;
                  case PickableTypes.FundsX5:
                      return 5;
                  case PickableTypes.FundsX10:
                      return 10;
                  default:
                      return 0;
              }
          }
      }
  }
}
