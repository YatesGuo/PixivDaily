using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixivDaily
{
    public partial class Pixiv : Form
    {
        public Pixiv()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            get_pixiv_images(dateTimePicker1.Value.ToString("yyyyMMdd"));
        }

        public void get_pixiv_images(string date)
        {
            try
            {
                DateTime start = DateTime.Now;
                int pagecount = 10;
                int page = 1;
                int index = 0;

                string savepath = $"{Application.StartupPath}\\original_images\\{date}";
                if (!System.IO.Directory.Exists(savepath))
                {
                    System.IO.Directory.CreateDirectory(savepath);
                }
                else
                {

                }
                string[] ps;
                List<int> indexes = new List<int>();
                List<string> imgurls = new List<string>();

                Regex reg = new Regex(@"(?<=data-filter=""thumbnail-filter lazy-image""data-src="")(.+?\.jpg)(?=""data-type=""illust"")");
                //while (page <= pagecount && index < 100)
                //{
                    //string html = get_html($"https://www.pixiv.net/ranking.php?mode=daily&content=illust&p={page}&date={date}");
                    string html = File.ReadAllText("1.txt");//读文件模拟返回页面

                    List<string> imgurl = new List<string>();
                    foreach (var url in reg.Matches(html))
                    {
                        imgurl.Add(url.ToString());
                    }
                    imgurls.AddRange(imgurl);
                    indexes.Add(index);
                    index += imgurl.Count;
                    page += 1;
                //}
                save_image(imgurls, savepath);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void save_image(List<string> urls, string save_path)
        {
            string tempFile = Application.StartupPath + @"\temp\downloading.temp";
            
            foreach (string _url in urls)
            {
                string url = _url.Replace("c/240x480/img-master", "img-original");
                url = url.Replace("_master1200", "");
                url = url.Replace(".jpg", ".png");
                string image_path = $"{save_path}\\{get_pixiv_id(url)}";
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                try
                {
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.92 Safari/537.36";
                        request.AllowAutoRedirect = false;
                        request.Timeout = 30000;
                        request.Referer = get_referer(url);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream sr = response.GetResponseStream();
                        byte[] bArr = new byte[1024];
                        int size = sr.Read(bArr, 0, (int)bArr.Length);
                        while (size>0)
                        {
                            fs.Write(bArr, 0, size);
                            size = sr.Read(bArr, 0, (int)bArr.Length);
                        }
                        fs.Close();
                        sr.Close();
                        File.Move(tempFile, image_path+".png");
                    }
                    catch (Exception e)
                    {
                        url = url.Replace(".png", ".jpg");
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.Method = "GET";
                        request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.92 Safari/537.36";
                        request.AllowAutoRedirect = false;
                        request.Timeout = 30000;
                        request.Referer = get_referer(url);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream sr = response.GetResponseStream();
                        byte[] bArr = new byte[102400];
                        int size = sr.Read(bArr, 0, (int)bArr.Length);
                        while (size > 0)
                        {
                            fs.Write(bArr, 0, size);
                            size = sr.Read(bArr, 0, (int)bArr.Length);
                        }
                        fs.Close();
                        sr.Close();
                        File.Move(tempFile, image_path+".jpg");
                    }
                }
                catch (Exception e)
                {
                    continue;
                    throw e;
                }
            }
        }

        public string get_pixiv_id(string url)
        {
            Regex reg = new Regex(@"(\d+)(?=_p0)");
            return reg.Match(url).Value;
        }

        public string get_referer(string url)
        {
            string reference = "https://www.pixiv.net/member_illust.php?mode=medium&illust_id=";
            return reference + get_pixiv_id(url)+ "&page=0";
        }
        public string get_html(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.92 Safari/537.36";
            request.AllowAutoRedirect = false;
            request.Timeout = 30000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string html = sr.ReadToEnd();
            sr.Close();
            return html;
        }
        public Stream stream<Stream,StreamReader>(string url)
            where Stream :Stream ,new()
            where StreamReader :StreamReader, new()
        {

        }
    }
}
