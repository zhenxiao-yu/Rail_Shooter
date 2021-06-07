using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOutPoint : MonoBehaviour
{
    public bool AreaCleared {get; private set;}
    private PlayerMove playerMove;

    public void Initialize(PlayerMove value)
    {
        playerMove = value;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerMove.SetPlayerMovement (false);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            playerMove.SetPlayerMovement (true);
        }      
    }
}
