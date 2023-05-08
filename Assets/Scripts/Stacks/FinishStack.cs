using System.Collections;
using UnityEngine;

public class FinishStack : Stack
{
    [SerializeField] private GameObject _perfectTapIndicator;

    #region UNITY EVENTS

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            StartCoroutine(ProcessOnWin());
    }

    #endregion

    #region PRIVATE METHODS

    private IEnumerator ProcessOnWin()
    {
        GameManager.Instance.InvokeOnWin();

        yield return Helpers.BetterWaitForSeconds(3f);
        _perfectTapIndicator.SetActive(true);
    }

    #endregion
}
