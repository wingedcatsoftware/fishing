
namespace HarpoonFishing.Ecs.Components
{
    class FishDemographicsComponent : Component
    {
        public FishDemographicsComponent(World world, EntityId id) : base(world, id)
        {
        }

        public int PopulationMax { get; set; }

        public int CurrentPopulation { get; set; }
    }
}
