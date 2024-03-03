using UnityEngine;
using System.Collections;

namespace Pamux.Interfaces
{
  public interface IPushNotifications
  {
    void Register();
    void Unregister();
  }
}
