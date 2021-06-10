using System;

using NodaTime; // Отсюда LocalTime
using NetTopologySuite.Geometries;
using NetTopologySuite.Mathematics;

using OSMLSGlobalLibrary.Modules; 

using ActorModule;

using OSMLSGlobalLibrary.Map;

using PathsFindingCoreModule;

using ActorHandlerModuleHome;
using CityDataExpansionModule;
using InitializeActorModule;

namespace ActorHandlerTestModuleHome
{
    public class ActorHandlerTestModuleHome : OSMLSModule
    {
        protected override void Initialize()
        {
        }
        int count = 0;
        public override void Update(long elapsedMilliseconds)
        {

            var actors = MapObjects.GetAll<Actor>();
            foreach (var actor in actors)
            {

                bool isActivity = actor.Activity != null;

                bool goActivity = isActivity ? actor.Activity is MovementActivityHome : false;
                bool timefal = isActivity ? actor.Activity is WaitingActivityHome : false;



                //Console.WriteLine($"Flags: {isActivity} {goActivity} {timefal}");

                if (!isActivity && count!=actors.Count)
                {
                    // Назначить актору путь до дома
                    actor.Activity = new MovementActivityHome();
                    Console.WriteLine("Said actor go home\n");
                    count++;
                }
            }
        }

        
    }
}
