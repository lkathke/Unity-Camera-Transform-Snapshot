using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class CameraSnapshot
    {
        public static bool DEBUG_MODE = true;

        public NearViewPortCoordinates NearViewPortCoordinates { get; set; }
        public Matrix4x4 WorldToCameraMatrix { get; set; }
        public Matrix4x4 ProjectionMatrix { get; set; }
        public Vector2 ScreenSize { get; set; }

        public CameraSnapshot(Camera camera)
        {
            NearViewPortCoordinates = new NearViewPortCoordinates(camera);
            WorldToCameraMatrix = camera.worldToCameraMatrix;
            ProjectionMatrix = camera.projectionMatrix;
            ScreenSize = new Vector2(Screen.width, Screen.height);
        }

        public Vector3 Screen2dToWorldPixel(Vector2 screenPosition, float depth)
        {
            return Screen2dToWorldPixel(new Vector3(screenPosition.x, screenPosition.y, 0), depth);
        }

        private Vector3 Screen2dToWorldPixel(Vector3 screenPosition, float depth)
        {
            Vector3 screenPositionViewport = new Vector3(0, 0, 0);
            screenPositionViewport.x = ((screenPosition.x / ScreenSize.x) - 0.5f) * 2;
            screenPositionViewport.y = ((screenPosition.y / ScreenSize.y) - 0.5f) * 2;
            screenPositionViewport.z = 0;

            Camera.main.ScreenToWorldPoint(new Vector3());

            Matrix4x4 mvpMatrix = ProjectionMatrix * WorldToCameraMatrix;
            Vector3 worldPosition = mvpMatrix.inverse.MultiplyPoint(screenPositionViewport);

            Vector3 cameraObjPosition = WorldToCameraMatrix.GetColumn(3);
            cameraObjPosition = new Vector3(-cameraObjPosition.x, -cameraObjPosition.y, cameraObjPosition.z);

            var nearClipPlaneDepth = Camera.main.nearClipPlane;
            var cameraPosition = NearViewPortCoordinates.TopLeft + (NearViewPortCoordinates.XVector * ((screenPositionViewport.x + 1) / 2)) + (NearViewPortCoordinates.YVector * ((screenPositionViewport.y + 1) / 2));
            var cameraObjToViewport = cameraObjPosition - cameraPosition;


            Vector3 direction = worldPosition - cameraPosition;
            worldPosition = cameraPosition + (direction.normalized * (depth - cameraObjToViewport.magnitude));

            if(DEBUG_MODE) Debug.Log("Entfernung: " + (worldPosition - cameraObjPosition).magnitude);

            return worldPosition;//worldPosition;
        }
    }
}
