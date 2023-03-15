namespace BFFProgramarDespacho.ViewModels
{
    public class PayloadEventoInventarioVerificado
    {
        public Guid ordenId { get; set; }
        public string user { get; set; }
        public string user_addres { get; set; }
        public Dictionary<string, Dictionary<string, string>> items_bodegas { get; set; }
        public Dictionary<string, Dictionary<string, string>> items_centros { get; set; }
    }
}
