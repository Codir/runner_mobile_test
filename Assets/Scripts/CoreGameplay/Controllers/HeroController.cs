using Configs;
using CoreGameplay.Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CoreGameplay.Controllers
{
    //Class with logic of moving, jump and fly of hero
    public class HeroController : BaseControllerWithConfig<HeroConfig>
    {
        private BaseEntityModel _entityModel;

        private bool _isGrounded;
        private Vector3 _velocity;

        private CollisionController _groundCollisionController;
        private CollisionController _powerUpsCollisionController;

        private PowerUpsController _powerUpsController;

        private Vector3 _tapOffset;
        private bool _isTapped;

        public void Set(Vector3 heroSpawnPosition, Transform levelContainer)
        {
            View.transform.SetParent(levelContainer);
            View.transform.position = heroSpawnPosition;

            _groundCollisionController =
                new CollisionController(Config.GroundColliderBox, Quaternion.identity, Config.GroundLayer);
            _powerUpsCollisionController = new CollisionController(Config.CollectRadius);
            _powerUpsController = new PowerUpsController();

            LoadFromConfig();

            _powerUpsController.OnActiveListUpdatedEvent += OnActiveListUpdated;
        }

        private void Dispatch()
        {
            _powerUpsController.OnActiveListUpdatedEvent -= OnActiveListUpdated;

            View.transform.gameObject.SetActive(false);
        }

        private void OnActiveListUpdated()
        {
            LoadFromConfig();
            ApplyPowerUps();
        }

        private void LoadFromConfig()
        {
            _entityModel = new BaseEntityModel
            {
                MovementType = Config.BaseMovementType,
                MoveSpeed = Config.MoveSpeed,
                JumpSpeed = Config.JumpSpeed,
                Gravity = Config.Gravity
            };
        }

        private void ApplyPowerUps()
        {
            _entityModel = _powerUpsController.ApplyToModel(_entityModel);
            _powerUpsController.Update(Time.fixedDeltaTime);
        }

        public void FixedUpdate()
        {
            var collectibleModels =
                _powerUpsCollisionController.GetCollisionsBySphere<CollectibleEntityModel>(View.transform.position);
            _powerUpsController.AddItems(collectibleModels);
        }

        public void CheckIsGrounded(float deltaTime)
        {
            _isGrounded = _groundCollisionController.CheckBox(GetGroundColliderPosition());

            _velocity.y = _isGrounded ? 0f : _velocity.y + _entityModel.Gravity * deltaTime;
            _velocity.x = _entityModel.MoveSpeed;
        }

        public void Update(float deltaTime)
        {
            CheckInput();

            switch (_entityModel.MovementType)
            {
                case MovementTypes.Run:
                    OnRun(deltaTime);
                    break;
                case MovementTypes.Fly:
                    OnFly(deltaTime);
                    break;
            }
        }

        private void CheckInput()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (_entityModel.MovementType == MovementTypes.Run && Input.GetMouseButtonDown(0))
            {
                Jump();
            }

            if (_entityModel.MovementType == MovementTypes.Fly && Input.GetMouseButtonDown(0))
            {
                _tapOffset = Input.mousePosition;
                _isTapped = true;
            }

            if (_entityModel.MovementType == MovementTypes.Fly && Input.GetMouseButtonUp(0))
            {
                _isTapped = false;
            }
        }

        private void OnRun(float deltaTime)
        {
            View.transform.position += _velocity * deltaTime;
        }

        private void OnFly(float deltaTime)
        {
            if (_isTapped)
            {
                var diff = Input.mousePosition - _tapOffset;
                _tapOffset = Input.mousePosition;
                _velocity.y = diff.y * _entityModel.MoveSpeed;
            }
            else
            {
                _velocity.y = 0f;
            }

            View.transform.position += _velocity * deltaTime;
        }

        private void Jump()
        {
            if (!_isGrounded && _entityModel.MovementType == MovementTypes.Run) return;

            _velocity.y += _entityModel.JumpSpeed;

            _isGrounded = false;
        }

        private Vector3 GetGroundColliderPosition()
        {
            return View.transform.position + Config.GroundColliderOffsetPosition;
        }
    }
}