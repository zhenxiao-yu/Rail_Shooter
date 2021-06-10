using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation; //Path Creator Ref

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PathCreator path; //preset path ref
    [SerializeField] EndOfPathInstruction endOfPath;
    [SerializeField] float speed = 3f; //camera movement speed
    [SerializeField] bool isMoving = true;
    [SerializeField] ShootOutEntry[] shootOutEntries;

    [Header("Debug Options")]
    [SerializeField] float previewDistance = 0f; //Debugging var
    [SerializeField] bool enableDebug;

    private float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var entry in shootOutEntries) //initialize shoot out points
        {
            entry.shootOutPoint.Initialize(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null && isMoving)
        {
            //Camera follows path on start
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPath);
            transform.rotation = path.path.GetRotationAtDistance(distanceTravelled, endOfPath);

            //Stop at shoot out points
            for (int i = 0; i < shootOutEntries.Length; i++)
            {
                if ((path.path.GetPointAtDistance(shootOutEntries[i].distance) - transform.position).sqrMagnitude < 0.01f)
                {
                    if (shootOutEntries[i].shootOutPoint.AreaCleared)
                        return;

                    if (isMoving)
                        shootOutEntries[i].shootOutPoint.StartShootOut();
                }
            }
        }
    }

    private void OnValidate() //Unity's Debug method
    {
        if (enableDebug)
        {   
            //Change preview distance to move along the path without starting game
            transform.position = path.path.GetPointAtDistance(previewDistance, endOfPath);
            transform.rotation = path.path.GetRotationAtDistance(previewDistance, endOfPath);
        }
    }

    public void SetPlayerMovement(bool isEnable)
    {
        isMoving = isEnable; //check box to enable preview distance
    }
}

[System.Serializable]
public class ShootOutEntry
{
    public ShootOutPoint shootOutPoint;
    public float distance;
}