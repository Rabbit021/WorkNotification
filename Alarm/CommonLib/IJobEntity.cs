namespace Alarm.CommonLib
{
    public interface IJobEntity : IEntity
    {
    }

    public interface ITriggerEntity : IEntity
    {
        string expression { get; set; }
        string display { get; set; }
    }

    public interface IEntity
    {
        string id { get; set; }
        string Group { get; set; }
    }
}