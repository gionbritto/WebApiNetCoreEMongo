using BooksApiComMongoDB.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApiComMongoDB.Servicos
{
    //Classe de serviço responsável pelas operações de crud
    public class BookService
    {
        //diz que vou tratar de uma coleção especifica (book)
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() => 
            _books.Find(book => true).ToList();

        public Book Get(string id) =>
            _books.Find(b => b.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(b => b.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(b => b.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(b => b.Id == id);
    }
}
