using Database.Model.Database.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    public class CardMapper : IMapper<Card>
    {
        public event Action<object> CreateEntityEvent;
        public void Create(Card obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Cards.Add(obj);
                    connection.SaveChanges();
                    CreateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Запись уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Create(Card[] obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Card obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Cards.Remove(obj);
                connection.SaveChanges();
            }
        }

        public void Delete(Card[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Cards.RemoveRange(obj);
                connection.SaveChanges();
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

        public Card GetElementById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Card obj)
        {
            throw new NotImplementedException();
        }
    }
}
