using System;
using System.IO;
using Sennit.RedisManager.Services;
using Newtonsoft.Json;
using Sennit.RedisManager.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sennit.RedisManager
{
    class Program
    {
        private static List<Exception> _errors = new List<Exception>();
        private static readonly string _fileName = "BackupConfig.conf";

        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando processo de backup");

            if (!File.Exists(_fileName))
            {
                Console.WriteLine("Arquivo de configuração não localizado");
                return;
            }

            var configFile = JsonConvert
                                .DeserializeObject<ConfigurationFileModel[]>(File.ReadAllText(_fileName).Replace(@"\", @"\\" ));


            if (null == configFile || !configFile.Any())
            {
                Console.WriteLine("Arquivo de configuração não localizado");
                return;
            }
            
            Parallel.ForEach(configFile,ExecuteBackup);

            if (_errors.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("O(s) processo(s) de backup foram finalizados com erros.");

                foreach (var error in _errors)
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine(JsonConvert.SerializeObject(error));
                    System.Console.WriteLine();
                }
            }
            else
            {
                System.Console.WriteLine("\nOs processos de backup foram finalizados.");
            }

            System.Console.WriteLine("---------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para sair");
            Console.ReadKey();
        }

        private static void ExecuteBackup(ConfigurationFileModel item)
        {
            try
            {
                var service = new RedisBackupService(item);

                service.Execute();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Erro ao tentar gerar backup");
                _errors.Add(ex);
            }
        }
    }
}
