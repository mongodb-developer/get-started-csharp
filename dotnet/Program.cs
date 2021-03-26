using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace getstarted
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = System.Environment.GetEnvironmentVariable("MONGODB_URI");

            var mongoURL = new MongoUrl(uri);
            var client = new MongoClient(mongoURL);
            var database = client.GetDatabase("getstarted");            
            var collection = database.GetCollection<BsonDocument>("dotnet");
            
            Console.WriteLine("Resetting collection");
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
            Console.WriteLine("Inserted one document");

            document = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine("Found one document from query:");
            Console.WriteLine("\t{0}", document.ToString());
            
            // Generate 100 documents with a counter ranging from 0 - 99 and insert
            var documents = Enumerable.Range(0, 100).Select(i => new BsonDocument("i", i));           
            collection.InsertMany(documents);
            Console.WriteLine("Inserted many documents");

            // Count number of documents 
            var count = collection.CountDocuments(new BsonDocument());
            Console.WriteLine("Number of documents in the collection : {0}", count);

            // Find a document with Sort and Limit 
            var filter = Builders<BsonDocument>.Filter.Exists("i");
            var sort = Builders<BsonDocument>.Sort.Descending("i");
            document = collection.Find(filter).Sort(sort).First();
            Console.WriteLine("Found a document with Sort():");
            Console.WriteLine("\t{0}", document);

            // Find a document with Projection and Limit 
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            document = collection.Find(new BsonDocument()).Project(projection).First();
            Console.WriteLine(document.ToString());
            Console.WriteLine("Found a document with Projection():");
            Console.WriteLine("\t{0}", document.ToString());

            // Update a document
            filter = Builders<BsonDocument>.Filter.Lt("i", 100);
            var update = Builders<BsonDocument>.Update.Inc("i", 100);
            var result = collection.UpdateOne(filter, update);

            if (result.IsModifiedCountAvailable)
            {   
                Console.WriteLine("Number of document updated: ");
                Console.WriteLine("\t{0}", result.ModifiedCount);
            }

            // Delete a document
            filter = Builders<BsonDocument>.Filter.Eq("i", 10);
            var deleteOneResult = collection.DeleteOne(filter);
            Console.WriteLine("Inserted many documents: {0}", deleteOneResult);

            // Example aggregation pipeline with $sum
            var pipeline = new BsonDocument[]{
                new BsonDocument{ {"$group", new BsonDocument{{"_id", BsonNull.Value}, {"total", new BsonDocument{{"$sum", "$i"}}}}} },
            };
            documents = collection.Aggregate<BsonDocument>(pipeline).ToList();
            Console.WriteLine("Aggregation result: ");
            Console.WriteLine("\t{0}", documents.First().ToJson());

            Console.WriteLine("Finished!");
        }
    }
}