using UnityEngine;


public static class CameraUtils
{
    
    
    public static bool InFieldOfViewX(Camera _cam, Vector3 _objectPosition, Vector2 _minMax)
    {
        Vector3 screenPos = _cam.WorldToViewportPoint(_objectPosition);
        
        // check if its outside camera view
        return screenPos.z < 0f || screenPos.x < _minMax.x || screenPos.x > _minMax.y || screenPos.y < 0f ||
               screenPos.y > 1f;

    }
}
