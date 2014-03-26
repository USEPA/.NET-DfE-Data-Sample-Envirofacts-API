using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;  
using System.Net;  
using System.Xml.Linq;
using System.Text;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<DesignForEnviroment> dataList = getDataSet();
            DesignForEnviromentList.DataSource = dataList;
            DesignForEnviromentList.DataBind();
        }

        public List<DesignForEnviroment> getDataSet()
        {
            // Create the web request  
            HttpWebRequest request = 
                WebRequest.Create("http://oasdev.saic.com/enviro2/efservice/t_design_for_environment/sector/All-Purpose%20Cleaners") 
                as HttpWebRequest;
            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //Console.WriteLine(reader.ReadToEnd());
                XDocument document = XDocument.Load(reader);
                List<DesignForEnviroment> ms = new List<DesignForEnviroment>();
                var linqA = from data in document.Descendants("t_design_for_environment")
                            select new DesignForEnviroment(data.Element("SECTOR_ID").Value,
                                data.Element("CATEGORY").Value,
                                data.Element("SECTOR").Value,
                                data.Element("PARTNER").Value,
                                data.Element("CITY").Value,
                                data.Element("STATE").Value,
                                data.Element("PARTNERSINCE").Value,
                                data.Element("PRODUCT_ID").Value,
                                data.Element("PRODUCT").Value);

                ms = linqA.ToList<DesignForEnviroment>();
                return ms;
            }

        }

        public class DesignForEnviroment
        {
            public DesignForEnviroment(String sector_id, String category, 
                                       String sector,String partner, 
                                       String city, String state,
                                       String partnersince, String product_id, 
                                       String product)
            {
                _sector_id = sector_id;
                _category = category;
                _sector = sector;
                _partner = partner;
                _city = city;
                _state = state;
                _partnersince = partnersince;
                _product_id = product_id;
                _product = product;
            }
            private String _sector_id;
            public String sector_id
            {
                get { return _sector_id; }
                set { _sector_id = value; }
            }
            private String _category;
            public String category
            {
                get { return _category; }
                set { _category = value; }
            }
            private String _sector;
            public String sector
            {
                get { return _sector; }
                set { _sector = value; }
            }
            private String _partner;
            public String partner
            {
                get { return _partner; }
                set { _partner = value; }
            }
            private String _city;
            public String city
            {
                get { return _city; }
                set { _city = value; }
            }
            private String _state;
            public String state
            {
                get { return _state; }
                set { _state = value; }
            }
            private String _partnersince;
            public String partnersince
            {
                get { return _partnersince; }
                set { _partnersince = value; }
            }
            private String _product_id;
            public String product_id
            {
                get { return _product_id; }
                set { _product_id = value; }
            }
            private String _product;
            public String product
            {
                get { return _product; }
                set { _product = value; }
            }    
        }
    }

}
