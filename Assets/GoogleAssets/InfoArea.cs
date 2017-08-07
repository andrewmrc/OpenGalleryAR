using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SimpleAndroidNotifications
{
    
    public class InfoArea : MonoBehaviour
    {
        public Murales muralesRef;

        public string name;
        public string info;

        private void Start()
        {
            info = muralesRef.author;
            name = muralesRef.name;
        }
    }
}