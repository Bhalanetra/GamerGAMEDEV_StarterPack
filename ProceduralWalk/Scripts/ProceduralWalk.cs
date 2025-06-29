using GamerGAMEDEV.Utility;
using UnityEngine;


public class ProceduralWalk : MonoBehaviour
{
    #region Variables
    //[Header("Thresholds")]
    //public float minWalkDistance = 0.01f;
    //public float minRotationAngle = 1f;

    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private float lastYRotation;

    [Space, Header("FEET")]
    [SerializeField] Transform leftFoot;
    [SerializeField] Transform rightFoot;
    [Space]
    [SerializeField] private float footMoveThreshold = 0.05f;
    [SerializeField] private float idleFootMoveThreshold = 0.001f;
    [SerializeField] private float footLerpSpeed = 5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform leftFootRayOrigin;
    [SerializeField] Transform rightFootRayOrigin;
    [SerializeField] private AnimationCurve footArcCurve; // e.g., default parabola (0,0)->(0.5,1)->(1,0)
    [SerializeField] private float arcMultiplier = 0.2f;

    public bool movingLeftFeet = false;
    public bool movingRightFeet = false;

    private bool isLeftStepTurn = true; // step alternation toggle
    private float leftFootProgress = 0f;
    public bool leftFootArrived = false;
    private float rightFootProgress = 0f;
    public bool rightFootArrived = false;


    [Space, Header("Movement Detection")]
    //[SerializeField] PlayerFeetSoundManager feetSound;
    [SerializeField] private float moveDetectionThreshold = 0.01f;
    [SerializeField] private float stepForwardOffset = 0.2f;
    [SerializeField] private Vector3 groundOffset;

    [Space]
    public float idleTimer = 0f;
    public float requiredIdleTime = 1.5f; // You can tweak this in the Inspector
    public bool isIdle { get; private set; }

    public bool resettingFeet = false;

    private Vector3 previousRootPos;
    private bool isCharacterMoving = false;


    private Vector3 currentLeftFootTarget;
    private Vector3 currentRightFootTarget;

    private enum MovementState
    {
        Idle,
        MovingForward,
        MovingBackward,
        MovingLeft,
        MovingRight,
        RotatingLeft,
        RotatingRight
    }

    private MovementState lastState = MovementState.Idle;
    #endregion Variables

    #region Unity Methods

    private void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        lastYRotation = transform.eulerAngles.y;

        currentLeftFootTarget = leftFoot.position;
        currentRightFootTarget = rightFoot.position;
    }

    private void Update()
    {
        //MovementMonitor();
        AutoDetectMovement();
        SetFeetPositions();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    #endregion Unity Methods

    #region Other
    #endregion Other

    #region Main

    //void MovementMonitor()
    //{
    //    Vector3 currentPosition = transform.position;
    //    Quaternion currentRotation = transform.rotation;
    //    float currentYRotation = transform.eulerAngles.y;

    //    float distanceMoved = Vector3.Distance(currentPosition, lastPosition);
    //    float angleRotated = Quaternion.Angle(currentRotation, lastRotation);

    //    MovementState currentState = MovementState.Idle;

    //    // Translation detection
    //    if (distanceMoved > minWalkDistance)
    //    {
    //        Vector3 moveDir = (currentPosition - lastPosition).normalized;
    //        Vector3 localMoveDir = transform.InverseTransformDirection(moveDir);

    //        if (Mathf.Abs(localMoveDir.z) > Mathf.Abs(localMoveDir.x))
    //        {
    //            currentState = localMoveDir.z > 0 ? MovementState.MovingForward : MovementState.MovingBackward;
    //        }
    //        else
    //        {
    //            currentState = localMoveDir.x > 0 ? MovementState.MovingRight : MovementState.MovingLeft;
    //        }
    //    }
    //    // Rotation detection
    //    else if (angleRotated > minRotationAngle)
    //    {
    //        float deltaY = Mathf.DeltaAngle(lastYRotation, currentYRotation);
    //        currentState = deltaY > 0 ? MovementState.RotatingRight : MovementState.RotatingLeft;
    //    }

    //    // State change logging (excluding Idle -> Idle)
    //    if (currentState != lastState && !(lastState == MovementState.Idle && currentState == MovementState.Idle))
    //    {
    //        Debug.Log($"[MovementMonitor] State Changed: {lastState} → {currentState}");
    //        lastState = currentState;
    //    }

    //    lastPosition = currentPosition;
    //    lastRotation = currentRotation;
    //    lastYRotation = currentYRotation;
    //}

    private void OnStateChanged(MovementState newState)
    {
        switch (newState)
        {
            case MovementState.Idle:


                break;
            case MovementState.MovingForward:


                break;
            case MovementState.MovingBackward:


                break;
            case MovementState.MovingLeft:


                break;
            case MovementState.MovingRight:


                break;
            case MovementState.RotatingLeft:


                break;
            case MovementState.RotatingRight:


                break;
            default:
                break;
        }
    }

    //void SetFeetPositions()
    //{
    //    Vector3 leftFootTarget = RaycastUtility.RaycastHitPoint(leftFootRayOrigin.position, Vector3.down, 10f, groundLayer);
    //    Vector3 rightFootTarget = RaycastUtility.RaycastHitPoint(rightFootRayOrigin.position, Vector3.down, 10f, groundLayer);

    //    float leftDist = Vector3.Distance(leftFoot.position, leftFootTarget);
    //    float rightDist = Vector3.Distance(rightFoot.position, rightFootTarget);

    //    // Update target if the foot needs to move
    //    if (leftDist > footMoveThreshold)
    //    {
    //        leftFootArrived = false;
    //        currentLeftFootTarget = leftFootTarget;
    //    }

    //    if (rightDist > footMoveThreshold)
    //    {
    //        rightFoortArrived = false;
    //        currentRightFootTarget = rightFootTarget;
    //    }

    //    leftFoot.position = LerpUtility.DoArcLerpLinear(leftFoot.position, currentLeftFootTarget, footLerpSpeed, footArcCurve, arcMultiplier, () =>
    //    {
    //        leftFootProgress = 0;
    //        Debug.Log("Left Feet Moving End!");
    //    }, ref leftFootProgress, ref leftFootArrived);

    //    rightFoot.position = LerpUtility.DoArcLerpLinear(rightFoot.position, currentRightFootTarget, footLerpSpeed, footArcCurve, arcMultiplier, () =>
    //    {
    //        rightFootProgress = 0;
    //        Debug.Log("Left Feet Moving End!");
    //    }, ref rightFootProgress, ref rightFoortArrived);
    //}

    void AutoDetectMovement()
    {
        float moveDist = Vector3.Distance(transform.position, previousRootPos);
        isCharacterMoving = moveDist > moveDetectionThreshold;
        previousRootPos = transform.position;
    }

    void SetFeetPositions()
    {
        // 1. Movement direction calculation
        Vector3 moveDir = (transform.position - previousRootPos).normalized;
        if (moveDir.sqrMagnitude < 0.0001f)
            moveDir = transform.forward; // fallback

        Debug.Log($"This is DIR : {moveDir}");

        // 2. Idle ray origins
        Vector3 leftIdleOrigin = leftFootRayOrigin.position;
        Vector3 rightIdleOrigin = rightFootRayOrigin.position;

        // 3. Idle targets (used for detecting step need)
        Vector3 leftFootIdleTarget = RaycastUtility.RaycastHitPoint(leftIdleOrigin, Vector3.down, 10f, groundLayer) - groundOffset;
        Vector3 rightFootIdleTarget = RaycastUtility.RaycastHitPoint(rightIdleOrigin, Vector3.down, 10f, groundLayer) - groundOffset;

        // 4. Move ray origins adjusted by movement direction
        Vector3 leftMoveOrigin = leftIdleOrigin;
        Vector3 rightMoveOrigin = rightIdleOrigin;

        Vector3 leftFootTarget = leftFootIdleTarget;
        Vector3 rightFootTarget = rightFootIdleTarget;

        if (isCharacterMoving)
        {
            //leftMoveOrigin += moveDir * stepForwardOffset;
            //rightMoveOrigin += moveDir * stepForwardOffset;

            leftFootTarget = HelperUtility.GetPointInOppositeDirection(leftFootIdleTarget, leftFoot.position, stepForwardOffset);
            rightFootTarget = HelperUtility.GetPointInOppositeDirection(rightFootIdleTarget, rightFoot.position, stepForwardOffset);

            idleTimer = 0;
            isIdle = false;
        }
        else if (!isCharacterMoving && !isIdle)
        {
            idleTimer += Time.deltaTime;

            if (idleTimer >= requiredIdleTime)
            {
                idleTimer = 0;
                isIdle = true;
                resettingFeet = true;
            }
        }

        float threshold = resettingFeet ? idleFootMoveThreshold : footMoveThreshold;

        //feetSound.PlayFeetSound(isCharacterMoving);

        // 6. Step distance from idle targets (used for determining if a step is needed)
        float leftDist = Vector3.Distance(leftFoot.position, leftFootIdleTarget);
        float rightDist = Vector3.Distance(rightFoot.position, rightFootIdleTarget);

        // 7. Step Rhythm + Planting Logic
        if (isLeftStepTurn && leftFootArrived && leftDist > threshold)
        {
            leftFootArrived = false;
            currentLeftFootTarget = leftFootTarget;
        }
        else if (!isLeftStepTurn && rightFootArrived && rightDist > threshold)
        {
            rightFootArrived = false;
            currentRightFootTarget = rightFootTarget;
        }

        // 8. Arc Lerp Foot Transitions
        leftFoot.position = LerpUtility.DoArcLerpLinear(
            leftFoot.position,
            currentLeftFootTarget,
            footLerpSpeed,
            footArcCurve,
            arcMultiplier,
            () => {
                leftFootProgress = 0;
                leftFootArrived = true;
                isLeftStepTurn = false;
                Debug.Log("Left Foot Step Complete");

                if (resettingFeet) resettingFeet = false;
            },
            ref leftFootProgress,
            ref leftFootArrived
        );

        rightFoot.position = LerpUtility.DoArcLerpLinear(
            rightFoot.position,
            currentRightFootTarget,
            footLerpSpeed,
            footArcCurve,
            arcMultiplier,
            () => {
                rightFootProgress = 0;
                rightFootArrived = true;
                isLeftStepTurn = true;
                Debug.Log("Right Foot Step Complete");


                if (resettingFeet) resettingFeet = false;
            },
            ref rightFootProgress,
            ref rightFootArrived
        );
    }

    private void OnDrawGizmos()
    {
        if (leftFootRayOrigin == null || rightFootRayOrigin == null) return;

        // Idle Ray Origins
        Vector3 leftOrigin = leftFootRayOrigin.position;
        Vector3 rightOrigin = rightFootRayOrigin.position;

        // Movement direction for debug
        Vector3 moveDir = Application.isPlaying
            ? (transform.position - previousRootPos).normalized
            : transform.forward;

        if (moveDir.sqrMagnitude < 0.001f)
            moveDir = transform.forward;

        // Show raycasts
        Gizmos.color = Color.green;
        Gizmos.DrawLine(leftOrigin, leftOrigin + Vector3.down * 10f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(rightOrigin, rightOrigin + Vector3.down * 10f);

        // Show move direction arrow
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + moveDir * 0.5f);
        Gizmos.DrawSphere(transform.position + moveDir * 0.5f, 0.025f);
    }

    #endregion Main
}