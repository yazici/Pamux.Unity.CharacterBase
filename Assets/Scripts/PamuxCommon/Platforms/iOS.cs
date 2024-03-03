using UnityEngine;
using System.Collections;

namespace Pamux
{
  namespace Platforms
  {
    public class iOS : Abstracts.Platform
    {
      public class OpenShop : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new OpenShop(); }
      }

      public class CloseShop : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new CloseShop(); }
      }


      public class GetAvailableOffers : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new CloseShop(); }
      }


      public class GetOfferDetails : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new CloseShop(); }
      }

      public class MakePurchase : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new CloseShop(); }
      }


      public class ConsumePurchase : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new CloseShop(); }
      }


      public class RestorePurchases : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new CloseShop(); }
      }


      public class DownloadPurchase : Abstracts.AsyncAction
      {
        public static Abstracts.AsyncAction Create() { return new CloseShop(); }
      }

    }
  }
}
