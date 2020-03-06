using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sennit.RedisManager.Models;
using StackExchange.Redis;
using Sennit.RedisManager.Helpers;

namespace Sennit.RedisManager.Services
{
    public interface IRedisBackupService
    {
        void Execute();
    }
    public class RedisBackupService : IRedisBackupService
    {
        private readonly IDatabase _redisDbFrom;
        private readonly ConnectionMultiplexer _cnxTo;
        private readonly IDatabase _redisDbBackupTo;
        private string _fileName;
        private readonly ConfigurationFileModel _conf;
        private readonly ConnectionMultiplexer _cnxFrom;

        public RedisBackupService(ConfigurationFileModel conf)
        {
            _conf = conf;
            if (!string.IsNullOrEmpty(_conf.ConnectionStringFrom))
            {
                _cnxFrom = ConnectionHelper.GetConnection(_conf.ConnectionStringFrom);
                _redisDbFrom = _cnxFrom.GetDatabase(conf.DbFromNumber);
            }

            if (!string.IsNullOrEmpty(_conf.ConnectionStringTo))
            {
                _cnxTo = ConnectionHelper.GetConnection(_conf.ConnectionStringTo);
                _redisDbBackupTo = _cnxTo.GetDatabase(conf.DbNumberToBackup);
            }
        }
        public void Execute()
        {
            if (_conf.ConfirmFileFromBackup)
            {
                ReadFile();
                return;
            }

            try
            {
                var query = from key in _cnxFrom.GetServer(FindHost(_conf.ConnectionStringFrom))
                                            .Keys(_conf.DbFromNumber).AsParallel()
                            select AddItemToBackup(key);

                if (_conf.OpenFolderAfterBackup)
                {
                    if (!Directory.Exists(_conf.GenerateBackupFilePath))
                        throw new Exception($"Não foi possivel localizar o caminho: {_conf.GenerateBackupFilePath}. ");

                    WriteFile(query);
                }
                else
                    Parallel.ForEach(query, BackupToRedisBd);

                Console.WriteLine($"\nForam carregados {query.Count()} registros. \n");
            }
            catch (System.Exception)
            {
                Console.WriteLine("Não foi possível realizar o backup por esse padrão de informções!");
            }

            try
            {
                if (_conf.OpenFolderAfterBackup)
                    WindowsHelper.OpenPath(_conf.GenerateBackupFilePath, _fileName);
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Não foi possível abrir caminho escolhido!");
            }
        }

        private string FindHost(string cnx)
        {
            string[] array = cnx.Split(',');
            return array.First();
        }

        private void BackupToRedisBd(RedisItem item)
        {
            _redisDbBackupTo.StringSet(key: item.key,
                    value: _redisDbFrom.StringGet(item.key));
        }

        private RedisItem AddItemToBackup(RedisKey key)
        {
            Console.WriteLine($"Copiando chave: {key.ToString()}");

            var item = new RedisItem
            {
                key = key.ToString(),
                Value = _redisDbFrom.StringGet(key)
            };

            return item;
        }
        private string GenerateFileName()
        {
            return $"bkp-db{_conf.DbFromNumber}-{DateTime.Now.ToString("yyyyMMddhhmmss")}.bkp";
        }

        private bool ReadFile()
        {
            if (!File.Exists(_conf.FileFrom))
                throw new Exception($"Não foi possivel localizar o arquivo de origem: {_conf.FileFrom}");

            var confFile = JsonConvert
                            .DeserializeObject<RedisItem[]>(File.ReadAllText(_conf.FileFrom));

            var query = from item in confFile.AsParallel()
                        select _redisDbBackupTo.StringSet(key: item.key, value: item.Value);

            query.ToArray();

            return true;
        }

        private void WriteFile(IEnumerable<RedisItem> items)
        {
            string json = JsonConvert.SerializeObject(items.ToArray());
            _fileName = this.GenerateFileName();

            var fullFileName = Path.Combine(_conf.GenerateBackupFilePath, _fileName);

            File.WriteAllText(fullFileName, json);

            Console.WriteLine();
            Console.WriteLine($"Arquivo Gerado: {_fileName}");
        }
    }
}
