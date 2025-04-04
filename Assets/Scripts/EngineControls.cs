using UnityEngine;
using UnityEngine.InputSystem;

public class EngineControls : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1f;
    private GameConstrols controls;
    private bool canHandle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controls = new GameConstrols();
    }

    void Update()
    {
        if (canHandle) HandleObjectTurning();
    }

    private void OnEnable()
    {
        GameEvents.OnCanHandle += CanHandle;
        GameEvents.OnNoCanHandle += NoCanHandle;
        controls.Enable();
    }
    private void OnDisable()
    {
        GameEvents.OnCanHandle -= CanHandle;
        GameEvents.OnNoCanHandle += NoCanHandle;
        controls.Disable();
    }

    private void CanHandle() => canHandle = true;

    private void NoCanHandle() => canHandle = false;

    private void HandleObjectTurning()
    {
        float rotationInput = 0;

        // Detecta las teclas A y D
        if (controls.CameraActions.TurnRight.IsPressed())
            rotationInput = 1; // Girar a la derecha
        else if (controls.CameraActions.TurnLeft.IsPressed())
            rotationInput = -1; // Girar a la izquierda

        // Realiza la rotación
        transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.deltaTime);
    }
}
