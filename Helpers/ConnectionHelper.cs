using StackExchange.Redis;

namespace Sennit.RedisManager.Helpers
{
    public class ConnectionHelper
    {
        public static ConnectionMultiplexer GetConnection(string connect)
        {
            return ConnectionMultiplexer.Connect(connect);
        }       
    }
}