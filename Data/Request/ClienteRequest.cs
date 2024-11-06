namespace Data.Request;

    public class ClienteRequest
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
    }