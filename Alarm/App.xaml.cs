using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace Alarm
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //初始化
            base.OnStartup(e);
            System.Environment.CurrentDirectory = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            this.LoadStyles("Theme");
        }

        void LoadStyles(string dirPath)
        {
            try
            {
                if (!Directory.Exists(dirPath)) return;
                var files = Directory.GetFiles(dirPath, "*.xaml");
                Application.Current.Resources.MergedDictionaries.Clear();
                foreach (var file in files)
                {
                    try
                    {
                        using (FileStream s = new FileStream(file, FileMode.Open))
                        {
                            ResourceDictionary rd = XamlReader.Load(s) as ResourceDictionary;
                            Application.Current.Resources.MergedDictionaries.Add(rd);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}
