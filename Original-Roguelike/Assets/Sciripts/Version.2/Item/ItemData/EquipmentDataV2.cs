namespace ItemSystemV2
{
    public interface IItemDataV2
    {
        int Id { get; }
        string PrefabName { get; }
        string ItemName { get; }
        int ItemType { get; }
        string Description { get; }
    }

    public class EquipmentDataV2 : IItemDataV2
    {
        public int Id { get; set; }
        public string PrefabName { get; set; }
        public string ItemName { get; set; }
        public int ItemType { get; set; }
        public string Description { get; set; }
        public int EquipType { get; set; }
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public int WeaponRange { get; set; }
        public int WeaponDistance { get; set; }
    }
}