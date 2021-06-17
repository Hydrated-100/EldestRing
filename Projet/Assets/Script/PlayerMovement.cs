using System;
using System.Collections;
using Photon.Pun;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

    public class PlayerMovement : Entity
    {
        public float moveTime = .0005f;
        public LayerMask blockingLayer;

        private Rigidbody2D rb;
        private BoxCollider2D boxCollid;
        private float reverse;
        private Animator anime;

        private Vector2 _movement;
        private Vector3 origine;
        protected int hp = 15;
        protected int damage = 5;


        private void Start()
        {
            origine = Vector3.zero;
            boxCollid = GetComponent<BoxCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            reverse = 1f / moveTime;
            anime = GetComponent<Animator>();
        }


        private bool CheckGameOver()
        {
            if (hp > 0) return false;
            GameManagement.instance.GameOver();
            return true;

        }
        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            Vector2 start = transform.position;
            Vector2 end = start + new Vector2(xDir, yDir);
            boxCollid.enabled = false;
            hit = Physics2D.Linecast(start, end, blockingLayer);
            boxCollid.enabled = true;

            if (hit.transform is null)
            {
                StartCoroutine(Mouvement(end));
                return true;
            }

            return false;
        }

        protected IEnumerator Mouvement(Vector3 fin)
        {
            float distance = (transform.position - fin).sqrMagnitude;

            while (distance > float.Epsilon)
            {
                Vector3 newPos = Vector3.MoveTowards(rb.position, fin, reverse * Time.deltaTime);
                rb.MovePosition(newPos);
                distance = (transform.position - fin).sqrMagnitude;
                yield return null;
            }
        }

        protected void MoveAttemp<T>(int xDir, int yDir)
        {
            bool able = Move(xDir, yDir, out var hit);
            

            T hitComp = hit.transform.GetComponent<T>();
            if (!able && hitComp != null)
            {
                cantMove(hitComp);
            }
        }
        

        void cantMove<T>(T faced)
        {
            switch (faced)
            {
                case Wall hit:
                    hit.Deleted();
                    break;
                case Entity hit1:
                    hit1.Damaged(damage);
                    break;
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Door")
            {
                Invoke("Restart", 0.5f);
            }
        }

        private void Restart()
        {
            gameObject.transform.position = origine;
            GameManagement.instance.InitGame();
        }

        void Update()
        {
            if (CheckGameOver()) return;
            int hori = 0;
            int vert = 0;

            hori = (int) Input.GetAxisRaw("Horizontal");
            vert = (int) Input.GetAxisRaw("Vertical");

            if (hori != 0)
            {
                vert = 0;
            }

            if (hori == 0 && vert == 0) return;
            MoveAttemp<Wall>(hori, vert);
            MoveAttemp<Entity>(hori, vert);
        }
    }