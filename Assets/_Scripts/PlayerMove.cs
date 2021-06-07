using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PathCreator path; //preset path ref
    [SerializeField] EndOfPathInstruction endOfPath;
    [SerializeField] float speed = 3f; //camera movement speed
    [SerializeField] bool isMoving = true;

    [Header("Debug Options")]
    [SerializeField] float previewDistance = 0f; //Debugging var
    [SerializeField] bool enableDebug;

    private float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (path != null && isMoving)
        {
            //Camera follows path
            distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPath);
            transform.rotation = path.path.GetRotationAtDistance(distanceTravelled, endOfPath);
        }
    }

    private void OnValidate() //Unity's Debug method
    {
        if (enableDebug)
        {
            transform.position = path.path.GetPointAtDistance(previewDistance, endOfPath);
            transform.rotation = path.path.GetRotationAtDistance(previewDistance, endOfPath);
        }
    }
}
