﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Button = System.Windows.Controls.Button;

namespace ResearchWindowGenerator.ResearchWindowFolder
{
    //TODO : アプリケーションのレイアウト とりあえずアプリの数値と一緒にしていくつか作ってみる
    //TODO : MainContents
    //TODO : ContentsBar
    //TODO : ToolBar　一つのものと2つのものをそれぞれ作ってみる　位置のやつも考える

    /// <summary>
    /// WindowTemplate.xaml の相互作用ロジック
    /// </summary>
    public partial class ResearchWindowLayout : Window
    {
        string WindowName;
        /*コンポーネントの宣言*/
        /*Grid*/
        Grid maingrid;

        /*機能*/
        MyMainContents maincontents;
        MyContentsBar contentsBar;
        MyToolBar toolBar;
        MyToolBar toolBar2;

        ColumnDefinition colDef1;
        ColumnDefinition colDef2;
        ColumnDefinition colDef3;
        RowDefinition rowDef1;
        RowDefinition rowDef2;
        RowDefinition rowDef3;

        Double columnWidth1;
        Double columnWidth2;
        Double columnWidth3;
        Double rowHeight1;
        Double rowHeight2;
        Double rowHeight3;

        /*Log関係*/
        private Timer _timer = null;
        static double TimeCount = 0.0;
        static bool LogFlag;
        string filePath;
        string clickfilePath;
        LogDrawing_Canvas logdrawing;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowname">このwindowの名前</param>
        /// <param name="patternnum">表示するWindowの種類</param>
        /// <param name="file_name">マウスの移動座標を保存する/したファイル名</param>
        /// <param name="click_file_name">クリック位置を保存する/したファイル名</param>
        /// <param name="log_flag">true : ログの線を描きだし
        ///                        false : ログ取得</param>
        ///                        
        public ResearchWindowLayout(string windowname, int patternnum, string file_name, string click_file_name, bool log_flag)
        {
            InitializeComponent();

            /*引数の保存*/
            WindowName = windowname;
            LogFlag = log_flag;
            filePath = file_name;
            clickfilePath = click_file_name;

            //this.WindowStyle = WindowStyle.None;
            //this.AllowsTransparency = true;


            /*Windowのサイズ指定*/
            this.Width = SystemParameters.WorkArea.Width;
            this.Height = SystemParameters.WorkArea.Height;
            this.WindowState = WindowState.Maximized;

            /*初期化メソッドの宣言*/
            //T
            this.GridInit();
            this.LogSetting();
        }

        /// <summary>
        /// Gridの初期化
        /// </summary>
        /// memo https://docs.microsoft.com/ja-jp/dotnet/api/system.windows.controls.grid?view=netframework-4.8
        private void GridInit()
        {
            maingrid = new Grid
            {
                Width = this.Width,
                Height = this.Height,
                //Background = Brushes.Aquamarine,
                #if DEBUG
                ShowGridLines = true
                #endif

            };

            this.AddChild(maingrid);
            this.CompornentInit();
            String layoutNum = "1";
            //setLayout("Layout" + layoutNum);
            setLayout("TestMode");
            //setObject2Layout("Layout" + layoutNum);

        }

        //TODO : 複数アプリケーションでのレイアウトにする

        /// <summary>
        /// Layout毎にGridを設定
        /// </summary>
        /// <param name="_layout_name"></param>
        private void setLayout(String _layout_name)
        {
            switch (_layout_name)
            {
                case ("Layout1"):
                    //Column : 行 Width
                    colDef1 = new ColumnDefinition { Width = new GridLength(columnWidth1) };
                    colDef2 = new ColumnDefinition { Width = new GridLength(columnWidth2) };
                    //Row : 列 Height
                    rowDef1 = new RowDefinition { Height = new GridLength(rowHeight1) };
                    rowDef2 = new RowDefinition { Height = new GridLength(rowHeight2) };
                    maingrid.ColumnDefinitions.Add(colDef1);
                    maingrid.ColumnDefinitions.Add(colDef2);
                    maingrid.RowDefinitions.Add(rowDef1);
                    maingrid.RowDefinitions.Add(rowDef2);
                    break;
                case ("Layout2"):
                    //Column : 行 Width
                    colDef1 = new ColumnDefinition { Width = new GridLength(columnWidth2) };
                    colDef2 = new ColumnDefinition { Width = new GridLength(columnWidth1) };
                    //Row : 列 Height
                    rowDef1 = new RowDefinition { Height = new GridLength(rowHeight1) };
                    rowDef2 = new RowDefinition { Height = new GridLength(rowHeight2) };
                    maingrid.ColumnDefinitions.Add(colDef1);
                    maingrid.ColumnDefinitions.Add(colDef2);
                    maingrid.RowDefinitions.Add(rowDef1);
                    maingrid.RowDefinitions.Add(rowDef2);
                    break;
                case ("Layout3"):
                    //Column : 行 Width
                    colDef1 = new ColumnDefinition { Width = new GridLength(columnWidth1) };
                    colDef2 = new ColumnDefinition { Width = new GridLength(columnWidth2) };
                    //Row : 列 Height
                    rowDef1 = new RowDefinition { Height = new GridLength(rowHeight2) };
                    rowDef2 = new RowDefinition { Height = new GridLength(rowHeight1) };
                    maingrid.ColumnDefinitions.Add(colDef1);
                    maingrid.ColumnDefinitions.Add(colDef2);
                    maingrid.RowDefinitions.Add(rowDef1);
                    maingrid.RowDefinitions.Add(rowDef2);
                    break;
                case ("Layout4"):
                    //Column : 行 Width
                    colDef1 = new ColumnDefinition { Width = new GridLength(columnWidth2) };
                    colDef2 = new ColumnDefinition { Width = new GridLength(columnWidth1) };
                    //Row : 列 Height
                    rowDef1 = new RowDefinition { Height = new GridLength(rowHeight2) };
                    rowDef2 = new RowDefinition { Height = new GridLength(rowHeight1) };
                    maingrid.ColumnDefinitions.Add(colDef1);
                    maingrid.ColumnDefinitions.Add(colDef2);
                    maingrid.RowDefinitions.Add(rowDef1);
                    maingrid.RowDefinitions.Add(rowDef2);
                    break;
                case ("TestMode"):
                    TestGridSetting();


                    break;
                default:
                    break;
            }
            


        }

        private void TestGridSetting()
        {

            //TODO:テストする
            /*
             * 1: PowerPoint
             * 2: Calender
             * 3: 
             */

            /* Memo
             * 設定ファイルに入れるべき項目
             * 1 Gridの幅等
             * 2 各機能のサイズと配置
             */
            int number = 2;
            switch(number){
                case(1):
                    columnWidth1 = contentsBar.GetWidth();
                    columnWidth2 = maincontents.GetWidth();
                    columnWidth3 = 0;
                    rowHeight1 = toolBar.GetHeight();
                    rowHeight2 = contentsBar.GetHeight();
                    rowHeight3 = 0;
                    break;
                case (2):
                    columnWidth1 = this.Width * 1 / 5;
                    columnWidth2 = this.Width * 3 / 5;
                    columnWidth3 = 0;
                    rowHeight1 = this.Height * 2 / 10;
                    rowHeight2 = this.Height * 10 / 10;
                    rowHeight3 = 0;
                    break;
                case (3):
                    break;
                default:
                    break;
            }


            //Column : 行 Width
            colDef1 = new ColumnDefinition { Width = new GridLength(columnWidth1) };
            colDef2 = new ColumnDefinition { Width = new GridLength(columnWidth2) };
            colDef3 = new ColumnDefinition { Width = new GridLength(columnWidth2) };
            //Row : 列 Height
            rowDef1 = new RowDefinition { Height = new GridLength(rowHeight1) };
            rowDef2 = new RowDefinition { Height = new GridLength(rowHeight2) };
            rowDef3 = new RowDefinition { Height = new GridLength(rowHeight3) };

            maingrid.ColumnDefinitions.Add(colDef1);
            maingrid.ColumnDefinitions.Add(colDef2);
            maingrid.ColumnDefinitions.Add(colDef3);

            maingrid.RowDefinitions.Add(rowDef1);
            maingrid.RowDefinitions.Add(rowDef2);
            maingrid.RowDefinitions.Add(rowDef3);

        }


        /// <summary>
        /// コンポーネントの読み込み
        /// </summary>
        private void CompornentInit()
        {
            //TODO : レイアウトのセッティングファイルを読み取る
            String layoutNum = "Layout1";
            //SettingReader.settingRead(layoutNum);

            //TODO : レイアウトを読み込み反映する
            contentsBar = new MyContentsBar(this.Width * 1 / 5, this.Height * 0.87);
            toolBar = new MyToolBar(this.Width, this.Height * 0.13);
            maincontents = new MyMainContents(this.Width * 4 / 5, this.Height * 0.87);

        }

        private void setObject2Layout(String _layout_num)
        {
            switch (_layout_num)
            {
                case ("Layout1"):
                    maingrid.Children.Add(maincontents.mainContentsGrid);
                    Grid.SetRow(maincontents.mainContentsGrid, 1);
                    Grid.SetColumn(maincontents.mainContentsGrid, 1);

                    maingrid.Children.Add(contentsBar.myContentsBarGrid);
                    Grid.SetRow(contentsBar.myContentsBarGrid, 1);
                    Grid.SetColumn(contentsBar.myContentsBarGrid, 0);

                    maingrid.Children.Add(toolBar.myToolBarGrid);
                    Grid.SetRow(toolBar.myToolBarGrid, 0);
                    Grid.SetColumn(toolBar.myToolBarGrid, 0);
                    Grid.SetColumnSpan(toolBar.myToolBarGrid, 2);

                    break;
                case ("Layout2"):
                    maingrid.Children.Add(maincontents.mainContentsGrid);
                    Grid.SetRow(maincontents.mainContentsGrid, 1);
                    Grid.SetColumn(maincontents.mainContentsGrid, 0);

                    maingrid.Children.Add(contentsBar.myContentsBarGrid);
                    Grid.SetRow(contentsBar.myContentsBarGrid, 1);
                    Grid.SetColumn(contentsBar.myContentsBarGrid, 1);

                    maingrid.Children.Add(toolBar.myToolBarGrid);
                    Grid.SetRow(toolBar.myToolBarGrid, 0);
                    Grid.SetColumn(toolBar.myToolBarGrid, 0);
                    Grid.SetColumnSpan(toolBar.myToolBarGrid, 2);
                    break;
                case ("Layout3"):
                    maingrid.Children.Add(maincontents.mainContentsGrid);
                    Grid.SetRow(maincontents.mainContentsGrid, 0);
                    Grid.SetColumn(maincontents.mainContentsGrid, 1);

                    maingrid.Children.Add(contentsBar.myContentsBarGrid);
                    Grid.SetRow(contentsBar.myContentsBarGrid, 0);
                    Grid.SetColumn(contentsBar.myContentsBarGrid, 0);

                    maingrid.Children.Add(toolBar.myToolBarGrid);
                    Grid.SetRow(toolBar.myToolBarGrid, 1);
                    Grid.SetColumn(toolBar.myToolBarGrid, 0);
                    Grid.SetColumnSpan(toolBar.myToolBarGrid, 2);
                    break;
                case ("Layout4"):
                    maingrid.Children.Add(maincontents.mainContentsGrid);
                    Grid.SetRow(maincontents.mainContentsGrid, 0);
                    Grid.SetColumn(maincontents.mainContentsGrid, 0);

                    maingrid.Children.Add(contentsBar.myContentsBarGrid);
                    Grid.SetRow(contentsBar.myContentsBarGrid, 0);
                    Grid.SetColumn(contentsBar.myContentsBarGrid, 1);

                    maingrid.Children.Add(toolBar.myToolBarGrid);
                    Grid.SetRow(toolBar.myToolBarGrid, 1);
                    Grid.SetColumn(toolBar.myToolBarGrid, 0);
                    Grid.SetColumnSpan(toolBar.myToolBarGrid, 2);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogSetting()
        {
            //TODO : マウスカーソルの座標位置のログを取得できるようにする
            if (LogFlag == false)
            {
                Logger.LoggerInitialize(WindowName);
                //ここでログ取得ボタンのクリック定義
                //loggerButton.Click += startbutton_Click;
                //StartTimer();
            }
            if (LogFlag == true)
            {
                //ログ描画機構のやつ
                logdrawing = new LogDrawing_Canvas
                {
                    Height = this.Height,
                    Width = this.Width,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new Thickness(0, 0, 0, 0),
                };
                logdrawing.setFilePath(filePath, clickfilePath);

                //Popup drawlogPopup = this.FindName("drawlogPopup") as Popup;
                Popup drawlogPopup = new Popup
                {
                    IsOpen = true,
                    AllowsTransparency = true,
                    Placement = PlacementMode.Center,
                    PlacementTarget = maingrid,
                    Child = logdrawing
                };
                maingrid.Children.Add(drawlogPopup);
                System.Threading.Thread.Sleep(500);
                DoEvents();
            }
        }

        /// <summary>
        /// ボタンを押したとき
        /// ログの押下とそのテキストでその後の動作がどうなるか
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startbutton_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Start"))
            {
                StartTimer();
                ((System.Windows.Controls.Button)sender).Content = "Finish";
                //System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
                //Logger.SaveMouseClickPosition(TimeCount, point.X, point.Y);

                Button sender1 = (System.Windows.Controls.Button)sender;
                Console.WriteLine("aaaaaaaaaa" + sender1.Name);
            }
            else if (((Button)sender).Content.Equals("Finish"))
            {
                StopTimer();
                ((Button)sender).Content = "End";
            }
        }


        /// <summary>
        /// 
        /// https://moewe-net.com/csharp/forms-timer
        /// </summary>

        private void StartTimer()
        {
            Timer timer = new Timer();
            timer.Tick += new EventHandler(TickHandler);
            //timer.Interval = 20;
            timer.Interval = 50;
            timer.Start();
            _timer = timer;
        }


        /// <summary>
        /// 
        /// </summary>
        private void StopTimer()
        {
            if (_timer == null)
            {
                return;
            }
            _timer.Stop();
            _timer = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TickHandler(object sender, EventArgs e)
        {
            //マウスカーソルの座標を取得
            System.Drawing.Point p = System.Windows.Forms.Control.MousePosition;
            TimeCount += 0.05;
            Logger.SaveMouseCursorPosition(TimeCount, p.X, p.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            var callback = new DispatcherOperationCallback(obj =>
            {
                ((DispatcherFrame)obj).Continue = false;
                return null;
            });
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, callback, frame);
            Dispatcher.PushFrame(frame);
        }
    }
}
