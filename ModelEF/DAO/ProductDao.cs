using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class ProductDao
    {
        private PhamVanDongContext db;
        public ProductDao()
        {
            db = new PhamVanDongContext();
        }
        public List<tblProduct> ListAll()
        {
            return db.tblProducts.ToList();
        }
        public tblProduct GetById(String maSP)
        {
            return db.tblProducts.SingleOrDefault(x => x.Id == maSP);
        }
        public tblProduct Find(String id)
        {
            return db.tblProducts.Find(id);
        }
        public String AutoIdSP()
        {
            String newID = db.tblProducts.Max(x => x.Id);
            newID = "00000" + (int.Parse(newID.Substring(newID.Length - 5)) + 1);
            newID = "SP" + newID.Substring(newID.Length - 5);
            return newID;
        }
        public String Insert(tblProduct entitySP)
        {
            String maSP = AutoIdSP();
            entitySP.Id = maSP;
            db.tblProducts.Add(entitySP);
            db.SaveChanges();
            return entitySP.Id;
        }
        public bool Update(tblProduct entity)
        {
            try
            {
                var SP = db.tblProducts.Find(entity.Id);
                SP.Name = entity.Name;
                SP.Quantity = entity.Quantity;
                SP.UnitCost = entity.UnitCost;
                SP.Description = entity.Description;
                SP.Status = entity.Status;
                SP.CategoryId = entity.CategoryId;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<tblProduct> ListWhereALL(string keysearch, int page = 1, int pagesize = 5)
        {
            IQueryable<tblProduct> model = db.tblProducts;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.Name.Contains(keysearch));
            }
            return model.OrderBy(x => x.Quantity).ThenByDescending(x => x.UnitCost).ToPagedList(page, pagesize);
        }
        public bool Delete(String id)
        {
            try
            {
                var SP = db.tblProducts.Find(id);
                SP.Status = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public IEnumerable<tblProduct> ListALLPaging(int page, int pageSize)
        {
            return db.tblProducts.OrderBy(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<tblProduct> ListAllProductId()
        {
            return db.tblProducts.Where(x => x.Status == true).ToList();
        }
    }
}
