using System;
using System.Collections.Generic;

namespace HackatonBtp.Domain.Models
{
    public class Time
    {
        public Time()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public bool ConfirmacaoEmail { get; private set; }
        public IList<Integrante> Integrantes { get; private set; }
    }
}