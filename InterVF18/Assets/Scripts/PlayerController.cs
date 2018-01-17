using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSpeed = 1.0f;
    private Rigidbody playerRB;

    bool isBuilding;
    PlaceableObject buildingObject;

    #region ObjectsOfInterest
    [SerializeField] GameObject OOIPlacementOuterIndicator;
    [SerializeField] GameObject OOIPlacementInnerIndicator;
    [SerializeField] GameObject OOIPlacementTopIndicator;
    [SerializeField] Light indicatorLight;
    Color lightRed = new Color(1,0,0,0.5f);
    Color lightGreen = new Color(1, 0, 0, 0.5f);
    float indicatorRotationAngle = 1f;
    [SerializeField] Transform OOISpawn;
    [SerializeField] GameObject tripWire;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject guard;
    GameObject activeOOI;
    int camCount = 0;
    int tripCount = 0;
    int guardCount = 0;
    float guardScaleMultiplier = 1.5f;
    #endregion

    void Start ()
    {
        playerRB = GetComponent<Rigidbody>();
        isBuilding = false;
        activeOOI = new GameObject();
        InitIndicatorsToOff();
    }
	
    void Update()
    {
        CheckForObjectOfInterestPlacement();
        RotateIndicators();
    }

    private void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetButton("Up"))
        {
            move += new Vector3(0.0f, 0.0f, 1.0f);
        }

        if (Input.GetButton("Down"))
        {
            move += new Vector3(0.0f, 0.0f, -1.0f);
        }

        if (Input.GetButton("Left"))
        {
            move += new Vector3(-1.0f, 0.0f, 0.0f);
        }

        if (Input.GetButton("Right"))
        {
            move += new Vector3(1.0f, 0.0f, 0.0f);
        }

        Vector3 movement = move.normalized * moveSpeed * Time.deltaTime;
        playerRB.MovePosition(playerRB.position + movement);

        if (move.magnitude > 0.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move.normalized), turnSpeed);
        }
    }

    void CheckForObjectOfInterestPlacement()
    {
        if(Input.GetButtonDown("BuildGuard"))
        {
            InitIndicatorsToOn();
            activeOOI = Instantiate(guard, OOISpawn.position, OOISpawn.rotation, OOISpawn);
            activeOOI.transform.localScale /= guardScaleMultiplier;
            activeOOI.name = "Guard" + ++guardCount;
            foreach (Behaviour behaviour in activeOOI.GetComponents<Behaviour>())
            {
                behaviour.enabled = false;
            }
        }
        else if (Input.GetButtonDown("BuildCamera"))
        {
            InitIndicatorsToOn();
            activeOOI = Instantiate(cam, OOISpawn.position, OOISpawn.rotation, OOISpawn);
            activeOOI.name = "Cam" + ++camCount;
        }
        else if (Input.GetButtonDown("BuildTripWire"))
        {
            InitIndicatorsToOn();
            activeOOI = Instantiate(tripWire, OOISpawn.position, OOISpawn.rotation, OOISpawn);
            activeOOI.GetComponent<TripWire>().enabled = false;
            activeOOI.name = "trip" + ++tripCount;
        }
            if (Input.GetButton("BuildGuard") || Input.GetButton("BuildCamera") || Input.GetButton("BuildTripWire"))
            {
                if (activeOOI.GetComponent<PlaceableObject>() != null)
                {
                    if (!activeOOI.GetComponent<PlaceableObject>().PlaceObject(false))
                    {
                        MakeIndicatorsRed();

                    }
                    else
                    {
                        MakeIndicatorsGreen();
                    }
                }
            }
        if (Input.GetButtonUp("BuildGuard") || Input.GetButtonUp("BuildCamera") || Input.GetButtonUp("BuildTripWire"))
        {
            if (activeOOI.GetComponent<PlaceableObject>() != null)
            {
                InitIndicatorsToOff();
                if (activeOOI.GetComponent<PlaceableObject>().PlaceObject(false))
                {
                    activeOOI.GetComponent<PlaceableObject>().PlaceObject(true);
                }

                if (activeOOI.GetComponent<PlaceableObject>().objectPlaced == false)
                {
                    if(activeOOI.GetComponent<TripWire>() != null)
                    {
                        activeOOI.GetComponent<TripWire>().enabled = true;
                    }
                    if(activeOOI.tag == "Guard")
                    {
                        activeOOI.transform.localScale *= guardScaleMultiplier;
                        foreach (Behaviour behaviour in activeOOI.GetComponents<Behaviour>())
                        {
                            behaviour.enabled = true;
                        }
                    }
                    Destroy(activeOOI);
                    activeOOI = null;
                }
            }
        }
    }

    void RotateIndicators()
    {
        OOIPlacementInnerIndicator.transform.Rotate(transform.up, indicatorRotationAngle);
        OOIPlacementOuterIndicator.transform.Rotate(transform.up, -indicatorRotationAngle);
        OOIPlacementTopIndicator.transform.Rotate(transform.up, -indicatorRotationAngle);
    }

    void InitIndicatorsToOff()
    {
        OOIPlacementInnerIndicator.GetComponent<Renderer>().enabled = false;
        OOIPlacementOuterIndicator.GetComponent<Renderer>().enabled = false;
        OOIPlacementTopIndicator.GetComponent<Renderer>().enabled = false;
        indicatorLight.enabled = false;
    }

    void InitIndicatorsToOn()
    {
        OOIPlacementInnerIndicator.GetComponent<Renderer>().enabled = true;
        OOIPlacementOuterIndicator.GetComponent<Renderer>().enabled = true;
        OOIPlacementTopIndicator.GetComponent<Renderer>().enabled = true;
        indicatorLight.enabled = true;
    }

    void MakeIndicatorsGreen()
    {
        OOIPlacementInnerIndicator.GetComponent<Renderer>().material.color = Color.green;
        OOIPlacementOuterIndicator.GetComponent<Renderer>().material.color = Color.green;
        OOIPlacementTopIndicator.GetComponent<Renderer>().material.color = Color.green;
        indicatorLight.color = lightGreen;
    }

    void MakeIndicatorsRed()
    {
        OOIPlacementInnerIndicator.GetComponent<Renderer>().material.color = Color.red;
        OOIPlacementOuterIndicator.GetComponent<Renderer>().material.color = Color.red;
        OOIPlacementTopIndicator.GetComponent<Renderer>().material.color = Color.red;
        indicatorLight.color = lightRed;
    }
}
