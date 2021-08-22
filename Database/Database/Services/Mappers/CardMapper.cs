using Database.Model.Database.Tables;
using Database.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    public class CardMapper : Observable, IMapper<Card>
    {
        public void Create(Card obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Cards.Add(obj);
                    connection.SaveChanges();
                    NotifyObserver();
                }
                catch
                {
                    MessageBox.Show("Запись уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Delete(Card[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Cards.RemoveRange(obj);
                connection.SaveChanges();
                NotifyObserver();
            }
        }

        public IEnumerable<Card> GetAll()
        {
            var cards = new List<Card>();
            using (var connection = new SqlModel())
            {
                cards = connection.Cards.ToList();
                connection.SaveChanges();
            }
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            var cards = new List<Card>();
            using (var connection = new SqlModel())
            {
                cards = await connection.Cards.ToListAsync();
                connection.SaveChanges();
            }
            return cards;
        }

        public void Update(Card obj)
        {
            throw new NotImplementedException();
        }
    }
}
