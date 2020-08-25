using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;

    public Text upgradeCost;
    public Text sellAmount;
    public Button upgradeButton;

    private SinglePlayAudio singlePlayAudio;

    private void Start()
    {
        singlePlayAudio = SinglePlayAudio.instance;
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
            sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeCost.text = "DONE";
            sellAmount.text = "$" + target.turretBlueprint.GetSellAmountOfUpgrade();
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        if (PlayerStats.Money >= target.turretBlueprint.upgradeCost)
            singlePlayAudio.PlayUpgradeTurretClip();
        else
            singlePlayAudio.PlayNotEnoughMoneyClip();
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        singlePlayAudio.PlaySellTurretClip();
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
