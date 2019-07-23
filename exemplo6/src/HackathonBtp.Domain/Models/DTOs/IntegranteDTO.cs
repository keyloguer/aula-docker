using System;

namespace HackatonBtp.Domain.Models.DTOs
{
    
    public class IntegranteDTO
    {
        public string Nome { get; set; }
        public string Email { get; set;}
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Universidade  { get; set; }
        public string Curso { get; set; }
        public bool PossuiDeficiencia { get; set; }
        public string DescricaoDeficiencia { get; set; }
        public string Linkedin { get; set; }
        public string Git { get; set; }
        public string Experiencia { get; set; }
        public string Categoria { get; set; }
        public string ComunidadeDev { get; set; }
    }
}
