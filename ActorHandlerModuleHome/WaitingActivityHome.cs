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
        TimeInterval Night = new TimeInterval(new LocalTime(23, 0), new LocalTime(8, 0));
        public bool Update(Actor actor, double deltaTime)
        {

            SpecState state = actor.GetState<SpecState>();
            HomeSeconds += deltaTime;
            if (HomeSeconds >= 1)
            {
                HomeSeconds -= 1;
                if (state.Satiety <= 0.1) state.Health -= 0.01;
                else state.Satiety += 0.001;

                if (state.Stamina <= 0.1) state.Health -= 0.01;
                else state.Stamina += 0.01;

                if (state.Mood <= 0.1) state.Health -= 0.001;
                else state.Mood -= 0.01;

                if (state.Health <= 0.01) state.Health = 0;

                if (state.Satiety >= 100) state.Satiety = 100;

                if (state.Stamina >= 100) state.Stamina = 100;

                if (state.Mood <= 0.01) state.Mood = 0;
            }
            if (Night.Ongoing)
            {
                Console.WriteLine("Waiting at home at night");
                return false;
            }
            if(state.Stamina <= 100 && state.Stamina > 80)
            {
                Priority = 3;
            }
            else if (state.Stamina <= 80 && state.Stamina > 60)
            {
                Priority = 23;
            }
            else if (state.Stamina <= 60 && state.Stamina > 40)
            {
                Priority = 43;
            }
            else if (state.Stamina <= 40 && state.Stamina > 20)
            {
                Priority = 63;
            }
            else if (state.Stamina <= 20 && state.Stamina > 5)
            {
                Priority = 83;
            }
            else
            {
                Priority = 93;
            }

            if (actor.GetState<JobState>().JobTimes[0].Ongoing)
            {
                if(Priority < 75)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    return true;
                }
            }

            if(state.Satiety <= 5)
            {
                if(Priority < 94)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 20 && state.Satiety > 5)
            {
                if (Priority < 84)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 40 && state.Satiety > 20)
            {
                if (Priority < 64)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 60 && state.Satiety > 40)
            {
                if (Priority < 44)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else if (state.Satiety <= 80 && state.Satiety > 60)
            {
                if (Priority < 24)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на поход на еду
                    return true;
                }
            }
            else
            {
                if (Priority < 4)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на поход на еду
                    return true;
                }
            }

            if (state.Mood <= 5)
            {
                if (Priority < 92)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 10 && state.Mood > 5)
            {
                if (Priority < 82)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 30 && state.Mood > 10)
            {
                if (Priority < 62)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 60 && state.Mood > 30)
            {
                if (Priority < 42)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на развлечения
                    return true;
                }
            }
            else if (state.Mood <= 80 && state.Mood > 60)
            {
                if (Priority < 22)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на развлечения
                    return true;
                }
            }
            else
            {
                if (Priority < 2)
                {
                    Console.WriteLine("Exit from waitingActivityHome");
                    //активность меняется на развлечения
                    return true;
                }
            }

            return false;

        }
    }
    
}
