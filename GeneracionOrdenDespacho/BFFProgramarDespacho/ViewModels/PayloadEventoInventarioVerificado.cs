namespace BFFProgramarDespacho.ViewModels
{
    public class PayloadEventoOrdenCreada
    {
        public Guid ordenId { get; set; }
        public string user { get; set; }
        public string user_addres { get; set; }
        public List<string> items { get; set; }
    }
}
