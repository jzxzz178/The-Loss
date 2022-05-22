using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft = false;
    public Transform activePlayer;
    public int lastX;

    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float upperLimit;

    public void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        activePlayer = null;
        FindPlayer(isLeft);
    }

    private void FindPlayer(bool playerIsLeft)
    {
        activePlayer = TotalNewControl.TakeActivePlayer()?.transform;
        if (activePlayer == null) return;
        lastX = Mathf.RoundToInt(activePlayer.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(activePlayer.position.x - offset.x, activePlayer.position.y - offset.y,
                transform.position.z);
        }
        else
        {
            transform.position = new Vector3(activePlayer.position.x + offset.x, activePlayer.position.y + offset.y,
                transform.position.z);
        }
    }

    public void Update()
    {
        activePlayer = TotalNewControl.TakeActivePlayer().transform;
        if (activePlayer != null && activePlayer)
        {
            var currentX = Mathf.RoundToInt(activePlayer.position.x);
            if (currentX > lastX) isLeft = false;
            else if (currentX < lastX) isLeft = true;
            lastX = Mathf.RoundToInt(activePlayer.position.x);

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(activePlayer.position.x - offset.x, activePlayer.position.y + offset.y,
                    transform.position.z);
            }
            else
            {
                target = new Vector3(activePlayer.position.x + offset.x, activePlayer.position.y + offset.y,
                    transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit), transform.position.z
        );
    }
}