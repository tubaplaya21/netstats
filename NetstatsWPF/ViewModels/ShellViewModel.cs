using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NetstatsWPF.Models;
using Netstats.ViewModels.Commands;
using System.Threading;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Npgsql;
using System.Data;
using Squirrel;
using System.Windows.Media.Media3D;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System.Windows.Input;
using System.Drawing;
using Netstats.ViewModels;
using static Netstats.ViewModels.MessageBoxViewModel;

namespace Netstats.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        #region Properties and such

        private string _storeNumber;
        private Thread[] _threads;
        private static string port = "5432";
        private static string wendysServer = "wendys_check.npci.com";
        private static string wendysDB = "wendys";
        private static string pizzaServer = "ngpos_check.npci.com";
        private static string pizzaDB = "store_check";
        private static string user = "ngpos";
        private static string password = "8awgl5qr";
        private NpgsqlConnection wendysCheck;
        private NpgsqlConnection ngposCheck;
        private bool _isWendys;
        private static string updatePath = @"S:\bmiller\netstats\Releases";
        RegistryKey netstats;
        private bool _wendysData;
        private bool _pizzaData;
        private readonly IWindowManager window = new WindowManager();

        Action<string, MessageBoxViewModel.MessageType, MessageBoxViewModel.MessageButtons> customBox;

        public Thread[] Threads
        {
            get { return _threads; }
            set { _threads = value; }
        }

        private bool _isDarkTheme;

        public bool IsDarkTheme
        {
            get { return _isDarkTheme; }
            set
            {
                if (value)
                {
                    SetDarkTheme();
                }
                else
                {
                    SetLightTheme();
                }
                _isDarkTheme = value;
                netstats = Registry.CurrentUser.OpenSubKey(@"Software\Netstats", true);
                netstats.SetValue("dark_mode", value);
            }
        }

        public bool IsPizzaHut { get; set; }

        public bool IsWendys
        {
            get { return _isWendys; }
            set 
            { 
                if(_isWendys != value)
                {
                    IsRunning = false;
                    IsNotRunning = true;
                }
                _isWendys = value;
                if (value)
                {
                    LabelKms1 = "FL/PUW";
                    LabelKms2 = "FL";
                    LabelKms3 = "PUW All";
                    LabelKms4 = "Fry Station";
                    LabelKms5 = "Front Runner";
                    LabelKms6 = "PUW All (2)";
                    LabelKms7 = "Drinks";
                    LabelKms8 = "Salad Station";
                    LabelKms9 = "Guest Order";
                    LabelOffice5 = "OCD";
                    LabelOffice8 = "HME Timer";
                    LabelReg1 = White;
                    LabelReg2 = White;
                    LabelReg3 = White;
                    LabelReg4 = White;
                    LabelReg5 = White;
                    LabelReg6 = White;
                    LabelReg7 = White;
                    LabelReg8 = White;
                    LabelReg9 = White;
                    LabelReg10 = White;
                    Cursor = Cursors.Hand;
                    IsPizzaHut = false;
                }
                else
                {
                    LabelKms1 = "Make 1";
                    LabelKms2 = "Make 2";
                    LabelKms3 = "Cut";
                    LabelKms4 = "Fryer";
                    LabelKms5 = "Fryer";
                    LabelKms6 = "Sandwich";
                    LabelKms7 = "Salad";
                    LabelKms8 = "Fryer Cut";
                    LabelKms9 = "CO Monitor";
                    LabelOffice5 = "Cisco";
                    LabelOffice8 = "Register 12";
                    Cursor = Cursors.Arrow;
                    IsPizzaHut = true;
                }
            }
        }

        public string Title { get; set; }

        public SolidColorBrush Reg1 { get; set; }
        public SolidColorBrush LabelReg1 { get; set; }
        public string ReplyReg1 { get; set; }
        public bool CheckedReg1 { get; set; }
        public string InfoReg1 { get; set; }
        public bool InfoEnabledReg1 { get; set; }
        public string RegIp1 { get; set; }
        public SolidColorBrush Reg2 { get; set; }
        public SolidColorBrush LabelReg2 { get; set; }
        public string ReplyReg2 { get; set; }
        public bool CheckedReg2 { get; set; }
        public string InfoReg2 { get; set; }
        public bool InfoEnabledReg2 { get; set; }
        public string RegIp2 { get; set; }
        public SolidColorBrush Reg3 { get; set; }
        public SolidColorBrush LabelReg3 { get; set; }
        public string ReplyReg3 { get; set; }
        public bool CheckedReg3 { get; set; }
        public string InfoReg3 { get; set; }
        public bool InfoEnabledReg3 { get; set; }
        public string RegIp3 { get; set; }
        public SolidColorBrush Reg4 { get; set; }
        public SolidColorBrush LabelReg4 { get; set; }
        public string ReplyReg4 { get; set; }
        public bool CheckedReg4 { get; set; }
        public string InfoReg4 { get; set; }
        public bool InfoEnabledReg4 { get; set; }
        public string RegIp4 { get; set; }
        public SolidColorBrush Reg5 { get; set; }
        public SolidColorBrush LabelReg5 { get; set; }
        public string ReplyReg5 { get; set; }
        public bool CheckedReg5 { get; set; }
        public string InfoReg5 { get; set; }
        public bool InfoEnabledReg5 { get; set; }
        public string RegIp5 { get; set; }
        public SolidColorBrush Reg6 { get; set; }
        public SolidColorBrush LabelReg6 { get; set; }
        public string ReplyReg6 { get; set; }
        public bool CheckedReg6 { get; set; }
        public string InfoReg6 { get; set; }
        public bool InfoEnabledReg6 { get; set; }
        public string RegIp6 { get; set; }
        public SolidColorBrush Reg7 { get; set; }
        public SolidColorBrush LabelReg7 { get; set; }
        public string ReplyReg7 { get; set; }
        public bool CheckedReg7 { get; set; }
        public string InfoReg7 { get; set; }
        public bool InfoEnabledReg7 { get; set; }
        public string RegIp7 { get; set; }
        public SolidColorBrush Reg8 { get; set; }
        public SolidColorBrush LabelReg8 { get; set; }
        public string ReplyReg8 { get; set; }
        public bool CheckedReg8 { get; set; }
        public string InfoReg8 { get; set; }
        public bool InfoEnabledReg8 { get; set; }
        public string RegIp8 { get; set; }
        public SolidColorBrush Reg9 { get; set; }
        public SolidColorBrush LabelReg9 { get; set; }
        public string ReplyReg9 { get; set; }
        public bool CheckedReg9 { get; set; }
        public string InfoReg9 { get; set; }
        public bool InfoEnabledReg9 { get; set; }
        public string RegIp9 { get; set; }
        public SolidColorBrush Reg10 { get; set; }
        public SolidColorBrush LabelReg10 { get; set; }
        public string ReplyReg10 { get; set; }
        public bool CheckedReg10 { get; set; }
        public string InfoReg10 { get; set; }
        public bool InfoEnabledReg10 { get; set; }
        public string RegIp10 { get; set; }
        public SolidColorBrush Kms1 { get; set; }
        public string ReplyKms1 { get; set; }
        public bool CheckedKms1 { get; set; }
        public string InfoKms1 { get; set; }
        public string LabelKms1 { get; set; }
        public bool InfoEnabledKms1 { get; set; }
        public string KmsIp1 { get; set; }
        public SolidColorBrush Kms2 { get; set; }
        public string ReplyKms2 { get; set; }
        public bool CheckedKms2 { get; set; }
        public string InfoKms2 { get; set; }
        public string LabelKms2 { get; set; }
        public bool InfoEnabledKms2 { get; set; }
        public string KmsIp2 { get; set; }
        public SolidColorBrush Kms3 { get; set; }
        public string ReplyKms3 { get; set; }
        public bool CheckedKms3 { get; set; }
        public string InfoKms3 { get; set; }
        public string LabelKms3 { get; set; }
        public bool InfoEnabledKms3 { get; set; }
        public string KmsIp3 { get; set; }
        public SolidColorBrush Kms4 { get; set; }
        public string ReplyKms4 { get; set; }
        public bool CheckedKms4 { get; set; }
        public string InfoKms4 { get; set; }
        public string LabelKms4 { get; set; }
        public bool InfoEnabledKms4 { get; set; }
        public string KmsIp4 { get; set; }
        public SolidColorBrush Kms5 { get; set; }
        public string ReplyKms5 { get; set; }
        public bool CheckedKms5 { get; set; }
        public string InfoKms5 { get; set; }
        public string LabelKms5 { get; set; }
        public bool InfoEnabledKms5 { get; set; }
        public string KmsIp5 { get; set; }
        public SolidColorBrush Kms6 { get; set; }
        public string ReplyKms6 { get; set; }
        public bool CheckedKms6 { get; set; }
        public string InfoKms6 { get; set; }
        public string LabelKms6 { get; set; }
        public bool InfoEnabledKms6 { get; set; }
        public string KmsIp6 { get; set; }
        public SolidColorBrush Kms7 { get; set; }
        public string ReplyKms7 { get; set; }
        public bool CheckedKms7 { get; set; }
        public string InfoKms7 { get; set; }
        public string LabelKms7 { get; set; }
        public bool InfoEnabledKms7 { get; set; }
        public string KmsIp7 { get; set; }
        public SolidColorBrush Kms8 { get; set; }
        public string ReplyKms8 { get; set; }
        public bool CheckedKms8 { get; set; }
        public string InfoKms8 { get; set; }
        public string LabelKms8 { get; set; }
        public bool InfoEnabledKms8 { get; set; }
        public string KmsIp8 { get; set; }
        public SolidColorBrush Kms9 { get; set; }
        public string ReplyKms9 { get; set; }
        public bool CheckedKms9 { get; set; }
        public string InfoKms9 { get; set; }
        public string LabelKms9 { get; set; }
        public bool InfoEnabledKms9 { get; set; }
        public string KmsIp9 { get; set; }
        public SolidColorBrush Office1 { get; set; }
        public string ReplyOffice1 { get; set; }
        public bool CheckedOffice1 { get; set; }
        public string InfoOffice1 { get; set; }
        public bool InfoEnabledOffice1 { get; set; }
        public string OfficeIp1 { get; set; }
        public SolidColorBrush Office2 { get; set; }
        public string ReplyOffice2 { get; set; }
        public bool CheckedOffice2 { get; set; }
        public string OfficeIp2 { get; set; }
        public SolidColorBrush Office3 { get; set; }
        public string ReplyOffice3 { get; set; }
        public bool CheckedOffice3 { get; set; }
        public string OfficeIp3 { get; set; }
        public SolidColorBrush Office4 { get; set; }
        public string ReplyOffice4 { get; set; }
        public bool CheckedOffice4 { get; set; }
        public string OfficeIp4 { get; set; }
        public SolidColorBrush Office5 { get; set; }
        public string ReplyOffice5 { get; set; }
        public bool CheckedOffice5 { get; set; }
        public string LabelOffice5 { get; set; }
        public string OfficeIp5 { get; set; }
        public SolidColorBrush Office6 { get; set; }
        public string ReplyOffice6 { get; set; }
        public bool CheckedOffice6 { get; set; }
        public string OfficeIp6 { get; set; }
        public SolidColorBrush Office7 { get; set; }
        public string ReplyOffice7 { get; set; }
        public bool CheckedOffice7 { get; set; }
        public string OfficeIp7 { get; set; }
        public SolidColorBrush Office8 { get; set; }
        public string ReplyOffice8 { get; set; }
        public bool CheckedOffice8 { get; set; }
        public string InfoOffice8 { get; set; }
        public string LabelOffice8 { get; set; }
        public bool InfoEnabledOffice8 { get; set; }
        public string OfficeIp8 { get; set; }
        public SolidColorBrush Mobile1 { get; set; }
        public string ReplyMobile1 { get; set; }
        public bool CheckedMobile1 { get; set; }
        public string MobileIp1 { get; set; }
        public SolidColorBrush Mobile2 { get; set; }
        public string ReplyMobile2 { get; set; }
        public bool CheckedMobile2 { get; set; }
        public string MobileIp2 { get; set; }
        public SolidColorBrush Mobile3 { get; set; }
        public string ReplyMobile3 { get; set; }
        public bool CheckedMobile3 { get; set; }
        public string MobileIp3 { get; set; }
        public SolidColorBrush Mobile4 { get; set; }
        public string ReplyMobile4 { get; set; }
        public bool CheckedMobile4 { get; set; }
        public string MobileIp4 { get; set; }
        public SolidColorBrush Mobile5 { get; set; }
        public string ReplyMobile5 { get; set; }
        public bool CheckedMobile5 { get; set; }
        public string MobileIp5 { get; set; }
        public SolidColorBrush Mobile6 { get; set; }
        public string ReplyMobile6 { get; set; }
        public bool CheckedMobile6 { get; set; }
        public string MobileIp6 { get; set; }
        public SolidColorBrush Mobile7 { get; set; }
        public string ReplyMobile7 { get; set; }
        public bool CheckedMobile7 { get; set; }
        public string MobileIp7 { get; set; }
        public SolidColorBrush Mobile8 { get; set; }
        public string ReplyMobile8 { get; set; }
        public bool CheckedMobile8 { get; set; }
        public string MobileIp8 { get; set; }
        public SolidColorBrush Mobile9 { get; set; }
        public string ReplyMobile9 { get; set; }
        public bool CheckedMobile9 { get; set; }
        public string MobileIp9 { get; set; }
        public SolidColorBrush Mobile10 { get; set; }
        public string ReplyMobile10 { get; set; }
        public bool CheckedMobile10 { get; set; }
        public string MobileIp10 { get; set; }



        public SolidColorBrush LightRed { get; set; }
        public SolidColorBrush Red { get; set; }
        public SolidColorBrush Green { get; set; }
        public SolidColorBrush Black { get; set; }
        public SolidColorBrush White { get; set; }
        public SolidColorBrush Yellow { get; set; }
        public SolidColorBrush Blue { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }
        public SolidColorBrush BorderColor { get; set; }
        public SolidColorBrush PrimaryDark { get; set; }
        public SolidColorBrush PrimaryMid { get; set; }
        public SolidColorBrush PrimaryLight { get; set; }
        public SolidColorBrush SecondaryAccent { get; set; }
        public bool IsTopMost { get; set; }
        public bool IsRunning { get; set; }
        public bool IsNotRunning { get; set; }
        public bool StopButtonEnabled { get; set; }
        public bool RunButtonEnabled { get; set; }
        public bool NeedsData { get; set; }
        public bool ShowingMenu { get; set; }
        public bool CanClick { get; set; }
        public Cursor Cursor { get; set; }
        public double WindowHeight { get; set; }
        public double WindowWidth { get; set; }
        public double LeftPosition { get; set; }
        public double TopPosition { get; set; }
        public double ResizeBorderThickness { get; set; } = 14;
        public double BorderThickness { get; set; } = 0;
        public double ShadowThickness { get; set; } = 10;
        public double CaptionHeight { get; set; } = 24;
        public string MaximizeSource { get; set; } = "/Maximize.png";
        public string MaximizeText { get; set; } = "Maximize";
        public CornerRadius Radius { get; set; } = new CornerRadius(8, 8, 0, 0);
        public CornerRadius ButtonRadius { get; set; } = new CornerRadius(0, 8, 0, 0);

        private WindowState _currentWindowState;
        public PaletteHelper PaletteHelper { get; set; } = new PaletteHelper();
        public ITheme Theme { get; set; }


        public WindowState CurrentWindowState
        {
            get { return _currentWindowState; }
            set 
            { 
                
                if(value == WindowState.Maximized)
                {
                    ShadowThickness = 0;
                    BorderThickness = 7;
                    ResizeBorderThickness = 0;
                    CaptionHeight = 34;
                    Radius = new CornerRadius(0);
                    ButtonRadius = new CornerRadius(0);
                    MaximizeSource = "/Restore.png";
                    MaximizeText = "Restore Down";
                }
                else
                {
                    ShadowThickness = 7;
                    BorderThickness = 0;
                    ResizeBorderThickness = 14;
                    CaptionHeight = 24;
                    Radius = new CornerRadius(8, 8, 0, 0);
                    ButtonRadius = new CornerRadius(0, 8, 0, 0);
                    MaximizeSource = "/Maximize.png";
                    MaximizeText = "Maximize";
                }
                _currentWindowState = value;
            }
        }

        public bool WendysData
        {
            get { return _wendysData; }
            set 
            { 
                _wendysData = value;
                netstats = Registry.CurrentUser.OpenSubKey(@"Software\Netstats", true);
                netstats.SetValue("wendys_data", value);
            }
        }
        public bool PizzaData
        {
            get { return _pizzaData; }
            set 
            { 
                _pizzaData = value;
                netstats = Registry.CurrentUser.OpenSubKey(@"Software\Netstats", true);
                netstats.SetValue("pizza_data", value);
            }
        }
        public bool[] Verifones { get; set; }

        private bool _checkedAll;

        public bool CheckedAll
        {
            get { return _checkedAll; }
            set
            {
                _checkedAll = value;
                CheckedReg1 = value;
                CheckedReg2 = value;
                CheckedReg3 = value;
                CheckedReg4 = value;
                CheckedReg5 = value;
                CheckedReg6 = value;
                CheckedReg7 = value;
                CheckedReg8 = value;
                CheckedReg9 = value;
                CheckedReg10 = value;
                CheckedKms1 = value;
                CheckedKms2 = value;
                CheckedKms3 = value;
                CheckedKms4 = value;
                CheckedKms5 = value;
                CheckedKms6 = value;
                CheckedKms7 = value;
                CheckedKms8 = value;
                CheckedKms9 = value;
                CheckedOffice1 = value;
                CheckedOffice2 = value;
                CheckedOffice3 = value;
                CheckedOffice4 = value;
                CheckedOffice5 = value;
                CheckedOffice6 = value;
                CheckedOffice7 = value;
                CheckedOffice8 = value;
                CheckedMobile1 = value;
                CheckedMobile2 = value;
                CheckedMobile3 = value;
                CheckedMobile4 = value;
                CheckedMobile5 = value;
                CheckedMobile6 = value;
                CheckedMobile7 = value;
                CheckedMobile8 = value;
                CheckedMobile9 = value;
                CheckedMobile10 = value;
            }
        }


        public UpdateCommand UpdateCommand { get; set; }
        public ClearCommand ClearCommand { get; set; }
        public RunCommand RunCommand { get; set; }
        public StopCommand StopCommand { get; set; }
        public DoallCommand DoallCommand { get; set; }
        public DamewareCommand DamewareCommand { get; set; }
        public ConnectDeviceCommand ConnectDeviceCommand { get; set; }
        public CopyCommand CopyCommand { get; set; }
        public ShowMenuCommand ShowMenuCommand { get; set; }
        public LoadedWindowCommand LoadedWindowCommand { get; set; }
        public CheckUpdateCommand CheckUpdateCommand { get; set; }
        public CheckChangedCommand CheckChangedCommand { get; set; }
        public ExitCommand ExitCommand { get; set; }
        public MaximizeCommand MaximizeCommand { get; set; }
        public MinimizeCommand MinimizeCommand { get; set; }
        public MenuCommand MenuCommand { get; set; }
        public SMBCommand SMBCommand { get; set; }

        public string StoreNumber
        {
            get 
            {
                return _storeNumber;
            }
            set
            {
                if(_storeNumber != value)
                {
                    IsRunning = false;
                    IsNotRunning = true;
                    NeedsData = true;
                }
                _storeNumber = value;
            }
        }

        #endregion

        #region Set light and dark theme
        private void SetDarkTheme()
        {
            this.Theme.Background = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#232323");
            this.Theme.Paper = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#3d3d3d");
            this.Theme.PrimaryDark = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#232323");
            this.Theme.Body = Colors.White;
            this.Theme.CheckBoxOff = Colors.LightGray;
            this.Theme.SecondaryMid = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#04d1ca");
            this.Theme.FlatButtonRipple = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#04d1ca");
            this.Theme.TextBoxBorder = Colors.DarkGray;
            this.PaletteHelper.SetTheme(this.Theme);
            Red = new SolidColorBrush(Colors.DarkRed);
            LightRed = new SolidColorBrush(Colors.LightCoral);
            Yellow = new SolidColorBrush(Colors.Gold);
            Green = new SolidColorBrush(Colors.DarkGreen);
            Blue = new SolidColorBrush(Colors.DodgerBlue);
            Black = new SolidColorBrush(Colors.Black);
            White = new SolidColorBrush(this.Theme.Body);
            LabelReg1 = White;
            LabelReg2 = White;
            LabelReg3 = White;
            LabelReg4 = White;
            LabelReg5 = White;
            LabelReg6 = White;
            LabelReg7 = White;
            LabelReg8 = White;
            LabelReg9 = White;
            LabelReg10 = White;
        }

        private void SetLightTheme()
        {
            this.Theme.Background = Colors.LightGray;
            this.Theme.Paper = Colors.White;
            this.Theme.PrimaryDark = Colors.LightGray;
            this.Theme.Body = Colors.Black;
            this.Theme.CheckBoxOff = Colors.DarkGray;
            this.Theme.SecondaryMid = Colors.LightSkyBlue;
            this.Theme.FlatButtonRipple = Colors.LightSkyBlue;
            this.Theme.TextBoxBorder = Colors.LightGray;
            this.PaletteHelper.SetTheme(this.Theme);
            Red = new SolidColorBrush(Colors.Red);
            LightRed = new SolidColorBrush(Colors.DarkRed);
            Yellow = new SolidColorBrush(Colors.Yellow);
            Green = new SolidColorBrush(Colors.ForestGreen);
            Blue = new SolidColorBrush(Colors.SteelBlue);
            Black = new SolidColorBrush(Colors.Black);
            White = new SolidColorBrush(this.Theme.Body);
            LabelReg1 = White;
            LabelReg2 = White;
            LabelReg3 = White;
            LabelReg4 = White;
            LabelReg5 = White;
            LabelReg6 = White;
            LabelReg7 = White;
            LabelReg8 = White;
            LabelReg9 = White;
            LabelReg10 = White;
        }
        #endregion

        #region Constructor for the ShellViewModel
        public ShellViewModel()
        {
            customBox = delegate (string message, MessageBoxViewModel.MessageType type, MessageBoxViewModel.MessageButtons buttons)
            {
                MessageBoxViewModel model = new MessageBoxViewModel(message, type, buttons);
                ActivateItem(model);
                window.ShowDialog(model);
                DeactivateItem(model, true);
            };
            this.UpdateCommand = new UpdateCommand(this);
            this.ClearCommand = new ClearCommand(this);
            this.RunCommand = new RunCommand(this);
            this.StopCommand = new StopCommand(this);
            this.DoallCommand = new DoallCommand(this);
            this.DamewareCommand = new DamewareCommand(this);
            this.ConnectDeviceCommand = new ConnectDeviceCommand(this);
            this.CopyCommand = new CopyCommand(this);
            this.ShowMenuCommand = new ShowMenuCommand(this);
            this.LoadedWindowCommand = new LoadedWindowCommand(this);
            this.CheckUpdateCommand = new CheckUpdateCommand(this);
            this.CheckChangedCommand = new CheckChangedCommand(this);
            this.ExitCommand = new ExitCommand(this);
            this.MaximizeCommand = new MaximizeCommand(this);
            this.MinimizeCommand = new MinimizeCommand(this);
            this.MenuCommand = new MenuCommand(this);
            this.SMBCommand = new SMBCommand(this);
            IsNotRunning = true;
            IsRunning = false;
            RunButtonEnabled = false;
            StopButtonEnabled = false;
            NeedsData = true;
            ShowingMenu = false;
            IsWendys = false;
            LabelKms1 = "Make 1";
            LabelKms2 = "Make 2";
            LabelKms3 = "Cut";
            LabelKms4 = "Fryer";
            LabelKms5 = "Fryer";
            LabelKms6 = "Sandwich";
            LabelKms7 = "Salad";
            LabelKms8 = "Fryer Cut";
            LabelKms9 = "CO Monitor";
            LabelOffice5 = "Cisco";
            LabelOffice8 = "Register 12";
            wendysCheck = new NpgsqlConnection();
            ngposCheck = new NpgsqlConnection();
            wendysCheck.ConnectionString = "Server=" + wendysServer + ";Port=" + port + ";UserId=" + user + ";Password=" + password + ";Database=" + wendysDB + ";";
            ngposCheck.ConnectionString = "Server=" + pizzaServer + ";Port=" + port + ";UserId=" + user + ";Password=" + password + ";Database=" + pizzaDB + ";";


            this.Theme = this.PaletteHelper.GetTheme();

            //Create registry settings if they don't exist. Otherwise read and settings and apply.
            netstats = Registry.CurrentUser.OpenSubKey(@"Software\Netstats", true);
            if (netstats == null)
            {
                netstats = Registry.CurrentUser.CreateSubKey(@"Software\Netstats", true);
                netstats.SetValue("wendys_data", true);
                netstats.SetValue("pizza_data", true);
                netstats.SetValue("dark_mode", true);
                IsDarkTheme = true;
                netstats.Close();
            }
            else
            {
                _wendysData = Convert.ToBoolean(netstats.GetValue("wendys_data")) ? true : false;

                _pizzaData = Convert.ToBoolean(netstats.GetValue("pizza_data")) ? true : false;

                IsDarkTheme = Convert.ToBoolean(netstats.GetValue("dark_mode")) ? true : false;

            }


            if (IsDarkTheme)
            {
                SetDarkTheme();
            }
            else
            {
                SetLightTheme();
            }

            Reg1 = Black;
            Reg2 = Black;
            Reg3 = Black;
            Reg4 = Black;
            Reg5 = Black;
            Reg6 = Black;
            Reg7 = Black;
            Reg8 = Black;
            Reg9 = Black;
            Reg10 = Black;
            Kms1 = Black;
            Kms2 = Black;
            Kms3 = Black;
            Kms4 = Black;
            Kms5 = Black;
            Kms6 = Black;
            Kms7 = Black;
            Kms8 = Black;
            Kms9 = Black;
            Office1 = Black;
            Office2 = Black;
            Office3 = Black;
            Office4 = Black;
            Office5 = Black;
            Office6 = Black;
            Office7 = Black;
            Office8 = Black;
            Mobile1 = Black;
            Mobile2 = Black;
            Mobile3 = Black;
            Mobile4 = Black;
            Mobile5 = Black;
            Mobile6 = Black;
            Mobile7 = Black;
            Mobile8 = Black;
            Mobile9 = Black;
            Mobile10 = Black;
            LabelReg1 = White;
            LabelReg2 = White;
            LabelReg3 = White;
            LabelReg4 = White;
            LabelReg5 = White;
            LabelReg6 = White;
            LabelReg7 = White;
            LabelReg8 = White;
            LabelReg9 = White;
            LabelReg10 = White;

            

            Verifones = new bool[10];

            AddVersionNumber();

            

            WindowHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            WindowWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            CurrentWindowState = WindowState.Normal;

        }

        #endregion

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();


        #region Update methods
        /// <summary>
        /// Checks for updates and applies them when the program is opened.
        /// </summary>
        /// <returns></returns>
        public async Task BackgroundUpdate()
        {
            try
            {
                using (var manager = new UpdateManager(updatePath))
                {
                    await manager.UpdateApp();
                }
            }

            catch (Exception e1)
            {
                var model = new MessageBoxViewModel(e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                ActivateItem(model);
                var result = window.ShowDialog(model);
                DeactivateItem(model, true);
            }
        }

        public async Task CheckForUpdates()
        {
            try
            {
                using var manager = new UpdateManager(updatePath);
                var info = await manager.CheckForUpdate();

                if (!info.ReleasesToApply.Any())
                {
                    bool doUpdate = true;
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        var model = new MessageBoxViewModel("An update is available. Would you like to download?", MessageBoxViewModel.MessageType.Confirmation, MessageBoxViewModel.MessageButtons.YesNo);
                        ActivateItem(model);
                        var result = window.ShowDialog(model);
                        if (result == false)
                        {
                            doUpdate = false;
                            DeactivateItem(model, true);
                            return;
                        }
                        else
                        {
                            DeactivateItem(model, true);
                        }
                    });

                    if (doUpdate)
                    {
                        await manager.DownloadReleases(info.ReleasesToApply);
                        await manager.ApplyReleases(info);
                    }
                }

                else
                {
                    object[] parameters = { "No updates found.", MessageType.Info, MessageButtons.Ok };
                    Application.Current.Dispatcher.Invoke(customBox, parameters);
                }
            }
            catch (Exception e1)
            {
                object[] parameters = { e1.Message, MessageType.Error, MessageButtons.Ok };
                Application.Current.Dispatcher.Invoke(customBox, parameters);
            }
        }

        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Title = "Netstats (v" + versionInfo.FileVersion + ")";
        }
        #endregion

        #region Set the property of the appropriate UI object

        /// <summary>
        /// Set the property of the appropriate UI object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void SetPropertyValue(string name, object value)
        {
            switch (name)
            {
                case "Reg1":
                    if(Convert.ToInt32(value) < 0)
                    {
                        ReplyReg1 = "Offline";
                        Reg1 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg1 = (string)value;
                        Reg1 = Green;
                    }
                    else
                    {
                        ReplyReg1 = (string)value;
                        Reg1 = Yellow;
                    }
                    break;
                case "Verifone1":
                    if(Convert.ToInt32(value) > 0)
                    {
                        LabelReg1 = Blue;
                    }
                    else
                    {
                        if (Verifones[0])
                        {
                            LabelReg1 = LightRed;
                        }
                        else
                        {
                            LabelReg1 = White;
                        }
                    }
                    break;
                //case "ReplyReg1":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg1 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg1 = (string)value;
                //    }
                //    break;
                case "Reg2":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg2 = "Offline";
                        Reg2 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg2 = (string)value;
                        Reg2 = Green;
                    }
                    else
                    {
                        ReplyReg2 = (string)value;
                        Reg2 = Yellow;
                    }
                    break;
                case "Verifone2":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg2 = Blue;
                    }
                    else
                    {
                        if (Verifones[1])
                        {
                            LabelReg2 = LightRed;
                        }
                        else
                        {
                            LabelReg2 = White;
                        }
                    }
                    break;
                //case "ReplyReg2":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg2 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg2 = (string)value;
                //    }
                //    break;
                case "Reg3":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg3 = "Offline";
                        Reg3 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg3 = (string)value;
                        Reg3 = Green;
                    }
                    else
                    {
                        ReplyReg3 = (string)value;
                        Reg3 = Yellow;
                    }
                    break;
                case "Verifone3":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg3 = Blue;
                    }
                    else
                    {
                        if (Verifones[2])
                        {
                            LabelReg3 = LightRed;
                        }
                        else
                        {
                            LabelReg3 = White;
                        }
                    }
                    break;
                //case "ReplyReg3":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg3 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg3 = (string)value;
                //    }
                //    break;
                case "Reg4":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg4 = "Offline";
                        Reg4 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg4 = (string)value;
                        Reg4 = Green;
                    }
                    else
                    {
                        ReplyReg4 = (string)value;
                        Reg4 = Yellow;
                    }
                    break;
                case "Verifone4":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg4 = Blue;
                    }
                    else
                    {
                        if (Verifones[3])
                        {
                            LabelReg4 = LightRed;
                        }
                        else
                        {
                            LabelReg4 = White;
                        }
                    }
                    break;
                //case "ReplyReg4":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg4 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg4 = (string)value;
                //    }
                //    break;
                case "Reg5":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg5 = "Offline";
                        Reg5 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg5 = (string)value;
                        Reg5 = Green;
                    }
                    else
                    {
                        ReplyReg5 = (string)value;
                        Reg5 = Yellow;
                    }
                    break;
                case "Verifone5":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg5 = Blue;
                    }
                    else
                    {
                        if (Verifones[4])
                        {
                            LabelReg5 = LightRed;
                        }
                        else
                        {
                            LabelReg5 = White;
                        }
                    }
                    break;
                //case "ReplyReg5":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg5 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg5 = (string)value;
                //    }
                //    break;
                case "Reg6":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg6 = "Offline";
                        Reg6 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg6 = (string)value;
                        Reg6 = Green;
                    }
                    else
                    {
                        ReplyReg6 = (string)value;
                        Reg6 = Yellow;
                    }
                    break;
                case "Verifone6":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg6 = Blue;
                    }
                    else
                    {
                        if (Verifones[5])
                        {
                            LabelReg6 = LightRed;
                        }
                        else
                        {
                            LabelReg6 = White;
                        }
                    }
                    break;
                //case "ReplyReg6":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg6 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg6 = (string)value;
                //    }
                //    break;
                case "Reg7":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg7 = "Offline";
                        Reg7 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg7 = (string)value;
                        Reg7 = Green;
                    }
                    else
                    {
                        ReplyReg7 = (string)value;
                        Reg7 = Yellow;
                    }
                    break;
                case "Verifone7":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg7 = Blue;
                    }
                    else
                    {
                        if (Verifones[6])
                        {
                            LabelReg7 = LightRed;
                        }
                        else
                        {
                            LabelReg7 = White;
                        }
                    }
                    break;
                //case "ReplyReg7":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg7 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg7 = (string)value;
                //    }
                //    break;
                case "Reg8":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg8 = "Offline";
                        Reg8 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg8 = (string)value;
                        Reg8 = Green;
                    }
                    else
                    {
                        ReplyReg8 = (string)value;
                        Reg8 = Yellow;
                    }
                    break;
                case "Verifone8":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg8 = Blue;
                    }
                    else
                    {
                        if (Verifones[7])
                        {
                            LabelReg8 = LightRed;
                        }
                        else
                        {
                            LabelReg8 = White;
                        }
                    }
                    break;
                //case "ReplyReg8":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg8 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg8 = (string)value;
                //    }
                //    break;
                case "Reg9":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg9 = "Offline";
                        Reg9 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg9 = (string)value;
                        Reg9 = Green;
                    }
                    else
                    {
                        ReplyReg9 = (string)value;
                        Reg9 = Yellow;
                    }
                    break;
                case "Verifone9":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg9 = Blue;
                    }
                    else
                    {
                        if (Verifones[8])
                        {
                            LabelReg9 = LightRed;
                        }
                        else
                        {
                            LabelReg9 = White;
                        }
                    }
                    break;
                //case "ReplyReg9":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg9 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg9 = (string)value;
                //    }
                //    break;
                case "Reg10":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyReg10 = "Offline";
                        Reg10 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyReg10 = (string)value;
                        Reg10 = Green;
                    }
                    else
                    {
                        ReplyReg10 = (string)value;
                        Reg10 = Yellow;
                    }
                    break;
                case "Verifone10":
                    if (Convert.ToInt32(value) > 0)
                    {
                        LabelReg10 = Blue;
                    }
                    else
                    {
                        if (Verifones[9])
                        {
                            LabelReg10 = LightRed;
                        }
                        else
                        {
                            LabelReg10 = White;
                        }
                    }
                    break;
                //case "ReplyReg10":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyReg10 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyReg10 = (string)value;
                //    }
                //    break;
                case "Kms1":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms1 = "Offline";
                        Kms1 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms1 = (string)value;
                        Kms1 = Green;
                    }
                    else
                    {
                        ReplyKms1 = (string)value;
                        Kms1 = Yellow;
                    }
                    break;
                //case "ReplyKms1":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms1 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms1 = (string)value;
                //    }
                //    break;
                case "Kms2":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms2 = "Offline";
                        Kms2 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms2 = (string)value;
                        Kms2 = Green;
                    }
                    else
                    {
                        ReplyKms2 = (string)value;
                        Kms2 = Yellow;
                    }
                    break;
                //case "ReplyKms2":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms2 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms2 = (string)value;
                //    }
                //    break;
                case "Kms3":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms3 = "Offline";
                        Kms3 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms3 = (string)value;
                        Kms3 = Green;
                    }
                    else
                    {
                        ReplyKms3 = (string)value;
                        Kms3 = Yellow;
                    }
                    break;
                //case "ReplyKms3":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms3 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms3 = (string)value;
                //    }
                //    break;
                case "Kms4":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms4 = "Offline";
                        Kms4 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms4 = (string)value;
                        Kms4 = Green;
                    }
                    else
                    {
                        ReplyKms4 = (string)value;
                        Kms4 = Yellow;
                    }
                    break;
                //case "ReplyKms4":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms4 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms4 = (string)value;
                //    }
                //    break;
                case "Kms5":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms5 = "Offline";
                        Kms5 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms5 = (string)value;
                        Kms5 = Green;
                    }
                    else
                    {
                        ReplyKms5 = (string)value;
                        Kms5 = Yellow;
                    }
                    break;
                //case "ReplyKms5":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms5 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms5 = (string)value;
                //    }
                //    break;
                case "Kms6":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms6 = "Offline";
                        Kms6 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms6 = (string)value;
                        Kms6 = Green;
                    }
                    else
                    {
                        ReplyKms6 = (string)value;
                        Kms6 = Yellow;
                    }
                    break;
                //case "ReplyKms6":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms6 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms6 = (string)value;
                //    }
                //    break;
                case "Kms7":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms7 = "Offline";
                        Kms7 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms7 = (string)value;
                        Kms7 = Green;
                    }
                    else
                    {
                        ReplyKms7 = (string)value;
                        Kms7 = Yellow;
                    }
                    break;
                //case "ReplyKms7":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms7 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms7 = (string)value;
                //    }
                //    break;
                case "Kms8":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms8 = "Offline";
                        Kms8 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms8 = (string)value;
                        Kms8 = Green;
                    }
                    else
                    {
                        ReplyKms8 = (string)value;
                        Kms8 = Yellow;
                    }
                    break;
                //case "ReplyKms8":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms8 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms8 = (string)value;
                //    }
                //    break;
                case "Kms9":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyKms9 = "Offline";
                        Kms9 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyKms9 = (string)value;
                        Kms9 = Green;
                    }
                    else
                    {
                        ReplyKms9 = (string)value;
                        Kms9 = Yellow;
                    }
                    break;
                //case "ReplyKms9":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyKms9 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyKms9 = (string)value;
                //    }
                //    break;
                case "Office1":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice1 = "Offline";
                        Office1 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice1 = (string)value;
                        Office1 = Green;
                    }
                    else
                    {
                        ReplyOffice1 = (string)value;
                        Office1 = Yellow;
                    }
                    break;
                //case "ReplyOffice1":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice1 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice1 = (string)value;
                //    }
                //    break;
                case "Office2":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice2 = "Offline";
                        Office2 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice2 = (string)value;
                        Office2 = Green;
                    }
                    else
                    {
                        ReplyOffice2 = (string)value;
                        Office2 = Yellow;
                    }
                    break;
                //case "ReplyOffice2":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice2 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice2 = (string)value;
                //    }
                //    break;
                case "Office3":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice3 = "Offline";
                        Office3 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice3 = (string)value;
                        Office3 = Green;
                    }
                    else
                    {
                        ReplyOffice3 = (string)value;
                        Office3 = Yellow;
                    }
                    break;
                //case "ReplyOffice3":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice3 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice3 = (string)value;
                //    }
                //    break;
                case "Office4":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice4 = "Offline";
                        Office4 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice4 = (string)value;
                        Office4 = Green;
                    }
                    else
                    {
                        ReplyOffice4 = (string)value;
                        Office4 = Yellow;
                    }
                    break;
                //case "ReplyOffice4":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice4 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice4 = (string)value;
                //    }
                //    break;
                case "Office5":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice5 = "Offline";
                        Office5 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice5 = (string)value;
                        Office5 = Green;
                    }
                    else
                    {
                        ReplyOffice5 = (string)value;
                        Office5 = Yellow;
                    }
                    break;
                //case "ReplyOffice5":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice5 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice5 = (string)value;
                //    }
                //    break;
                case "Office6":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice6 = "Offline";
                        Office6 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice6 = (string)value;
                        Office6 = Green;
                    }
                    else
                    {
                        ReplyOffice6 = (string)value;
                        Office6 = Yellow;
                    }
                    break;
                //case "ReplyOffice6":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice6 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice6 = (string)value;
                //    }
                //    break;
                case "Office7":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice7 = "Offline";
                        Office7 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice7 = (string)value;
                        Office7 = Green;
                    }
                    else
                    {
                        ReplyOffice7 = (string)value;
                        Office7 = Yellow;
                    }
                    break;
                //case "ReplyOffice7":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice7 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice7 = (string)value;
                //    }
                //    break;
                case "Office8":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyOffice8 = "Offline";
                        Office8 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyOffice8 = (string)value;
                        Office8 = Green;
                    }
                    else
                    {
                        ReplyOffice8 = (string)value;
                        Office8 = Yellow;
                    }
                    break;
                //case "ReplyOffice8":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyOffice8 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyOffice8 = (string)value;
                //    }
                //    break;
                case "Mobile1":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile1 = "Offline";
                        Mobile1 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile1 = (string)value;
                        Mobile1 = Green;
                    }
                    else
                    {
                        ReplyMobile1 = (string)value;
                        Mobile1 = Yellow;
                    }
                    break;
                //case "ReplyMobile1":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile1 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile1 = (string)value;
                //    }
                //    break;
                case "Mobile2":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile2 = "Offline";
                        Mobile2 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile2 = (string)value;
                        Mobile2 = Green;
                    }
                    else
                    {
                        ReplyMobile2 = (string)value;
                        Mobile2 = Yellow;
                    }
                    break;
                //case "ReplyMobile2":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile2 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile2 = (string)value;
                //    }
                //    break;
                case "Mobile3":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile3 = "Offline";
                        Mobile3 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile3 = (string)value;
                        Mobile3 = Green;
                    }
                    else
                    {
                        ReplyMobile3 = (string)value;
                        Mobile3 = Yellow;
                    }
                    break;
                //case "ReplyMobile3":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile3 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile3 = (string)value;
                //    }
                //    break;
                case "Mobile4":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile4 = "Offline";
                        Mobile4 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile4 = (string)value;
                        Mobile4 = Green;
                    }
                    else
                    {
                        ReplyMobile4 = (string)value;
                        Mobile4 = Yellow;
                    }
                    break;
                //case "ReplyMobile4":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile4 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile4 = (string)value;
                //    }
                //    break;
                case "Mobile5":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile5 = "Offline";
                        Mobile5 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile5 = (string)value;
                        Mobile5 = Green;
                    }
                    else
                    {
                        ReplyMobile5 = (string)value;
                        Mobile5 = Yellow;
                    }
                    break;
                //case "ReplyMobile5":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile5 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile5 = (string)value;
                //    }
                //    break;
                case "Mobile6":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile6 = "Offline";
                        Mobile6 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile6 = (string)value;
                        Mobile6 = Green;
                    }
                    else
                    {
                        ReplyMobile6 = (string)value;
                        Mobile6 = Yellow;
                    }
                    break;
                //case "ReplyMobile6":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile6 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile6 = (string)value;
                //    }
                //    break;
                case "Mobile7":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile7 = "Offline";
                        Mobile7 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile7 = (string)value;
                        Mobile7 = Green;
                    }
                    else
                    {
                        ReplyMobile7 = (string)value;
                        Mobile7 = Yellow;
                    }
                    break;
                //case "ReplyMobile7":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile7 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile7 = (string)value;
                //    }
                //    break;
                case "Mobile8":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile8 = "Offline";
                        Mobile8 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile8 = (string)value;
                        Mobile8 = Green;
                    }
                    else
                    {
                        ReplyMobile8 = (string)value;
                        Mobile8 = Yellow;
                    }
                    break;
                //case "ReplyMobile8":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile8 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile8 = (string)value;
                //    }
                //    break;
                case "Mobile9":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile9 = "Offline";
                        Mobile9 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile9 = (string)value;
                        Mobile9 = Green;
                    }
                    else
                    {
                        ReplyMobile9 = (string)value;
                        Mobile9 = Yellow;
                    }
                    break;
                //case "ReplyMobile9":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile9 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile9 = (string)value;
                //    }
                //    break;
                case "Mobile10":
                    if (Convert.ToInt32(value) < 0)
                    {
                        ReplyMobile10 = "Offline";
                        Mobile10 = Red;
                    }
                    else if (Convert.ToInt32(value) < 350)
                    {
                        ReplyMobile10 = (string)value;
                        Mobile10 = Green;
                    }
                    else
                    {
                        ReplyMobile10 = (string)value;
                        Mobile10 = Yellow;
                    }
                    break;
                //case "ReplyMobile10":
                //    if (Convert.ToInt32(value) < 0)
                //    {
                //        ReplyMobile10 = "Offline";
                //    }
                //    else
                //    {
                //        ReplyMobile10 = (string)value;
                //    }
                //    break;
            }
        }

        #endregion


        #region Sends multi-threaded pings to all devices

        public void Update()
        {
            if (StoreNumber == null || StoreNumber.Length < 4)
            {
                var model = new MessageBoxViewModel("You must enter a valid store number.", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                ActivateItem(model);
                window.ShowDialog(model);
                DeactivateItem(model, true);
            }
            else
            {
                RunButtonEnabled = true;
                //if (wendysCheck.State == ConnectionState.Open)
                //{
                //    wendysCheck.Close();
                //}
                //if (ngposCheck.State == ConnectionState.Open)
                //{
                //    ngposCheck.Close();
                //}


                


                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg1");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg2");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg3");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg4");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg5");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg6");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg7");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg8");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg9");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Reg10");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms1");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms2");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms3");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms4");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms5");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms6");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms7");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms8");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Kms9");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office1");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office2");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office3");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office4");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office5");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office6");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office7");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Office8");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile1");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile2");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile3");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile4");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile5");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile6");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile7");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile8");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile9");
                new Thread(new ParameterizedThreadStart(PingDevice)).Start("Mobile10");
                if (!IsWendys)
                {
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone1");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone2");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone3");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone4");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone5");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone6");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone7");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone8");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone9");
                    new Thread(new ParameterizedThreadStart(PingDevice)).Start("Verifone10");
                }


                if (NeedsData)
                {
                    new Thread(new ThreadStart(GetData)).Start();
                    NeedsData = false;
                }
            }

        }

        #endregion


        #region Pulls data on all devices from the from the appropriate database
        public void GetData()
        {
            InfoEnabledReg1 = false;
            InfoEnabledReg2 = false;
            InfoEnabledReg3 = false;
            InfoEnabledReg4 = false;
            InfoEnabledReg5 = false;
            InfoEnabledReg6 = false;
            InfoEnabledReg7 = false;
            InfoEnabledReg8 = false;
            InfoEnabledReg9 = false;
            InfoEnabledReg10 = false;
            InfoEnabledKms1 = false;
            InfoEnabledKms2 = false;
            InfoEnabledKms3 = false;
            InfoEnabledKms4 = false;
            InfoEnabledKms5 = false;
            InfoEnabledKms6 = false;
            InfoEnabledKms7 = false;
            InfoEnabledKms8 = false;
            InfoEnabledKms9 = false;
            InfoEnabledOffice1 = false;
            InfoEnabledOffice8 = false;
            InfoReg1 = null;
            InfoReg2 = null;
            InfoReg3 = null;
            InfoReg4 = null;
            InfoReg5 = null;
            InfoReg6 = null;
            InfoReg7 = null;
            InfoReg8 = null;
            InfoReg9 = null;
            InfoReg10 = null;
            InfoKms1 = null;
            InfoKms2 = null;
            InfoKms3 = null;
            InfoKms4 = null;
            InfoKms5 = null;
            InfoKms6 = null;
            InfoKms7 = null;
            InfoKms8 = null;
            InfoKms9 = null;
            InfoOffice1 = null;
            InfoOffice8 = null;
            
            for(int i = 0; i < 10; i++)
            {
                Verifones[i] = false;
            }

            if (IsWendys && WendysData)
            {
                try
                {
                    wendysCheck.Open();
                    NpgsqlCommand command = new NpgsqlCommand("SELECT term, st, model, last_updated FROM store.terminal WHERE store_number=" + _storeNumber, wendysCheck); //Check column names.
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string regNumber;
                        string temp;
                        int time;
                        string newTime;
                        regNumber = reader.GetInt32(0).ToString();
                        time = reader.GetInt32(3);
                        newTime = ConvertToDate(time).ToString();
                        temp = "Model: " + reader.GetString(2) + "\nS/N: " + reader.GetString(1) + "\nLast Updated: " + newTime;

                        switch (regNumber)
                        {
                            case "1":
                                InfoReg1 = temp;
                                InfoEnabledReg1 = true;
                                break;
                            case "2":
                                InfoReg2 = temp;
                                InfoEnabledReg2 = true;
                                break;
                            case "3":
                                InfoReg3 = temp;
                                InfoEnabledReg3 = true;
                                break;
                            case "4":
                                InfoReg4 = temp;
                                InfoEnabledReg4 = true;
                                break;
                            case "5":
                                InfoReg5 = temp;
                                InfoEnabledReg5 = true;
                                break;
                            case "6":
                                InfoReg6 = temp;
                                InfoEnabledReg6 = true;
                                break;
                            case "7":
                                InfoReg7 = temp;
                                InfoEnabledReg7 = true;
                                break;
                            case "8":
                                InfoReg8 = temp;
                                InfoEnabledReg8 = true;
                                break;
                            case "9":
                                InfoReg9 = temp;
                                InfoEnabledReg9 = true;
                                break;
                            case "10":
                                InfoReg10 = temp;
                                InfoEnabledReg10 = true;
                                break;
                            case "81":
                                InfoKms1 = temp;
                                InfoEnabledKms1 = true;
                                break;
                            case "82":
                                InfoKms2 = temp;
                                InfoEnabledKms2 = true;
                                break;
                            case "83":
                                InfoKms3 = temp;
                                InfoEnabledKms3 = true;
                                break;
                            case "84":
                                InfoKms4 = temp;
                                InfoEnabledKms4 = true;
                                break;
                            case "85":
                                InfoKms5 = temp;
                                InfoEnabledKms5 = true;
                                break;
                            case "86":
                                InfoKms6 = temp;
                                InfoEnabledKms6 = true;
                                break;
                            case "87":
                                InfoKms7 = temp;
                                InfoEnabledKms7 = true;
                                break;
                            case "88":
                                InfoKms8 = temp;
                                InfoEnabledKms8 = true;
                                break;
                            case "89":
                                InfoKms9 = temp;
                                InfoEnabledKms9 = true;
                                break;
                            case "254":
                                InfoOffice1 = temp;
                                InfoEnabledOffice1 = true;
                                break;
                        }
                    }
                    wendysCheck.Close();
                }
                catch (Exception e)
                {
                    object[] parameters = new object[] {e.Message, MessageType.Error, MessageButtons.Ok };
                    Application.Current.Dispatcher.Invoke(customBox, parameters);
                }

            }

            else if (!IsWendys && PizzaData)
            {
                try
                {
                    ngposCheck.Open();
                    NpgsqlCommand command = new NpgsqlCommand("SELECT reg, service_tag, model, last_updated, device_on, corp_ip FROM ngpos.ng_check WHERE store=" + _storeNumber, ngposCheck);
                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string regNumber;
                        string temp;
                        int time;
                        string newTime;
                        string whprocessor;
                        regNumber = reader.GetInt32(0).ToString();
                        time = reader.GetInt32(3);
                        newTime = ConvertToDate(time).ToString();
                        whprocessor = reader.GetString(5);
                        temp = "Model: " + reader.GetString(2) + "\nS/N: " + reader.GetString(1) + "\nLast updated: " + newTime + "\n" + whprocessor;
                        int hasVerifone = reader.GetInt32(4);

                        if(Convert.ToInt32(regNumber) <= 10)
                        {
                            if (hasVerifone == 1)
                            {
                                Verifones[Convert.ToInt32(regNumber) - 1] = true;
                            }
                            else
                            {
                                Verifones[Convert.ToInt32(regNumber) - 1] = false;
                            }
                        }

                        switch (regNumber)
                        {
                            case "1":
                                InfoReg1 = temp;
                                InfoEnabledReg1 = true;
                                break;
                            case "2":
                                InfoReg2 = temp;
                                InfoEnabledReg2 = true;
                                break;
                            case "3":
                                InfoReg3 = temp;
                                InfoEnabledReg3 = true;
                                break;
                            case "4":
                                InfoReg4 = temp;
                                InfoEnabledReg4 = true;
                                break;
                            case "5":
                                InfoReg5 = temp;
                                InfoEnabledReg5 = true;
                                break;
                            case "6":
                                InfoReg6 = temp;
                                InfoEnabledReg6 = true;
                                break;
                            case "7":
                                InfoReg7 = temp;
                                InfoEnabledReg7 = true;
                                break;
                            case "8":
                                InfoReg8 = temp;
                                InfoEnabledReg8 = true;
                                break;
                            case "9":
                                InfoReg9 = temp;
                                InfoEnabledReg9 = true;
                                break;
                            case "10":
                                InfoReg10 = temp;
                                InfoEnabledReg10 = true;
                                break;
                            case "12":
                                InfoOffice8 = temp;
                                InfoEnabledOffice8 = true;
                                break;
                            case "254":
                                InfoOffice1 = temp;
                                InfoEnabledOffice1 = true;
                                break;
                        }
                    }
                    ngposCheck.Close();
                }
                catch (Exception e)
                {
                    object[] parameters = new object[] { e.Message, MessageType.Error, MessageButtons.Ok };
                    Application.Current.Dispatcher.Invoke(customBox, parameters);
                }
            }
            if(wendysCheck.State == ConnectionState.Open)
            {
                wendysCheck.Close();
            }
            if(ngposCheck.State == ConnectionState.Open)
            {
                ngposCheck.Close();
            }
        }

        #endregion

        #region Send a ping to the appropriate IP based on the device being pinged

        private void PingDevice(object device)
        {
            string address;
            if (StoreNumber[2] != '0')
            {
                address = "10." + StoreNumber.Substring(0, 2) + "." + StoreNumber.Substring(2) + ".";
            }
            else
            {
                address = "10." + StoreNumber.Substring(0, 2) + "." + StoreNumber[3] + ".";
            }

            int number = Convert.ToInt32(StoreNumber);
            int val = 0;
            bool devicePinging = false;

            switch (device)
            {
                case "Reg1":
                    val = 1;
                    RegIp1 = address + "1";
                    break;

                case "Reg2":
                    val = 2;
                    RegIp2 = address + "2";
                    break;

                case "Reg3":
                    val = 3;
                    RegIp3 = address + "3";
                    break;

                case "Reg4":
                    val = 4;
                    RegIp4 = address + "4";
                    break;

                case "Reg5":
                    val = 5;
                    RegIp5 = address + "5";
                    break;

                case "Reg6":
                    val = 6;
                    RegIp6 = address + "6";
                    break;

                case "Reg7":
                    val = 7;
                    RegIp7 = address + "7";
                    break;

                case "Reg8":
                    val = 8;
                    RegIp8 = address + "8";
                    break;

                case "Reg9":
                    val = 9;
                    RegIp9 = address + "9";
                    break;

                case "Reg10":
                    val = 10;
                    RegIp10 = address + "10";
                    break;

                case "Kms1":
                    val = 200;
                    KmsIp1 = address + "200";
                    if (IsWendys)
                    {
                        val = 81;
                        KmsIp1 = address + "81";
                    }
                    break;

                case "Kms2":
                    val = 201;
                    KmsIp2 = address + "201";
                    if (IsWendys)
                    {
                        val = 82;
                        KmsIp2 = address + "82";
                    }
                    break;

                case "Kms3":
                    val = 202;
                    KmsIp3 = address + "202";
                    if (IsWendys)
                    {
                        val = 83;
                        KmsIp3 = address + "83";
                    }
                    break;

                case "Kms4":
                    val = 203;
                    KmsIp4 = address + "203";
                    if (IsWendys)
                    {
                        val = 84;
                        KmsIp4 = address + "84";
                    }
                    break;

                case "Kms5":
                    val = 207;
                    KmsIp5 = address + "207";
                    if (IsWendys)
                    {
                        val = 85;
                        KmsIp5 = address + "85";
                    }
                    break;

                case "Kms6":
                    val = 208;
                    KmsIp6 = address + "208";
                    if (IsWendys)
                    {
                        val = 86;
                        KmsIp6 = address + "86";
                    }
                    break;

                case "Kms7":
                    val = 209;
                    KmsIp7 = address + "209";
                    if (IsWendys)
                    {
                        val = 87;
                        KmsIp7 = address + "87";
                    }
                    break;

                case "Kms8":
                    val = 212;
                    KmsIp8 = address + "212";
                    if (IsWendys)
                    {
                        val = 88;
                        KmsIp8 = address + "88";
                    }
                    break;

                case "Kms9":
                    val = 219;
                    KmsIp9 = address + "219";
                    if (IsWendys)
                    {
                        val = 89;
                        KmsIp9 = address + "89";
                    }
                    break;

                case "Office1":
                    val = 254;
                    OfficeIp1 = address + "254";
                    break;

                case "Office2":
                    val = 15;
                    OfficeIp2 = address + "15";
                    break;

                case "Office3":
                    val = 16;
                    OfficeIp3 = address + "16";
                    break;

                case "Office4":
                    val = 220;
                    OfficeIp4 = address + "220";
                    break;

                case "Office5":
                    val = 100;
                    OfficeIp5 = address + "100";
                    break;

                case "Office6":
                    val = 253;
                    OfficeIp6 = address + "253";
                    break;

                case "Office7":
                    val = 250;
                    OfficeIp7 = address + "250";
                    break;

                case "Mobile1":
                    val = 30;
                    MobileIp1 = address + "30";
                    break;

                case "Mobile2":
                    val = 31;
                    MobileIp2 = address + "31";
                    break;

                case "Mobile3":
                    val = 32;
                    MobileIp3 = address + "32";
                    break;

                case "Mobile4":
                    val = 33;
                    MobileIp4 = address + "33";
                    break;

                case "Mobile5":
                    val = 34;
                    MobileIp5 = address + "34";
                    break;

                case "Mobile6":
                    val = 35;
                    MobileIp6 = address + "35";
                    break;

                case "Mobile7":
                    val = 36;
                    MobileIp7 = address + "36";
                    break;

                case "Mobile8":
                    val = 37;
                    MobileIp8 = address + "37";
                    break;

                case "Mobile9":
                    val = 38;
                    MobileIp9 = address + "38";
                    break;

                case "Mobile10":
                    val = 39;
                    MobileIp10 = address + "39";
                    break;

                case "Verifone1":
                    val = 101;
                    break;

                case "Verifone2":
                    val = 102;
                    break;

                case "Verifone3":
                    val = 103;
                    break;

                case "Verifone4":
                    val = 104;
                    break;

                case "Verifone5":
                    val = 105;
                    break;

                case "Verifone6":
                    val = 106;
                    break;

                case "Verifone7":
                    val = 107;
                    break;

                case "Verifone8":
                    val = 108;
                    break;

                case "Verifone9":
                    val = 109;
                    break;

                case "Verifone10":
                    val = 110;
                    break;

                case "Office8":
                    val = 12;
                    OfficeIp8 = address + "12";
                    if (IsWendys)
                    {
                        val = 236;
                        OfficeIp8 = address + "236";
                    }
                    break;
            }

            IcmpPing icmpping = new IcmpPing(ref number, ref val);

            do
            {
                icmpping.Reset();
                string result = Math.Round(icmpping.DoATest()).ToString();

                SetPropertyValue((string)device, result);
                Thread.Sleep(1000);

                switch (device)
                {
                    case "Reg1":
                        devicePinging = CheckedReg1;
                        break;

                    case "Reg2":
                        devicePinging = CheckedReg2;
                        break;

                    case "Reg3":
                        devicePinging = CheckedReg3;
                        break;

                    case "Reg4":
                        devicePinging = CheckedReg4;
                        break;

                    case "Reg5":
                        devicePinging = CheckedReg5;
                        break;

                    case "Reg6":
                        devicePinging = CheckedReg6;
                        break;

                    case "Reg7":
                        devicePinging = CheckedReg7;
                        break;

                    case "Reg8":
                        devicePinging = CheckedReg8;
                        break;

                    case "Reg9":
                        devicePinging = CheckedReg9;
                        break;

                    case "Reg10":
                        devicePinging = CheckedReg10;
                        break;

                    case "Kms1":
                        devicePinging = CheckedKms1;
                        break;

                    case "Kms2":
                        devicePinging = CheckedKms2;
                        break;

                    case "Kms3":
                        devicePinging = CheckedKms3;
                        break;

                    case "Kms4":
                        devicePinging = CheckedKms4;
                        break;

                    case "Kms5":
                        devicePinging = CheckedKms5;
                        break;

                    case "Kms6":
                        devicePinging = CheckedKms6;
                        break;

                    case "Kms7":
                        devicePinging = CheckedKms7;
                        break;

                    case "Kms8":
                        devicePinging = CheckedKms8;
                        break;

                    case "Kms9":
                        devicePinging = CheckedKms9;
                        break;

                    case "Office1":
                        devicePinging = CheckedOffice1;
                        break;

                    case "Office2":
                        devicePinging = CheckedOffice2;
                        break;

                    case "Office3":
                        devicePinging = CheckedOffice3;
                        break;

                    case "Office4":
                        devicePinging = CheckedOffice4;
                        break;

                    case "Office5":
                        devicePinging = CheckedOffice5;
                        break;

                    case "Office6":
                        devicePinging = CheckedOffice6;
                        break;

                    case "Office7":
                        devicePinging = CheckedOffice7;
                        break;

                    case "Mobile1":
                        devicePinging = CheckedMobile1;
                        break;

                    case "Mobile2":
                        devicePinging = CheckedMobile2;
                        break;

                    case "Mobile3":
                        devicePinging = CheckedMobile3;
                        break;

                    case "Mobile4":
                        devicePinging = CheckedMobile4;
                        break;

                    case "Mobile5":
                        devicePinging = CheckedMobile5;
                        break;

                    case "Mobile6":
                        devicePinging = CheckedMobile6;
                        break;

                    case "Mobile7":
                        devicePinging = CheckedMobile7;
                        break;

                    case "Mobile8":
                        devicePinging = CheckedMobile8;
                        break;

                    case "Mobile9":
                        devicePinging = CheckedMobile9;
                        break;

                    case "Mobile10":
                        devicePinging = CheckedMobile10;
                        break;

                    case "Verifone1":
                        devicePinging = CheckedReg1;
                        break;

                    case "Verifone2":
                        devicePinging = CheckedReg2;
                        break;

                    case "Verifone3":
                        devicePinging = CheckedReg3;
                        break;

                    case "Verifone4":
                        devicePinging = CheckedReg4;
                        break;

                    case "Verifone5":
                        devicePinging = CheckedReg5;
                        break;

                    case "Verifone6":
                        devicePinging = CheckedReg6;
                        break;

                    case "Verifone7":
                        devicePinging = CheckedReg7;
                        break;

                    case "Verifone8":
                        devicePinging = CheckedReg8;
                        break;

                    case "Verifone9":
                        devicePinging = CheckedReg9;
                        break;

                    case "Verifone10":
                        devicePinging = CheckedReg10;
                        break;

                    case "Office8":
                        devicePinging = CheckedOffice8;
                        break;
                }
            } while (IsRunning && devicePinging);
        }

        #endregion

        #region Pings store devices until alerted to stop

        public void Run()
        {
            

            if (Threads == null)
            {
                Threads = new Thread[47];
            }

            if (StoreNumber == null || StoreNumber.Length < 4)
            {
                var model = new MessageBoxViewModel("Please enter a valid store number", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                ActivateItem(model);
                window.ShowDialog(model);
                DeactivateItem(model, true);
            }

            else
            {
                StopButtonEnabled = true;
                IsRunning = true;
                IsNotRunning = false;
                bool flag = false;

                Threads[0] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[1] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[2] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[3] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[4] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[5] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[6] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[7] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[8] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[9] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[10] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[11] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[12] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[13] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[14] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[15] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[16] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[17] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[18] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[19] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[20] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[21] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[22] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[23] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[24] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[25] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[26] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[27] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[28] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[29] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[30] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[31] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[32] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[33] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[34] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[35] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[36] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[37] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[38] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[39] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[40] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[41] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[42] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[43] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[44] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[45] = new Thread(new ParameterizedThreadStart(PingDevice));
                Threads[46] = new Thread(new ParameterizedThreadStart(PingDevice));

                if (CheckedReg1)
                {
                    Threads[0].Start("Reg1");
                    flag = true;
                }

                if (CheckedReg2)
                {
                    Threads[1].Start("Reg2");
                    flag = true;
                }

                if (CheckedReg3)
                {
                    Threads[2].Start("Reg3");
                    flag = true;
                }

                if (CheckedReg4)
                {
                    Threads[3].Start("Reg4");
                    flag = true;
                }

                if (CheckedReg5)
                {
                    Threads[4].Start("Reg5");
                    flag = true;
                }

                if (CheckedReg6)
                {
                    Threads[5].Start("Reg6");
                    flag = true;
                }

                if (CheckedReg7)
                {
                    Threads[6].Start("Reg7");
                    flag = true;
                }

                if (CheckedReg8)
                {
                    Threads[7].Start("Reg8");
                    flag = true;
                }

                if (CheckedReg9)
                {
                    Threads[8].Start("Reg9");
                    flag = true;
                }

                if (CheckedReg10)
                {
                    Threads[9].Start("Reg10");
                    flag = true;
                }

                if (CheckedKms1)
                {
                    Threads[10].Start("Kms1");
                    flag = true;
                }

                if (CheckedKms2)
                {
                    Threads[11].Start("Kms2");
                    flag = true;
                }

                if (CheckedKms3)
                {
                    Threads[12].Start("Kms3");
                    flag = true;
                }

                if (CheckedKms4)
                {
                    Threads[13].Start("Kms4");
                    flag = true;
                }

                if (CheckedKms5)
                {
                    Threads[14].Start("Kms5");
                    flag = true;
                }

                if (CheckedKms6)
                {
                    Threads[15].Start("Kms6");
                    flag = true;
                }

                if (CheckedKms7)
                {
                    Threads[16].Start("Kms7");
                    flag = true;
                }

                if (CheckedKms8)
                {
                    Threads[17].Start("Kms8");
                    flag = true;
                }

                if (CheckedKms9)
                {
                    Threads[18].Start("Kms9");
                    flag = true;
                }

                if (CheckedOffice1)
                {
                    Threads[19].Start("Office1");
                    flag = true;
                }

                if (CheckedOffice2)
                {
                    Threads[20].Start("Office2");
                    flag = true;
                }

                if (CheckedOffice3)
                {
                    Threads[21].Start("Office3");
                    flag = true;
                }

                if (CheckedOffice4)
                {
                    Threads[22].Start("Office4");
                    flag = true;
                }

                if (CheckedOffice5)
                {
                    Threads[23].Start("Office5");
                    flag = true;
                }

                if (CheckedOffice6)
                {
                    Threads[24].Start("Office6");
                    flag = true;
                }

                if (CheckedOffice7)
                {
                    Threads[25].Start("Office7");
                    flag = true;
                }

                if (CheckedOffice8)
                {
                    Threads[26].Start("Office8");
                    flag = true;
                }

                if (!IsWendys)
                {
                    Threads[27].Start("Verifone1");
                }

                if (!IsWendys)
                {
                    Threads[28].Start("Verifone2");
                }

                if (!IsWendys)
                {
                    Threads[29].Start("Verifone3");
                }

                if (!IsWendys)
                {
                    Threads[30].Start("Verifone4");
                }

                if (!IsWendys)
                {
                    Threads[31].Start("Verifone5");
                }

                if (!IsWendys)
                {
                    Threads[32].Start("Verifone6");
                }

                if (!IsWendys)
                {
                    Threads[33].Start("Verifone7");
                }

                if (!IsWendys)
                {
                    Threads[34].Start("Verifone8");
                }

                if (!IsWendys)
                {
                    Threads[35].Start("Verifone9");
                }

                if (!IsWendys)
                {
                    Threads[36].Start("Verifone10");
                }

                if (CheckedMobile1)
                {
                    Threads[37].Start("Mobile1");
                    flag = true;
                }

                if (CheckedMobile2)
                {
                    Threads[38].Start("Mobile2");
                    flag = true;
                }

                if (CheckedMobile3)
                {
                    Threads[39].Start("Mobile3");
                    flag = true;
                }

                if (CheckedMobile4)
                {
                    Threads[40].Start("Mobile4");
                    flag = true;
                }

                if (CheckedMobile5)
                {
                    Threads[41].Start("Mobile5");
                    flag = true;
                }

                if (CheckedMobile6)
                {
                    Threads[42].Start("Mobile6");
                    flag = true;
                }

                if (CheckedMobile7)
                {
                    Threads[43].Start("Mobile7");
                    flag = true;
                }

                if (CheckedMobile8)
                {
                    Threads[44].Start("Mobile8");
                    flag = true;
                }

                if (CheckedMobile9)
                {
                    Threads[45].Start("Mobile9");
                    flag = true;
                }

                if (CheckedMobile10)
                {
                    Threads[46].Start("Mobile10");
                    flag = true;
                }

                if (flag)
                {
                    RunButtonEnabled = false;
                }
                else
                {
                    IsRunning = false;
                    IsNotRunning = true;
                }
            }
            
        }

        #endregion

        #region Clears UI elements
        public void ClearForm()
        {
            Reg1 = Black;
            Reg2 = Black;
            Reg3 = Black;
            Reg4 = Black;
            Reg5 = Black;
            Reg6 = Black;
            Reg7 = Black;
            Reg8 = Black;
            Reg9 = Black;
            Reg10 = Black;
            Kms1 = Black;
            Kms2 = Black;
            Kms3 = Black;
            Kms4 = Black;
            Kms5 = Black;
            Kms6 = Black;
            Kms7 = Black;
            Kms8 = Black;
            Kms9 = Black;
            Office1 = Black;
            Office2 = Black;
            Office3 = Black;
            Office4 = Black;
            Office5 = Black;
            Office6 = Black;
            Office7 = Black;
            Office8 = Black;
            Mobile1 = Black;
            Mobile2 = Black;
            Mobile3 = Black;
            Mobile4 = Black;
            Mobile5 = Black;
            Mobile6 = Black;
            Mobile7 = Black;
            Mobile8 = Black;
            Mobile9 = Black;
            Mobile10 = Black;
            LabelReg1 = White;
            LabelReg2 = White;
            LabelReg3 = White;
            LabelReg4 = White;
            LabelReg5 = White;
            LabelReg6 = White;
            LabelReg7 = White;
            LabelReg8 = White;
            LabelReg9 = White;
            LabelReg10 = White;

            StoreNumber = "";
            ReplyReg1 = "";
            ReplyReg2 = "";
            ReplyReg3 = "";
            ReplyReg4 = "";
            ReplyReg5 = "";
            ReplyReg6 = "";
            ReplyReg7 = "";
            ReplyReg8 = "";
            ReplyReg9 = "";
            ReplyReg10 = "";
            ReplyKms1 = "";
            ReplyKms2 = "";
            ReplyKms3 = "";
            ReplyKms4 = "";
            ReplyKms5 = "";
            ReplyKms6 = "";
            ReplyKms7 = "";
            ReplyKms8 = "";
            ReplyKms9 = "";
            ReplyOffice1 = "";
            ReplyOffice2 = "";
            ReplyOffice3 = "";
            ReplyOffice4 = "";
            ReplyOffice5 = "";
            ReplyOffice6 = "";
            ReplyOffice7 = "";
            ReplyOffice8 = "";
            ReplyMobile1 = "";
            ReplyMobile2 = "";
            ReplyMobile3 = "";
            ReplyMobile4 = "";
            ReplyMobile5 = "";
            ReplyMobile6 = "";
            ReplyMobile7 = "";
            ReplyMobile8 = "";
            ReplyMobile9 = "";
            ReplyMobile10 = "";

            InfoReg1 = null;
            InfoReg2 = null;
            InfoReg3 = null;
            InfoReg4 = null;
            InfoReg5 = null;
            InfoReg6 = null;
            InfoReg7 = null;
            InfoReg8 = null;
            InfoReg9 = null;
            InfoReg10 = null;
            InfoKms1 = null;
            InfoKms2 = null;
            InfoKms3 = null;
            InfoKms4 = null;
            InfoKms5 = null;
            InfoKms6 = null;
            InfoKms7 = null;
            InfoKms8 = null;
            InfoKms9 = null;
            InfoOffice1 = null;
            InfoOffice8 = null;

            RegIp1 = null;
            RegIp2 = null;
            RegIp3 = null;
            RegIp4 = null;
            RegIp5 = null;
            RegIp6 = null;
            RegIp7 = null;
            RegIp8 = null;
            RegIp9 = null;
            RegIp10 = null;
            KmsIp1 = null;
            KmsIp2 = null;
            KmsIp3 = null;
            KmsIp4 = null;
            KmsIp5 = null;
            KmsIp6 = null;
            KmsIp7 = null;
            KmsIp8 = null;
            KmsIp9 = null;
            OfficeIp1 = null;
            OfficeIp2 = null;
            OfficeIp3 = null;
            OfficeIp4 = null;
            OfficeIp5 = null;
            OfficeIp6 = null;
            OfficeIp7 = null;
            OfficeIp8 = null;
            MobileIp1 = null;
            MobileIp2 = null;
            MobileIp3 = null;
            MobileIp4 = null;
            MobileIp5 = null;
            MobileIp6 = null;
            MobileIp7 = null;
            MobileIp8 = null;
            MobileIp9 = null;
            MobileIp10 = null;
        }
        #endregion

        #region Stops all pings
        public void Stop()
        {
            IsRunning = false;
            IsNotRunning = true;
            StopButtonEnabled = false;
            RunButtonEnabled = true;
        }
        #endregion

        #region Connects to the Doall computer
        public void Doall()
        {
            string address = "172.20.12.96";
            Process process = new Process();

            try
            {
                process.StartInfo.FileName = @"S:\bmiller\QuickDameware.exe";
                process.StartInfo.Arguments = address;
                process.Start();
            }
            catch (Exception e1)
            {
                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                ActivateItem(model);
                window.ShowDialog(model);
                DeactivateItem(model, true);
            }
        }
        #endregion

        #region Opens SMB in file explorer

        public void OpenSMB(string device)
        {
            string address = "";
            string path = "cmd.exe";
            string cmdText = @"/C net use \\";

            if (!String.IsNullOrEmpty(StoreNumber))
            {
                if (StoreNumber[2] != '0')
                {
                    address = "10." + StoreNumber.Substring(0, 2) + "." + StoreNumber.Substring(2);
                }
                else
                {
                    address = "10." + StoreNumber.Substring(0, 2) + "." + StoreNumber[3];
                }
            }

            switch (device)
            {
                case "Reg1":
                    if (Reg1.Color == Colors.Black || Reg1.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".1";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg2":
                    if (Reg2.Color == Colors.Black || Reg2.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".2";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg3":
                    if (Reg3.Color == Colors.Black || Reg3.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".3";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg4":
                    if (Reg4.Color == Colors.Black || Reg4.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".4";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg5":
                    if (Reg5.Color == Colors.Black || Reg5.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".5";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg6":
                    if (Reg6.Color == Colors.Black || Reg6.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".6";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg7":
                    if (Reg7.Color == Colors.Black || Reg7.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".7";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg8":
                    if (Reg8.Color == Colors.Black || Reg8.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".8";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg9":
                    if (Reg9.Color == Colors.Black || Reg9.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".9";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg10":
                    if (Reg10.Color == Colors.Black || Reg10.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".10";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile1":
                    if (Mobile1.Color == Colors.Black || Mobile1.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".30";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile2":
                    if (Mobile2.Color == Colors.Black || Mobile2.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".31";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile3":
                    if (Mobile3.Color == Colors.Black || Mobile3.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".32";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile4":
                    if (Mobile4.Color == Colors.Black || Mobile4.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".33";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile5":
                    if (Mobile5.Color == Colors.Black || Mobile5.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".34";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile6":
                    if (Mobile6.Color == Colors.Black || Mobile6.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".35";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile7":
                    if (Mobile7.Color == Colors.Black || Mobile7.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".36";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile8":
                    if (Mobile8.Color == Colors.Black || Mobile8.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".37";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile9":
                    if (Mobile9.Color == Colors.Black || Mobile9.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".38";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile10":
                    if (Mobile10.Color == Colors.Black || Mobile10.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".39";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Kms1":
                    if (IsWendys)
                    {
                        if (Kms1.Color == Colors.Black || Kms1.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".81";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms2":
                    if (IsWendys)
                    {
                        if (Kms2.Color == Colors.Black || Kms2.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".82";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms3":
                    if (IsWendys)
                    {
                        if (Kms3.Color == Colors.Black || Kms3.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".83";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms4":
                    if (IsWendys)
                    {
                        if (Kms4.Color == Colors.Black || Kms4.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".84";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms5":
                    if (IsWendys)
                    {
                        if (Kms5.Color == Colors.Black || Kms5.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".85";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms6":
                    if (IsWendys)
                    {
                        if (Kms6.Color == Colors.Black || Kms6.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".86";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms7":
                    if (IsWendys)
                    {
                        if (Kms7.Color == Colors.Black || Kms7.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".88";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms8":
                    if (IsWendys)
                    {
                        if (Kms8.Color == Colors.Black || Kms8.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".88";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms9":
                    if (IsWendys)
                    {
                        if (Kms9.Color == Colors.Black || Kms9.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".89";
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;

                            try
                            {
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.UseShellExecute = false;
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = cmdText;
                                process.Start();
                                process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Office1":
                    if (Office1.Color == Colors.Black || Office1.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".254";
                        if (IsWendys)
                        {
                            cmdText += address + " /user:wendys \"\"\nexplorer \\" + address;
                        }
                        else
                        {
                            cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;
                        }

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Office8":
                    if (Office8.Color == Colors.Black || Office8.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".12";
                        cmdText += address + " /user:ngpos \"\"\nexplorer \\" + address;

                        try
                        {
                            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.CreateNoWindow = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = cmdText;
                            process.Start();
                            process.StartInfo.Arguments = "/C explorer \\\\" + address + "\\c$";
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region Opens the Dameware application
        public void Dameware()
        {
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = @"C:\Program Files\SolarWinds\DameWare Remote Support 9.0\DWRCC.exe";
                if (File.Exists(process.StartInfo.FileName))
                {
                    process.Start();
                }
                else
                {
                    process.StartInfo.FileName = @"C:\Program Files\SolarWinds\DameWare Mini Remote Control 9.0\DWRCC.exe";
                    process.Start();
                }
            }
            catch (Exception e1)
            {
                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                ActivateItem(model);
                window.ShowDialog(model);
                DeactivateItem(model, true);
            }
        }
        #endregion

        #region Connect to a given computer
        public void ConnectDevice(string device)
        {
            string address = "";
            string path = @"S:\bmiller\QuickDameware.exe";
            if (!String.IsNullOrEmpty(StoreNumber))
            {
                if (StoreNumber[2] != '0')
                {
                    address = "10." + StoreNumber.Substring(0, 2) + "." + StoreNumber.Substring(2);
                }
                else
                {
                    address = "10." + StoreNumber.Substring(0, 2) + "." + StoreNumber[3];
                }
            }
            

            switch (device)
            {
                case "Reg1":
                    if (Reg1.Color == Colors.Black || Reg1.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".1";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg2":
                    if (Reg2.Color == Colors.Black || Reg2.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".2";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg3":
                    if (Reg3.Color == Colors.Black || Reg3.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".3";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg4":
                    if (Reg4.Color == Colors.Black || Reg4.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".4";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg5":
                    if (Reg5.Color == Colors.Black || Reg5.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".5";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg6":
                    if (Reg6.Color == Colors.Black || Reg6.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".6";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg7":
                    if (Reg7.Color == Colors.Black || Reg7.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".7";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg8":
                    if (Reg8.Color == Colors.Black || Reg8.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".8";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg9":
                    if (Reg9.Color == Colors.Black || Reg9.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".9";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Reg10":
                    if (Reg10.Color == Colors.Black || Reg10.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".10";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile1":
                    if (Mobile1.Color == Colors.Black || Mobile1.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".30";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile2":
                    if (Mobile2.Color == Colors.Black || Mobile2.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".31";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile3":
                    if (Mobile3.Color == Colors.Black || Mobile3.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".32";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile4":
                    if (Mobile4.Color == Colors.Black || Mobile4.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".33";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile5":
                    if (Mobile5.Color == Colors.Black || Mobile5.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".34";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile6":
                    if (Mobile6.Color == Colors.Black || Mobile6.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".35";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile7":
                    if (Mobile7.Color == Colors.Black || Mobile7.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".36";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile8":
                    if (Mobile8.Color == Colors.Black || Mobile8.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".37";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile9":
                    if (Mobile9.Color == Colors.Black || Mobile9.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".38";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Mobile10":
                    if (Mobile10.Color == Colors.Black || Mobile10.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".39";
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Kms1":
                    if (IsWendys)
                    {
                        if (Kms1.Color == Colors.Black || Kms1.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".81";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms2":
                    if (IsWendys)
                    {
                        if (Kms2.Color == Colors.Black || Kms2.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".82";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms3":
                    if (IsWendys)
                    {
                        if (Kms3.Color == Colors.Black || Kms3.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".83";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms4":
                    if (IsWendys)
                    {
                        if (Kms4.Color == Colors.Black || Kms4.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".84";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms5":
                    if (IsWendys)
                    {
                        if (Kms5.Color == Colors.Black || Kms5.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".85";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms6":
                    if (IsWendys)
                    {
                        if (Kms6.Color == Colors.Black || Kms6.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".86";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms7":
                    if (IsWendys)
                    {
                        if (Kms7.Color == Colors.Black || Kms7.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".87";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms8":
                    if (IsWendys)
                    {
                        if (Kms8.Color == Colors.Black || Kms8.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".88";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Kms9":
                    if (IsWendys)
                    {
                        if (Kms9.Color == Colors.Black || Kms9.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Controller is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".89";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;

                case "Office1":
                    if (Office1.Color == Colors.Black || Office1.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".254";

                        try
                        {
                            process.StartInfo.FileName = path;
                            process.StartInfo.Arguments = address;
                            process.Start();
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Office3":
                    if (Office3.Color == Colors.Black || Office3.Color == Colors.DarkRed)
                    {
                        var model = new MessageBoxViewModel("Printer is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                        ActivateItem(model);
                        window.ShowDialog(model);
                        DeactivateItem(model, true);
                    }
                    else
                    {
                        Process process = new Process();
                        address += ".16";

                        try
                        {
                            process.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                            process.StartInfo.Arguments = address;
                            if (File.Exists(process.StartInfo.FileName))
                            {
                                process.Start();
                            }
                            else
                            {
                                process.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                                process.Start();
                            }
                        }
                        catch (Exception e1)
                        {
                            var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                    }
                    break;

                case "Office8":
                    if (IsWendys)
                    {
                        if (Office8.Color == Colors.Black || Office8.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Timer is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            address += ".236";
                            Process process = new Process();
                            try
                            {
                                process.StartInfo.FileName = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
                                process.StartInfo.Arguments = address;
                                if (File.Exists(process.StartInfo.FileName))
                                {
                                    process.Start();
                                }
                                else
                                {
                                    process.StartInfo.FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe";
                                    process.Start();
                                }
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }

                    else
                    {
                        if (Office8.Color == Colors.Black || Office8.Color == Colors.DarkRed)
                        {
                            var model = new MessageBoxViewModel("Register is not online!", MessageBoxViewModel.MessageType.Warning, MessageBoxViewModel.MessageButtons.Ok);
                            ActivateItem(model);
                            window.ShowDialog(model);
                            DeactivateItem(model, true);
                        }
                        else
                        {
                            Process process = new Process();
                            address += ".12";

                            try
                            {
                                process.StartInfo.FileName = path;
                                process.StartInfo.Arguments = address;
                                process.Start();
                            }
                            catch (Exception e1)
                            {
                                var model = new MessageBoxViewModel("Unable to connect!\n\n" + e1.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                                ActivateItem(model);
                                window.ShowDialog(model);
                                DeactivateItem(model, true);
                            }
                        }
                    }
                    break;
            }
        }
        #endregion

        #region Timestamp converter
        private DateTime ConvertToDate(int timeStamp)
        {
            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = time.AddSeconds(Convert.ToDouble(timeStamp)).ToLocalTime();
            return time;
        }
        #endregion

        #region Copy device info from the database
        public void CopyServiceInfo(string device)
        {
            try
            {

                string info = null;
                string[] array;

                switch (device)
                {
                    case "Reg1":
                        array = InfoReg1.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg2":
                        array = InfoReg2.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg3":
                        array = InfoReg3.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg4":
                        array = InfoReg4.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg5":
                        array = InfoReg5.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg6":
                        array = InfoReg6.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg7":
                        array = InfoReg7.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg8":
                        array = InfoReg8.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg9":
                        array = InfoReg9.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Reg10":
                        array = InfoReg10.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms1":
                        array = InfoKms1.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms2":
                        array = InfoKms2.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms3":
                        array = InfoKms3.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms4":
                        array = InfoKms4.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms5":
                        array = InfoKms5.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms6":
                        array = InfoKms6.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms7":
                        array = InfoKms7.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms8":
                        array = InfoKms8.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Kms9":
                        array = InfoKms9.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Office1":
                        array = InfoOffice1.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;

                    case "Office8":
                        array = InfoOffice8.Split('\n');
                        array[0] = array[0].Substring(7);
                        array[1] = array[1].Substring(5);
                        info = "Model- " + array[0] + "\nS/N- " + array[1];
                        break;
                }

            
                if (!string.IsNullOrEmpty(info))
                {
                    Clipboard.Clear();
                    Clipboard.SetText(info);
                }
            }
            
            catch (Exception e)
            {
                //object[] parameters = { e.Message, MessageType.Error, MessageButtons.Ok };
                //Application.Current.Dispatcher.Invoke(customBox, parameters);

                //var model = new MessageBoxViewModel(e.Message, MessageBoxViewModel.MessageType.Error, MessageBoxViewModel.MessageButtons.Ok);
                //ActivateItem(model);
                //window.ShowDialog(model);
                //DeactivateItem(model, true);
            }
        }
        #endregion

        #region Change all check boxes when 'Check all' is changed
        public void CheckChanged(string checkbox)
        {
            switch (checkbox)
            {
                case "Reg1":
                    if(IsRunning && CheckedReg1)
                    {
                        Threads[0] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[0].Start("Reg1");
                    }
                    break;
                case "Reg2":
                    if(IsRunning && CheckedReg2)
                    {
                        Threads[1] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[1].Start();
                    }
                    break;
                case "Reg3":
                    if (IsRunning && CheckedReg3)
                    {
                        Threads[2] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[2].Start("Reg3");
                    }
                    break;
                case "Reg4":
                    if (IsRunning && CheckedReg4)
                    {
                        Threads[3] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[3].Start("Reg4");
                    }
                    break;
                case "Reg5":
                    if (IsRunning && CheckedReg5)
                    {
                        Threads[4] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[4].Start("Reg5");
                    }
                    break;
                case "Reg6":
                    if (IsRunning && CheckedReg6)
                    {
                        Threads[5] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[5].Start("Reg6");
                    }
                    break;
                case "Reg7":
                    if (IsRunning && CheckedReg7)
                    {
                        Threads[6] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[6].Start("Reg7");
                    }
                    break;
                case "Reg8":
                    if (IsRunning && CheckedReg8)
                    {
                        Threads[7] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[7].Start("Reg8");
                    }
                    break;
                case "Reg9":
                    if (IsRunning && CheckedReg9)
                    {
                        Threads[8] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[8].Start("Reg9");
                    }
                    break;
                case "Reg10":
                    if (IsRunning && CheckedReg10)
                    {
                        Threads[9] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[9].Start("Reg10");
                    }
                    break;
                case "Kms1":
                    if (IsRunning && CheckedKms1)
                    {
                        Threads[10] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[10].Start("Kms1");
                    }
                    break;
                case "Kms2":
                    if (IsRunning && CheckedKms2)
                    {
                        Threads[11] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[11].Start("Kms2");
                    }
                    break;
                case "Kms3":
                    if (IsRunning && CheckedKms3)
                    {
                        Threads[12] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[12].Start("Kms3");
                    }
                    break;
                case "Kms4":
                    if (IsRunning && CheckedKms4)
                    {
                        Threads[13] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[13].Start("Kms4");
                    }
                    break;
                case "Kms5":
                    if (IsRunning && CheckedKms5)
                    {
                        Threads[14] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[14].Start("Kms5");
                    }
                    break;
                case "Kms6":
                    if (IsRunning && CheckedKms6)
                    {
                        Threads[15] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[15].Start("Kms6");
                    }
                    break;
                case "Kms7":
                    if (IsRunning && CheckedKms7)
                    {
                        Threads[16] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[16].Start("Kms7");
                    }
                    break;
                case "Kms8":
                    if (IsRunning && CheckedKms8)
                    {
                        Threads[17] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[17].Start("Kms8");
                    }
                    break;
                case "Kms9":
                    if (IsRunning && CheckedKms9)
                    {
                        Threads[18] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[18].Start("Kms9");
                    }
                    break;
                case "Office1":
                    if (IsRunning && CheckedOffice1)
                    {
                        Threads[19] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[19].Start("Office1");
                    }
                    break;
                case "Office2":
                    if (IsRunning && CheckedOffice2)
                    {
                        Threads[20] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[20].Start("Office2");
                    }
                    break;
                case "Office3":
                    if (IsRunning && CheckedOffice3)
                    {
                        Threads[21] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[21].Start("Office3");
                    }
                    break;
                case "Office4":
                    if (IsRunning && CheckedOffice4)
                    {
                        Threads[22] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[22].Start("Office4");
                    }
                    break;
                case "Office5":
                    if (IsRunning && CheckedOffice5)
                    {
                        Threads[23] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[23].Start("Office5");
                    }
                    break;
                case "Office6":
                    if (IsRunning && CheckedOffice6)
                    {
                        Threads[24] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[24].Start("Office6");
                    }
                    break;
                case "Office7":
                    if (IsRunning && CheckedOffice7)
                    {
                        Threads[25] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[25].Start("Office7");
                    }
                    break;
                case "Office8":
                    if (IsRunning && CheckedOffice8)
                    {
                        Threads[26] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[26].Start("Office8");
                    }
                    break;
                case "Mobile1":
                    if (IsRunning && CheckedMobile1)
                    {
                        Threads[27] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[27].Start("Mobile1");
                    }
                    break;
                case "Mobile2":
                    if (IsRunning && CheckedMobile2)
                    {
                        Threads[28] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[28].Start("Mobile2");
                    }
                    break;
                case "Mobile3":
                    if (IsRunning && CheckedMobile3)
                    {
                        Threads[29] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[29].Start("Mobile3");
                    }
                    break;
                case "Mobile4":
                    if (IsRunning && CheckedMobile4)
                    {
                        Threads[30] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[30].Start("Mobile4");
                    }
                    break;
                case "Mobile5":
                    if (IsRunning && CheckedMobile5)
                    {
                        Threads[31] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[31].Start("Mobile5");
                    }
                    break;
                case "Mobile6":
                    if (IsRunning && CheckedMobile6)
                    {
                        Threads[32] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[32].Start("Mobile6");
                    }
                    break;
                case "Mobile7":
                    if (IsRunning && CheckedMobile7)
                    {
                        Threads[33] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[33].Start("Mobile7");
                    }
                    break;
                case "Mobile8":
                    if (IsRunning && CheckedMobile8)
                    {
                        Threads[34] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[34].Start("Mobile8");
                    }
                    break;
                case "Mobile9":
                    if (IsRunning && CheckedMobile9)
                    {
                        Threads[35] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[35].Start("Mobile9");
                    }
                    break;
                case "Mobile10":
                    if (IsRunning && CheckedMobile10)
                    {
                        Threads[36] = new Thread(new ParameterizedThreadStart(PingDevice));
                        Threads[36].Start("Mobile10");
                    }
                    break;
            }
        }
        #endregion

        #region Theme alteration
        private void ApplyBase(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
            if (isDark)
            {
                System.Drawing.Color temp2 = ColorTranslator.FromHtml("#232323");
                System.Windows.Media.Color temp = System.Windows.Media.Color.FromArgb(temp2.A, temp2.R, temp2.G, temp2.B);
                theme.Background = temp;
                ModifyTheme(theme => theme.SetPrimaryColor(temp));
                
            }
            else
            {
                ModifyTheme(theme => theme.SetPrimaryColor(Colors.Blue));
            }
            _paletteHelper.SetTheme(theme);
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
        #endregion

        #region Window buttons and menus
        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void Maximize()
        {
            if(Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                CurrentWindowState = WindowState.Normal;
            }
            else
            {
                WindowHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                WindowWidth = SystemParameters.MaximizedPrimaryScreenWidth;
                CurrentWindowState = WindowState.Maximized;
            }
        }

        public void Minimize()
        {
            CurrentWindowState = WindowState.Minimized;
        }

        public void Menu()
        {
            Window mWindow = Application.Current.MainWindow;
            System.Windows.Point p;

            if (CurrentWindowState == WindowState.Maximized)
            {
                p = new System.Windows.Point(0, 25);
            }
            else
            {
                p = new System.Windows.Point(LeftPosition, TopPosition + 25);
            }
            SystemCommands.ShowSystemMenu(mWindow, p);
        }

        public void ShowMenu()
        {
            if (ShowingMenu)
            {
                HideMenu();
            }
            else
            {
                ShowingMenu = true;
            }
        }

        public void HideMenu()
        {
            ShowingMenu = false;
        }
        #endregion
    }
}
