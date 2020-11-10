using System;
using System.Collections.Generic;
using System.Text;
using ClinicAppDAL.EF;
using ClinicAppDAL.Models.AuthModel;
using System.Security.Cryptography;

namespace ClinicAppDAL.Repos.AuthRepo
{
    public class UserRepo : BaseRepo<User>
    {
        private readonly SHA1Managed sha;

        public UserRepo(ClinicAppAuthContext context) : base(context)
        {
            sha = new SHA1Managed();
        }

        private string Encode(String pass)
        {
            if (pass != null)
            {
                byte[] hash1 = sha.ComputeHash(Encoding.UTF8.GetBytes(pass));
                byte[] hash2 = sha.ComputeHash(hash1);
                return Convert.ToBase64String(hash2);
            }
            else
            {
                return null;
            }
        }

        public new int Add(User entity)
        {
            entity.Password = Encode(entity.Password);
            return base.Add(entity);
        }

        public new int Add(IList<User> entities)
        {
            foreach (User entity in entities)
            {
                entity.Password = Encode(entity.Password);
            }
            return base.Add(entities);
        }

        public bool CheckLogin(String login)
        {
            return GetSome(x => x.Login == login).Count != 0;
        }

        public bool CheckPassword(String pass)
        {
            pass = Encode(pass);
            return GetSome(x => x.Password == pass).Count != 0;
        }

        public int MakeAdmin(User entity)
        {
            entity.Role = 1;
            return Update(entity);
        }

        public int Approve(User entity)
        {
            entity.Access = true;
            return Update(entity);
        }

    }
}
