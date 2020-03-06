namespace Sennit.RedisManager.Models
{
    public class ConfigurationFileModel
    {
        /// <summary>
        /// Número da instância do 
        /// banco de dados de origem do backup
        /// /// </summary>
        /// <value></value>
        public int DbFromNumber { get; set; }

        /// <summary>
        /// Local em que será salvo 
        /// o arquivo de backup gerado.
        /// </summary>
        /// <value></value>
        public string GenerateBackupFilePath { get; set; }

        /// <summary>
        /// Abre o local em que o 
        /// arquivo de backup foi feito.
        /// </summary>
        /// <value></value>
        public bool OpenFolderAfterBackup { get; set; } = false;

        /// <summary>
        /// Banco de dados em que 
        /// será realizado o backup.
        /// </summary>
        /// <value></value>
        public int DbNumberToBackup { get; set; }

        /// <summary>
        /// String de conexão de origem com o banco de 
        /// dados a ser realizado o backup.
        /// </summary>
        /// <value></value>
        public string ConnectionStringFrom { get; set; }

        /// <summary>
        /// String de conexão de destino com o banco
        /// de dados a ser recuperado o backup
        /// </summary>
        /// <value></value>
        public string ConnectionStringTo { get; set; }

        /// <summary>
        /// Arquivo de backup em .Json.
        /// </summary>
        /// <value></value>
        public string FileFrom { get; set; }

        /// <summary>
        /// Confirmar recuperação de backup 
        /// a partir de arquivo de backup.
        /// </summary>
        /// <value></value>
        public bool ConfirmFileFromBackup { get; set; } = false;

    }
}