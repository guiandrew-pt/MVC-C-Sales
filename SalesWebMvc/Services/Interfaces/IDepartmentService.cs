using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
	public interface IDepartmentService
	{
		public List<Department> FindAll();
	}
}

