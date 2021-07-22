using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    class ProfileMapper : IMapper<Profile>
    {
        public void Create(Profile obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Profiles.Add(obj);
                    connection.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }

        public void Create(Profile[] obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Profile obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Profile[] obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Profile> GetAll()
        {
            throw new NotImplementedException();
        }

        public Profile GetElementById(int id)
        {
            throw new NotImplementedException();
        }
        public Profile GetElementByName(string name)
        {
            var profile = new Profile();
            using (var connection = new SqlModel())
            {
                profile = connection.Profiles.Where(p => p.Name == name).FirstOrDefault();
            }
            return profile;
        }

        public void Update(Profile obj)
        {
            throw new NotImplementedException();
        }
    }
}
