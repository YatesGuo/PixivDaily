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


        public void get_pixiv_images(string date)
        {
            try
            {
                DateTime start = DateTime.Now;
                int pagecount = 1;//控制排行拉取榜页数，最高10
                int page = 1;

                string savepath = $"{Application.StartupPath}\\original_images\\{date}";
                if (!System.IO.Directory.Exists(savepath))
                {
                    System.IO.Directory.CreateDirectory(savepath);
                }
                else
                {

                }
                List<string> imgurls = new List<string>();

                //Regex reg = new Regex(@"(?<=data-filter=""thumbnail-filter lazy-image""data-src="")(.+?\.jpg)(?=""data-type=""illust"")");
                //Regex reg = new Regex(@"(?<=""new""(.+?)data-filter=""thumbnail-filter lazy-image""data-src="")(.+?\.jpg)(?=""data-type=""illust"")");
                Regex reg = new Regex(@"(?<=class=""new""((?!之前 #\d).)+data-filter=""thumbnail-filter lazy-image""data-src="")(.+?\.jpg)(?=""data-type=""illust"")");
                while (page <= pagecount )
                {
                    //string html = get_html($"https://www.pixiv.net/ranking.php?mode=daily&content=illust&p={page}&date={date}");
                    string html = File.ReadAllText("1.html");//读文件模拟返回页面

                    List<string> imgurl = new List<string>();
                    foreach (var url in reg.Matches(html))
                    {
                        imgurl.Add(url.ToString());
                    }
                    imgurls.AddRange(imgurl);
                    page += 1;
                }
                save_image(imgurls, savepath);
            }
            catch (Exception e)
            {
                SetText(e.Message);
                throw e;
            }
        }

        public void save_image(List<string> urls, string save_path)
        {
            foreach (string _url in urls)
            {
                string url = _url.Replace("c/240x480/img-master", "img-original");
                url = url.Replace("_master1200", "");
                url = url.Replace(".jpg", ".png");
                string image_path = $"{save_path}\\{get_pixiv_id(url)}";
                try
                {
                    DownloadFile(url, image_path + ".png");
                    SetText(get_pixiv_id(url)+"\r\n");
                }
                catch (Exception e)
                {
                    if (e.Message== "远程服务器返回错误: (404) 未找到。")
                    {
                        SetText($"{e.Message }{ get_pixiv_id(url)}，使用.JPG尝试\r\n");
                        url = url.Replace(".png", ".jpg");
                        DownloadFile(url, image_path + ".jpg");
                        SetText(get_pixiv_id(url) + "\r\n");
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }

        public void DownloadFile(string url, string FilePath)
        {
            try
            {
                if (File.Exists(FilePath)) return;
                string tempFile = Application.StartupPath + @"\temp\downloading.temp";
                File.Delete(tempFile);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.92 Safari/537.36";
                request.AllowAutoRedirect = false;
                request.Timeout = 300000;
                request.Referer = get_referer(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream sr = response.GetResponseStream();
                
                byte[] bArr = new byte[1024];
                int size = sr.Read(bArr, 0, (int)bArr.Length);
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                while (size > 0)
                {
                    fs.Write(bArr, 0, size);
                    size = sr.Read(bArr, 0, (int)bArr.Length);
                }
                fs.Close();
                sr.Close();
                File.Move(tempFile, FilePath);
            }
            catch (Exception)
            {
                throw;
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

        private delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.TextBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                TextBox.AppendText(text);
            }
        }

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            get_pixiv_images(dateTimePicker1.Value.ToString("yyyyMMdd"));
            //System.Threading.Thread.Sleep(10);
            
        }

        private void BGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TextBox.AppendText(e.UserState.ToString()+"%\r\n");
        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TextBox.AppendText("Completed");
            button_images.Enabled = true;
        }

        private void button_BGW_Click(object sender, EventArgs e)
        {
            if (BGWorker.IsBusy) return;
            this.button_images.Enabled = false;
            BGWorker.RunWorkerAsync();
        }

        private void button_images_Click(object sender, EventArgs e)
        {
            get_pixiv_images(dateTimePicker1.Value.ToString("yyyyMMdd"));
        }
    }
}