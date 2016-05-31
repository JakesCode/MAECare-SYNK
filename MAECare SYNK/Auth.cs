using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;

namespace MAECare_SYNK
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        public class Session
        {
            public async Task<string> GetAuthCode()
            {
                var client = new HttpClient();
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("client_id", "0cdc08f76f734b048620b621993bc244"),
                    new KeyValuePair<string, string>("scope", "read"),
                    new KeyValuePair<string, string>("state", "jakeisadeveloper")
                });
                HttpResponseMessage response = await client.PostAsync(
                    "https://todoist.com/oauth/authorize",
                    requestContent);
                HttpContent responseContent = response.Content;
                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                {
                    foreach(var x in response.Headers)
                    {
                        MessageBox.Show(x.Key);
                    }
                    return await reader.ReadToEndAsync();
                    
                }
            }
        }

        private async void Auth_Load(object sender, EventArgs e)
        {
            Session clientSession = new Session();
            var response = await clientSession.GetAuthCode();
            MessageBox.Show(response);
        }
    }
}
