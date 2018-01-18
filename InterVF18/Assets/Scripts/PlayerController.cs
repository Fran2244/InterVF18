using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float turnSpeed = 1.0f;
    [SerializeField] private Transform eye;
    [HideInInspector] public Animator animator;
    private Rigidbody playerRB;
    [HideInInspector] public bool isChasing = false;
    private RaycastHit hit;
    [HideInInspector] public GameObject enemyGameObject = null;

    bool isBuilding;
    PlaceableObject buildingObject;

    #region ObjectsOfInterest
    [SerializeField] GameObject OOIPlacementOuterIndicator;
    [SerializeField] GameObject OOIPlacementInnerIndicator;
    [SerializeField] GameObject OOIPlacementTopIndicator;
    [SerializeField] Light indicatorLight;
    Color lightRed = new Color(1,0,0,0.5f);
    Color lightGreen = new Color(1, 0, 0, 0.5f);
    float indicatorRotationAngle = 2f;
    float distGuardFromPlayer = 1f;
    [SerializeField] Transform OOISpawn;
    [SerializeField] GameObject tripWire;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject guard;
    [SerializeField] GameObject guardDummy;
    GameObject activeOOI;
    int camCount = 0;
    int tripCount = 0;
    int guardCount = 0;
    float scaleMultiplier = 3f;
    #endregion

    void Start ()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isBuilding = false;
        activeOOI = new GameObject();
        InitIndicatorsToOff();
        isChasing = false;
    }
	
    void Update()
    {
        CheckForObjectOfInterestPlacement();
        RotateIndicators();

        Debug.DrawRay(eye.position, eye.forward.normalized * 2.0f, Color.green);

        if (!isChasing)
        {
            if (Physics.SphereCast(eye.position,
                              0.3f,
                              eye.forward,
                              out hit,
                              2.0f)
           && hit.collider.CompareTag("Enemy"))
            {
                CharacterVisibility enemy = hit.collider.gameObject.GetComponent<CharacterVisibility>();
                if (enemy && !enemy.isChased)
                {
                    enemy.isChased = true;

                    StateController targetState = enemy.gameObject.GetComponent<StateController>();
                    targetState.currentState = null;
                    targetState.navMeshAgent.enabled = false;
                    enemy.gameObject.GetComponent<Collider>().enabled = false;
                    enemyGameObject = enemy.gameObject;
                    StartCoroutine(FetchEnemy());
                }
            }
        }
        else
        {
            if (enemyGameObject != null)
            {
                enemyGameObject.transform.localPosition = new Vector3(0.15f, 1.3f, 3.75f);
            }
        }
    }

    private IEnumerator FetchEnemy()
    {
        animator.SetBool("RightHandUp", true);
        animator.SetBool("LeftHandUp", true);

        yield return new WaitForSeconds(1.0f);

        if (enemyGameObject != null)
        {
            //enemyGameObject.transform.localPosition = new Vector3(0.15f, 1.3f, 3.75f);
            enemyGameObject.transform.SetParent(gameObject.transform);
            isChasing = true;
        }
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
        //playerRB.MovePosition(playerRB.position + movement);
        transform.position += movement;

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
            activeOOI = Instantiate(guardDummy, OOISpawn.position, OOISpawn.rotation, transform);
            activeOOI.name = "Guard" + ++guardCount;
        }
        else if (Input.GetButtonDown("BuildCamera"))
        {
            InitIndicatorsToOn();
            activeOOI = Instantiate(cam, OOISpawn.position, OOISpawn.rotation, OOISpawn);
            activeOOI.transform.localScale *= scaleMultiplier;
            activeOOI.name = "Cam" + ++camCount;
        }
        else if (Input.GetButtonDown("BuildTripWire"))
        {
            InitIndicatorsToOn();
            activeOOI = Instantiate(tripWire, OOISpawn.position, OOISpawn.rotation, OOISpawn);
            activeOOI.transform.localScale *= scaleMultiplier;
            activeOOI.GetComponent<TripWire>().enabled = false;
            activeOOI.name = "trip" + ++tripCount;
        }
        if (Input.GetButton("BuildGuard") || Input.GetButton("BuildCamera") || Input.GetButton("BuildTripWire"))
        {
            if (activeOOI.GetComponent<PlaceableObject>() != null)
            {
                if (activeOOI.GetComponent<PlaceableObject>().PlaceObject(false) == false)
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

                if (activeOOI.GetComponent<PlaceableObject>().objectPlaced == true)
                {
                    if(activeOOI.GetComponent<TripWire>() != null)
                    {
                        activeOOI.GetComponent<TripWire>().enabled = true;
                    }
                    if(activeOOI.tag == "Guard")
                    {
                        GameObject guardInstance = Instantiate(guard, transform.position + transform.forward * distGuardFromPlayer, Quaternion.LookRotation(guardDummy.transform.position - transform.position));
                        Destroy(activeOOI);
                    }
                    activeOOI = null;
                }
                else
                {
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
        indicatorLight.color = Color.green;
    }

    void MakeIndicatorsRed()
    {
        OOIPlacementInnerIndicator.GetComponent<Renderer>().material.color = Color.red;
        OOIPlacementOuterIndicator.GetComponent<Renderer>().material.color = Color.red;
        OOIPlacementTopIndicator.GetComponent<Renderer>().material.color = Color.red;
        indicatorLight.color = Color.red;
    }
}
