namespace ItemSystemSQL
{
    public interface IItemData
    {
        int Id { get; }
        string PrefabName { get; }
        string ItemName { get; }
        int ItemType { get; }
        string Description { get; }
    }

    public class EquipmentData : IItemData
    {
        public int Id { get; set; }
        public string PrefabName { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Description { get; set; }
        public int EquipType { get; set; }
        public float AttackBonus { get; set; }
        public float DefenseBonus { get; set; }
        public float WeaponRange { get; set; }
        public float WeaponDistance { get; set; }
    }
}