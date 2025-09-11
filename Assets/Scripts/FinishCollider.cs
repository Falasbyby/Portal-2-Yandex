using UnityEngine;
using Xuwu.FourDimensionalPortals.Demo;

public class FinishCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
           UiGame.Instance.WinActiveContainer();
           
        }
      
    }
}
