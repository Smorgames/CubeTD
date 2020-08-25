using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standartTurret;
    public TurretBlueprint missleLauncher;
    public TurretBlueprint laserBeamer;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandartTurret()
    {
        buildManager.SelectTurretToBuild(standartTurret);
    }

    public void SelectMissleLauncher()
    {
        buildManager.SelectTurretToBuild(missleLauncher);
    }

    public void SelectLaserBeamer()
    {
        buildManager.SelectTurretToBuild(laserBeamer);
    }
}
