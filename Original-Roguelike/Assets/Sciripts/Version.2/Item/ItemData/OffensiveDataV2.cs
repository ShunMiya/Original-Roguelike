namespace ItemSystemV2
{
    public class OffensiveDataV2 : IItemDataV2
    {
        public int Id { get; set; }
        public string PrefabName { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Description { get; set; }
        public int OffensiveType { get; set; }
        public int MaxStock { get; set; }
        public int DamageNum { get; set; }
        public int Range { get; set; }
    }
}