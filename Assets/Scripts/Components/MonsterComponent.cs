using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Core.MapDto;
using UnityEngine;

namespace Components
{
    public class MonsterComponent : MonoBehaviour
    {
        public void Start()
        {
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();
            transformComponent = GetComponent<TransformComponent>();
        }
        
        public void FixedUpdate()
        {
            var botPosition = transform.position.ToVector2();
            var botTilePosition = botPosition.ToVectorInt();

            var (updated, newTarget) = UpdateTarget(target);
            target = newTarget;
            if (target == null) return;
        
            var targetPosition = target.transform.position.ToVector2();
            var targetTilePosition = targetPosition.ToVectorInt();

            if (botPosition.CloseToPoint(targetPosition)) return;

            if (localPathTarget == null || botPosition.CloseToPoint(localPathTarget.Value) || updated)
            {
                var targetTile = map[targetTilePosition];
                var botTile = map[botTilePosition];
                var path = map.GetPath(botTile, targetTile);
                localPathTarget = path.Length > 0 ? path[0].Cords.TileCenter() : botTile.Cords;
            }

            if (localPathTarget.HasValue)
            {
                MoveTo(localPathTarget.Value);
            }
        }
        
        public void Update()
        {
            var velocity = transform.GetComponent<Rigidbody2D>().velocity;
            if (velocity.magnitude > 0)
            {
                var rotateDirection = velocity.ToVector3().normalized;
                if (rotateDirection.magnitude > 0) transform.rotation = Geometry.GetQuaternionFromCathetuses(rotateDirection);
            }
        }
        
        private (bool, GameObject) UpdateTarget(GameObject prevTarget)
        {
            if (prevTarget != null && gameStateController.ContainsEntity(prevTarget.GetInstanceID()))
                return (false, prevTarget);

            var newTarget = FindObjectsOfType<TargetComponent>()
                .FirstOrDefault()
                ?.gameObject;
            return (true, newTarget);
        }

        private void MoveTo(Vector2 position)
        {
            transformComponent.Direction = position - transform.position.ToVector2();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var isTarget = other.gameObject.TryGetComponent<TargetComponent>(out _);
            if (isTarget)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }

        private GameObject target;
        private Vector2? localPathTarget;
        private Map map;        
        
        private GameStateController gameStateController;

        private TransformComponent transformComponent;
    }
}