using System;
using System.Collections.Generic;
using System.Text;
using OSMLSGlobalLibrary.Modules;
using InitializeActorModule;
using ActorModule;
using NodaTime;

namespace ActorHandlerModuleHome
{
    public class ActorHandlerModuleHome : OSMLSModule
    {
        /// <summary>
        /// Инициализация модуля. В отладочной конфигурации выводит сообщение
        /// </summary>
        protected override void Initialize()
        {
#if DEBUG
            Console.WriteLine("ActorHandlerModule: Initialize");
#endif
        }
        /// <summary>
        /// Вызывает Update на всех акторах
        /// </summary>
        public override void Update(long elapsedMilliseconds)
        {
            int Priority = 0;
            var actors = MapObjects.GetAll<Actor>();
            TimeInterval Night = new TimeInterval(new LocalTime(23, 0), new LocalTime(8, 0));
            foreach (var actor in actors)
            {
                if (actor.GetState<SpecState>().Stamina <= 100 && actor.GetState<SpecState>().Stamina > 80)
                {
                    Priority = 3;
                }
                else if (actor.GetState<SpecState>().Stamina <= 80 && actor.GetState<SpecState>().Stamina > 60)
                {
                    Priority = 23;
                }
                else if (actor.GetState<SpecState>().Stamina <= 60 && actor.GetState<SpecState>().Stamina > 40)
                {
                    Priority = 43;
                }
                else if (actor.GetState<SpecState>().Stamina <= 40 && actor.GetState<SpecState>().Stamina > 20)
                {
                    Priority = 63;
                }
                else if (actor.GetState<SpecState>().Stamina <= 20 && actor.GetState<SpecState>().Stamina > 5)
                {
                    Console.WriteLine("fat5");
                    Priority = 83;
                }
                else
                {
                    Console.WriteLine("fat6");
                    Priority = 93;
                }

                //Проверка на ночное время
                if (Night.Ongoing)
                {
                    Priority = 100;
                }


                bool isActivity = actor.Activity != null;

                bool isMovementActivityHome = isActivity && actor.Activity is MovementActivityHome;
                bool isWaitingActivityHome = isActivity && actor.Activity is WaitingActivityHome;


                //Console.WriteLine($"Flags: {isActivity} {goActivity} {timefal}");

                if (!isActivity || (!isMovementActivityHome && !isWaitingActivityHome && Priority > actor.Activity.Priority))
                {
                    // Назначить актору путь до дома
                    actor.Activity = new MovementActivityHome();
                    Console.WriteLine("Said actor go home\n");
                }
            }
        }
    }
}
