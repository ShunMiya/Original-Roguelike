namespace ItemSystemV2
{
    public class ReinforceDataV2 : IItemDataV2
    {
        public int Id { get; set; }
        public string PrefabName { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Description { get; set; }
        public int ReinType { get; set; }
        public int ReinNum { get; set; }
    }
}