using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class UserDao
    {
        private PhamVanDongContext db;
        public UserDao()
        {
            db = new PhamVanDongContext();
        }
        public int Login(string user, string pass)
        {
            var result = db.tblUserAccounts.SingleOrDefault(x => x.Username == user && x.Password.Contains(pass));
            if (result == null)
            {
                return 0;
            }
            return 1;
        }
        public List<tblUserAccount> ListAll()
        {
            return db.tblUserAccounts.ToList();
        }
        public tblUserAccount GetById(String user)
        {
            return db.tblUserAccounts.SingleOrDefault(x => x.Username == user);
        }
        public tblUserAccount Find(string username)
        {

            return db.tblUserAccounts.Find(username);
        }
        public String AutoIdUser()
        {
            String newID = db.tblUserAccounts.Max(x => x.Id);
            newID = "00000" + (int.Parse(newID.Substring(newID.Length - 5)) + 1);
            newID = "ID" + newID.Substring(newID.Length - 5);
            return newID;
        }
        public string Insert(tblUserAccount entityUser)
        {
            String id = AutoIdUser();
            entityUser.Id = id;
            db.tblUserAccounts.Add(entityUser);
            db.SaveChanges();
            return entityUser.Username;
        }
        public bool Update(tblUserAccount entity)
        {
            try
            {
                var user = db.tblUserAccounts.Find(entity.Id);
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<tblUserAccount> ListWhereALL(string keysearch, int page = 1, int pagesize = 5)
        {
            IQueryable<tblUserAccount> model = db.tblUserAccounts;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.Username.Contains(keysearch));
            }
            return model.OrderBy(x => x.Username).ToPagedList(page, pagesize);
        }
        public bool Delete(string id)
        {
            try
            {
                var user = db.tblUserAccounts.Find(id);
                db.tblUserAccounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public IEnumerable<tblUserAccount> ListALLPaging(int page, int pageSize)
        {
            return db.tblUserAccounts.OrderBy(x => x.Id).ToPagedList(page, pageSize);
        }
        public List<tblUserAccount> ListAllProductId()
        {
            return db.tblUserAccounts.Where(x => x.Status == true).ToList();
        }
    }
}
