using LazurdIT.FluentOrm.MsSql;
using System.Data.SqlClient;
using PetAdoption.Shared;

namespace PetAdoption.Server.Repository
{
    public class OwnerRepository : MsSqlFluentRepository<OwnerModel>
    {
        public OwnerRepository(string connectionString) : base(new SqlConnection(connectionString))
        {
        }
    }
}
