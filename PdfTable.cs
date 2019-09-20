using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Utilities
{
    public class PdfTable
    {
        const string format = "csv";
        const string apiKey = "t5y8qfpqqeu8";
        const string uploadURL = "https://pdftables.com/api?key=" + apiKey + "&format=" + format;

        public static async Task<HttpStatusCode> PDFToExcel(string pdfFilename, string xlsxFilename)
        {
            using (var f = System.IO.File.OpenRead("wwwroot/HBHvsMLR-1QXO1MJRITL6U-252869806.pdf"))
            {
                var client = new HttpClient();
                var mpcontent = new MultipartFormDataContent();
                mpcontent.Add(new StreamContent(f));

                using (var response = await client.PostAsync(uploadURL, mpcontent))
                {
                    if ((int)response.StatusCode == 200)
                    {
                        using (
                            Stream contentStream = await response.Content.ReadAsStreamAsync(),
                            stream = File.Create("test"))
                        {
                            await contentStream.CopyToAsync(stream);
                        }
                    }
                    return response.StatusCode;
                }
            }
        }
    }
}
