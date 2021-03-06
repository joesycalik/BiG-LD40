﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace Assets.Scripts.Player
{
    public class PlayerCam : MonoBehaviour
    {
        private new Transform transform;
        private Vector3 DesiredPos;
        public List<Transform> Players;
        public float camSpeed;
        private Camera cam;


        private float distance = -10f;
        void Awake()
        {
            transform = GetComponent<Transform>();
            cam = GetComponent<Camera>();
        }

        private IEnumerator Start()
        {
            yield return null;
            var p = GameObject.FindGameObjectsWithTag("Player");
            camSpeed = GameManager.instance.camSpeed;
            Players = new List<Transform>();
            for (int i = 0; i < p.Length; i++)
            {
                Players.Add(p[i].GetComponent<Transform>());
            }
        }

        void Update()
        {
            if (Players.Count <= 0)//early out if no players have been found
                return;
            DesiredPos = Vector3.zero;
            //float distance = 0f;
            var hSort = Players.OrderByDescending(p => p.position.y);
            var wSort = Players.OrderByDescending(p => p.position.x);
            var mHeight = hSort.First().position.y - hSort.Last().position.y;
            var mWidth = wSort.First().position.x - wSort.Last().position.x;
            var distanceH = -(mHeight + 5f) * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
            var distanceW = -(mWidth / cam.aspect + 5f) * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);

            distance = distanceH < distanceW ? distanceH : distanceW;
            //distance = Mathf.Sqrt(distanceH * distanceH + distanceW * distanceW);

            for (int i = 0; i < Players.Count; i++)
            {
                DesiredPos += Players[i].position;
            }
            if (distance > -10f) distance = -10f;
            DesiredPos /= Players.Count;
            DesiredPos.z = transform.position.z;
        }

        void LateUpdate()
        {
            if ((transform.position - DesiredPos).magnitude > .01)
            {
                transform.position = Vector3.MoveTowards(transform.position, DesiredPos, camSpeed);
            }
                
                cam.orthographicSize = (distance * -1.5f)  / 2;
        }
    }
}