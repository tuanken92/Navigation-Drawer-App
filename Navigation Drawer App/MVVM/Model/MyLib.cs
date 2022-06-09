using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Navigation_Drawer_App.MVVM.Model
{
    public class MyDefine
    {

        public static uint WM_LBUTTONDOWN = 0x201;
        public static uint WM_LBUTTONUP = 0x202;

        public static readonly string workingDirectory = Environment.CurrentDirectory;
        public static readonly string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        public static readonly string workspaceDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        public static readonly string resources_folder = String.Format($"{workingDirectory}\\resources");

        public static readonly string regex_get_image_file = @"[^\s]+(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$";
        public static readonly string regex_get_ip = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";


        #region Path file json
        public static readonly string file_brand = String.Format($"{workingDirectory}\\Configs\\brand.json");
        public static readonly string file_category = String.Format($"{workingDirectory}\\Configs\\category.json");
        public static readonly string file_user = String.Format($"{workingDirectory}\\Configs\\user.json");
        public static readonly string file_customer = String.Format($"{workingDirectory}\\Configs\\customer.json");
        public static readonly string file_warehouse = String.Format($"{workingDirectory}\\Configs\\warehouse.json");
        public static readonly string file_product = String.Format($"{workingDirectory}\\Configs\\product.json");
        public static readonly string file_unit = String.Format($"{workingDirectory}\\Configs\\unit.json");
        public static readonly string file_setting = String.Format($"{workingDirectory}\\Configs\\setting.json");
        public static readonly string file_import_product_manager = String.Format($"{workingDirectory}\\Configs\\product_import_manager.json");
        public static readonly string file_export_product_manager = String.Format($"{workingDirectory}\\Configs\\product_export_manager.json");
        public static readonly string import_product_tmp = String.Format($"{workingDirectory}\\Data\\Import\\") + @"product_import_{0}_{1}_{2}.json";
        public static readonly string export_product_tmp = String.Format($"{workingDirectory}\\Data\\Export\\") + @"product_export_{0}_{1}_{2}.json";


        public static readonly string file_config = String.Format($"{workingDirectory}\\Configs\\config_param.json");
        public static readonly string file_excel = String.Format($"{workingDirectory}\\Data\\ImportData.xlsx");

        public static readonly string file_config_format_data = String.Format($"{workingDirectory}\\Data\\configs\\format_data.json");
        public static readonly string file_config_common_param = String.Format($"{workingDirectory}\\Data\\configs\\common_param.json");
        public static readonly string file_config_filter_window = String.Format($"{workingDirectory}\\Data\\configs\\filter_window.json");
        public static readonly string path_load_img_database = @"C:\Program Files\Cognex\VisionPro\Images";
        public static readonly string path_load_vpp_file = @"C:\Users\Admin\Desktop\Vpp_file";
        public static readonly string path_save_images = String.Format("{0}\\Images", projectDirectory);

        public static readonly string key_thh = @"https://tanhungha.com.vn/";
        public static readonly string hash_key = "";
        #endregion

        #region api
        public static string API_OK = "success";
        public static string API_NG = "error";
        public static string API_Warning = "warning";
        public static string API_LOSS_CONNECTION = "network";
        #endregion

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

    }

    public class SaveLoadParameter
    {
        public static void Save_Parameter(object param)
        {
            //save
            if (MyLib.File_Is_Exist(MyDefine.file_config))
            {
                Save_Parameter(param, MyDefine.file_config);
            }
            else
            {
                //create folder
                FileInfo fileInfo = new FileInfo(MyDefine.file_config);
                if (!fileInfo.Exists)
                    Directory.CreateDirectory(fileInfo.Directory.FullName);

                //create file
                using (FileStream f = File.Create(MyDefine.file_config))
                {
                    f.Close();
                    Console.WriteLine($"Create file {MyDefine.file_config}");
                }

                //save param to file
                Save_Parameter(param, MyDefine.file_config);
            }
        }

        public static object Load_Parameter(object param)
        {
            if (MyLib.File_Is_Exist(MyDefine.file_config))
            {
                using (StreamReader file = File.OpenText(MyDefine.file_config))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    param = serializer.Deserialize(file, param.GetType());
                }
            }
            else
            {
                //MyLib.ShowDlgError($"Not found {MyDefine.file_config}");
                //MessageBox
                throw new Exception($"Not found {MyDefine.file_config}");
            }

            return param;
        }

        public static void Save_Parameter(object param, string file_name)
        {
            //save
            if (MyLib.File_Is_Exist(file_name))
            {
                // serialize JSON directly to a file
                Console.WriteLine("Save parameter to file " + file_name);
                using (StreamWriter file = File.CreateText(file_name))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, param);
                }
            }
            else
            {
                //create folder
                FileInfo fileInfo = new FileInfo(file_name);
                if (!fileInfo.Exists)
                    Directory.CreateDirectory(fileInfo.Directory.FullName);

                //create file
                using (FileStream f = File.Create(file_name))
                {
                    f.Close();
                    Console.WriteLine($"Create file {file_name}");
                }

                // serialize JSON directly to a file
                Console.WriteLine("Save parameter to file " + file_name);
                using (StreamWriter file = File.CreateText(file_name))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, param);
                }
            }
        }

        public static object Load_Parameter(object param, string file_name)
        {
            if (MyLib.File_Is_Exist(file_name))
            {
                using (StreamReader file = File.OpenText(file_name))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    param = serializer.Deserialize(file, param.GetType());
                }
            }
            else
            {
                //MyLib.ShowDlgError($"Not found {file_name}");
                throw new Exception($"Not found {file_name}");
            }

            return param;
        }
    }

    public class MyLib
    {
        public static void SetThemeColor(Color colorPrimary, Color colorSecondary)
        {
            //This is the API in 4.2.1
            PaletteHelper paletteHelper = new PaletteHelper();
            //For versions after this point use MaterialDesignThemes.Wpf.Theming.ThemeManager

            //Get current theme
            var theme = paletteHelper.GetTheme();

            //Apply primary color
            theme.SetPrimaryColor(colorPrimary);
            theme.SetSecondaryColor(colorSecondary);

            //Apply theme to application
            paletteHelper.SetTheme(theme);
        }

        public static string GetLocalIPAddress()
        {
            var localhost = "127.0.0.1";
            bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            if (isNetwork)
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return localhost;
            }
            else
            {
                return localhost;
            }
        }

        public static bool File_Is_Exist(string file_name)
        {
            return File.Exists(file_name);
        }

        //public static void ShowDlgWarning(string message)
        //{
        //    using (DialogMessage dialogMessage = new DialogMessage(MyDefine.API_Warning, message))
        //    {
        //        dialogMessage.ShowDialog();
        //    }
        //}

        //public static void ShowDlgError(string message)
        //{
        //    using (DialogMessage dialogMessage = new DialogMessage(MyDefine.API_NG, message))
        //    {
        //        dialogMessage.ShowDialog();
        //    }
        //}

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public static void DragWindow(IntPtr hwnd)
        {
            ReleaseCapture();
            SendMessage(hwnd, 0x112, 0xf012, 0);
        }



        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

     
        
        


        public static DateTime Timestamp_To_Datetime(UInt64 timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }

        public static UInt64 Datetime_To_TimeStamp(DateTime datetime)
        {
            return (UInt64)(TimeZoneInfo.ConvertTimeToUtc(datetime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

       

        static public bool Download_Image(string url, string path2save)
        {
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                try
                {
                    webClient.DownloadFileAsync(new Uri(url), path2save);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Download exception: {e.Message}");
                    return false;
                }

            }
            return true;
        }

       

        static readonly string s1 = @"ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝàáâãèéêìíòóôõùúýĂăĐđĨĩŨũƠơƯưẠạẢảẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẸẹẺẻẼẽẾếỀềỂểỄễỆệỈỉỊịỌọỎỏỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợỤụỦủỨứỪừỬửỮữỰựỲỳỴỵỶỷỸỹ";
        static readonly string s0 = @"AAAAEEEIIOOOOUUYaaaaeeeiioooouuyAaDdIiUuOoUuAaAaAaAaAaAaAaAaAaAaAaAaEeEeEeEeEeEeEeEeIiIiOoOoOoOoOoOoOoOoOoOoOoOoUuUuUuUuUuUuUuYyYyYyYy";
        public static string RemoveDiacritics(string accentedStr)
        {
            List<char> list_char = new List<char>();
            foreach (var c in accentedStr)
            {
                var pos = s1.IndexOf(c);
                if (pos >= 0)
                {
                    list_char.Add(s0[pos]);
                }
                else
                {
                    list_char.Add(c);
                }
            }
            return new string(list_char.ToArray());
        }


        public static bool CreateFolder(string path_folder)
        {
            bool result = Directory.Exists(path_folder);
            if (!result)
            {
                Directory.CreateDirectory(path_folder);
                result = Directory.Exists(path_folder);
            }
            return result;
        }
        public static string GenerateNameImage()
        {
            CreateFolder(MyDefine.path_save_images);
            return String.Format("{0}\\{1}.jpg", MyDefine.path_save_images, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
        }

        //public static void Save_BitMap(Bitmap bm)
        //{
        //    string file_name = GenerateNameImage();
        //    bm.Save(file_name, ImageFormat.Jpeg);
        //    Console.WriteLine("Saved file {0}", file_name);
        //}


        public static List<string> Get_All_File_In_Folder(string path, bool debug = false)
        {
            List<string> list_files = null;
            try
            {
                string[] files_xxx = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                list_files = new List<string>(files_xxx);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e.Message}");
            }


            if (debug)
            {
                foreach (var file in list_files)
                {
                    Console.WriteLine(file);
                }
            }
            return list_files;
        }

        public static bool IsImagePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            Regex regex = new Regex(MyDefine.regex_get_image_file);
            Match match = regex.Match(path);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        public static bool IsIPAddress(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return false;
            Regex regex = new Regex(MyDefine.regex_get_ip);
            Match match = regex.Match(ip);
            if (match.Success)
            {
                return true;
            }
            return false;
        }

        public static List<string> Filter_Software_Type(List<string> list_files, string type, bool debug = false)
        {
            List<string> list_files_filter = new List<string>();
            Regex regex = new Regex(type);

            foreach (var file in list_files)
            {
                Match match = regex.Match(file);
                if (match.Success)
                {
                    list_files_filter.Add(file);
                }
            }

            if (debug)
            {
                foreach (var file in list_files_filter)
                {
                    Console.WriteLine(file);
                }
            }
            return list_files_filter;
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        

    }
}
