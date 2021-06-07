using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] PathCreator path;
    [SerializeField] EndOfPathInstruction endOfPath;
    [SerializeField] float speed = 3f; //camera movement speed



    private float distanceTravelled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = path.path.GetPointAtDistance(distanceTravelled, endOfPath);
        transform.rotation = path.path.GetRotationAtDistance(distanceTravelled, endOfPath);
    }
}
