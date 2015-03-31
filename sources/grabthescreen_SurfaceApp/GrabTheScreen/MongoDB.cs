using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using GrabTheScreen;


namespace GrabTheScreen
{

    class MongoDB
    {

        public static void mongoDBconnection(Auto auto)
        {
            Auto temp = auto;
            var connectionString = "mongodb://141.19.142.50:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var gts = server.GetDatabase("gts");
            var pictures = gts.GetCollection<BsonDocument>("pictures");

            // Reference to Collection Object
            var autoCollection = gts.GetCollection<Auto>("auto");


            try
            {
                // insert new Auto
                pictures.Save(new Auto { model = temp.getModel(), modelDescription = temp.getModelDescription(), price = temp.getPrice(), source = temp.getSource(), id = temp.getId(), color = temp.getColor(), status = temp.getStatus() });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
