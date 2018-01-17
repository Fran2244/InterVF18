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
    [SerializeField] ParticleSystem OOIPlacementIndicator;
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
        OOIPlacementIndicator.Stop();
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
            OOIPlacementIndicator.Play();
            activeOOI = Instantiate(guard, OOISpawn.position, OOISpawn.rotation, OOISpawn);
        }
        else if (Input.GetButtonDown("BuildCamera"))
        {
            OOIPlacementIndicator.Play();
            activeOOI = Instantiate(cam, OOISpawn.position, OOISpawn.rotation, OOISpawn);
        }
        else if (Input.GetButtonDown("BuildTripWire"))
        {
            OOIPlacementIndicator.Play();
            activeOOI = Instantiate(tripWire, OOISpawn.position, OOISpawn.rotation, OOISpawn);
        }

        if(Input.GetButton("BuildGuard") || Input.GetButton("BuildCamera") || Input.GetButton("BuildTripWire"))
        {
            if(!activeOOI.GetComponent<PlaceableObject>().PlaceObject())
            {
                OOIPlacementIndicator.startColor = Color.red;
            }
            else
            {
                OOIPlacementIndicator.startColor = Color.green;
            }
        }
        else
        {
            OOIPlacementIndicator.Stop();
            if(!activeOOI.GetComponent<PlaceableObject>().objectPlaced)
            {
                Destroy(activeOOI);
                activeOOI = new GameObject();
            }
        }
    }
}
