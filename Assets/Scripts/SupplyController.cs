using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SupplyType
{
    WaterAmmo, Hammer, SpeedBoost
}

public class SupplyController : MonoBehaviour
{
    public SupplyType type;

    public void Supply(GameObject player)
    {
        PopupController popup = player.GetComponentInChildren<PopupController>();
        switch (type)
        {
            case SupplyType.WaterAmmo:
                player.GetComponent<WaterController>().ammo += 10;
                popup.SetMessage("Water +10");
                break;
            case SupplyType.Hammer:
                player.GetComponent<PlayerController>().AddTool(PlayerController.Tools.Hammer);
                popup.SetMessage("Hammer");
                break;
            case SupplyType.SpeedBoost:
                player.GetComponent<PlayerMovement>().speed += 1;
                popup.SetMessage("Speed +1");
                break;
            default:
                throw new UnityException("WTF Unknown supply type");
        }
        popup.Show();
        gameObject.SetActive(false);
    }   

}
