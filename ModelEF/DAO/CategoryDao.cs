using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class CategoryDao
    {
        private PhamVanDongContext db;
        public CategoryDao()
        {
            db = new PhamVanDongContext();
        }
        public List<tblCategory> ListAll()
        {
            return db.tblCategories.ToList();
        }
        public tblCategory GetById(String maDM)
        {
            return db.tblCategories.SingleOrDefault(x => x.Id == maDM);
        }
        public tblCategory Find(String id)
        {
            return db.tblCategories.Find(id);
        }
        public String AutoIdDM()
        {
            String newID = db.tblCategories.Max(x => x.Id);
            newID = "00000" + (int.Parse(newID.Substring(newID.Length - 5)) + 1);
            newID = "DM" + newID.Substring(newID.Length - 5);
            return newID;
        }
        public String Insert(tblCategory entityDMSP)
        {
            String maDM = AutoIdDM();
            entityDMSP.Id = maDM;
            db.tblCategories.Add(entityDMSP);
            db.SaveChanges();
            return entityDMSP.Id;
        }
        public bool Update(tblCategory entity)
        {
            try
            {
                var DMSP = db.tblCategories.Find(entity.Id);
                DMSP.Name = entity.Name;
                DMSP.Description = entity.Description;
                DMSP.status = entity.status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<tblCategory> ListWhereALL(string keysearch, int page = 1, int pagesize = 5)
        {
            IQueryable<tblCategory> model = db.tblCategories;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.Name.Contains(keysearch));
            }
            return model.OrderBy(x => x.Id).ToPagedList(page, pagesize);
        }
        public bool Delete(String id)
        {
            try
            {
                var dmSP = db.tblCategories.Find(id);
                dmSP.status = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public IEnumerable<tblCategory> ListALLPaging(int page, int pageSize)
        {
            return db.tblCategories.OrderBy(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<tblCategory> ListAllCategoryId()
        {
            return db.tblCategories.Where(x => x.status == true).ToList();
        }
    }
}
