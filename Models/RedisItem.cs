
namespace Sennit.RedisManager.Models
{
    public class RedisItem
    {
        
        /// <summary>
        /// Chave do arquivo de backup
        /// </summary>
        /// <value></value>
        public string key { get; set; }

        /// <summary>
        /// Valor contido na chave respectiva, do arquivo de backup
        /// </summary>
        /// <value></value>
        public string Value { get; set; }
    }
}