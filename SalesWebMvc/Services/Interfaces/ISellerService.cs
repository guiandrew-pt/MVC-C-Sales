﻿using SalesWebMvc.Models;

namespace SalesWebMvc.Services.Interfaces
{
	public interface ISellerService
	{
        public List<Seller> FindAll();

        public void Insert(Seller obj);

        public Seller FindById(int id);

        public void Remove(int id);

        public void Update(Seller seller);
    }
}

