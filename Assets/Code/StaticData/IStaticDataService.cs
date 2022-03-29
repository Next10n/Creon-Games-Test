namespace Code.StaticData
{
    public interface IStaticDataService
    {
        GameStaticData Data { get; }
        void Load();
    }
}