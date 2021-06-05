using System;

using ActorModule;
using NodaTime.Extensions;
using NodaTime;
using InitializeActorModule;


namespace ActorHandlerModuleHome
{
    public class WaitingActivityHome : IActivity
    {
        public int Priority { get; private set; } = 35;
        private double HomeSeconds { get; set; }
        public WaitingActivityHome()
        {
        }
        public WaitingActivityHome(double second)
        {
            HomeSeconds = second;
        }
        public bool Update(Actor actor, double deltaTime)
        {

            SpecState state = actor.GetState<SpecState>();
            var zonedClock = SystemClock.Instance.InTzdbSystemDefaultZone();
            Console.WriteLine($"Flags: {state.Satiety}");
            Console.WriteLine($"Flags: {state.Stamina}");
            Console.WriteLine($"Flags: {state.Mood}");
            HomeSeconds += deltaTime;
            if (HomeSeconds >= 1)
            {
                HomeSeconds -= 1;
                if (state.Health <= 0.01) state.Health = 0;

                if (state.Satiety >= 99.99) state.Satiety = 100;

                if (state.Stamina >= 99.95) state.Stamina = 100;

                if (state.Mood <= 0) state.Mood = 0;

                if (state.Satiety <= 0.1) state.Health -= 0.01;
                else state.Satiety += 0.001;

                if (state.Stamina <= 0.1) state.Health -= 0.01;
                else state.Stamina += 0.01;

                if (state.Mood <= 0.1) state.Health -= 0.001;
                else state.Mood -= 0.01;
            }
            Console.WriteLine($"Flags1: {state.Satiety}");
            Console.WriteLine($"Flags1: {state.Stamina}");
            Console.WriteLine($"Flags1: {state.Mood}");
            if (zonedClock.GetCurrentTimeOfDay() < new LocalTime(8, 0) && zonedClock.GetCurrentTimeOfDay() > new LocalTime(23, 0))
            {
                return false;
            }
            if(state.Stamina <= 100 && state.Stamina > 80)
            {
                Console.WriteLine("fat1");
                Priority = 3;
            }
            else if (state.Stamina <= 80 && state.Stamina > 60)
            {
                Console.WriteLine("fat2");
                Priority = 23;
            }
            else if (state.Stamina <= 60 && state.Stamina > 40)
            {
                Console.WriteLine("fat3");
                Priority = 43;
            }
            else if (state.Stamina <= 40 && state.Stamina > 20)
            {
                Console.WriteLine("fat4");
                Priority = 63;
            }
            else if (state.Stamina <= 20 && state.Stamina > 5)
            {
                Console.WriteLine("fat5");
                Priority = 83;
            }
            else
            {
                Console.WriteLine("fat6");
                Priority = 93;
            }

            if (actor.GetState<JobState>().JobTimes[0].Ongoing)//неизвестно
            {
                if(Priority < 75)
                {
                    //активность меняется на поход на работу
                    //actor.Activity = //активность похода на работу
                    return true;
                }
            }

            if(state.Satiety <= 5)
            {
                if(Priority < 94)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 20 && state.Satiety > 5)
            {
                if (Priority < 84)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 40 && state.Satiety > 20)
            {
                if (Priority < 64)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 60 && state.Satiety > 40)
            {
                if (Priority < 44)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 80 && state.Satiety > 60)
            {
                if (Priority < 24)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else
            {
                if (Priority < 4)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }

            if (state.Mood <= 5)
            {
                if (Priority < 92)
                {
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 10 && state.Mood > 5)
            {
                if (Priority < 82)
                {
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 30 && state.Mood > 10)
            {
                if (Priority < 62)
                {
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 60 && state.Mood > 30)
            {
                if (Priority < 42)
                {
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 80 && state.Mood > 60)
            {
                if (Priority < 22)
                {
                    //активность меняется на развлечения
                    return true;
                }
            }
            else
            {
                if (Priority < 2)
                {
                    //активность меняется на развлечения
                    return true;
                }
            }

            return false;

        }
    }
    
}
