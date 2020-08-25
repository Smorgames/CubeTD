using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Color startColor;

    private Renderer rend;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    public Vector3 positionOffset;

    private BuildManager buildManager;

    public GameObject[] previewTurret;

    private SinglePlayAudio singlePlayAudio;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        singlePlayAudio = SinglePlayAudio.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
            Debug.Log("Hover color");
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            buildManager.SelectedNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        singlePlayAudio.PlayBuildTurretClip();
        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void SellTurret()
    {
        if (isUpgraded)
        {
            PlayerStats.Money += turretBlueprint.GetSellAmountOfUpgrade();
        }
        else
        {
            PlayerStats.Money += turretBlueprint.GetSellAmount();
        }

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = false;
        Destroy(turret);
        turretBlueprint = null;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        //Get rid of the old turret
        Destroy(turret);

        //Build a new one
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse enter");
        if (turret == null)
            PreviewDisplay();
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
        PreviewHide();
    }

    private void PreviewDisplay()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;
        int turretCost = buildManager.GetTurretToBuild().cost;

        //if (turretCost == 100)
        //{
        //    previewTurret[0].transform.position = GetBuildPosition();
        //    previewTurret[0].SetActive(true);
        //}
        //if (turretCost == 250)
        //{
        //    previewTurret[1].transform.position = GetBuildPosition();
        //    previewTurret[1].SetActive(true);
        //}
        //if (turretCost == 350)
        //{
        //    previewTurret[2].transform.position = GetBuildPosition();
        //    previewTurret[2].SetActive(true);
        //}

        int[] massive = new int[] { 100, 250, 350 };

        for (int i = 0; i < massive.Length; i++)
        {
            if (turretCost == massive[i])
            {
                previewTurret[i].transform.position = GetBuildPosition();
                previewTurret[i].SetActive(true);
            }
        }
    }

    private void PreviewHide()
    {
        for (int i = 0; i < previewTurret.Length; i++)
        {
            previewTurret[i].SetActive(false);
        }
    }
}
