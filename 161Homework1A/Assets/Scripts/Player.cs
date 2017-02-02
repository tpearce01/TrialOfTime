using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class Player : Unit
{
    public static Player i;
    public GameObject target = null;
    public GameObject targetHUD;
    private TargettingSystem ts;

    public override void PlayerInput()
    {
        //not implemented
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ts.ClearTarget();
        }
    }

    public override void Initialize()
    {
        i = this;
        ts = gameObject.GetComponent<TargettingSystem>();
    }
}
