using System;

namespace HackatonBtp.Domain.Models
{
    public class Integrante
    {
        public Integrante()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set;}
        public string RG { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public string Universidade  { get; private set; }
        public string Curso { get; private set; }
        public string Linkedin { get; private set; }
        public string Git { get; private set; }
        public string Experiencia { get; private set; }
        public string Categoria { get; private set; }
        public string ComunidadeDev { get; private set; }
        public bool PossuiDeficiencia { get; private set; }
        public string DescricaoDeficiencia { get; private set; }
        public DateTime DataRegistro { get; private set; }
    }
}