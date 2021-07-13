//
// © Arthur Vasseur arthurvasseur.fr
//

using System.Collections;
using UnityEngine;

namespace Enemy.FolowPath.Bezier
{
    public class FolowPath : MonoBehaviour
    {
        public float timeToWait;
    
        [SerializeField] private Transform[] routes;
        [SerializeField]private float speedModifier;
        [SerializeField]private bool destroyOnFinishedRoute;
        [SerializeField]private bool loop;
        private bool coroutineAllowed;
        private int routeToGo;
        private float tParam;
        private Vector2 objectPosition;
        private Vector2 p0;
        private Vector2 p1;
        private Vector2 p2;
        private Vector2 p3;
        private float currentTime;
        private void Start()
        {
            routeToGo = 0;
            coroutineAllowed = true;
        }
        private void Update()
        {
            currentTime += Time.deltaTime;
            if (coroutineAllowed && currentTime > timeToWait)
                StartCoroutine(GoByTheRoute(routeToGo));
            p0 = routes[routeToGo].GetChild(0).position;
            p1 = routes[routeToGo].GetChild(1).position;
            p2 = routes[routeToGo].GetChild(2).position;
            p3 = routes[routeToGo].GetChild(3).position;
        }
        private IEnumerator GoByTheRoute(int routeNumber)
        {
            coroutineAllowed = false;
            while (tParam < 1) {
                tParam += Time.deltaTime * speedModifier;
                //mathematical formula of bezier curve
                transform.position = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam,3) * p3;
                yield return new WaitForEndOfFrame();
            }
            if (destroyOnFinishedRoute)
                Destroy(transform.parent.gameObject);
            if (loop) {
                tParam = 0f;
                routeToGo++;
                if (routeToGo > routes.Length-1)
                    routeToGo = 0;
                coroutineAllowed = true;
            }
            else Destroy(this);
        }
    }
}
