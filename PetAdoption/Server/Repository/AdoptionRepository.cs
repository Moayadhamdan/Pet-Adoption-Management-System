using LazurdIT.FluentOrm.MsSql;
using System.Data.SqlClient;
using PetAdoption.Shared;

namespace PetAdoption.Server.Repository
{
    public class AdoptionRepository : MsSqlFluentRepository<AdoptionModel>
    {
        public AdoptionRepository(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}
