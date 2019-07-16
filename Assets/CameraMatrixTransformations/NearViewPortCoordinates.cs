using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class NearViewPortCoordinates
    {
        public NearViewPortCoordinates(Camera camera)
        {
  
            TopLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
            TopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane));
            BottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane));
            BottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

            XVector = TopRight - TopLeft;
            YVector = BottomLeft - TopLeft;
        }

        public Vector3 TopLeft { get; private set; }
        public Vector3 TopRight { get; private set; }
        public Vector3 BottomLeft { get; private set; }
        public Vector3 BottomRight { get; private set; }
        public Vector3 XVector { get; private set; }
        public Vector3 YVector { get; private set; }
    }
}
