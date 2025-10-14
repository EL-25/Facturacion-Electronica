namespace FacturacionElectronicaSV.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NumDocumento { get; set; }
        public int TipoDocumento { get; set; }
        public string CodActividad { get; set; }
        public string DescActividad { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Complemento { get; set; }
    }

}
