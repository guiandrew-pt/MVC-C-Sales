using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
	public interface ISellerService
	{
        public List<Seller> FindAll();

        public void Insert(Seller obj);
    }
}

