public class BackupService
{

    private readonly BackupConfiguration _configuration;
    public BackupService(BackupConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string BuscarDiretorioOrigem()
    {
        Console.WriteLine("Informe o diretório que deseja ser copiado");
        string? diretorio = Console.ReadLine();

        if(string.IsNullOrWhiteSpace(diretorio))
        {
            throw new ArgumentException("O diretório não foi encontrado");

        }
        if(!Directory.Exists(diretorio))
        {
            throw new DirectoryNotFoundException($"o diretório {diretorio} não foi encontrado.");
        }
        return diretorio;
    }

    public void CopiarDiretorio(string diretorioOrigem, string diretorioDestino)
    {
        // 1. Descobre o nome da pasta de origem
        string nomeDiretorio = Path.GetFileName(diretorioOrigem.TrimEnd(Path.DirectorySeparatorChar));

        // 2. criando data no formato americano
        string data = DateTime.Now.ToString("yyyyMMdd");
        
        // 3. concatenando o nome do diretorio com a data
        string nomeDiretorioBackup = $"{nomeDiretorio}_{data}";

        // 4 Cria o caminho completo da pasta no destino
        string novoDiretorio = Path.Combine(diretorioDestino,nomeDiretorioBackup);

        // 5. Cria a pasta principal no destino
        Directory.CreateDirectory(novoDiretorio);

        // 6. Busca todas as subpastas da origem
        string[] diretorios = Directory.GetDirectories(diretorioOrigem,"*",SearchOption.AllDirectories);

        // 7. Cria cada subpasta no destino
        foreach (string diretorio in diretorios)
        {
            string caminhoRelativo = Path.GetRelativePath(diretorioOrigem,diretorio);

            string novoCaminho = Path.Combine(novoDiretorio,caminhoRelativo);

            Directory.CreateDirectory(novoCaminho);
        }

        // 8. Busca todos os arquivos da origem
        string[] arquivos = Directory.GetFiles(diretorioOrigem, "*", SearchOption.AllDirectories);

        // 9. Percorre todos os arquivos encontrados
        foreach (string arquivo in arquivos)
        {
            // 10. Descobre o caminho relativo do arquivo
            string caminhoRelativo = Path.GetRelativePath(diretorioOrigem, arquivo);

            // 11. Monta o caminho final do arquivo
            string caminhoDestino = Path.Combine(novoDiretorio, caminhoRelativo);

            // 12. Copia o arquivo
            File.Copy(arquivo,caminhoDestino,true);

            Console.WriteLine($"Arquivo copiado: {caminhoRelativo}");
        }
    }
}



