namespace Poc.Apryse.Requests
{
    public class CombinarArquivosRequest
    {
        public IFormFileCollection ArquivosPdf { get; set; }

        public string NomeArquivo { get; set; }
    }
}
