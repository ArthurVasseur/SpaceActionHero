//
// © Arthur Vasseur arthurvasseur.fr
//

using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("Instance")]public static PlayerController instance;
        [SerializeField][Tooltip("Position to instantiate shot")]private Transform shotPoint;
        [SerializeField][Tooltip("Default player shot")]private GameObject defaultShot;
        [SerializeField][Tooltip("Time between shots")]private float timeBetweenShots = 0.2f;
        [SerializeField][Tooltip("Offset between shot")]private float doubleShotOffset;
    
        private Camera camera;
        private bool shotBoost;
        private const float TimeBetweenShotsBoost = 0.1f;
        private float timeShotsBoost = 5f;
        private float _timeBetweenShots;
        private float shotCounter = 0f;
        private float normalSpeed = 0;
        private GameObject shotToFire;
        private Vector3 bottomLeftLimit;
        private Vector3 topRightLimit;

        public bool DoubleShotActive { get; set; }
        public bool DisableInputs { get; set; }

        private void Awake()
        {
            instance = this;
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            bottomLeftLimit = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
            topRightLimit = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
            _timeBetweenShots = timeBetweenShots;
        }

        private void Start()
        {
            shotToFire = defaultShot;
        }
    
        private void Update()
        {
            if (!DisableInputs) {
                transform.position =
                    new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                        Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), 0);

#if UNITY_ANDROID
                DraggingPlayerFinger();
#endif
#if (UNITY_EDITOR)
                DraggingPlayerCursor();
#endif
                timeShotsBoost -= Time.deltaTime;
                if (timeShotsBoost <= 0) {
                    shotBoost = false;
                    timeShotsBoost = 3f;
                    _timeBetweenShots = timeBetweenShots;
                }
                if (shotBoost)
                    _timeBetweenShots = TimeBetweenShotsBoost;
            }
        }
        private void DraggingPlayerFinger()
        {
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                if (TouchPhase.Moved == touch.phase || TouchPhase.Stationary == touch.phase) {
                    Fire();
                    transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * 0.007f, transform.position.y + touch.deltaPosition.y * 0.007f, transform.position.z) ;
                }
            }
        }
  
#if (UNITY_EDITOR)
        private void DraggingPlayerCursor()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x,mousePosition.y);
            if (Input.GetMouseButton(0))
                Fire();
        }
#endif

        public void ActivateSpeedBoost()
        {
            shotBoost = true;
        }

        private void Fire()
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0) {
                if (!DoubleShotActive) 
                    Instantiate(shotToFire, shotPoint.position,new Quaternion(0,0,0,0));
                else {
                    Instantiate(shotToFire, shotPoint.position + new Vector3(doubleShotOffset, 0f, 0f), new Quaternion(0,0,0,0));
                    Instantiate(shotToFire, shotPoint.position - new Vector3(doubleShotOffset, 0f, 0f), new Quaternion(0,0,0,0));
                }
                shotCounter = _timeBetweenShots;
            }
        }
    }
}