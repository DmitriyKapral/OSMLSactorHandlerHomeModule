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
        public WaitingActivityHome()
        {
        }
        public WaitingActivityHome(int priority)
        {
            Priority = priority;
        }
        public bool Update(Actor actor, double deltaTime)
        {

            SpecState state = actor.GetState<SpecState>();
            var zonedClock = SystemClock.Instance.InTzdbSystemDefaultZone();
            Console.WriteLine($"Flags: {state.Hunger}");
            Console.WriteLine($"Flags: {state.Fatigue}");
            Console.WriteLine($"Flags: {state.Mood}");
            Console.WriteLine($"Flags1: {state.Hunger}");
            Console.WriteLine($"Flags1: {state.Fatigue}");
            Console.WriteLine($"Flags1: {state.Mood}");
            if (zonedClock.GetCurrentTimeOfDay() < new LocalTime(8, 0) && zonedClock.GetCurrentTimeOfDay() > new LocalTime(23, 0))
            {
                return false;
            }
            if(state.Fatigue <= 100 && state.Fatigue > 80)
            {
                Console.WriteLine("fat1");
                Priority = 3;
            }
            else if (state.Fatigue <= 80 && state.Fatigue > 60)
            {
                Console.WriteLine("fat2");
                Priority = 23;
            }
            else if (state.Fatigue <= 60 && state.Fatigue > 40)
            {
                Console.WriteLine("fat3");
                Priority = 43;
            }
            else if (state.Fatigue <= 40 && state.Fatigue > 20)
            {
                Console.WriteLine("fat4");
                Priority = 63;
            }
            else if (state.Fatigue <= 20 && state.Fatigue > 5)
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

            if(state.Hunger <= 5)
            {
                if(Priority < 94)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Hunger <= 20 && state.Hunger > 5)
            {
                if (Priority < 84)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Hunger <= 40 && state.Hunger > 20)
            {
                if (Priority < 64)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Hunger <= 60 && state.Hunger > 40)
            {
                if (Priority < 44)
                {
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Hunger <= 80 && state.Hunger > 60)
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
