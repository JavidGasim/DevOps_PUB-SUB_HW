using DevOps_PUB_SUB_HW.Services.Abstracts;
using StackExchange.Redis;

namespace DevOps_PUB_SUB_HW.Services.Concretes
{
    public class ChannelService : IChannelService
    {
        private readonly IConnectionMultiplexer _redis;

        public ChannelService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task AddMessage(string channelName, string message)
        {
            if (channelName.Length != 0 && !string.IsNullOrEmpty(channelName) && message.Length != 0 && !string.IsNullOrEmpty(message))
            {
                var db = _redis.GetDatabase();
                await db.ListLeftPushAsync($"Channel Name: {channelName}, Messages: ", message);
            }
        }

        public async Task CreateChannel(string channelName)
        {
            if (channelName.Length != 0 && !string.IsNullOrEmpty(channelName))
            {
                var db = _redis.GetDatabase();
                await db.SetAddAsync("Channels", channelName);
            }
        }

        public async Task<List<string>> GetChannels()
        {
            var db = _redis.GetDatabase();
            var channels = await db.SetMembersAsync("Channels");
            return channels.Select(c => c.ToString()).ToList();
        }

        public async Task<List<string>> GetMessagesFromChannel(string channelName)
        {
            var db = _redis.GetDatabase();
            var messages = await db.ListRangeAsync($"Channel Name: {channelName}, Messages: ");
            return messages.Select(m => m.ToString()).ToList();
        }
    }
}
