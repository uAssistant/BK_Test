namespace Shop.Save
{
    public interface ISavable
    {
        public void Load(PlayerSaveData saveData);
        public void Write(PlayerSaveData saveData);
    }
}