using UnityEngine;

public class AimUi : Singleton<AimUi>
{
    [SerializeField] private GameObject bluetPortalIcon;
    [SerializeField] private GameObject orangePortalIcon;




    public void ActiveOrangePortal(bool active)
    {
        orangePortalIcon.SetActive(active);
    }
    public void ActivebluetPortal(bool active)
    {
        bluetPortalIcon.SetActive(active);
    }
}
