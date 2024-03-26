using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private void Update()
    {
        // Check PlayerPrefs for the rotation setting
        if (PlayerPrefs.GetInt("RotationEnabled", 1) == 1)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
        }
    }

    // Call this method to toggle rotation on/off globally
    public static void ToggleRotation(bool enabled)
    {
        PlayerPrefs.SetInt("RotationEnabled", enabled ? 1 : 0);

        PlayerPrefs.Save();
    }



}
