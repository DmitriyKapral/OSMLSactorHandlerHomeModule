using System;

using NodaTime; // Отсюда LocalTime
using NetTopologySuite.Geometries;  // Отсюда Point и другая геометрия
using NetTopologySuite.Mathematics; // Отсюда векторы

using OSMLSGlobalLibrary.Modules;  // Отсюда OSMLSModule

using ActorModule;

using OSMLSGlobalLibrary.Map;

using PathsFindingCoreModule;
using InitializeActorModule;

namespace ActorHandlerModuleHome
{
    // Активность которая заставляет актора куда-то идти
    public class MovementActivityHome : IActivity
    {
        public Coordinate[] Path;
        public int i = 0;

        private bool IsPath = true;
        public int Priority { get; private set; } = 1;
        private double HomeSeconds { get; set; }
        bool End = true;
        public MovementActivityHome()
        {
        }


        public bool Update(Actor actor, double deltaTime)
        {

            double speed = actor.GetState<SpecState>().Speed;
            SpecState state = actor.GetState<SpecState>();
            // Расстояние, которое может пройти актор с заданной скоростью за прошедшее время
            double distance = speed * deltaTime;
            HomeSeconds += deltaTime;
            if (HomeSeconds >= 1)
            {
                HomeSeconds -= 1;
                if (state.Satiety <= 0.1) state.Health -= 0.01;
                else state.Satiety -= 0.001;

                if (state.Stamina <= 0.1) state.Health -= 0.01;
                else state.Stamina -= 0.001;

                if (state.Mood <= 0.1) state.Health -= 0.001;
                else state.Mood -= 0.001;

                if (state.Health <= 0.01) state.Health = 0;

                if (state.Satiety <= 0.001) state.Satiety = 0;

                if (state.Stamina <= 0.001) state.Stamina = 0;

                if (state.Mood <= 0.001) state.Mood = 0;
            }
            
            if (IsPath)
            {
                var firstCoordinate = new Coordinate(actor.X, actor.Y);
                Console.WriteLine("11");
                var secondCoordinate = new Coordinate(actor.GetState<PlaceState>().Home.X, actor.GetState<PlaceState>().Home.Y);
                Console.WriteLine("22");
                if(!PathsFinding.GetPath(firstCoordinate, secondCoordinate, "Walking").IsCompleted && !End)
                    return false;
                End = false;
                Path = PathsFinding.GetPath(firstCoordinate, secondCoordinate, "Walking").Result.Coordinates;
                End = true;
                Console.WriteLine("Ending");
                IsPath = false;
            }
            Vector2D direction = new Vector2D(actor.Coordinate, Path[i]);
            // Проверка на перешагивание
            if (direction.Length() <= distance)
            {
                // Шагаем в точку, если она ближе, чем расстояние которое можно пройти
                actor.X = Path[i].X;
                actor.Y = Path[i].Y;
            }
            else
            {
                // Вычисляем новый вектор, с направлением к точке назначения и длинной в distance
                direction = direction.Normalize().Multiply(distance);

                // Смещаемся по вектору
                actor.X += direction.X;
                actor.Y += direction.Y;
            }

            if (actor.X == Path[i].X && actor.Y == Path[i].Y && i < Path.Length - 1)
            {
                i++;
                Console.WriteLine(i);
                Console.WriteLine(Path.Length);
            }

            // Если в процессе шагания мы достигли точки назначения
            if (actor.X == Path[^1].X && actor.Y == Path[^1].Y)
            {
                
                actor.Activity = new WaitingActivityHome(HomeSeconds);
                i = 0;
                IsPath = true;
                //return true;
            }

            return false;
                
        }
    }
}
