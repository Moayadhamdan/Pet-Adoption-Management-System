using LazurdIT.FluentOrm.MsSql;
using System.Data.SqlClient;
using PetAdoption.Shared;

namespace PetAdoption.Server.Repository
{
    public class PetRepository : MsSqlFluentRepository<PetModel>
    {
        public PetRepository(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}
