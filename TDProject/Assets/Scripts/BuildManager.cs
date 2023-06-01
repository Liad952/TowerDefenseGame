using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public Button turret1;
    public Button turret2;
    public Button turret3;

    public TurretBlueprint standardTurret;
    public TurretBlueprint AoeTurret;
    public TurretBlueprint LaserTurret;

    public static int cash = 30;
    public Text currCash;

    public Builder builder;

    // Start is called before the first frame update
    void Start()
    {
        turret1.interactable = false;
        turret2.interactable = false;
        turret3.interactable = false;
        InvokeRepeating("UpdateMenu", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        currCash.text = "$" + (cash.ToString());
    }

    void UpdateMenu()
    {
        if(cash >= 10)
            turret1.interactable=true;
        else
            turret1.interactable=false;
        if (cash >= 20)
            turret2.interactable=true;
        else
            turret2.interactable = false;
        if (cash >= 30)
            turret3.interactable=true;
        else
            turret3.interactable = false;
    }
    
    public void UpdateTurret(int i)
    {
        if (i == 0)
            builder.GetTurretToBuild(standardTurret);
        if (i == 1)
            builder.GetTurretToBuild(AoeTurret);
        if (i == 2)
            builder.GetTurretToBuild(LaserTurret);

    }
}
