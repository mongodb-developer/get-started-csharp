using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace quickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = System.Environment.GetEnvironmentVariable("MONGODB_URI");

            var mongoURL = new MongoUrl(uri);
            var client = new MongoClient(mongoURL);
            var database = client.GetDatabase("quickstart");            
            var collection = database.GetCollection<BsonDocument>("dotnet");

            database.DropCollection("dotnet");

            var document = new BsonDocument
            {
                { "name", "MongoDB" },
                { "modified", DateTime.UtcNow },
                { "count", 1 },
                { "info", new BsonDocument
                    {
                        { "x", 203 },
                        { "y", 102 }
                    }}
            };

            // Insert one document 
            collection.InsertOne(document);

            document = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine(document.ToString());
            
            // Generate 100 documents with a counter ranging from 0 - 99 and insert
            var documents = Enumerable.Range(0, 100).Select(i => new BsonDocument("i", i));           
            collection.InsertMany(documents);

            // Count number of documents 
            var count = collection.CountDocuments(new BsonDocument());
            Console.WriteLine(count);

            // Find a document with Sort and Limit 
            var filter = Builders<BsonDocument>.Filter.Exists("i");
            var sort = Builders<BsonDocument>.Sort.Descending("i");
            document = collection.Find(filter).Sort(sort).First();
            Console.WriteLine(document);

            // Find a document with Projection and Limit 
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            document = collection.Find(new BsonDocument()).Project(projection).First();
            Console.WriteLine(document.ToString());

            // Update a document
            filter = Builders<BsonDocument>.Filter.Lt("i", 100);
            var update = Builders<BsonDocument>.Update.Inc("i", 100);
            var result = collection.UpdateOne(filter, update);

            if (result.IsModifiedCountAvailable)
            {   
                Console.WriteLine("Number of document modified: ", result.ModifiedCount);
            }

            // Delete a document
            filter = Builders<BsonDocument>.Filter.Eq("i", 10);
            collection.DeleteOne(filter);

            // Example aggregation pipeline with $sum
            var pipeline = new BsonDocument[]{
                new BsonDocument{ {"$group", new BsonDocument{{"_id", BsonNull.Value}, {"total", new BsonDocument{{"$sum", "$i"}}}}} },
            };
            documents = collection.Aggregate<BsonDocument>(pipeline).ToList();
            Console.WriteLine(documents.First().ToJson());

            Console.WriteLine("Finished!");
        }
    }
}