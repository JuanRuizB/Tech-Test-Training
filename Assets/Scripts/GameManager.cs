using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

// Main class that manages the training logic
public class GameManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject MenuStart;
    [SerializeField] private GameObject Guide;
    [SerializeField] private GameObject Interface;
    [SerializeField] private GameObject InteractUI;
    [SerializeField] private TextMeshProUGUI stepText;

    [Header("Training Settings")]
    [SerializeField] private GameObject engineSprite;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject greenLight;

    // Class that represents a step of training
    [System.Serializable]
    public class TrainingStep
    {
        public string stepName;
        public Transform objectAnimation;
        public Transform targetTransform;
        public Transform initialPosition = null;
        public Action<Transform> stepAnimation;
    }

    [SerializeField] private TrainingStep[] trainingSteps;
    [SerializeField] private float waitDuration = 1f;

    private int currentStep = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        SetupSteps();
    }

    #region Application Behavior

    // Start the game from the menu
    public void StartGame()
    {
        MenuStart.SetActive(false);
        Interface.SetActive(true);
        StartStep();
    }
    // Close the game
    public void CloseGame()
    {
        Application.Quit();
    }

    // Displays final message and restarts to the menu
    private void ShowEndMessage()
    {
        stepText.text = "Thanks for completing the Jet Engine Blade swap training.";
        DesactiveActionButtons();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(engineSprite.transform.DORotate(Vector3.zero, 1f));
        sequence.AppendInterval(waitDuration*2);
        sequence.AppendCallback(() =>
        {
            EngineOn(false);
            currentStep = 0;
            MenuStart.SetActive(true);
            Interface.SetActive(false);
        });
        sequence.Play();
    }

    #endregion

    #region Steps Flow

    // Launches the animation of the current step
    public void StartSequence()
    {
        GameEvents.NoCanHandle();
        DesactiveActionButtons();
        trainingSteps[currentStep].stepAnimation(trainingSteps[currentStep].objectAnimation);
    }

    // Start the current step
    private void StartStep()
    {
        stepText.text = trainingSteps[currentStep].stepName;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(engineSprite.transform.DORotate(Vector3.zero, 0.5f));
        sequence.AppendCallback(StartSequence);
        sequence.Play();
    }

    // Move on to the next step
    public void NextStep()
    {
        DesactiveActionButtons();
        if (currentStep < trainingSteps.Length - 1)
        {
            currentStep++;
            StartStep();
        }
        else
        {
            ShowEndMessage();
        }
    }

    #endregion

    #region UI
    // Open the guide panel and pause the game
    public void OpenGuide()
    {
        Guide.SetActive(true);
        Interface.SetActive(false);
        Time.timeScale = 0f;
        GameEvents.NoCanHandle(); // Custom event (disables input)
    }

    // Close the guide and resume the game
    public void CloseGuide()
    {
        Guide.SetActive(false);
        Interface.SetActive(true);
        Time.timeScale = 1f;
        GameEvents.CanHandle(); // Custom event (enables input)
    }

    // Activate the action buttons (continue, repeat)
    private void ActiveActionButtons()
    {
        GameEvents.CanHandle();
        InteractUI.SetActive(true);
    }

    // Disable action buttons
    private void DesactiveActionButtons()
    {
        GameEvents.NoCanHandle();
        InteractUI.SetActive(false);
    }

    // Controls engine lights on/off
    private void EngineOn(bool value)
    {
        redLight.SetActive(!value);
        greenLight.SetActive(value);
    }

    #endregion

    #region Setup Steps
    // Configure all custom animations for the training steps
    private void SetupSteps()
    {
        if (trainingSteps.Length != 8)
            Debug.LogError("The trainingSteps array must have exactly 8 elements, but it has " + trainingSteps.Length);

        // Step 1
        trainingSteps[0].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.DOMove(trainingSteps[0].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .Join(obj.DORotate(trainingSteps[0].targetTransform.eulerAngles, moveDuration))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOMove(trainingSteps[0].initialPosition.position, moveDuration).SetEase(Ease.InOutQuad))
                    .Join(obj.DORotate(trainingSteps[0].initialPosition.eulerAngles, moveDuration))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };

        // Step 2
        trainingSteps[1].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.DOScale(Vector3.one, 0.1f))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOMove(trainingSteps[1].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .Join(obj.DORotate(trainingSteps[1].targetTransform.eulerAngles, moveDuration))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOScale(Vector3.zero, 0.1f))
                    .Append(obj.DOMove(trainingSteps[1].initialPosition.position, 0.1f))
                    .Join(obj.DORotate(trainingSteps[1].initialPosition.eulerAngles, 0.1f))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };

        // Step 3
        trainingSteps[2].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.DOMove(trainingSteps[2].initialPosition.position, 0.1f).SetEase(Ease.InOutQuad))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOMove(trainingSteps[2].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };

        // Step 4
        trainingSteps[3].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.DOMove(trainingSteps[3].initialPosition.position, 0.1f))
                    .Append(obj.DOScale(Vector3.one, 0.1f))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOMove(new Vector3(trainingSteps[3].targetTransform.position.x, trainingSteps[3].initialPosition.position.y, trainingSteps[3].targetTransform.position.z), moveDuration).SetEase(Ease.InOutQuad))
                    .Append(obj.DOMove(trainingSteps[3].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOScale(Vector3.zero, 0.1f))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };

        // Step 5
        trainingSteps[4].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.DOScale(Vector3.zero, 0.1f))
                    .Append(obj.DOMove(trainingSteps[4].initialPosition.position, 0.1f))
                    .Append(obj.DOScale(Vector3.one, 0.1f))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOMove(new Vector3(trainingSteps[4].initialPosition.position.x, trainingSteps[4].targetTransform.position.y, trainingSteps[4].targetTransform.position.z), moveDuration).SetEase(Ease.InOutQuad))
                    .Append(obj.DOMove(trainingSteps[4].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };

        // Step 6
        trainingSteps[5].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.DOMove(trainingSteps[5].initialPosition.position, 0.1f).SetEase(Ease.InOutQuad))
                    .AppendInterval(waitDuration)
                    .Append(obj.DOMove(trainingSteps[5].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };

        // Step 7
        trainingSteps[6].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(obj.DOScale(Vector3.zero, 0.1f))
                    .Append(obj.DOMove(trainingSteps[6].initialPosition.position, 0.1f))
                    .Join(obj.DORotate(trainingSteps[6].initialPosition.eulerAngles, 0.1f))
                    .Append(obj.DOScale(Vector3.one, 0.1f))
                    .Append(obj.DOMove(trainingSteps[6].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .Join(obj.DORotate(trainingSteps[6].targetTransform.eulerAngles, moveDuration))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };

        // Step 8
        trainingSteps[7].stepAnimation = (obj) =>
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() => { EngineOn(false); })
                    .Append(obj.DOMove(trainingSteps[7].targetTransform.position, moveDuration).SetEase(Ease.InOutQuad))
                    .Join(obj.DORotate(trainingSteps[7].targetTransform.eulerAngles, moveDuration))
                    .AppendInterval(waitDuration)
                    .AppendCallback(() => { EngineOn(true); })
                    .AppendInterval(waitDuration)
                    .Append(obj.DOMove(trainingSteps[7].initialPosition.position, moveDuration).SetEase(Ease.InOutQuad))
                    .Join(obj.DORotate(trainingSteps[7].initialPosition.eulerAngles, moveDuration))
                    .AppendInterval(waitDuration)
                    .AppendCallback(ActiveActionButtons);
            sequence.Play();
        };
    }
    #endregion
}
