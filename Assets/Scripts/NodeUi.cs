using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUi : MonoBehaviour
{
    public GameObject ui;
    private Node target;
    public TextMeshProUGUI upgradeCost;
    public TextMeshProUGUI sellAmouunt;
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition();
        ui.SetActive(true);
        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;

        }
        else
        {
            upgradeCost.text = "Done";
            upgradeButton.interactable = false;

        }
        sellAmouunt.text="$" + target.turretBlueprint.GetSellAmount();
        ui.SetActive(true); 

    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
