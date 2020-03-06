# Introdução

Esta aplicação tem como objetivo realizar processos de backup utilizando o Redis, sendo possível realizá-los por meio de uma string de conexão de origem, string de conexão de destino ou ainda um arquivo de base de dados para backup.

# Para início

Nesta aplicação é possível realizar o backup de três maneiras:

De banco de dados Redis para banco de dados Redis;
De banco de dados Redis para arquivo;
De arquivo para banco de dados Redis;
Necessário conhecimento da string de conexão com o banco de dados, para uso.

# Como funciona

Para cada modo de realizar o backup, existem informações específicas que devem ser adicionadas no arquivo de configuração BackupConfig.conf.

De banco de dados para banco de dados:
No arquivo deve ser informado: em “DbFromNumber” o número de referência do banco de dados de origem, onde os dados para backup estão localizados; em “ConnectionStringFrom” a string de conexão do banco de origem; em “DbNumberToBackup” o número de referência do banco de dados em que o backup será salvo; em “ConnectionStringTo” a string de conexão do banco de destino do backup;

Exemplo:
{
    "DbFromNumber": "número de referência do banco de dados de origem",
    "ConnectionStringFrom":"string de conexão Redis da origem",
    "DbNumberToBackup": "número de referência do banco de dados Redis, onde será salvo o backup",
    "ConnectionStringTo":"string de conexão Redis do destino"
}

De banco de dados para arquivo:
No arquivo deve ser informado: em “DbFromNumber” o número de referência do banco de dados de origem; em “ConnectionStringFrom” a string de conexão do banco de origem; em “GenerateBackupFilePath” o caminho onde será criado o arquivo de backup; para abrir o local onde o arquivo de backup foi salvo, “OpenFolderAfterBackup”: true;

Exemplo 1: "para abrir o local onde o arquivo de backup foi salvo: “OpenFolderAfterBackup”: true"
{
    "DbFromNumber": "número de referência do banco de dados de origem",
    "ConnectionStringFrom":"string de conexão Redis da origem",
    "GenerateBackupFilePath": "C:\temp\Sennit.RedisManager\BackupFolder"; 
    "OpenFolderAfterBackup": true
}

Exemplo 2: "para não abrir o local onde o arquivo de backup foi salvo  o item "OpenFolderAfterBackup", não precisa ser informado"
{
    "DbFromNumber": "número de referência do banco de dados de origem",
    "ConnectionStringFrom":"string de conexão Redis da origem",
    "GenerateBackupFilePath": "C:\temp\Sennit.RedisManager\BackupFolder". 
}

De arquivo para banco de dados:
No arquivo deve ser informado: em “DbNumberToBackup” o número de referência do banco de dados de destino onde será salvo o backup; em “ConnectionStringTo” a string de conexão do banco de destino do backup; em “FileFrom” o nome do arquivo com o tipo (ex: arquivo.json) de onde virá os dados para backup; em “ConfirmFileFromBackup”, informar ‘true’ para confirmar que os dados virão de um arquivo.

Exemplo: 
{
    "DbNumberToBackup": "número de referência do banco de dados Redis, onde será salvo o backup",   
    "ConnectionStringTo": "string de conexão Redis do destino",
    "FileFrom": "o nome do arquivo com o tipo (ex: arquivo.json) de onde virá os dados para backup",
    "ConfirmFileFromBackup": true
}


* Descrição de cada item do arquivo BackupConfig.conf:


- "DbFromNumber": "número de referência do banco de dados de origem";

- "ConnectionStringFrom": "string de conexão Redis da origem";

- "DbNumberToBackup": "número de referência do banco de dados Redis, onde será salvo o backup";

- "ConnectionStringTo": "string de conexão Redis do destino";

- "FileFrom": "o nome do arquivo com o tipo (ex: arquivo.json) de onde virá os dados para backup";

- "ConfirmFileFromBackup": "informar ‘true’ para confirmar que os dados virão de um arquivo";

- "GenerateBackupFilePath": "caminho onde será criado o arquivo de backup";

- "OpenFolderAfterBackup": "abrir o local onde o arquivo de backup foi salvo".