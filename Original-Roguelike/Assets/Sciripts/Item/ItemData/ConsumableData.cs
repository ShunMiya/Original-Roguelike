namespace ItemSystemSQL
{
    public class ConsumableData : IItemData
    {
        public int Id { get; set; }
        public string PrefabName { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Description { get; set; }
        public int MaxStock { get; set; }
        public int HealValue { get; set; }
    }
}