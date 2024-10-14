using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Balli_Stick.Totem
{
    public class TotemManager : MonoBehaviour
    {
        [SerializeField] private Totem[] totems;
        
        [Header("Metal BallPool")]
        [SerializeField] private MetalBall metalBallPrefab;
        [SerializeField, Min(1)] private int defaultCapacity = 5;
        [SerializeField, Min(5)] private int maxCapacity = 10;

        [Header("Balls in Playground")] 
        [SerializeField, Range(2, 10)] private int maxBalls = 5;
        [SerializeField, Min(5)] private float secondsForUpgrade; 
        
        private static ObjectPool<MetalBall> _ballsPool;

        private Dictionary<int, PooledObject<MetalBall>> _ballsInPlay = new(10);
        private int _maxBallsInPlay = 1;
        
        private void Start()
        {
            _ballsPool = new ObjectPool<MetalBall>(CreateBall, OnGetBall, OnReleaseBall, DestroyBall,
                true, defaultCapacity, maxCapacity);
        }
        
        public void Activate()
        {
            _upgradeCoroutine = StartCoroutine(UpgradeMaxBalls());
        }
        
        public void Deactivate()
        {
            if (_upgradeCoroutine != null)
                StopCoroutine(_upgradeCoroutine);

            foreach (var ballInPlay in _ballsInPlay)
            {
                using (ballInPlay.Value)
                {    
                }
            }
        }

        private Coroutine _upgradeCoroutine;
        private IEnumerator UpgradeMaxBalls()
        {
            for (int i = 0; i < maxBalls; i++)
            {
                _maxBallsInPlay = i + 1;
                CheckBallsInPlay();
                yield return new WaitForSeconds(secondsForUpgrade);
            }

            _upgradeCoroutine = null;
        }

        private void CheckBallsInPlay()
        {
            for (int i = _ballsInPlay.Count; i < _maxBallsInPlay; i++)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            if (_ballsInPlay.Count >= _maxBallsInPlay) return;
            
            var pooledObject = _ballsPool.Get(out var ball);
            _ballsInPlay.Add(ball.id, pooledObject);
            totems[Random.Range(0, totems.Length)].AppearBall(ball);
        }
        
        private void BallDisappear(MetalBall ball)
        {
            if (!_ballsInPlay.ContainsKey(ball.id)) return;
            
            //Es un IDisposable, por eso necesito usar esta logica
            using (_ballsInPlay[ball.id])
            {
            }
            _ballsInPlay.Remove(ball.id);

            Spawn(); //Terminando spawnea otra bola
        }

        private void OnEnable() => MetalBall.OnMetalLifeHit += BallDisappear;
        private void OnDisable() => MetalBall.OnMetalLifeHit -= BallDisappear;



        #region BallPool

        private int _lastID;
        private MetalBall CreateBall()
        {
            var ball = Instantiate(metalBallPrefab);
            ball.id = _lastID;
            _lastID++;
            return ball;
        }

        private void OnGetBall(MetalBall ball)
        {
            ball.gameObject.SetActive(true);
        }

        private void OnReleaseBall(MetalBall ball)
        {
            ball.transform.position = Vector3.down * 25; //la esconde bajo tierra
            ball.gameObject.SetActive(false);
        }

        private void DestroyBall(MetalBall ball)
        {
            Destroy(ball);
        }

        #endregion
    }
}
