using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;


namespace GrabTheScreen
{

    class MongoDB
    {

       public static void mongoDBconnection()
        {
            var connectionString = "mongodb://141.19.142.50:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var gts = server.GetDatabase("gts");
            var pictures = gts.GetCollection<BsonDocument>("pictures");
            
            // Reference to Collection Object
            //var auto = gts.GetCollection<Auto>("auto");


            try
            {
                foreach (var doc in pictures.FindAll())
                {
                    Console.WriteLine(doc.ToJson());
                }
                //var query = new QueryDocument(new BsonElement("name", "description"));
                //var pic = pictures.FindOne(query);
                //pictures.Save(getSomeDocument());
              
                // delete data set from DB 
                //pictures.Remove(query, RemoveFlags.None);


                // insert new Auto
                var opel = new Auto { model = "Opel", modelDescription = "Corsa", price = "10.000 EUR", source = "" };
                pictures.Insert(opel);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            } 
        }

       // public static BsonDocument getSomeDocument()
        //{
          //  return new BsonDocument().Add(new BsonElement("", ""));
        //}
    }
}
