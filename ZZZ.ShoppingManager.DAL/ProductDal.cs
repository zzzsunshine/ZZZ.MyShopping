using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZZZ.ShoppingManager.Model;
using System.Data.SqlClient;

namespace ZZZ.ShoppingManager.DAL
{
    public class ProductDal
    {
        /// <summary>
        /// 得到所有的商品
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProduct(int startRow,int endRow)
        {
            string sql = "select * from(select ROW_NUMBER() over (order by p.PID) RowNum, p.PID,p.PName,p.ImageUrl,p.UnitPrice,p.PDescription,c.CategoryName from Product p inner join Category c on p.CID=c.CID) p where RowNum between @startRow and @endRow";
            SqlParameter[] p ={
                                  new SqlParameter("@startRow",startRow),
                                  new SqlParameter("@endRow",endRow)
                              };
            return DBHelper.Query(sql, dr =>
            {
                Product Pro = new Product
                {
                    PID = Convert.ToInt32(dr["PID"]),
                    CategoryName = dr["CategoryName"].ToString(),
                    ImageUrl = dr["ImageUrl"].ToString(),
                    PDescription = dr["PDescription"].ToString(),
                    PName = dr["PName"].ToString(),
                    UnitPrice = Convert.ToDecimal(dr["UnitPrice"])
                };
                return Pro;
            },p);
        }
        //通过类型得到所有的商品
        public List<Product> GetProductByCID(int CID,int startRow,int endRow)
        {
            string sql = "select * from(select ROW_NUMBER() over (order by p.PID) RowNum, p.PID,p.PName,p.ImageUrl,p.UnitPrice,p.PDescription,c.CategoryName from Product p inner join Category c on p.CID=c.CID where p.CID=@CID) p where RowNum between @startRow and @endRow";
            SqlParameter[] p ={
                                  new SqlParameter("@CID",CID),
                                  new SqlParameter("@startRow",startRow),
                                  new SqlParameter("@endRow",endRow)
                              };
            return DBHelper.Query(sql, dr =>
            {
                Product Pro = new Product
                {
                    PID = Convert.ToInt32(dr["PID"]),
                    CategoryName = dr["CategoryName"].ToString(),
                    ImageUrl = dr["ImageUrl"].ToString(),
                    PDescription = dr["PDescription"].ToString(),
                    PName = dr["PName"].ToString(),
                    UnitPrice = Convert.ToDecimal(dr["UnitPrice"])
                };
                return Pro;
            },p);
        }
        //通过文本框输入的内容查询
        public List<Product> GetProductBySearch(string Info,int startRow,int endRow)
        {
            string sql = "select * from(select ROW_NUMBER() over (order by p.PID) RowNum, p.PID,p.PName,p.ImageUrl,p.UnitPrice,p.PDescription,c.CategoryName from Product p inner join Category c on p.CID=c.CID where p.PName like @Info or c.CategoryName like @Info) t where RowNum between @startRow and @endRow";
            SqlParameter[] p ={
                                  new SqlParameter("@Info","%"+Info+"%"),
                                  new SqlParameter("@startRow",startRow),
                                  new SqlParameter("@endRow",endRow)
                              };
            return DBHelper.Query(sql, dr =>
            {
                Product Pro = new Product
                {
                    PID = Convert.ToInt32(dr["PID"]),
                    CategoryName = dr["CategoryName"].ToString(),
                    ImageUrl = dr["ImageUrl"].ToString(),
                    PDescription = dr["PDescription"].ToString(),
                    PName = dr["PName"].ToString(),
                    UnitPrice = Convert.ToDecimal(dr["UnitPrice"])
                };
                return Pro;
            }, p);
        }
        //得到所有的类型
        public List<Category> GetCategory()
        {
            string sql = "select * from Category";
            return DBHelper.Query(sql, dr =>
            {
                Category ca = new Category
                {
                    CategoryName = dr["CategoryName"].ToString(),
                    CID = Convert.ToInt32(dr["CID"])
                };
                return ca;
            });
        }
        //得到总记录数
        public int GetRecondsCount()
        {
            string sql = "select count(*) from Product";
            return (int)DBHelper.ExecuteScalar(sql);
        }
        //通过类型得到总计录数
        public int GetRecondsCountByCID(int CID)
        {
            string sql = "select count(*) from Product where CID=@CID";
            SqlParameter p = new SqlParameter("@CID",CID);
            return (int)DBHelper.ExecuteScalar(sql, p);
        }
        public int GetRecondsCountByCIDAndPName(int CID,string PName)
        {
            string sql = "select count(*) from Product p inner join Category c on p.CID=c.CID where p.CID=@CID or (p.PName like @PName or c.CategoryName like @PName)";
            SqlParameter[] p ={
                                 new SqlParameter("@CID",CID),
                                 new SqlParameter("@PName","%"+PName+"%")
                             };
            return (int)DBHelper.ExecuteScalar(sql, p);
        }
        //添加商品
        public int InsertProduct(Product Pro)
        {
            string sql = "insert into Product values(@PName,@UnitPrice,@ImageUrl,@PDescription,@CID)";
            SqlParameter[] p ={
                                  new SqlParameter("@PName",Pro.PName),
                                  new SqlParameter("@UnitPrice",Pro.UnitPrice),
                                  new SqlParameter("@PDescription",Pro.PDescription),
                                  new SqlParameter("@ImageUrl",Pro.ImageUrl),
                                  new SqlParameter("@CID",Pro.CID)
                             };
            return DBHelper.ExecuteNonQuery(sql, p);
        }
        //删除商品
        public int DeleteProduct(Product Pro)
        {
            string sql = "delete Product where PID=@PID";
            SqlParameter p = new SqlParameter("@PID", Pro.PID);
            return DBHelper.ExecuteNonQuery(sql, p);
        }
        //修改商品
        public int UpdateProduct(Product Pro)
        {
            string sql = "update product set PName=@PName,UnitPrice=@UnitPrice,ImageUrl=@ImageUrl,PDescription=@PDescription,CID=@CID where PID=@PId";
            SqlParameter[] p ={
                                  new SqlParameter("@PName",Pro.PName),
                                  new SqlParameter("@UnitPrice",Pro.UnitPrice),
                                  new SqlParameter("@PDescription",Pro.PDescription),
                                  new SqlParameter("@ImageUrl",Pro.ImageUrl),
                                  new SqlParameter("@CID",Pro.CID),
                                  new SqlParameter("@PId",Pro.PID)
                             };
            return DBHelper.ExecuteNonQuery(sql, p);
        }
        //通过商品ID查询
        public List<Product> GetProductByPID(Product product)
        {
            string sql = "select * from Product where PID=@PID";
            SqlParameter p = new SqlParameter("@PID", product.PID);
            return DBHelper.Query(sql, dr =>
            {
                Product Pro = new Product
                {
                    PID = Convert.ToInt32(dr["PID"]),
                    ImageUrl = dr["ImageUrl"].ToString(),
                    PDescription = dr["PDescription"].ToString(),
                    PName = dr["PName"].ToString(),
                    UnitPrice = Convert.ToDecimal(dr["UnitPrice"]),
                    CID=Convert.ToInt32(dr["CID"])
                };
                return Pro;
            }, p);

        }
        
    }
}
