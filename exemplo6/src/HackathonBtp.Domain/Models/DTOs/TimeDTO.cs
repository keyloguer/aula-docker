using System.Collections.Generic;

namespace HackatonBtp.Domain.Models.DTOs
{
    public class TimeDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public IList<IntegranteDTO> Integrantes { get; set; }
    }
}
