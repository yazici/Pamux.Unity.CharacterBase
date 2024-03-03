using UnityEngine;
using System.Collections;

namespace Pamux.Abstracts
{
  public abstract class AsyncAction
  {
    public void Begin() { }
    public void Cancel() { }

    protected virtual void DoBegin() { }
    protected virtual void DoCancel() { }

    protected virtual void OnProgress() { }
    protected virtual void OnSuccess() { }
    protected virtual void OnFailure() { }
    protected virtual void OnCancel() { }

    private int id;
  }

  public abstract class InAppPurchases : MonoBehaviour
  {

    protected abstract bool IsBillingSupported();
    protected abstract string GetStoreName();

    protected abstract bool CanMakePayments();
    protected abstract bool IsPurchased();
  }
}
