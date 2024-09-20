namespace SalesWebMvc.Models.ViewModels
{
	public struct SellerFormViewModel
	{
		public Seller? Seller { get; set; }
		public ICollection<Department>? Departments { get; set; }
    }
}

