using UnityEngine;

public class Killer : MonoBehaviour
{
   private bool _isTriggered;

   #region UNITY EVENT

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player") && !_isTriggered)
      {
         _isTriggered = true;
         GameManager.Instance.InvokeOnGameOver();
      }
   }

   #endregion
}
