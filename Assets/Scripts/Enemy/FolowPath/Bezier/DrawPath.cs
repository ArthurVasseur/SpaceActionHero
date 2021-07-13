//
// © Arthur Vasseur arthurvasseur.fr
//

using System.Collections.Generic;
using UnityEngine;

namespace Enemy.FolowPath.Bezier
{
    public class DrawPath : MonoBehaviour
    {
        [SerializeField]
        private Transform[] controlPoints;
        private Vector2 gizmosPosition;
        private List<Vector2> points;
    
        //Gizmos are used to give visual debugging
        private void OnDrawGizmos()
        {
            points = new List<Vector2>();
            for (float i = 0; i <= 1; i += 0.05f) {
                //mathematical formula of bezier curve
                gizmosPosition = Mathf.Pow(1 - i, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - i, 2) * i * controlPoints[1].position + 3 * (1 - i) * Mathf.Pow(i, 2) * controlPoints[2].position + Mathf.Pow(i, 3) * controlPoints[3].position;
                points.Add(gizmosPosition);
            }
        
            Gizmos.color = Color.yellow;
            for (int i = 0; i <= points.Count-1 ; i++) {
                if (i+1 <= points.Count-1)
                    Gizmos.DrawLine(points[i],points[i+1]);
            }
            Gizmos.DrawLine(points[points.Count-1],controlPoints[controlPoints.Length-1].position);
            Gizmos.DrawSphere(points[0],0.25f);
            Gizmos.DrawSphere(controlPoints[controlPoints.Length-1].position,0.25f);
        }
    }
}
