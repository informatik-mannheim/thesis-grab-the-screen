using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace GrabTheScreen
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {

        public Auto audi, bmw;
        String baseString;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled
            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }
 

        // Erzeugung der Auto-Informationen und Autobild im rechten Block
        private void SurfaceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Random Preis generieren 
            Random random = new Random();
            int randomPrice = random.Next(12000, 33000);

            // Auto Objekt #1 erzeugen
            audi = new Auto();
            audi.setModel("Audi A3 Sportback");
            audi.setModelDescription("Ambition 2.0 TDI Clean Diesel");
            audi.setPrice(randomPrice + " EUR");
            audi.setSource("Resources/small_audi.jpg");

            // Auto Objekt #2 erzeugen
            bmw = new Auto();
            bmw.setModel("BMW");
            bmw.setModelDescription("");
            bmw.setPrice("");
            bmw.setSource("");

            // Miniaturbild (thumbnail) erzeugen
            Uri uri = new Uri(audi.getSource(), UriKind.Relative);
            BitmapImage imageBitmap = new BitmapImage(uri);
            System.Windows.Controls.Image thumbnail = new System.Windows.Controls.Image();
            thumbnail.Source = imageBitmap;
            thumbnail_car.Children.Add(thumbnail);
        }

        // Ausgabe der Auto-Informationen im Rechten Block 
        public void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            Label_carModel.Content = audi.getModel();
            Label_carDescription.Content = audi.getModelDescription();
            Label_carPrice.Content = audi.getPrice();
        }

        // Methode, die aufgerufen wird bei Klick auf "grab it" Button
        private void btn_grabIt_Click(object sender, RoutedEventArgs e)
        {
            // damit Miniatur-Bild erst zur Laufzeit angezeigt wird
            placeholder_smartphone.Children.Clear();

            // Erstellen des Vizualizer's
            TagVisualizer visualizer = new TagVisualizer();
            visualizer.Name = "MyTagVisualizer";

            // Visualization Definitionen
            TagVisualizationDefinition tagDefinition = new TagVisualizationDefinition();

            // Tag Value 0x18 - wichtig für Input Simulator
            tagDefinition.Value = "0x18";
            tagDefinition.Source = new Uri("CameraVisualization.xaml", UriKind.Relative);
            tagDefinition.LostTagTimeout = 2000;
            tagDefinition.MaxCount = 2;
            tagDefinition.OrientationOffsetFromTag = 0;
            tagDefinition.TagRemovedBehavior = TagRemovedBehavior.Fade;
            tagDefinition.UsesTagOrientation = true;

            // Definitionen dem Visualizer hinzufügen
            visualizer.Definitions.Add(tagDefinition);
            visualizer.VisualizationAdded += OnVisualizationAdded;

            // Miniaturbild auf gts-Fläche
            System.Windows.Controls.Image newImage = new System.Windows.Controls.Image();
            newImage.Source = konfig_auto.Source;
            Thickness margin = newImage.Margin;
            margin.Left = 20;
            margin.Right = 20;
            newImage.Margin = margin;

            // zur Laufzeit Bild und Visualizer erzeugen
            placeholder_smartphone.Children.Add(newImage);
            placeholder_smartphone.Children.Add(visualizer);

            // WPF-Image zu Drawing-Image konvertieren
            System.Drawing.Image drawingImage = ConvertWpfImageToImage(newImage);
            baseString = GetStringFromImage(drawingImage);

            // Auto Transfer Objekt erzeugen (mit erbender Klasse AutoTO.cs)
            //AutoTO autoTo = new AutoTO();
            //autoTo.setModel(auto.getModel());
            //autoTo.setModelDescription(auto.getModelDescription());
            //autoTo.setPrice(auto.getPrice());
            //autoTo.setBaseString64(baseString);

            //JSON-String erzeugen aus Objekt Auto und Base64-String (= autoTo)
            //var javaScriptSerializer = new JavaScriptSerializer();
            //string jsonString = javaScriptSerializer.Serialize(autoTo);
            //Console.WriteLine("Auto-Objekt:" + jsonString);

            // Methodenaufruf, damit JSON-String auf den Server gepusht wird
           //  postJSONtoServer();
            MongoDB.mongoDBconnection();
        }

        // erzeugt Tag-Bereich
        private void OnVisualizationAdded(object sender, TagVisualizerEventArgs e)
        {
            CameraVisualization camera = (CameraVisualization)e.TagVisualization;
            camera.GRABIT.Content = "Auflagefläche des Smartphones";
            camera.myRectangle.Fill = SurfaceColors.Accent1Brush;
        }

        // kodiert Image in Base64 String
        public static string GetStringFromImage(System.Drawing.Image image)
        {
            if (image != null)
            {
                ImageConverter ic = new ImageConverter();
                byte[] buffer = (byte[])ic.ConvertTo(image, typeof(byte[]));
                return Convert.ToBase64String(
                    buffer,
                    Base64FormattingOptions.InsertLineBreaks);
            }
            else
                return null;
        }

        // Methode zur Konvertierung von System.Windows.Control.Image in System.Drawing
        public static System.Drawing.Image ConvertWpfImageToImage(System.Windows.Controls.Image image)
        {
            if (image == null)
                throw new ArgumentNullException("image", "Image darf nicht null sein.");

            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            MemoryStream ms = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
            encoder.Save(ms);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

        // Methode, die den JSON-String via HTTP POST auf den Server pusht 
        // Methode wird aufgerufen nach Klick auf den "Grab it"-Button
        public void postJSONtoServer()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://141.19.142.50:28017/gts/pictures/");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                // + "', image: '"+ baseString
                string json = "{name: '" + audi.getModel() + "', type: '" + audi.getModelDescription() + "', price: '" + audi.getPrice() + "'}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }


    }
}