
using UnityEngine;
 
 
//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

public class CameraController : MonoBehaviour
{
    [Header("--------axis------")]
    public GameObject yAxis;
    public GameObject xAxis;

    [Header("---------rotate speed------")]
    public float volicity;

    [Header("--------camera angel-----")]
    public float maxAngel = 30f;
    public float minAngel = -40f;
    
    private GameObject _player;
    private ActorController _controller;

    private GameObject camera;
    private Vector3 cameraVolicity;

    private float _eulerAngelX;
    private Vector3 _preFramePlayerEulerAngel;

    // Use this for initialization
    void Awake()
    {
        _controller = GetComponentInParent<ActorController>();
        _player = _controller.player;

        camera = Camera.main.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        TranslateCameraPos();


        camera.transform.position = Vector3.Slerp(camera.transform.position, xAxis.transform.position, 0.5f);
        camera.transform.eulerAngles = xAxis.transform.eulerAngles;
    }

    private void TranslateCameraPos()
    {
        _preFramePlayerEulerAngel = _player.transform.eulerAngles;

        yAxis.transform.Rotate(Vector3.up, _controller.InputSignal.CRightValue * volicity * Time.fixedDeltaTime);
        _eulerAngelX -= _controller.InputSignal.CUpValue * volicity * Time.fixedDeltaTime;
        _eulerAngelX = Mathf.Clamp(_eulerAngelX, minAngel, maxAngel);
        xAxis.transform.localEulerAngles = new Vector3(_eulerAngelX, 0, 0);

        _player.transform.eulerAngles = _preFramePlayerEulerAngel;

    }




}

