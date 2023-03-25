using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	[SerializeField] private PlayerController playerController;
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;
	public bool shakeFlag = false;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}
    private void Start()
    {
        playerController.OnShoot += PlayerController_OnShoot;

	}

    private void PlayerController_OnShoot(object sender, PlayerController.OnShootEventArgs e)
    {
		shakeDuration = 0.2f;
		shakeFlag = true;
		originalPos = camTransform.localPosition;
	}

    void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
        if (shakeFlag)
        {
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
			}
			else
			{
				shakeDuration = 0f;
				shakeFlag = false;
			}
		}
		
	}
}