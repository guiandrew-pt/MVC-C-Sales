using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
	public interface ISellerService
	{
        public Task<List<Seller>> FindAllAsync();

        public Task InsertAsync(Seller seller);

        public Task<Seller> FindByIdAsync(int id);

        public Task UpdateAsync(Seller seller);

        public Task RemoveAsync(int id);
    }
}

