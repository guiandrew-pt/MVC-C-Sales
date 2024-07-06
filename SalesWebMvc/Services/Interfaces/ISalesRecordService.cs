using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
	public interface ISalesRecordService
	{
		public Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);

    }
}

