using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;

    private TurretBlueprint turret;
    private GameObject currTurret;
    public GameObject buildEffect;

    public Button upgrade;
    public Button sell;

    public Text upgradeTxt;
    public Text sellTxt;

    private void Update()
    {
        upgradeTxt.text = "$" + turret.upgradedCost;
        sellTxt.text = "$" + (turret.cost / 2);
        if (BuildManager.cash < turret.upgradedCost)
            upgrade.interactable = false;
        else
            upgrade.interactable = true;

        if (Turret.Upgraded)
        {
            upgrade.interactable = false;
            upgradeTxt.text = null;
            sellTxt.text = "$" + (turret.upgradedCost / 2);
        }


    }

    public void SetTarget(GameObject _turret)
    {
        ui.SetActive(false);
        currTurret = _turret;
        turret = currTurret.GetComponent<Turret>().myBlueprint;
        transform.position = _turret.transform.position;
        ui.SetActive(true);
        
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void UpgradeTurret()
    {
        if (upgrade.interactable)
        {
            Instantiate(turret.upgradedPrefab, currTurret.transform.position, currTurret.transform.rotation);
            BuildManager.cash -= turret.upgradedCost;
            Destroy(currTurret);
            GameObject effect = Instantiate(buildEffect, currTurret.transform.position, Quaternion.identity);
            Destroy(effect, 5);
            Hide();
        } 

    }

    public void Sell()
    {
        if (!Turret.Upgraded)
        {
            Destroy(currTurret);
            BuildManager.cash += turret.cost / 2;
            Hide();
        }
        if (Turret.Upgraded)
        {
            Destroy(currTurret);
            BuildManager.cash += turret.upgradedCost / 2;
            Hide();
        }
    }

}

