using System;
using Core.MapDto;
using EntityFactoryDto;
using UnityEngine;

namespace Controllers
{
    public class SpawnController : MonoBehaviour
    {
        void Start()
        {
            var gameController = GameObject.Find(nameof(GameController))?.GetComponent<GameController>();
            if (gameController != null) map = gameController.Map;

            gameStateController = GameObject.Find(nameof(GameStateController))?.GetComponent<GameStateController>();
        }

        void FixedUpdate()
        {
            var now = DateTime.Now;
            if (lastSpawn + generatePeriod < now)
            {
                Generate();
                lastSpawn = now;
            }
        }

        void Generate()
        {
            var cords = transform.position.ToVector2();
            var bot = EntityFactory.CreateMonster(new EntityCreateOptions
            {
                EntityType = EntityType.Monster,
                Position = cords,
                Damage = 20,
                Health = 100
            });
            gameStateController.AddEntity(bot);
        }

        private GameStateController gameStateController;
        private Map map;
        private readonly TimeSpan generatePeriod = TimeSpan.FromSeconds(5);
        private DateTime lastSpawn;
    }
}
