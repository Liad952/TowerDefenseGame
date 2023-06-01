using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Builder : MonoBehaviour
{

    private int cost;
    private GameObject turretToBuild;
    public GameObject buildEffect;

    public TurretUI turretUI;
    private Vector3 mousePos;

    public LayerMask buildable;
    public LayerMask turret;

   public void GetTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild= turret.prefab;
        turretUI.Hide();
        this.cost = turret.cost;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 300, turret))
            {
                turretUI.SetTarget(hit.collider.gameObject);
            }
            else
            if (Physics.Raycast(ray,out hit,300,buildable))
            {
                mousePos = hit.point;
                Instantiate(turretToBuild, mousePos, turretToBuild.transform.rotation);
                GameObject effect = Instantiate(buildEffect, mousePos, Quaternion.identity);
                BuildManager.cash -= cost;
                Destroy(effect, 5);
            }
            else
            {
                turretUI.Hide();
            }
            

        }
        if (BuildManager.cash < cost)
        {
            turretToBuild = null;
        }
        if(Input.GetMouseButtonDown(1))
        {
            turretToBuild = null;
            turretUI.Hide();
        }

    }
   

}
