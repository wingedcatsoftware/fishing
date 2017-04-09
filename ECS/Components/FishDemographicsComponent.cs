
namespace HarpoonFishing.Ecs.Components
{
    class FishDemographicsComponent : Component
    {
        public int PopulationMax { get; set; }

        public int CurrentPopulation { get; set; }

        public double SpawnChancePerSecond { get; set; }
    }
}
