﻿namespace TrumanAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}