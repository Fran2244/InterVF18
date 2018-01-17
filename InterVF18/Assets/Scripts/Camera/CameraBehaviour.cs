using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


class CameraBehaviour : MonoBehaviour
{
    static private CameraBehaviour _camera;

    static public CameraBehaviour cameraSingleton
    {
        get
        {
            if (_camera == null)
            {
                _camera = FindObjectOfType<CameraBehaviour>();
            }
            return _camera;
        }
    }
    
    Transform player;
    GameObject relativeWorldPos;

    enum Modes {FOCUSED, SCREENEDGE};
    Modes currentMode;

    #region Constants
    const float defaultZoom = 20f;
    const float zoomSpeed = 0.25f;
    const float minZoom = 10f;
    const float maxZoom = 50f;
    const float camScreenBorderTranslateSpeed = 0.1f;
    const float camScreenBorderFollowSpeed = 5f;
    const float screenEdgeDetection = 0.95f;
    const float camFollowPlayerSpeed = 50f;
    const float zoomStrafeMultiplier = 0.1f;
    const float maxTimer = 1f;
    #endregion

    #region Variables
    float zoom;
    float countDownToReposition;
    Vector2 screenXY = new Vector2();
    Vector2 screenEdgeTranslate = new Vector2();
    Vector3 offsetTopDownMode = new Vector3();
    bool shouldStartCoundown;
    bool screenEdgeDetected = false;
    float timer = 0f;
    bool initComplete = false;
    #endregion

    void Awake()
    {
        Init();
    }

    void _Init()
    {
        try
        {
            zoom = defaultZoom;
            player = GameObject.FindGameObjectWithTag("Player").transform ?? FindPlayerByLayer();
            offsetTopDownMode = (transform.position - player.transform.position).normalized;
            offsetTopDownMode *= zoom;
            transform.LookAt(player.position);
            currentMode = Modes.FOCUSED;
            screenXY = new Vector2(Screen.width, Screen.height);
            relativeWorldPos = new GameObject();
            relativeWorldPos.transform.rotation = player.rotation;
            shouldStartCoundown = false;
            Cursor.lockState = CursorLockMode.Confined;
            initComplete = true;
        }
        catch(System.Exception e)
        {
            Debug.LogError("Camera init failed: " + e.Message);
        }
    }

    public static void Init()
    {
        cameraSingleton._Init(); 
    }
    void Update()
    {
        if (initComplete)
        {
            AdjustZoom();
            Countdown();
            CheckModes();
            AdjustTopDownView();
        }
    }

    void CheckModes()
    {
        screenEdgeDetected = false;
        screenEdgeTranslate = Vector2.zero;
        if (Input.mousePosition.x > screenXY.x * screenEdgeDetection)
        {
            screenEdgeTranslate.x += camScreenBorderTranslateSpeed;
            currentMode = Modes.SCREENEDGE;
            screenEdgeDetected = true;
        }
        else if (Input.mousePosition.x < screenXY.x * (1 - screenEdgeDetection))
        {
            screenEdgeTranslate.x -= camScreenBorderTranslateSpeed;
            currentMode = Modes.SCREENEDGE;
            screenEdgeDetected = true;
        }
        if (Input.mousePosition.y < screenXY.y * (1 - screenEdgeDetection))
        {
            screenEdgeTranslate.y -= camScreenBorderTranslateSpeed;
            currentMode = Modes.SCREENEDGE;
            screenEdgeDetected = true;
        }
        else if (Input.mousePosition.y > screenXY.y * screenEdgeDetection)
        {
            screenEdgeTranslate.y += camScreenBorderTranslateSpeed;
            currentMode = Modes.SCREENEDGE;
            screenEdgeDetected = true;
        }
        shouldStartCoundown = screenEdgeDetected == false && currentMode == Modes.SCREENEDGE;
        if (timer <= 0f && screenEdgeDetected == false)
        {
            currentMode = Modes.FOCUSED;
        }
    }
    
    void AdjustTopDownView()
    {
        if(currentMode == Modes.FOCUSED)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + offsetTopDownMode, Time.deltaTime * camFollowPlayerSpeed * (1/Vector3.Distance(transform.position, player.position + offsetTopDownMode)));
            relativeWorldPos.transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z + Mathf.Abs(player.position.z - transform.position.z));
        }
        else
        {
            if (screenEdgeDetected)
            {
                relativeWorldPos.transform.Translate(screenEdgeTranslate.x * zoom * zoomStrafeMultiplier, 0f, screenEdgeTranslate.y * zoom * zoomStrafeMultiplier);
            }
            transform.position = Vector3.Lerp(transform.position, relativeWorldPos.transform.position + offsetTopDownMode, Time.deltaTime * camScreenBorderFollowSpeed);
        }
    }

    void AdjustZoom()
    {
        if(Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomSpeed * Vector3.Distance(transform.position, player.position);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomSpeed * Vector3.Distance(transform.position, player.position);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }
        offsetTopDownMode = offsetTopDownMode.normalized * zoom;
    }

    Transform FindPlayerByLayer()
    {
        List<GameObject> allObjects = FindObjectsOfType<GameObject>().ToList();
        GameObject player = new GameObject();
        foreach(GameObject obj in allObjects)
        {
            if(obj.layer == LayerMask.NameToLayer("Player"))
            {
                player = obj;
                break;
            }
        }
        return player.transform;
    }

    void Countdown()
    {
        if(shouldStartCoundown)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = maxTimer;
        }
    }
}