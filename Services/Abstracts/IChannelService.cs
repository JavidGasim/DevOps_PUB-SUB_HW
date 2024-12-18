namespace DevOps_PUB_SUB_HW.Services.Abstracts
{
    public interface IChannelService
    {
        Task<List<string>> GetChannels();
        Task CreateChannel(string channelName);
        Task<List<string>> GetMessagesFromChannel(string channelName);
        Task AddMessage(string channelName, string message);
    }
}
