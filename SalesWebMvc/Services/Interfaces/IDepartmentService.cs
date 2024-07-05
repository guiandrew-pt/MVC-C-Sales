using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
	public interface IDepartmentService
	{
        public Task<List<Department>> FindAllAsync();

    }
}

