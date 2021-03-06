﻿using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

namespace Assets.SimpleAndroidNotifications
{

    public class SimpleController : MonoBehaviour
    {
        public float speed = 6.0F;
        public float gravity = 20.0F;

        private Vector3 moveDirection = Vector3.zero;
        public CharacterController controller;

        public RectTransform notificationPanel;

        public GameObject infoText;
        public GameObject nameText;

        void Start()
        {
            // Store reference to attached component
            controller = GetComponent<CharacterController>();
        }

        public void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "ObjectOnMap")
            {
                NotificationManager.Send(TimeSpan.FromSeconds(1), coll.GetComponent<InfoArea>().name, coll.GetComponent<InfoArea>().info, new Color(1, 0.3f, 0.15f));
                notificationPanel.GetComponent<CanvasGroup>().alpha = 1;
                infoText.GetComponent<Text>().text = coll.GetComponent<InfoArea>().info;
                nameText.GetComponent<Text>().text = coll.GetComponent<InfoArea>().name;
                Debug.Log(coll.GetComponent<InfoArea>().name + " : " + coll.GetComponent<InfoArea>().info);
                coll.gameObject.SetActive(false);
            }
                
        }

        void Update()
        {
            
            // Use input up and down for direction, multiplied by speed
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;


            // Move Character Controller
            if (moveDirection.magnitude > 0.001)
                controller.Move(moveDirection * Time.deltaTime);

        }
    }
}