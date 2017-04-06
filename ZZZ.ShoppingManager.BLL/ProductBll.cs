using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZZZ.ShoppingManager.DAL;
using ZZZ.ShoppingManager.Model;

namespace ZZZ.ShoppingManager.BLL
{
    public class ProductBll
    {
        public List<Product> GetProduct(int PageNo,int PageSize)
        {
            int startRow = (PageNo - 1) * PageSize + 1;
            int endRow = PageNo * PageSize;
            return new ProductDal().GetProduct(startRow,endRow);
        }
        public List<Category> GetCategory()
        {
            return new ProductDal().GetCategory();
        }
        public List<Product> GetProductByCID(int CID,int PageNo,int PageSize)
        {
            int startRow = (PageNo - 1) * PageSize + 1;
            int endRow = PageNo * PageSize;
            return new ProductDal().GetProductByCID(CID,startRow,endRow);
        }
        public int GetRecondsCount()
        {
            return new ProductDal().GetRecondsCount();
        }
        public int GetRecondsCount(int CID)
        {
            return new ProductDal().GetRecondsCountByCID(CID);
        }
        public int InsertProduct(Product Pro)
        {
            return new ProductDal().InsertProduct(Pro);
        }
        public int DeleteProduct(Product Pro)
        {
            return new ProductDal().DeleteProduct(Pro);
        }
        public int UpdateProduct(Product Pro)
        {
            return new ProductDal().UpdateProduct(Pro);
        }
        public List<Product> GetProductByPID(Product product)
        {
            return new ProductDal().GetProductByPID(product);
        }
        public List<Product> GetProductBySearch(string Info, int PageNo, int PageSize)
        {
            int startRow = (PageNo - 1) * PageSize + 1;
            int endRow = PageNo * PageSize;
            return new ProductDal().GetProductBySearch(Info, PageNo, PageSize);
        }
        public int GetRecondsCountByCIDAndPName(int CID, string PName)
        {
            return new ProductDal().GetRecondsCountByCIDAndPName(CID, PName);
        }
    }
}
