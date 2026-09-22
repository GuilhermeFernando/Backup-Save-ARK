Console.WriteLine(" ___   _   ___ _  _ _  _ ___    ___  _  _  ___   _   ___ _  _ ");
Console.WriteLine("| _ ) /_\\ / __| |/ / | | | _ \\  / __|/_\\ \\ / /__| | / _ \\ |/ /");
Console.WriteLine("| _ \\/ _ \\ (__| ' <| |_| |  _/  \\__ / _ \\ V /| _| ||  _ / ' < ");
Console.WriteLine("|___/_/ \\_\\___|_|\\_\\\\___/|_|    |___/_/ \\_\\_/ |___|_|_| \\_\\_|\\_\\");


ConfigurationService configurationService = new ConfigurationService();

BackupConfiguration configuration = 
    configurationService.GetBackupConfiguration();

BackupService backupService = new BackupService(configuration);

backupService.CopiarDiretorio(
    configuration.SourceDirectory,
    configuration.DestinationDirectory
);

Console.WriteLine("Backup realizado com sucesso!");