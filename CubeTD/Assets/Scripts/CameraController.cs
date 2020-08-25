using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThikness = 10f;
    public float scrollSpeed = 5000f;

    [Header("Border")]

    public float minY = 10;
    public float maxY = 50;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    void Update()
    {
        if (GameManager.gameIsOver)
        {
            enabled = false;
            return;
        }

        ButtonManager();

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * scrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }

    private void ButtonManager()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThikness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThikness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThikness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThikness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }
}
