using Newtonsoft.Json;
using StudentsModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using System.Text;

namespace SyndicationServiceLibrary
{
    public class Feed : IFeed
    {
        public SyndicationFeedFormatter CreateFeed()
        {
            // Создать новый веб-канал.
            SyndicationFeed feed = new SyndicationFeed("Feed Title", "A WCF Syndication Feed", null);
            List<SyndicationItem> items = new List<SyndicationItem>();

            // Создать новый элемент рассылки.
            SyndicationItem item = new SyndicationItem("An item", "Item content", null);
            items.Add(item);
            feed.Items = items;

            // Возвращать канал ATOM или RSS, основываясь на строке запроса
            // RSS-&gt; http://localhost:8733/Design_Time_Addresses/Lab7_Service/Feed1/
            // Atom-&gt; http://localhost:8733/Design_Time_Addresses/Lab7_Service/Feed1/?format=atom
            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            SyndicationFeedFormatter formatter = null;
            if (query == "atom") formatter = new Atom10FeedFormatter(feed);
            else formatter = new Rss20FeedFormatter(feed); 
            return formatter;
        }

        public object GetStudentNotes(string studentId)
        {
            SyndicationFeed feed = new SyndicationFeed("Subjects & Notes", "Get list of notes by all subjects for the student", null);
            List<SyndicationItem> items = new List<SyndicationItem>();
            StudentsEntities notes = new StudentsEntities(new Uri("http://localhost:1395/Service1.svc/"));

            foreach (var note in notes.note.AsEnumerable().Where(i => i.stud_id == int.Parse(studentId)))
            {
                items.Add(new SyndicationItem($"{note.subject}", $"{note.id}.Note: {note.note1}. StudentID: {note.stud_id}", null));
            }
            feed.Items = items;

            string query = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["format"];
            SyndicationFeedFormatter formatter = null;
            if (query == "atom") formatter = new Atom10FeedFormatter(feed);
            else if (query == "json")
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:1395/Service1.svc/note?$format=json");
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string responseString = reader.ReadToEnd();
                var notesResp = JsonConvert.DeserializeObject<Object>(responseString);

                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return responseString; 
            }
            else formatter = new Rss20FeedFormatter(feed);
            return formatter;
        }
    }
}
