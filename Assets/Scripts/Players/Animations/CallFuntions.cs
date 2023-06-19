using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFuntions : MonoBehaviour
{
    [SerializeField] ProjectileEchoAttack pEchoAttack;
    [SerializeField] DetectorEchoAttack dEchoAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPlayerFuntionAttack()
    {
        if (pEchoAttack) pEchoAttack.SpawnProjectile();
        if (dEchoAttack) dEchoAttack.CastAttack();
    }
}
