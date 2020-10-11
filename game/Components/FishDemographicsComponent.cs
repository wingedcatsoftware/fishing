
namespace HarpoonFishing.Components
{
    using MonoGameEcs;

    class FishDemographicsComponent : Component
    {
        public int PopulationMax { get; set; }

        public int CurrentPopulation { get; set; }

        public double SpawnChancePerSecond { get; set; }
    }
}
