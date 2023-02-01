using JetBrains.Annotations;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Door : MonoBehaviour
{
    public bool isOpen = false;
    private bool isRotatinDoor = true;
    private float speed = 1f;

    [SerializeField] private float rotationAmount = 90f;
    [SerializeField] private float forwardDirection = 0f;


    private Vector3 startRotarion;
    private Vector3 forward;

    private Coroutine animationCoroutine;


    private void Awake()
    {
        startRotarion = transform.rotation.eulerAngles;

        forward = transform.right;
    }

    public void Open(Vector3 _userPosition)
    {
        if (!isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
        
            if (isRotatinDoor)
            {
                float _dot = Vector3.Dot(forward, (_userPosition - transform.position).normalized);
                Debug.Log($"Dot: {_dot.ToString("N3")}");
                animationCoroutine = StartCoroutine(DoRotationOpen(_dot));

            }
        }
    }

        private IEnumerator DoRotationOpen(float _forwardAmount)
        {
            Quaternion _startRotation = transform.rotation;
            Quaternion _endRotation;

            if(_forwardAmount >= forwardDirection)
            {
                _endRotation = Quaternion.Euler(new Vector3(0, startRotarion.y + rotationAmount, 0));
            }
            else
            {
                _endRotation = Quaternion.Euler(new Vector3(0,startRotarion.y - rotationAmount, 0));
            }

            isOpen = true;
            float _time = 0;
            while(_time<1)
            {
                transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, _time);
                yield return null;
                _time += Time.deltaTime * speed;
            }

        }

    public void Close()
    {
        if (isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }
            if (isRotatinDoor)
            {
                animationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }
            private IEnumerator DoRotationClose()
            {
                Quaternion _startRotation = transform.rotation;
                Quaternion _endRotation = Quaternion.Euler(startRotarion);

                isOpen = false;

                float _time = 0;
                while(_time<1)
                {
                    transform.rotation = Quaternion.Slerp(_startRotation, _endRotation, _time);
                    yield return null;
                    _time += Time.deltaTime * speed;
                }
            }
}

       

 

