using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
        private float startPos, length;
        private new GameObject camera;
        public float parallaxEffect;

        void Start() {
            this.camera = Camera.main.gameObject;
            startPos = transform.position.x;
            length   = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        void Update() {
            float temp = camera.transform.position.x * (1 - parallaxEffect);
            float dist = camera.transform.position.x * parallaxEffect;

            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            if (temp > startPos + length)
                startPos += length;
            else if (temp < startPos - length)
                startPos -= length;
        }
    }