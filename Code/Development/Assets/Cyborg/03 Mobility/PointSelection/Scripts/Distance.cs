
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Distance : MonoBehaviour
{
    [Tooltip("Duration in seconds that the user needs to keep looking at the target to select it via dwell activation.")]
    [Range(0, 10)]
    [SerializeField]
    public float dwellTimeInSec = 0.8f;
    private double _rotation = 0.0f;
    public GameObject destination;
    public Transform player;

    float _time = 0.0f;
    Vector3 _previousPosition;
    private float _targetDistance = 0.0f;
    private Vector3 _targetposition;
    private Vector3 _previousSelectedPoint;
    private bool selected = false;
    public Text textbox;

    // Start is called before the first frame update
    void Start()
    {
        textbox.text = "Hello";
        //_rotation = -30;
        //_targetDistance = 10;
        rotation(new UnityEngine.Vector3(0, 0, 3), new UnityEngine.Vector3(4, 0, 3));

        //Debug.Log("http://192.168.43.39:5000/move?rotation=" + _rotation + "&distance=" + _targetDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
        {
            Vector3 currentPosition = CoreServices.InputSystem.EyeGazeProvider.HitPosition;
            if (Vector3.Distance(_previousPosition, currentPosition) < 0.3f)
            {
                _previousPosition = currentPosition;
                _time += Time.deltaTime;
            }
            else
            {
                _previousPosition = currentPosition;
                _time = 0;
            }
        }
        else
        {
            textbox.text = "Moving";
        }
        if (_time > dwellTimeInSec)
        {
            selected = true;
            Select();
            Instantiate(destination);
            _targetposition.y = player.position.y;

            destination.transform.position = _targetposition;
            _rotation = -30;
            StartCoroutine(GetRequest("http://192.168.43.39:5000/move?rotation="+_rotation+"&distance="+_targetDistance));
            _time = 0;
        }

        if (Vector3.Distance(destination.transform.position, Camera.main.transform.position)< 0.2f & selected)
        {
            _previousSelectedPoint = _targetposition;
            Destroy(destination);
            selected = false;
        }

        //checking dwelling time 
        // locking the dwell point
        //creating some visual message about the selected point 

    }


    public void Select()
    {
        //var eyeGazeProvider = CoreServices.InputSystem?.EyeGazeProvider;
        _targetDistance = 0.0f;
        RaycastHit hit;
       // Ray lookRay = new Ray(eyeGazeProvider.GazeOrigin, eyeGazeProvider.GazeDirection.normalized);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Debug.Log("Distance" + hit.distance);
            textbox.text = " Target selected and is at a distance of: " + _targetDistance;
            _targetDistance = hit.distance;
            _targetposition = hit.point;



        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

    }


    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    public void rotation(Vector3 x, Vector3 y)
    {
        float a = Vector3.Distance(player.transform.position, x);
        float b = Vector3.Distance(player.transform.position, y);
        float c = Vector3.Distance(x, y);
        //cosine rule

        

        double mycalcInRadians = Math.Acos((a*a + b*b - c*c) / 2*a*b );
        _rotation = mycalcInRadians * 180 / Math.PI;

        Debug.Log("Player position" + player.transform.position);
        Debug.Log("length of a: " + a);
        Debug.Log("length of b: " + b);
        Debug.Log("Length of c : " + c);
        Debug.Log("Exp value"+ (a*a + b*b - c*c) / 2*a*b);
        Debug.Log("Angle in radians" + mycalcInRadians);
        Debug.Log("Rotation"+ _rotation);

    }
}
