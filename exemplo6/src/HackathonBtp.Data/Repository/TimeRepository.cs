using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using HackatonBtp.Domain.Interfaces.Repository;
using HackatonBtp.Domain.Models;
using HackatonBtp.Data.Policies;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Transactions;
using System;
using System.Collections.Generic;

namespace HackatonBtp.Data.Repository
{
    public class TimeRepository : ITimeRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionStrings;

        public TimeRepository(IConfiguration config)
        {
            _config = config;
        }

        public TimeRepository(string connectionStrings) //Used for azure
        {
            _connectionStrings = connectionStrings;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_config == null)
                    return new SqlConnection(_connectionStrings);

                return new SqlConnection(_config.GetConnectionString("BtpConnectionString"));
            }
        }
        
        public async Task<Time> BuscarPorNome(string nomeTime)
        {
            if (nomeTime == null) return null;

            using (SqlConnection conn = (SqlConnection)Connection)
            {
                string query = "SELECT * FROM Times WHERE LOWER(Nome) = @nome";

                var result = await conn.OpenWithRetryAsync(x => x.QueryAsync<Time>(query, new { nome = nomeTime.ToLower() }));

                return result.FirstOrDefault();
            }
        }

        public async Task<Time> BuscarPorEmail(string emailTime)
        {
            if (emailTime == null) return null;

            using (SqlConnection conn = (SqlConnection)Connection)
            {
                string query = "SELECT * FROM Times WHERE LOWER(Email) = @emailTime";

                var result = await conn.OpenWithRetryAsync(x => x.QueryAsync<Time>(query, new { emailTime = emailTime.ToLower() }));

                return result.FirstOrDefault();
            }
        }

        public async Task<Time> ObterTime(Guid timeId)
        {
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                string query = "SELECT * FROM Times WHERE Id = @id";

                var result = await conn.OpenWithRetryAsync(x => x.QueryAsync<Time>(query, new { id = timeId }));
                
                return result.FirstOrDefault();
            }
        }

        public async Task<int> Save(Time obj)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (SqlConnection conn = (SqlConnection)Connection)
            {

                int affectedRows = 0;

                string queryTime = "INSERT INTO Times VALUES(@id, @nome, @email, @confirmacaoEmail, @emailEnviado)";
                affectedRows+= await conn.OpenWithRetryAsync(x => x.ExecuteAsync(queryTime, new { id = obj.Id, nome = obj.Nome, email = obj.Email, confirmacaoEmail = obj.ConfirmacaoEmail, emailEnviado = false }));

                string queryIntegrante = "INSERT INTO Integrantes VALUES(@id, @nome, @rg, @dataNascimento, @telefone, @universidade, @curso, @possuiDeficiencia, @descDeficiencia, @timeId, @dataRegistro, @linkedin, @git, @experiencia, @categoria, @comunidadeDev, @emailIntegrante)";

                foreach (Integrante integrante in obj.Integrantes)
                {
                    string email = "";

                    string curso = integrante.Curso.Trim() == string.Empty ? null : integrante.Curso;
                    string universidade = integrante.Universidade.Trim() == string.Empty ? null : integrante.Universidade;
                    string descDeficiencia = integrante.DescricaoDeficiencia.Trim() == string.Empty ? null : integrante.DescricaoDeficiencia;

                    string linkedin = integrante.Linkedin.Trim() == string.Empty ? null : integrante.Linkedin;
                    string git = integrante.Git.Trim() == string.Empty ? null : integrante.Git;
                    string experiencia = integrante.Experiencia.Trim() == string.Empty ? null : integrante.Experiencia;
                    string comunidade = integrante.ComunidadeDev.Trim() == string.Empty ? null : integrante.ComunidadeDev;
                    email = integrante.Email;

                    if (obj.Integrantes.Count == 1)
                        email = null;

                    affectedRows+= await conn.ExecuteWithRetryAsync(x => x.ExecuteAsync(queryIntegrante, new { id = integrante.Id, nome = integrante.Nome, rg = integrante.RG, dataNascimento = integrante.DataNascimento.Date, telefone = integrante.Telefone, universidade = universidade, curso = curso, possuiDeficiencia = integrante.PossuiDeficiencia, descDeficiencia = descDeficiencia, timeId = obj.Id, dataRegistro = DateTime.Now, linkedin = linkedin, git = git, experiencia = experiencia, categoria = integrante.Categoria, comunidadeDev = comunidade, emailIntegrante = email }));
                }

                scope.Complete();

                return affectedRows;
            }
        }

        public async Task<int> ConfirmarEmail(Guid timeId)
        {
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                string query = "UPDATE Times set ConfirmacaoEmail = 'true' where Id = @id";

                return await conn.OpenWithRetryAsync(x => x.ExecuteAsync(query, new { id = timeId }));
            }
        }

        public async Task<int> ConfirmarEnvioEmail(Guid timeId)
        {
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                string query = "UPDATE Times set EmailEnviado = 'true' where Id = @id";

                return await conn.OpenWithRetryAsync(x => x.ExecuteAsync(query, new { id = timeId }));
            }
        }

        public async Task<IEnumerable<Time>> ObterTimesSemEnvioEmail()
        {
            using (SqlConnection conn = (SqlConnection)Connection)
            {
                string query = "SELECT * FROM Times where EmailEnviado = @emailEnviado";

                return await conn.OpenWithRetryAsync(x => x.QueryAsync<Time>(query, new { emailEnviado = false }));

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}