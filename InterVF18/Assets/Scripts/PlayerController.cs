using UnityEngine;

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
    float indicatorRotationAngle = 1f;
    [SerializeField] Transform OOISpawn;
    [SerializeField] GameObject tripWire;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject guard;
    GameObject activeOOI;
    #endregion

    void Start ()
    {
        playerRB = GetComponent<Rigidbody>();
        isBuilding = false;
        activeOOI = new GameObject();
	}
	
    void Update()
    {
        CheckForObjectOfInterestPlacement();
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
            OOIPlacementInnerIndicator.GetComponent<Renderer>().enabled = true;
            OOIPlacementOuterIndicator.GetComponent<Renderer>().enabled = true;
            OOIPlacementTopIndicator.GetComponent<Renderer>().enabled = true;
            activeOOI = Instantiate(guard, OOISpawn.position, OOISpawn.rotation, OOISpawn);
        }
        else if (Input.GetButtonDown("BuildCamera"))
        {
            activeOOI = Instantiate(cam, OOISpawn.position, OOISpawn.rotation, OOISpawn);
        }
        else if (Input.GetButtonDown("BuildTripWire"))
        {
            activeOOI = Instantiate(tripWire, OOISpawn.position, OOISpawn.rotation, OOISpawn);
        }

        if(Input.GetButton("BuildGuard") || Input.GetButton("BuildCamera") || Input.GetButton("BuildTripWire"))
        {
            if(!activeOOI.GetComponent<PlaceableObject>().PlaceObject())
            {
                OOIPlacementInnerIndicator.GetComponent<Renderer>().material.color = Color.red;
                OOIPlacementOuterIndicator.GetComponent<Renderer>().material.color = Color.red;
                OOIPlacementTopIndicator.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                OOIPlacementInnerIndicator.GetComponent<Renderer>().material.color = Color.green;
                OOIPlacementOuterIndicator.GetComponent<Renderer>().material.color = Color.green;
                OOIPlacementTopIndicator.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            OOIPlacementInnerIndicator.GetComponent<Renderer>().enabled = false;
            OOIPlacementOuterIndicator.GetComponent<Renderer>().enabled = false;
            OOIPlacementTopIndicator.GetComponent<Renderer>().enabled = false;
            if (!activeOOI.GetComponent<PlaceableObject>().objectPlaced)
            {
                Destroy(activeOOI);
                activeOOI = new GameObject();
            }
        }
    }

    void RotateIndicators()
    {
        OOIPlacementInnerIndicator.transform.Rotate(transform.up, indicatorRotationAngle);
        OOIPlacementOuterIndicator.transform.Rotate(transform.up, -indicatorRotationAngle);
        OOIPlacementTopIndicator.transform.Rotate(transform.up, -indicatorRotationAngle);
    }
}
