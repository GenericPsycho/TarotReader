using UnityEngine;

public class RotateCard : MonoBehaviour
{
    public float RotationSpeed = 10f;
    public Camera camera;
    public GameObject card;
    void OnMouseDrag ()
    {
        float rX = Input.GetAxis("Mouse X") * RotationSpeed;
        float rY = Input.GetAxis("Mouse Y") * RotationSpeed;

        Vector3 changeX = Vector3.Cross(camera.transform.up, card.transform.position - camera.transform.position);
        Vector3 changeY = Vector3.Cross(card.transform.position - camera.transform.position, changeX);

        card.transform.rotation = Quaternion.AngleAxis(-rX, changeY) * card.transform.rotation;
        card.transform.rotation = Quaternion.AngleAxis(rY, changeX) * card.transform.rotation;
    }

    void Update ()
    {
        foreach (Touch touch in Input.touches)
        {
            Ray camRay = camera.ScreenPointToRay(touch.position);
            RaycastHit raycastHit;
            if (Physics.Raycast (camRay, out raycastHit, 10))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Begin touch at : " + touch.position);
                } else if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log("Moved touch at : " + touch.position);
                    card.transform.Rotate(touch.deltaPosition.y * (RotationSpeed / 2), touch.deltaPosition.x * RotationSpeed, 0, Space.World);
                } else if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Touch ended");
                }
            }
        }
    }
}
